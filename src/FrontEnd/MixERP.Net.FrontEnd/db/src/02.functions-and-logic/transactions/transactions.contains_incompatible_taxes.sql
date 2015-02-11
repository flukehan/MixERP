DROP FUNCTION IF EXISTS transactions.contains_incompatible_taxes(VARIADIC arr bigint[]);


CREATE FUNCTION transactions.contains_incompatible_taxes(VARIADIC arr bigint[])
RETURNS boolean
AS
$$
BEGIN
    IF
    (
        SELECT COUNT(DISTINCT non_taxable) FROM transactions.non_gl_stock_master
        WHERE non_gl_stock_master_id = any($1)
    ) > 1 THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;   



