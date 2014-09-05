CREATE FUNCTION core.get_agent_name_by_agent_id(integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT agent_name
		FROM core.agents
		WHERE agent_id=$1
	);
END
$$
LANGUAGE plpgsql;
