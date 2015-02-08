DROP FUNCTION IF EXISTS office.hash_password() CASCADE;

CREATE FUNCTION office.hash_password()
RETURNS trigger
AS
$$
    DECLARE _password   text;
    DECLARE _is_sys     boolean;
BEGIN
    _is_sys     := office.is_sys_user(NEW.user_id);
    _password   := encode(digest(NEW.user_name || NEW.password, 'sha512'), 'hex');

    IF(NOT _is_sys) THEN
        UPDATE office.users
        SET password = _password
        WHERE office.users.user_name=NEW.user_name;
    END IF;
    
    RETURN new;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER hash_password
AFTER INSERT ON office.users
FOR EACH ROW
EXECUTE PROCEDURE office.hash_password();