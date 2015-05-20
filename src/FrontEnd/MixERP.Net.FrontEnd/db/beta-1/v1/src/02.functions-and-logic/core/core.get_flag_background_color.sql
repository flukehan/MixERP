CREATE FUNCTION core.get_flag_background_color(flag_type_id_ integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN background_color
    FROM core.flag_types
    WHERE core.flag_types.flag_type_id=$1;
END
$$
LANGUAGE plpgsql;
