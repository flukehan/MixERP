CREATE OR REPLACE FUNCTION core.disable_editing_sys_type()
RETURNS TRIGGER
AS
$$
BEGIN
    IF TG_OP='UPDATE' OR TG_OP='DELETE' THEN
        IF EXISTS
        (
            SELECT *
            FROM core.accounts
            WHERE (sys_type=true)
            AND account_id=OLD.account_id
        ) THEN
            RAISE EXCEPTION 'You are not allowed to change system accounts.'
            USING ERRCODE='P8990';
        END IF;
    END IF;
    
    IF TG_OP='INSERT' THEN
        IF (NEW.sys_type=true) THEN
            RAISE EXCEPTION 'You are not allowed to add system accounts.'
            USING ERRCODE='P8991';
        END IF;
    END IF;

    IF TG_OP='DELETE' THEN
        RETURN OLD;
    END IF;

    RETURN NEW; 
END
$$
LANGUAGE plpgsql;
