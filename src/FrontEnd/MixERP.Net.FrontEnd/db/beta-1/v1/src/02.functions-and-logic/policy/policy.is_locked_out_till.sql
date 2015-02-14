CREATE FUNCTION policy.is_locked_out_till(user_id integer_strict)
RETURNS TIMESTAMP
AS
$$
BEGIN
    RETURN
    (
        SELECT MAX(policy.lock_outs.lock_out_till)::TIMESTAMP WITHOUT TIME ZONE FROM policy.lock_outs
        WHERE policy.lock_outs.user_id=$1
    );
END
$$
LANGUAGE plpgsql;

