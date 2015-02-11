CREATE FUNCTION transactions.are_sales_quotations_already_merged(VARIADIC arr bigint[])
RETURNS boolean
AS
$$
BEGIN
    IF
    (
        SELECT 
        COUNT(*) 
        FROM transactions.non_gl_stock_master_relations 
        WHERE quotation_non_gl_stock_master_id = any($1)
    ) > 0 THEN
        RETURN true;
    END IF;

    IF
    (
        SELECT 
        COUNT(*) 
        FROM transactions.stock_master_non_gl_relations
        WHERE non_gl_stock_master_id = any($1)
    ) > 0 THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;   

