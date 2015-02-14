DROP FUNCTION IF EXISTS core.get_workflow_model();

CREATE FUNCTION core.get_workflow_model()
RETURNS TABLE
(
    flagged_transactions        integer,
    in_verification_stack       integer,
    auto_approved               integer,
    approved                    integer,
    rejected                    integer,
    closed                      integer,
    withdrawn                   integer
)
VOLATILE
AS
$$
    DECLARE _flagged            integer;
    DECLARE _in_verification    integer;
    DECLARE _auto_approved      integer;
    DECLARE _approved           integer;
    DECLARE _rejected           integer;
    DECLARE _closed             integer;
    DECLARE _withdrawn          integer;
BEGIN
    SELECT COUNT(*) INTO _flagged 
    FROM core.flags;

    SELECT COUNT(*) INTO _in_verification
    FROM transactions.transaction_master
    WHERE verification_status_id = 0;

    SELECT COUNT(*) INTO _auto_approved
    FROM transactions.transaction_master
    WHERE verification_status_id = 1;

    SELECT COUNT(*) INTO _approved
    FROM transactions.transaction_master
    WHERE verification_status_id = 2;

    SELECT COUNT(*) INTO _rejected
    FROM transactions.transaction_master
    WHERE verification_status_id = -3;

    SELECT COUNT(*) INTO _closed
    FROM transactions.transaction_master
    WHERE verification_status_id = -2;

    SELECT COUNT(*) INTO _withdrawn
    FROM transactions.transaction_master
    WHERE verification_status_id = -1;

    RETURN QUERY
    SELECT
        _flagged, 
        _in_verification, 
        _auto_approved,
        _approved,
        _rejected,
        _closed,
        _withdrawn;
END
$$
LANGUAGE plpgsql;