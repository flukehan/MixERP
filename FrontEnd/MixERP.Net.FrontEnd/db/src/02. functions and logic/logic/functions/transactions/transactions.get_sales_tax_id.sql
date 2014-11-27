DROP FUNCTION IF EXISTS transactions.get_sales_tax_id
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12),
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_code                  national character varying(12)    
);

CREATE FUNCTION transactions.get_sales_tax_id
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12),
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_code                  national character varying(12)    
)
RETURNS integer
AS
$$
BEGIN
    IF(COALESCE(_tran_book, '') = '') THEN
        RETURN 0;
    END IF;

    IF(COALESCE(_store_id, 0) = 0) THEN
        RETURN 0;
    END IF;

    IF(COALESCE(_party_code, '') = '') THEN
        RETURN 0;
    END IF;
    

    RETURN 2; --Todo
END
$$
LANGUAGE plpgsql;