DROP FUNCTION IF EXISTS core.calculate_interest(principal numeric, rate numeric, days integer, num_of_days_in_year integer, round_up integer);
CREATE FUNCTION core.calculate_interest(principal numeric, rate numeric, days integer, round_up integer, num_of_days_in_year integer)
RETURNS numeric
AS
$$
    DECLARE interest numeric;
BEGIN
    IF num_of_days_in_year = 0 OR num_of_days_in_year IS NULL THEN
        RAISE EXCEPTION 'Cannot calculate interest. The number of days in a year was not provided.'
        USING ERRCODE='P1301';
    END IF;
    
    interest := ROUND(principal * rate * days / (num_of_days_in_year * 100), round_up);

    RETURN interest;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;


DROP FUNCTION IF EXISTS core.calculate_interest(principal numeric, rate numeric, days integer, round_up integer);
CREATE FUNCTION core.calculate_interest(principal numeric, rate numeric, days integer, round_up integer)
RETURNS numeric
AS
$$
    DECLARE num_of_days_in_year integer = 365;
BEGIN
    IF core.is_leap_year() THEN
        num_of_days_in_year = 366;
    END IF;
    
    RETURN core.calculate_interest(principal, rate, days, round_up, num_of_days_in_year);
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;



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


