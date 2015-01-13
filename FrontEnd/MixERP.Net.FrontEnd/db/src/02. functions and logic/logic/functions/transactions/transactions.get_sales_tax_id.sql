DROP FUNCTION IF EXISTS transactions.get_sales_tax_id
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12),
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_code                  national character varying(12),
    _unit_id                    integer,
    _price                      money_strict
);

CREATE FUNCTION transactions.get_sales_tax_id
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12),
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_code                  national character varying(12),
    _unit_id                    integer,--todo
    _price                      money_strict
)
RETURNS integer
AS
$$
    DECLARE _item_id                    integer;
    DECLARE _party_id                   bigint;
    DECLARE _party_type_id              integer;
    DECLARE _value_date                 date;
    DECLARE _sales_tax_id               integer;
    DECLARE _item_group_id              integer;
    DECLARE _entity_id                  integer;
    DECLARE _industry_id                integer;
    DECLARE _office_id                  integer;
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

    IF(COALESCE(_price, 0) = 0 ) THEN
        RETURN 0;
    END IF;

    _office_id      := office.get_office_id_by_store_id(_store_id);
    _item_id        := core.get_item_id_by_item_code(_item_code);
    _party_id       := core.get_party_id_by_party_code(_party_code);
    _party_type_id  := core.get_party_type_id_by_party_id(_party_id);
    _value_date     := transactions.get_value_date(_office_id);
    _item_group_id  := core.get_item_group_id_by_item_id(_item_id);
    _entity_id      := core.get_entity_id_by_party_id(_party_id);
    _industry_id    := core.get_industry_id_by_party_id(_party_id);
       
    --Exempt by item
    SELECT core.sales_tax_exempts.sales_tax_id INTO _sales_tax_id
    FROM core.sales_tax_exempts
    INNER JOIN core.sales_tax_exempt_details
    ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
    WHERE (item_id = _item_id)
    AND store_id = _store_id
    AND price_from <= _price AND price_to >= _price
    AND core.sales_tax_exempts.valid_from <= _value_date AND core.sales_tax_exempts.valid_till >= _value_date;

    IF(_sales_tax_id IS NOT NULL) THEN
        RETURN _sales_tax_id;
    END IF;

    --Exempt by item group
    SELECT core.sales_tax_exempts.sales_tax_id INTO _sales_tax_id
    FROM core.sales_tax_exempts
    INNER JOIN core.sales_tax_exempt_details
    ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
    WHERE (item_group_id = _item_group_id)
    AND store_id = _store_id
    AND price_from <= _price AND price_to >= _price
    AND core.sales_tax_exempts.valid_from <= _value_date AND core.sales_tax_exempts.valid_till >= _value_date;

    IF(_sales_tax_id IS NOT NULL) THEN
        RETURN _sales_tax_id;
    END IF;

    --Exempt by party
    SELECT core.sales_tax_exempts.sales_tax_id INTO _sales_tax_id
    FROM core.sales_tax_exempts
    INNER JOIN core.sales_tax_exempt_details
    ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
    WHERE (party_id = _party_id)
    AND store_id = _store_id
    AND price_from <= _price AND price_to >= _price
    AND core.sales_tax_exempts.valid_from <= _value_date AND core.sales_tax_exempts.valid_till >= _value_date;

    --Exempt by party type
    SELECT core.sales_tax_exempts.sales_tax_id INTO _sales_tax_id
    FROM core.sales_tax_exempts
    INNER JOIN core.sales_tax_exempt_details
    ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
    WHERE (party_type_id = _party_type_id)
    AND store_id = _store_id
    AND price_from <= _price AND price_to >= _price
    AND core.sales_tax_exempts.valid_from <= _value_date AND core.sales_tax_exempts.valid_till >= _value_date;

    IF(_sales_tax_id IS NOT NULL) THEN
        RETURN _sales_tax_id;
    END IF;

    --Exempt by entity
    IF(_entity_id IS NOT NULL) THEN
        SELECT core.sales_tax_exempts.sales_tax_id INTO _sales_tax_id
        FROM core.sales_tax_exempts
        INNER JOIN core.sales_tax_exempt_details
        ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
        WHERE (entity_id = _entity_id)
        AND store_id = _store_id
        AND price_from <= _price AND price_to >= _price
        AND core.sales_tax_exempts.valid_from <= _value_date AND core.sales_tax_exempts.valid_till >= _value_date;

        IF(_sales_tax_id IS NOT NULL) THEN
            RETURN _sales_tax_id;
        END IF;
    END IF;

    --Exempt by industry
    IF(_industry_id IS NOT NULL) THEN
        SELECT core.sales_tax_exempts.sales_tax_id INTO _sales_tax_id
        FROM core.sales_tax_exempts
        INNER JOIN core.sales_tax_exempt_details
        ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
        WHERE (industry_id = _industry_id)
        AND store_id = _store_id
        AND price_from <= _price AND price_to >= _price
        AND core.sales_tax_exempts.valid_from <= _value_date AND core.sales_tax_exempts.valid_till >= _value_date;

        IF(_sales_tax_id IS NOT NULL) THEN
            RETURN _sales_tax_id;
        END IF;
    END IF;
    

    --Get default tax from store
    SELECT sales_tax_id INTO _sales_tax_id FROM office.stores WHERE store_id=_store_id;    
    IF(_sales_tax_id IS NOT NULL) THEN
        RETURN _sales_tax_id;
    END IF;

    --Fallback to item sales tax

    RETURN
        sales_tax_id
    FROM
        core.items
    WHERE
        item_id=_item_id;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_sales_tax_id('Purchase', 1, 'JASMI-0002', '', 1, 'RMBP', 1, 30000);