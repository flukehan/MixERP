CREATE FUNCTION transactions.has_nexus(_state_id integer)
RETURNS boolean
AS
$$
BEGIN
    RETURN false;--Todo
END
$$
LANGUAGE plpgsql;