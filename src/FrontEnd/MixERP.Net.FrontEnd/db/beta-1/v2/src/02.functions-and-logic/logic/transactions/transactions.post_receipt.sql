DROP FUNCTION IF EXISTS transactions.post_receipt_function
(
    _user_id                                    integer, 
    _office_id                                  integer, 
    _login_id                                   bigint,
    _party_code                                 national character varying(12), 
    _currency_code                              national character varying(12), 
    _amount                                     public.money_strict, 
    _exchange_rate_debit                        public.decimal_strict, 
    _exchange_rate_credit                       public.decimal_strict,
    _reference_number                           national character varying(24), 
    _statement_reference                        national character varying(128), 
    _cost_center_id                             integer,
    _cash_repository_id                         integer,
    _posted_date                                date,
    _bank_account_id                            integer,
    _bank_instrument_code                       national character varying(128),
    _bank_tran_code                             national character varying(128)
);

DROP FUNCTION IF EXISTS transactions.post_receipt
(
    _user_id                                    integer, 
    _office_id                                  integer, 
    _login_id                                   bigint,
    _party_code                                 national character varying(12), 
    _currency_code                              national character varying(12), 
    _amount                                     public.money_strict, 
    _exchange_rate_debit                        public.decimal_strict, 
    _exchange_rate_credit                       public.decimal_strict,
    _reference_number                           national character varying(24), 
    _statement_reference                        national character varying(128), 
    _cost_center_id                             integer,
    _cash_repository_id                         integer,
    _posted_date                                date,
    _bank_account_id                            integer,
    _bank_instrument_code                       national character varying(128),
    _bank_tran_code                             national character varying(128),
    _cascading_tran_id                          bigint
);

DROP FUNCTION IF EXISTS transactions.post_receipt
(
    _user_id                                    integer, 
    _office_id                                  integer, 
    _login_id                                   bigint,
    _party_code                                 national character varying(12), 
    _currency_code                              national character varying(12), 
    _amount                                     public.money_strict, 
    _exchange_rate_debit                        public.decimal_strict, 
    _exchange_rate_credit                       public.decimal_strict,
    _reference_number                           national character varying(24), 
    _statement_reference                        national character varying(128), 
    _cost_center_id                             integer,
    _cash_repository_id                         integer,
    _posted_date                                date,
    _bank_account_id                            bigint,
    _payment_card_id                            integer,
    _bank_instrument_code                       national character varying(128),
    _bank_tran_code                             national character varying(128),
    _cascading_tran_id                          bigint
);

CREATE FUNCTION transactions.post_receipt
(
    _user_id                                    integer, 
    _office_id                                  integer, 
    _login_id                                   bigint,
    _party_code                                 national character varying(12), 
    _currency_code                              national character varying(12), 
    _amount                                     public.money_strict, 
    _exchange_rate_debit                        public.decimal_strict, 
    _exchange_rate_credit                       public.decimal_strict,
    _reference_number                           national character varying(24), 
    _statement_reference                        national character varying(128), 
    _cost_center_id                             integer,
    _cash_repository_id                         integer,
    _posted_date                                date,
    _bank_account_id                            bigint,
    _payment_card_id                            integer,
    _bank_instrument_code                       national character varying(128),
    _bank_tran_code                             national character varying(128),
    _cascading_tran_id                          bigint DEFAULT NULL
)
RETURNS bigint
AS
$$
    DECLARE _value_date                         date;
    DECLARE _book                               text;
    DECLARE _transaction_master_id              bigint;
    DECLARE _base_currency_code                 national character varying(12);
    DECLARE _local_currency_code                national character varying(12);
    DECLARE _party_id                           bigint;
    DECLARE _party_account_id                   bigint;
    DECLARE _debit                              public.money_strict2;
    DECLARE _credit                             public.money_strict2;
    DECLARE _lc_debit                           public.money_strict2;
    DECLARE _lc_credit                          public.money_strict2;
    DECLARE _is_cash                            boolean;
    DECLARE _cash_account_id                    bigint;
    DECLARE _is_merchant                        boolean=false;
    DECLARE _merchant_rate                      public.decimal_strict2=0;
    DECLARE _customer_pays_fee                  boolean=false;
    DECLARE _merchant_fee_accont_id             bigint;
    DECLARE _merchant_fee_statement_reference   text;
    DECLARE _merchant_fee                       public.money_strict2;
    DECLARE _merchant_fee_lc                    public.money_strict2;
