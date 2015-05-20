DROP FUNCTION IF EXISTS office.get_office_ids(root_office_id integer);

CREATE FUNCTION office.get_office_ids(root_office_id integer)
RETURNS SETOF integer
AS
$$
BEGIN
    RETURN QUERY 
    (
        WITH RECURSIVE office_cte(office_id, path) AS (
         SELECT
            tn.office_id,  tn.office_id::TEXT AS path
            FROM office.offices AS tn WHERE tn.office_id =$1
        UNION ALL
         SELECT
            c.office_id, (p.path || '->' || c.office_id::TEXT)
            FROM office_cte AS p, office.offices AS c WHERE parent_office_id = p.office_id
        )

        SELECT office_id FROM office_cte
    );
END
$$LANGUAGE plpgsql;