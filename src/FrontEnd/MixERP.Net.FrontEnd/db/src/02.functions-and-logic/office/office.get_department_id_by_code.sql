DROP FUNCTION IF EXISTS office.get_department_id_by_code(text);

CREATE FUNCTION office.get_department_id_by_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN department_id
    FROM office.departments
    WHERE department_code=$1;
END
$$
LANGUAGE plpgsql;