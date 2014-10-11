CREATE FUNCTION office.get_office_name_by_id(office_id integer_strict)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT office.offices.office_name FROM office.offices
        WHERE office.offices.office_id=$1
    );
END
$$
LANGUAGE plpgsql;
