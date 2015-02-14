DROP FUNCTION IF EXISTS core.get_tax_master_id_by_tax_master_code(text);

CREATE FUNCTION core.get_tax_master_id_by_tax_master_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN tax_master_id
    FROM core.tax_master
    WHERE tax_master_code=$1;
END
$$
LANGUAGE plpgsql;