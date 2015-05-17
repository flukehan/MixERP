DROP FUNCTION IF EXISTS core.get_attachment_lookup_info(national character varying(50));

CREATE FUNCTION core.get_attachment_lookup_info(_book national character varying(50))
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN resource || resource_key
    FROM core.attachment_lookup
    WHERE book=$1;
END
$$
LANGUAGE plpgsql;


