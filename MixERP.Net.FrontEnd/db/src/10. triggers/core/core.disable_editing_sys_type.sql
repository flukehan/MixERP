CREATE FUNCTION core.disable_editing_sys_type()
RETURNS TRIGGER
AS
$$
BEGIN
	IF TG_OP='UPDATE' OR TG_OP='DELETE' THEN
		IF EXISTS
		(
			SELECT *
			FROM core.accounts
			WHERE (sys_type=true OR is_cash=true)
			AND account_id=OLD.account_id
		) THEN
			RAISE EXCEPTION 'You are not allowed to change system accounts.';
		END IF;
	END IF;
	
	IF TG_OP='INSERT' THEN
		IF (NEW.sys_type=true OR NEW.is_cash=true) THEN
			RAISE EXCEPTION 'You are not allowed to add system accounts.';
		END IF;
	END IF;

	IF TG_OP='DELETE' THEN
		RETURN OLD;
	END IF;

	RETURN NEW;	
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER restrict_delete_sys_type_trigger
BEFORE DELETE
ON core.accounts
FOR EACH ROW EXECUTE PROCEDURE core.disable_editing_sys_type();

CREATE TRIGGER restrict_update_sys_type_trigger
BEFORE UPDATE
ON core.accounts
FOR EACH ROW EXECUTE PROCEDURE core.disable_editing_sys_type();

CREATE TRIGGER restrict_insert_sys_type_trigger
BEFORE INSERT
ON core.accounts
FOR EACH ROW EXECUTE PROCEDURE core.disable_editing_sys_type();

