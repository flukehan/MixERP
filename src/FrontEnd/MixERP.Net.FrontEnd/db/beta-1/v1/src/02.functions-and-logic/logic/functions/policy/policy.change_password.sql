DROP FUNCTION IF EXISTS policy.change_password
(
    _user_name          text,
    _current_password   text,
    _new_password       text
);

CREATE FUNCTION policy.change_password
(
    _user_name          text,
    _current_password   text,
    _new_password       text
)
RETURNS boolean
VOLATILE
AS
$$
BEGIN
    IF(COALESCE($1, '') = '') THEN
        RAISE EXCEPTION 'Invalid user name.'
        USING ERRCODE='P3001';
    END IF;

    IF(COALESCE($2, '') = '' OR COALESCE($3, '') = '') THEN
        RAISE EXCEPTION 'Password cannot be empty.'
        USING ERRCODE='P3005';
    END IF;

    IF($2=$3) THEN
        RAISE EXCEPTION 'Please provide a new password.'
        USING ERRCODE='P3006';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM office.users
        WHERE user_name = $1
        AND can_change_password
        AND role_id NOT IN
        (
            SELECT role_id FROM office.roles
            WHERE is_system
        )
    ) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9001';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM office.users 
        WHERE office.users.user_name=$1
        AND encode(digest($1 || $2, 'sha512'), 'hex') = office.users.password 
    ) THEN
        RAISE EXCEPTION 'Your current password is incorrect.'
        USING ERRCODE='P3104';
    END IF;

    UPDATE office.users
    SET password = encode(digest($1 || $3, 'sha512'), 'hex')
    WHERE office.users.user_name=$1;
    

    RETURN true;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS policy.change_password
(
    _admin_user_id          integer,
    _user_name              text,
    _new_password           text
);

CREATE FUNCTION policy.change_password
(
    _admin_user_id          integer,
    _user_name              text,
    _new_password           text
)
RETURNS void
VOLATILE
AS
$$
    DECLARE _user_id            integer;
    DECLARE _office_id          integer;
    DECLARE _admin_office_id    integer;
BEGIN
    IF(COALESCE($2, '') = '') THEN
        RAISE EXCEPTION 'Invalid user name.'
        USING ERRCODE='P3001';
    END IF;

    IF(COALESCE($3, '') = '') THEN
        RAISE EXCEPTION 'Password cannot be empty.'
        USING ERRCODE='P3005';
    END IF;

    SELECT 
        office.users.user_id,
        office.users.office_id
    INTO
        _user_id,
        _office_id
    FROM office.users
    WHERE office.users.user_name=_user_name;

    IF(COALESCE(_user_id, 0) = 0) THEN
        RAISE EXCEPTION 'Invalid user name.'
        USING ERRCODE='P3001';
    END IF;

    IF(NOT office.is_admin(_admin_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9001';
    END IF;

    SELECT office.users.office_id INTO _admin_office_id
    FROM office.users
    WHERE office.users.user_id = _admin_user_id;

    IF(_admin_office_id != _office_id AND NOT office.is_parent_office(_admin_office_id, _office_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9001';
    END IF;

    UPDATE office.users
    SET password = encode(digest($2 || $3, 'sha512'), 'hex')
    WHERE office.users.user_name=$2;    
END
$$
LANGUAGE plpgsql;