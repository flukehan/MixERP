DROP FUNCTION IF EXISTS policy.check_menu_policy_trigger() CASCADE;


CREATE FUNCTION policy.check_menu_policy_trigger()
RETURNS trigger
AS
$$
    DECLARE count integer=0;
BEGIN
    IF NEW.office_id IS NOT NULL THEN
        count := count + 1;
    END IF;

    IF NEW.role_id IS NOT NULL THEN
        count := count + 1;
    END IF;
    
    IF NEW.user_id IS NOT NULL THEN
        count := count + 1;
    END IF;

    IF count <> 1 THEN
        RAISE EXCEPTION 'Only one of the following columns is required : %', 'office_id, role_id, user_id.'
        USING ERRCODE='P8501';
    END IF;

    RETURN NEW;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER check_menu_policy_trigger BEFORE INSERT
ON policy.menu_policy
FOR EACH ROW EXECUTE PROCEDURE policy.check_menu_policy_trigger();
