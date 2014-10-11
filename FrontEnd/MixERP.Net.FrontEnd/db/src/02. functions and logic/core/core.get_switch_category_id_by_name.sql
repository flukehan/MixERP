CREATE FUNCTION core.get_switch_category_id_by_name(text)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT switch_category_id
        FROM core.switch_categories
        WHERE core.switch_categories.switch_category_name=$1
    );
END
$$
LANGUAGE plpgsql;

