CREATE FUNCTION core.get_flag_type_id
(
    user_id_        integer,
    resource_       text,
    resource_key_   text,
    resource_id_    text
)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN flag_type_id
    FROM core.flags
    WHERE user_id=$1
    AND resource=$2
    AND resource_key=$3
    AND resource_id=$4;
END
$$
LANGUAGE plpgsql;

-- CREATE FUNCTION core.get_flag_type_id
-- (
--     user_id_        integer,
--     resource_       text,
--     resource_id_    text
-- )
-- RETURNS integer
-- STABLE
-- AS
-- $$
-- BEGIN
--     RETURN core.get_flag_type_id($1, $2, $3::text);
-- END
-- $$
-- LANGUAGE plpgsql;

