CREATE TYPE office.office_type AS
(
    office_id                   integer,
    office_code                 national character varying(12),
    office_name                 national character varying(150),
    address text
);

CREATE FUNCTION office.get_offices()
RETURNS setof office.office_type
AS
$$
BEGIN
    RETURN QUERY
    SELECT office_id, office_code,office_name,street || ' ' || city AS Address FROM office.offices;
END
$$
LANGUAGE plpgsql;
