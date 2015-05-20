DROP FUNCTION IF EXISTS office.user_trigger() CASCADE;

CREATE FUNCTION office.user_trigger()
RETURNS trigger
AS
$$
BEGIN
    IF(office.is_sys(NEW.user_id) AND NEW.password != '') THEN
        RAISE EXCEPTION 'A sys user cannot have a password.'
        USING ERRCODE='P8992';
    END IF; 

    RETURN new;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER user_trigger
AFTER INSERT OR UPDATE ON office.users
FOR EACH ROW
EXECUTE PROCEDURE office.user_trigger();

