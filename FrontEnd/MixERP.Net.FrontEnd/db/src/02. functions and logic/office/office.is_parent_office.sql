
CREATE FUNCTION office.is_parent_office(parent integer_strict, child integer_strict)
RETURNS boolean
AS
$$      
BEGIN
    IF $1!=$2 THEN
        IF EXISTS
        (
            WITH RECURSIVE office_cte(office_id, path) AS (
             SELECT
                tn.office_id,  tn.office_id::TEXT AS path
                FROM office.offices AS tn WHERE tn.parent_office_id IS NULL
            UNION ALL
             SELECT
                c.office_id, (p.path || '->' || c.office_id::TEXT)
                FROM office_cte AS p, office.offices AS c WHERE parent_office_id = p.office_id
            )
            SELECT * FROM
            (
                SELECT regexp_split_to_table(path, '->')
                FROM office_cte AS n WHERE n.office_id = $2
            ) AS items
            WHERE regexp_split_to_table=$1::text
        ) THEN
            RETURN TRUE;
        END IF;
    END IF;
    RETURN false;
END
$$
LANGUAGE plpgsql;


ALTER TABLE office.offices
ADD CONSTRAINT offices_check_if_parent_chk
        CHECK
        (
            office.is_parent_office(office_id, parent_office_id) = FALSE
            AND
            parent_office_id != office_id
        );
