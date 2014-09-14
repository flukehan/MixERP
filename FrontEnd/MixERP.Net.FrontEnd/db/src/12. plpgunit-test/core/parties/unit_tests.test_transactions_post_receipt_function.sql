DROP FUNCTION IF EXISTS unit_tests.test_transactions_post_receipt_function();

CREATE FUNCTION unit_tests.test_transactions_post_receipt_function()
RETURNS public.test_result
AS
$$
	DECLARE message                                 test_result;
	DECLARE _user_id				integer;
	DECLARE _office_id				integer; 
	DECLARE _login_id				bigint;
	DECLARE _party_code				national character varying(12); 
	DECLARE _currency_code				national character varying(12); 
	DECLARE _amount					money_strict; 
	DECLARE _exchange_rate_debit			decimal_strict; 
	DECLARE _exchange_rate_credit			decimal_strict;
	DECLARE _reference_number			national character varying(24); 
	DECLARE _statement_reference			national character varying(128); 
	DECLARE _cost_center_id				integer;
	DECLARE _cash_repository_id			integer;
	DECLARE _posted_date                            date;
	DECLARE _bank_account_id			integer;
	DECLARE _bank_instrument_code			national character varying(128);
	DECLARE _bank_tran_code				national character varying(128);
	DECLARE _result                                 bigint;
BEGIN



        _user_id                                        := (SELECT user_id FROM office.users WHERE user_name != 'sys' LIMIT 1);
        _office_id                                      := (SELECT office_id FROM office.offices LIMIT 1);
	_login_id				        := (SELECT login_id FROM audit.logins LIMIT 1);
	_party_code				        := (SELECT party_code FROM core.parties LIMIT 1);
	_currency_code				        := 'USD';
	_amount					        := 1000.00;
	_exchange_rate_debit			        := 100.00;
	_exchange_rate_credit			        := 100.00;
	_reference_number			        := 'PL-PG-UNIT-TEST';
	_statement_reference			        := 'This transaction should have been rollbacked already.';
	_cost_center_id				        := (SELECT cost_center_id FROM office.cost_centers LIMIT 1);
	_cash_repository_id			        := (SELECT cash_repository_id FROM office.cash_repositories LIMIT 1);
	_posted_date                                    := NULL;
	_bank_account_id			        := NULL;
	_bank_instrument_code			        := NULL;
	_bank_tran_code				        := NULL;
                                                        
        _result                                         := transactions.post_receipt_function
                                                                (
                                                                        _user_id, 
                                                                        _office_id, 
                                                                        _login_id,
                                                                        _party_code, 
                                                                        _currency_code, 
                                                                        _amount, 
                                                                        _exchange_rate_debit, 
                                                                        _exchange_rate_credit,
                                                                        _reference_number, 
                                                                        _statement_reference, 
                                                                        _cost_center_id,
                                                                        _cash_repository_id,
                                                                        _posted_date,
                                                                        _bank_account_id,
                                                                        _bank_instrument_code,
                                                                        _bank_tran_code	
                                                                );

        IF(_result <= 0) THEN
		SELECT assert.fail('Cannot compile transactions.post_receipt_function.') INTO message;
		RETURN message;
        END IF;


	SELECT assert.ok('End of test.') INTO message;  
	RETURN message;
END
$$
LANGUAGE plpgsql;


