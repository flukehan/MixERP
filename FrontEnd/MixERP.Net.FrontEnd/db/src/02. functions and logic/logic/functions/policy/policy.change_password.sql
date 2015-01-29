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
        RAISE EXCEPTION 'Invalid user name.';
    END IF;

    IF(COALESCE($2, '') = '' OR COALESCE($3, '') = '') THEN
        RAISE EXCEPTION 'Password cannot be empty.';
    END IF;

    IF($2=$3) THEN
        RAISE EXCEPTION 'Please provide a new password.';
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
        RAISE EXCEPTION 'Access is denied.';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM office.users 
        WHERE office.users.user_name=$1
        AND encode(digest($1 || $2, 'sha512'), 'hex') = office.users.password 
    ) THEN
        RAISE EXCEPTION 'Your current password is incorrect.';
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
    user_name           text,
    new_password        text
);

CREATE FUNCTION policy.change_password
(
    user_name           text,
    new_password        text
)
RETURNS boolean
VOLATILE
AS
$$
BEGIN
    IF(COALESCE($1, '') = '') THEN
        RAISE EXCEPTION 'Invalid user name.';
    END IF;

    IF(COALESCE($2, '') = '') THEN
        RAISE EXCEPTION 'Password cannot be empty.';
    END IF;

    UPDATE office.users
    SET password = encode(digest($1 || $2, 'sha512'), 'hex')
    WHERE office.users.user_name=$1;
    
    RETURN true;
END
$$
LANGUAGE plpgsql;