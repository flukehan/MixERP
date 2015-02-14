DROP FUNCTION IF EXISTS office.get_cost_center_id_by_cost_center_code(text);

CREATE FUNCTION office.get_cost_center_id_by_cost_center_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN cost_center_id
    FROM office.cost_centers
    WHERE cost_center_code=$1;
END
$$
LANGUAGE plpgsql;