CREATE FUNCTION core.get_account_id_by_parameter(text)
RETURNS bigint
AS
$$
BEGIN
    RETURN
    (
        SELECT
            account_id
        FROM    
            core.account_parameters
        WHERE
            parameter_name=$1
    );
END
$$
LANGUAGE plpgsql;
