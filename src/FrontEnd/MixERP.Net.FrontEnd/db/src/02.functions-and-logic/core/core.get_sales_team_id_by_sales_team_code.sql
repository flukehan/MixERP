DROP FUNCTION IF EXISTS core.get_sales_team_id_by_sales_team_code(text);

CREATE FUNCTION core.get_sales_team_id_by_sales_team_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN sales_team_id
    FROM core.sales_teams
    WHERE sales_team_code=$1;
END
$$
LANGUAGE plpgsql;