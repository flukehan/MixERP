DROP FUNCTION IF EXISTS transactions.post_receipt_function
(
    _user_id                                integer, 
    _office_id                              integer, 
    _login_id                               bigint,
    _party_code                             national character varying(12), 
    _currency_code                          national character varying(12), 
    _amount                                 money_strict, 
    _exchange_rate_debit                    decimal_strict, 
    _exchange_rate_credit                   decimal_strict,
    _reference_number                       national character varying(24), 
    _statement_reference                    national character varying(128), 
    _cost_center_id                         integer,
    _cash_repository_id                     integer,
    _posted_date                            date,
    _bank_account_id                        integer,
    _bank_instrument_code                   national character varying(128),
    _bank_tran_code                         national character varying(128)
);

CREATE FUNCTION transactions.post_receipt_function
(
    _user_id                                integer, 
    _office_id                              integer, 
    _login_id                               bigint,
    _party_code                             national character varying(12), 
    _currency_code                          national character varying(12), 
    _amount                                 money_strict, 
    _exchange_rate_debit                    decimal_strict, 
    _exchange_rate_credit                   decimal_strict,
    _reference_number                       national character varying(24), 
    _statement_reference                    national character varying(128), 
    _cost_center_id                         integer,
    _cash_repository_id                     integer,
    _posted_date                            date,
    _bank_account_id                        integer,
    _bank_instrument_code                   national character varying(128),
    _bank_tran_code                         national character varying(128)
)
RETURNS bigint
AS
$$
    DECLARE _value_date                     date;
    DECLARE _book                           text;
    DECLARE _transaction_master_id          bigint;
    DECLARE _base_currency_code             national character varying(12);
    DECLARE _local_currency_code            national character varying(12);
    DECLARE _party_id                       bigint;
    DECLARE _party_account_id               bigint;
    DECLARE _debit                          money_strict2;
    DECLARE _credit                         money_strict2;
    DECLARE _lc_debit                       money_strict2;
    DECLARE _lc_credit                      money_strict2;
    DECLARE _is_cash                        boolean;
    DECLARE _cash_account_id                bigint;
BEGIN
    _value_date                             := transactions.get_value_date(_office_id);

    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book, _value_date) = false) THEN
        RETURN 0;
    END IF;

    IF(_cash_repository_id > 0) THEN
        IF(_posted_Date IS NOT NULL OR _bank_account_id IS NOT NULL OR COALESCE(_bank_instrument_code, '') != '' OR COALESCE(_bank_tran_code, '') != '') THEN
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

    _debit                                  := _amount;
    _lc_debit                               := _amount * _exchange_rate_debit;

    _credit                                 := _amount * (_exchange_rate_debit/ _exchange_rate_credit);
    _lc_credit                              := _amount * _exchange_rate_debit;
    

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
        statement_reference
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
        _statement_reference;


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
    
    
    INSERT INTO transactions.customer_receipts(transaction_master_id, party_id, currency_code, amount, er_debit, er_credit, cash_repository_id, posted_date, bank_account_id, bank_instrument_code, bank_tran_code)
    SELECT _transaction_master_id, _party_id, _currency_code, _amount,  _exchange_rate_debit, _exchange_rate_credit, _cash_repository_id, _posted_date, _bank_account_id, _bank_instrument_code, _bank_tran_code;

    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    ------------TODO-----------------
    RETURN _transaction_master_id;
END
$$
LANGUAGE plpgsql;



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


