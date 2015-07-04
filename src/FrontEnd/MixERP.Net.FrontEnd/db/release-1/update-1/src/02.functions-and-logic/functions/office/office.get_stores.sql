DROP FUNCTION IF EXISTS office.get_stores
(
    _office_id  integer,
    _user_id    integer
);

CREATE FUNCTION office.get_stores
(
    _office_id  integer,
    _user_id    integer
)
RETURNS SETOF office.stores
AS
$$
    DECLARE _store_id   integer;
BEGIN
    SELECT store_id
    INTO _store_id
    FROM office.users
    WHERE user_id = _user_id;

    IF(_store_id IS NOT NULL) THEN
        RETURN QUERY
        SELECT * FROM office.stores
        WHERE store_id = _store_id;
        RETURN;
    END IF;

    RETURN QUERY
    SELECT * FROM office.stores
    WHERE office_id IN (SELECT office.get_office_ids(_office_id));
END
$$
LANGUAGE plpgsql;

--SELECT * FROM office.get_stores(2, 1);