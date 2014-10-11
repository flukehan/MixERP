CREATE FUNCTION core.is_parent_unit(parent integer, child integer)
RETURNS boolean
AS
$$      
BEGIN
    IF $1!=$2 THEN
        IF EXISTS
        (
            WITH RECURSIVE unit_cte(unit_id) AS 
            (
             SELECT tn.compare_unit_id
                FROM core.compound_units AS tn WHERE tn.base_unit_id = $1
            UNION ALL
             SELECT
                c.compare_unit_id
                FROM unit_cte AS p, 
              core.compound_units AS c 
                WHERE base_unit_id = p.unit_id
            )

            SELECT * FROM unit_cte
            WHERE unit_id=$2
        ) THEN
            RETURN TRUE;
        END IF;
    END IF;
    RETURN false;
END
$$
LANGUAGE plpgsql;
