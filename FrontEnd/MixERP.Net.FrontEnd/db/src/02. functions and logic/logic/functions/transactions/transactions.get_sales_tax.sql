DROP FUNCTION IF EXISTS transactions.get_sales_tax
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12), 
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_id                    national character varying(12),
    _price                      money_strict2,
    _quantity                   integer_strict2,
    _discount                   money_strict2,
    _shipping_charge            money_strict2,
    _sales_tax_id               integer
);

CREATE FUNCTION transactions.get_sales_tax
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12), 
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_id                    national character varying(12),
    _price                      money_strict2,
    _quantity                   integer_strict2,
    _discount                   money_strict2,
    _shipping_charge            money_strict2,
    _sales_tax_id               integer
)
RETURNS money_strict2
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
    
    IF(COALESCE(_price, 0) = 0) THEN
        RETURN 0;
    END IF;
    
    IF(COALESCE(_quantity, 0) = 0) THEN
        RETURN 0;
    END IF;
    
    IF(COALESCE(_sales_tax_id, 0) = 0) THEN
        RETURN 0;
    END IF;

    RETURN 100;--Todo;
END
$$
LANGUAGE plpgsql;
    