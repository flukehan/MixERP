CREATE FUNCTION office.get_office_id_by_office_code(office_code text)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT office.offices.office_id FROM office.offices
        WHERE office.offices.office_code=$1
    );
END
$$
LANGUAGE plpgsql;
