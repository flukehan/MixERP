DROP FUNCTION IF EXISTS policy.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date);

CREATE FUNCTION policy.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date)
RETURNS bool
AS
$$
BEGIN
    IF(audit.is_valid_login_id(_login_id) = false) THEN
        RAISE EXCEPTION 'Invalid LoginId.';
    END IF; 

    IF(office.is_valid_office_id(_office_id) = false) THEN
        RAISE EXCEPTION 'Invalid OfficeId.';
    END IF; 

    IF(policy.is_restricted_mode()) THEN
        RAISE EXCEPTION 'Cannot post transaction during end of day operation.';
    END IF;

    IF(_value_date < transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Past dated transactions are not allowed';
    END IF;
    
    IF NOT EXISTS 
    (
        SELECT *
        FROM office.users
        INNER JOIN office.roles
        ON office.users.role_id = office.roles.role_id
        WHERE is_system=false
        AND user_id = $2
    ) THEN
        RAISE EXCEPTION 'Access is denied. You are not authorized to post this transaction.';
    END IF;

    RETURN true;
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