BEGIN
    _value_date                             := transactions.get_value_date(_office_id);

    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book, _value_date) = false) THEN
        RETURN 0;
    END IF;

    IF(_cash_repository_id > 0) THEN
        IF(_posted_date IS NOT NULL OR _bank_account_id IS NOT NULL OR COALESCE(_bank_instrument_code, '') != '' OR COALESCE(_bank_tran_code, '') != '') THEN
            RAISE EXCEPTION 'Invalid bank transaction information provided.'
            USING ERRCODE='P5111';
        END IF;
        _is_cash                            := true;
    END IF;

    _book                                   := 'Sales.Receipt';
    
    _party_id                               := core.get_party_id_by_party_code(_party_code);    
    _party_account_id                       := core.get_account_id_by_party_id(_party_id);
    _cash_account_id                        := core.get_cash_account_id();
    
    _local_currency_code                    := core.get_currency_code_by_office_id(_office_id);
    _base_currency_code                     := core.get_currency_code_by_party_id(_party_id);


    IF EXISTS
    (
        SELECT true FROM core.bank_accounts
        WHERE is_merchant_account
        AND account_id = _bank_account_id
    ) THEN
        _is_merchant = true;
    END IF;

    SELECT 
        rate,
        customer_pays_fee,
        account_id,
        statement_reference
    INTO
        _merchant_rate,
        _customer_pays_fee,
        _merchant_fee_accont_id,
        _merchant_fee_statement_reference
    FROM core.merchant_fee_setup
    WHERE merchant_account_id = _bank_account_id
    AND payment_card_id = _payment_card_id;

    _merchant_rate      := COALESCE(_merchant_rate, 0);
    _customer_pays_fee  := COALESCE(_customer_pays_fee, false);

    IF(_is_merchant AND COALESCE(_payment_card_id, 0) = 0) THEN
        RAISE EXCEPTION 'Invalid payment card information.'
        USING ERRCODE='P5112';
    END IF;

    IF(_merchant_rate > 0 AND COALESCE(_merchant_fee_accont_id, 0) = 0) THEN
        RAISE EXCEPTION 'Could not find an account to post merchant fee expenses.'
        USING ERRCODE='P5113';
    END IF;

    IF(_local_currency_code = _currency_code AND _exchange_rate_debit != 1) THEN
        RAISE EXCEPTION 'Invalid exchange rate.'
        USING ERRCODE='P3055';
    END IF;

    IF(_base_currency_code = _currency_code AND _exchange_rate_credit != 1) THEN
        RAISE EXCEPTION 'Invalid exchange rate.'
        USING ERRCODE='P3055';
    END IF;
        
    _debit                                  := _amount;
    _lc_debit                               := _amount * _exchange_rate_debit;

    _credit                                 := _amount * (_exchange_rate_debit/ _exchange_rate_credit);
    _lc_credit                              := _amount * _exchange_rate_debit;
    _merchant_fee                           := (_debit * _merchant_rate) / 100;
    _merchant_fee_lc                        := (_lc_debit * _merchant_rate)/100;
    
    INSERT INTO transactions.transaction_master
    (
        transaction_master_id, 
        transaction_counter, 
        transaction_code, 
        book, 
        value_date, 
        user_id, 
        login_id, 
        office_id, 
        cost_center_id, 
        reference_number, 
        statement_reference,
        cascading_tran_id
    )
    SELECT 
        nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id),
        _book,
        _value_date,
        _user_id,
        _login_id,
        _office_id,
        _cost_center_id,
        _reference_number,
        _statement_reference,
        _cascading_tran_id;


    _transaction_master_id := currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

    --Debit
    IF(_is_cash) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
        SELECT _transaction_master_id, _value_date, 'Dr', _cash_account_id, _statement_reference, _cash_repository_id, _currency_code, _debit, _local_currency_code, _exchange_rate_debit, _lc_debit, _user_id;
    ELSE
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
        SELECT _transaction_master_id, _value_date, 'Dr', _bank_account_id, _statement_reference, NULL, _currency_code, _debit, _local_currency_code, _exchange_rate_debit, _lc_debit, _user_id;        
    END IF;

    --Credit
    INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
    SELECT _transaction_master_id, _value_date, 'Cr', _party_account_id, _statement_reference, NULL, _base_currency_code, _credit, _local_currency_code, _exchange_rate_credit, _lc_credit, _user_id;


    IF(_is_merchant AND _merchant_rate > 0 AND _merchant_fee_accont_id > 0) THEN
        --Debit: Merchant Fee Expenses
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
        SELECT _transaction_master_id, _value_date, 'Dr', _merchant_fee_accont_id, _merchant_fee_statement_reference, NULL, _currency_code, _merchant_fee, _local_currency_code, _exchange_rate_debit, _merchant_fee_lc, _user_id;

        --Credit: Merchant A/C
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
        SELECT _transaction_master_id, _value_date, 'Cr', _bank_account_id, _merchant_fee_statement_reference, NULL, _currency_code, _merchant_fee, _local_currency_code, _exchange_rate_debit, _merchant_fee_lc, _user_id;

        IF(_customer_pays_fee) THEN
            --Debit: Party Account Id
            INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
            SELECT _transaction_master_id, _value_date, 'Dr', _party_account_id, _merchant_fee_statement_reference, NULL, _currency_code, _merchant_fee, _local_currency_code, _exchange_rate_debit, _merchant_fee_lc, _user_id;

            --Credit: Reverse Merchant Fee Expenses
            INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
            SELECT _transaction_master_id, _value_date, 'Cr', _merchant_fee_accont_id, _merchant_fee_statement_reference, NULL, _currency_code, _merchant_fee, _local_currency_code, _exchange_rate_debit, _merchant_fee_lc, _user_id;
        END IF;
    END IF;
    
    
    INSERT INTO transactions.customer_receipts(transaction_master_id, party_id, currency_code, amount, er_debit, er_credit, cash_repository_id, posted_date, bank_account_id, bank_instrument_code, bank_tran_code)
    SELECT _transaction_master_id, _party_id, _currency_code, _amount,  _exchange_rate_debit, _exchange_rate_credit, _cash_repository_id, _posted_date, _bank_account_id, _bank_instrument_code, _bank_tran_code;

    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    PERFORM transactions.settle_party_due(_party_id, _office_id);
    RETURN _transaction_master_id;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.post_receipt(2, 2, 94, 'JASMI-0002', 'NPR', 1, 1, 1, '323234', 'ASDFASDF', 1, 1, NULL, NULL, NULL, NULL);

/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/


