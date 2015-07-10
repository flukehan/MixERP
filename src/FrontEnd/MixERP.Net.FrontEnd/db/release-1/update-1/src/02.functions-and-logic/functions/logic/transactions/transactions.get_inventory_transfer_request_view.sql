DROP FUNCTION IF EXISTS transactions.get_inventory_transfer_request_view
(
    _user_id                integer,
    _login_id               bigint,
    _office_id              integer,
    _from                   date,
    _to                     date,
    _office                 text,
    _store                  text,
    _authorized             text,
    _delivered              text,
    _received               text,
    _user                   text,
    _reference_number       text,
    _statement_reference    text
);

CREATE FUNCTION transactions.get_inventory_transfer_request_view
(
    _user_id                integer,
    _login_id               bigint,
    _office_id              integer,
    _from                   date,
    _to                     date,
    _office                 text,
    _store                  text,
    _authorized             text,
    _delivered              text,
    _received               text,
    _user                   text,
    _reference_number       text,
    _statement_reference    text
)
RETURNS TABLE
(
    id                      bigint,
    value_date              date,
    office                  text,
    user_name               text,
    store                   text,
    reference_number        text,
    statement_reference     text,
    authorized              text,
    delivered               text,
    received                text,
    flag_background_color   text,
    flag_foreground_color   text
)
AS
$$
    DECLARE _store_id       integer;
BEGIN
    SELECT 
        store_id 
    INTO 
        _store_id
    FROM office.users
    WHERE user_id = _user_id
    AND office_id = _office_id;

    IF(_store_id IS NULL) THEN
        RETURN QUERY
        SELECT
            transactions.inventory_transfer_requests.inventory_transfer_request_id,
            transactions.inventory_transfer_requests.value_date,
            office.offices.office_code || ' (' || office.offices.office_name || ')'::text AS office,
            office.users.user_name::text,
            office.stores.store_code || ' (' || office.stores.store_name || ')'::text AS store,
            transactions.inventory_transfer_requests.reference_number::text,
            transactions.inventory_transfer_requests.statement_reference::text,
            core.verification_statuses.verification_status_id::text || ' (' || core.verification_statuses.verification_status_name || ')'::text AS authorized,
            transactions.inventory_transfer_requests.delivered::text,
            transactions.inventory_transfer_requests.received::text,
            core.get_flag_background_color(core.get_flag_type_id(_user_id, 'transactions.inventory_transfer_requests', 'inventory_transfer_request_id', transactions.inventory_transfer_requests.inventory_transfer_request_id::text)) AS flag_bg,
            core.get_flag_foreground_color(core.get_flag_type_id(_user_id, 'transactions.inventory_transfer_requests', 'inventory_transfer_request_id', transactions.inventory_transfer_requests.inventory_transfer_request_id::text)) AS flag_fg            
        FROM transactions.inventory_transfer_requests
        INNER JOIN office.offices
        ON transactions.inventory_transfer_requests.office_id = office.offices.office_id
        INNER JOIN office.users
        ON transactions.inventory_transfer_requests.user_id = office.users.user_id
        INNER JOIN office.stores
        ON transactions.inventory_transfer_requests.store_id = office.stores.store_id
        INNER JOIN core.verification_statuses
        ON transactions.inventory_transfer_requests.authorization_status_id = core.verification_statuses.verification_status_id
        WHERE transactions.inventory_transfer_requests.value_date >= _from
        AND transactions.inventory_transfer_requests.value_date <= _to
        AND lower(office_code || ' (' || office_name || ')') LIKE '%' || lower(_office) || '%'
        AND lower(office.users.user_name) LIKE '%' || lower(_user) || '%'
        AND lower(office.stores.store_code || ' (' || office.stores.store_name || ')') LIKE '%' || lower(_store) || '%'
        AND lower(transactions.inventory_transfer_requests.reference_number) LIKE '%' || lower(_reference_number) || '%'
        AND lower(transactions.inventory_transfer_requests.statement_reference) LIKE '%' || lower(_statement_reference) || '%'
        AND CASE WHEN core.verification_statuses.verification_status_id > 0 THEN 'true' WHEN verification_status_id <= 0 AND NOT verification_status_id = -1 THEN 'false' ELSE 'withdrawn' END LIKE '%' || lower(_authorized) || '%'
        AND lower(transactions.inventory_transfer_requests.delivered::text) LIKE '%' || lower(_delivered) || '%'
        AND lower(transactions.inventory_transfer_requests.received::text) LIKE '%' || lower(_received) || '%';
        RETURN;
    END IF;

    RETURN QUERY
    SELECT
        transactions.inventory_transfer_requests.inventory_transfer_request_id,
        transactions.inventory_transfer_requests.value_date,
        office.offices.office_code || ' (' || office.offices.office_name || ')'::text AS office,
        office.users.user_name::text,
        office.stores.store_code || ' (' || office.stores.store_name || ')'::text AS store,
        transactions.inventory_transfer_requests.reference_number::text,
        transactions.inventory_transfer_requests.statement_reference::text,
        core.verification_statuses.verification_status_id::text || ' (' || core.verification_statuses.verification_status_name || ')'::text AS authorized,
        transactions.inventory_transfer_requests.delivered::text,
        transactions.inventory_transfer_requests.received::text,
        core.get_flag_background_color(core.get_flag_type_id(_user_id, 'transactions.inventory_transfer_requests', 'inventory_transfer_request_id', transactions.inventory_transfer_requests.inventory_transfer_request_id::text)) AS flag_bg,
        core.get_flag_foreground_color(core.get_flag_type_id(_user_id, 'transactions.inventory_transfer_requests', 'inventory_transfer_request_id', transactions.inventory_transfer_requests.inventory_transfer_request_id::text)) AS flag_fg            
    FROM transactions.inventory_transfer_requests
    INNER JOIN office.offices
    ON transactions.inventory_transfer_requests.office_id = office.offices.office_id
    INNER JOIN office.users
    ON transactions.inventory_transfer_requests.user_id = office.users.user_id
    INNER JOIN office.stores
    ON transactions.inventory_transfer_requests.store_id = office.stores.store_id
    INNER JOIN core.verification_statuses
    ON transactions.inventory_transfer_requests.authorization_status_id = core.verification_statuses.verification_status_id
    WHERE transactions.inventory_transfer_requests.value_date >= _from
    AND transactions.inventory_transfer_requests.value_date <= _to
    AND transactions.inventory_transfer_requests.store_id = _store_id
    AND lower(office_code || ' (' || office_name || ')') LIKE '%' || lower(_office) || '%'
    AND lower(office.users.user_name) LIKE '%' || lower(_user) || '%'
    AND lower(transactions.inventory_transfer_requests.reference_number) LIKE '%' || lower(_reference_number) || '%'
    AND lower(transactions.inventory_transfer_requests.statement_reference) LIKE '%' || lower(_statement_reference) || '%'
    AND CASE WHEN core.verification_statuses.verification_status_id > 0 THEN 'true' WHEN verification_status_id <= 0 AND NOT verification_status_id = -1 THEN 'false' ELSE 'withdrawn' END LIKE '%' || lower(_authorized) || '%'
    AND lower(transactions.inventory_transfer_requests.delivered::text) LIKE '%' || lower(_delivered) || '%'
    AND lower(transactions.inventory_transfer_requests.received::text) LIKE '%' || lower(_received) || '%';
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.get_inventory_transfer_request_view(2, 5, 1,'2010-1-1','2020-1-1','','','','','','','','');