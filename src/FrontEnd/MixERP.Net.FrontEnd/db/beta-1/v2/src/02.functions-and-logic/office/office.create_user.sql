DROP FUNCTION IF EXISTS office.create_user
(
    _role_id		integer,
    _department_id	integer,
    _office_id		integer,
    _user_name 		text,
    _password 		text,
    _full_name 		text,
    _elevated 		boolean
);

CREATE FUNCTION office.create_user
(
    _role_id		integer,
    _department_id	integer,
    _office_id		integer,
    _user_name 		text,
    _password 		text,
    _full_name 		text,
    _elevated 		boolean = false
)
RETURNS VOID
AS
$$
BEGIN
    IF(COALESCE(_user_name, '')) THEN
        RETURN;
    END IF;

    INSERT INTO office.users(role_id, department_id, office_id, user_name, password, full_name, elevated)
    SELECT _role_id, _department_id, _office_id, _user_name, _password, _full_name, _elevated;
    RETURN;
END
$$
LANGUAGE plpgsql;
