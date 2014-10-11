CREATE TYPE office.office_type AS
(
    office_id               integer_strict,
    office_code                 national character varying(12),
    office_name                 national character varying(150),
    address text
);

CREATE FUNCTION office.get_offices()
RETURNS setof office.office_type
AS
$$
DECLARE "@record" office.office_type%rowtype;
BEGIN
    FOR "@record" IN SELECT office_id, office_code,office_name,street || ' ' || city AS Address FROM office.offices WHERE parent_office_id IS NOT NULL
    LOOP
        RETURN NEXT "@record";
    END LOOP;

    IF NOT FOUND THEN
        FOR "@record" IN SELECT office_id, office_code,office_name,street || ' ' || city AS Address FROM office.offices WHERE parent_office_id IS NULL
        LOOP
            RETURN NEXT "@record";
        END LOOP;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;
