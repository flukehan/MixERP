DROP FUNCTION IF EXISTS transactions.check_cash_balance_trigger() CASCADE;
CREATE FUNCTION transactions.check_cash_balance_trigger()
RETURNS TRIGGER
AS
$$
    DECLARE cash_balance money_strict2;
BEGIN
    IF(NEW.cash_repository_id IS NOT NULL) THEN
        IF(TG_OP='UPDATE') THEN
            IF (OLD.amount_in_currency != NEW.amount_in_currency) OR (OLD.amount_in_local_currency != NEW.amount_in_local_currency) THEN
                RAISE EXCEPTION 'Acess is denied. You cannot update the "transaction_details" table.';
            END IF;
        END IF;

        IF(TG_OP='INSERT') THEN
            cash_balance := transactions.get_cash_repository_balance(NEW.cash_repository_id, NEW.currency_code);

            IF(cash_balance < NEW.amount_in_currency) THEN
                RAISE EXCEPTION 'Acess is denied. Posting this transaction would produce a negative cash balance.';
            END IF;
        END IF;
    END IF;

    RETURN NEW;
END
$$
LANGUAGE 'plpgsql';


CREATE TRIGGER check_cash_balance_trigger
BEFORE INSERT OR UPDATE
ON transactions.transaction_details
FOR EACH ROW 
EXECUTE PROCEDURE transactions.check_cash_balance_trigger();

