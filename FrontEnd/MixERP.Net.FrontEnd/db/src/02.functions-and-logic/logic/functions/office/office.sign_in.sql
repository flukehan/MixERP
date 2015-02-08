DROP FUNCTION IF EXISTS office.sign_in
(
    office_id       integer_strict, 
    user_name       text, 
    password        text, 
    browser         text, 
    ip_address      text, 
    remote_user     text, 
    culture         text, 
    challenge       text, 
    OUT login_id    bigint, 
    OUT message     text
);
CREATE FUNCTION office.sign_in
(
    office_id       integer_strict, 
    user_name       text, 
    password        text, 
    browser         text, 
    ip_address      text, 
    remote_user     text, 
    culture         text, 
    challenge       text, 
    OUT login_id    bigint, 
    OUT message     text
)
RETURNS RECORD
AS
$$
    DECLARE _user_id            integer;
    DECLARE _lock_out_till      TIMESTAMP WITH TIME ZONE;
    DECLARE result              boolean;
BEGIN
    _user_id        :=office.get_user_id_by_user_name($2);
    login_id        := 0;
    
    IF _user_id IS NULL THEN
        INSERT INTO audit.failed_logins(user_name,browser,ip_address,remote_user,details)
        SELECT $2, $4, $5, $6, 'Invalid user name.';
        message := 'Invalid login attempt.';
    ELSE
        _lock_out_till:=policy.is_locked_out_till(_user_id);


        IF NOT ((_lock_out_till IS NOT NULL) AND (_lock_out_till>NOW())) THEN
            IF(office.validate_login($2,$3, $8)) THEN

                SELECT * FROM office.can_login(_user_id,$1) 
                INTO result, message;

                IF(result) THEN
                    INSERT INTO audit.logins(office_id,user_id,browser,ip_address,remote_user, culture)
                    SELECT $1, _user_id, $4, $5, $6, $7;

                    login_id := currval('audit.logins_login_id_seq')::bigint;
                ELSE
                    IF(COALESCE(message, '') = '') THEN
                        message := format('A user from %1$s cannot login to %2$s.', office.get_office_name_by_id(office.get_office_id_by_user_id(_user_id)), office.get_office_name_by_id($1));
                    END IF;

                    INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
                    SELECT $1, _user_id, $2, $4, $5, $6, message;
                END IF;
            ELSE
                IF(COALESCE(message, '') = '') THEN
                    message := 'Invalid login attempt.';
                END IF;
                
                INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
                SELECT $1, _user_id, $2, $4, $5, $6, message;
            END IF;
        ELSE
             message        := format('You are locked out till %1$s.', _lock_out_till);

            INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
            SELECT $1, _user_id, $2, $4, $5, $6, message;
        END IF;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM office.sign_in(2, 'binod2', '4e99cb7523794ad53b4da66c91f56d0143a679e1c6d396cda9ad0c9b41ed53e90bd5c59bf98255a4f1946b216b3ba539074a8a86cedd4af8bb208a8fad748e82', 'Firefox', '0.0.0.0', 'N/A', 'en-US', 'cd0ad7446ab64801837bfd43197d19c1');


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


