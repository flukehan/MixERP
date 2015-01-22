CREATE FUNCTION core.create_flag
(
    user_id_            integer,
    flag_type_id_       integer,
    resource_           text,
    resource_key_       text,
    resource_id_        text
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS(SELECT * FROM core.flags WHERE user_id=user_id_ AND resource=resource_ AND resource_key=resource_key_ AND resource_id=resource_id_) THEN
        INSERT INTO core.flags(user_id, flag_type_id, resource, resource_key, resource_id)
        SELECT user_id_, flag_type_id_, resource_, resource_key_, resource_id_;
    ELSE
        UPDATE core.flags
        SET
            flag_type_id=flag_type_id_
        WHERE 
            user_id=user_id_ 
        AND 
            resource=resource_ 
        AND 
            resource_key=resource_key_ 
        AND 
            resource_id=resource_id_;
    END IF;
END
$$
LANGUAGE plpgsql;

