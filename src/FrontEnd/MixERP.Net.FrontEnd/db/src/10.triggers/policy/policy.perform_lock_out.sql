--TODO: Create a lockout policy.
CREATE FUNCTION policy.perform_lock_out()
RETURNS TRIGGER
AS
$$
BEGIN
    IF(
        SELECT COUNT(*) FROM audit.failed_logins
        WHERE audit.failed_logins.user_id=NEW.user_id
        AND audit.failed_logins.failed_date_time 
        BETWEEN NOW()-'5minutes'::interval 
        AND NOW()
    )::integer>5 THEN

    INSERT INTO policy.lock_outs(user_id)SELECT NEW.user_id;
END IF;
RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER lockout_user
AFTER INSERT
ON audit.failed_logins
FOR EACH ROW EXECUTE PROCEDURE policy.perform_lock_out();

