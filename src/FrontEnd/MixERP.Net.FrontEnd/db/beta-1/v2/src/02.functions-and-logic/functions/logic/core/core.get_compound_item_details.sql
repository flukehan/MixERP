DROP FUNCTION IF EXISTS core.get_compound_item_details
(
    _compound_item_code         national character varying(12),
    _sales_tax_code             national character varying(24),
    _tran_book                  national character varying(48),
    _store_id                   integer,
    _party_code                 national character varying(12),
    _price_type_id              integer    
);

CREATE FUNCTION core.get_compound_item_details
(
    _compound_item_code         national character varying(12),
    _sales_tax_code             national character varying(24),
    _tran_book                  national character varying(48),
    _store_id                   integer,
    _party_code                 national character varying(12),
    _price_type_id              integer    
)
RETURNS TABLE
(
    id                          integer,
    item_id                     integer,
    item_code                   text,
    item_name                   text,
    quantity                    public.integer_strict,
    unit_id                     integer,
    unit_name                   text,
    price                       public.money_strict2,
    discount                    public.money_strict2,
    sales_tax_id                integer,
    sales_tax_code              text,
    computed_tax                public.money_strict2
)
AS
$$
    DECLARE this                RECORD;
BEGIN
    DROP TABLE IF EXISTS temp_details;

    CREATE TEMPORARY TABLE temp_details
    (
        id                          SERIAL NOT NULL,
        item_id                     integer,
        item_code                   text,
        item_name                   text,
        quantity                    public.integer_strict,
        unit_id                     integer,
        unit_name                   text,
        price                       public.money_strict2,
        discount                    public.money_strict2,
        sales_tax_id                integer,
        sales_tax_code              text,
        computed_tax                public.money_strict2
    ) ON COMMIT DROP;
    
    INSERT INTO temp_details(item_id, quantity, unit_id, price, discount, sales_tax_code)
    SELECT 
        core.compound_item_details.item_id, 
        core.compound_item_details.quantity, 
        core.compound_item_details.unit_id, 
        core.compound_item_details.price, 
        core.compound_item_details.discount,
        _sales_tax_code
    FROM core.compound_item_details
    INNER JOIN core.compound_items
    ON core.compound_items.compound_item_id = core.compound_item_details.compound_item_id
    WHERE compound_item_code = _compound_item_code;

    UPDATE temp_details
    SET 
        item_code = core.items.item_code,
        item_name = core.items.item_name
    FROM core.items
    WHERE temp_details.item_id = core.items.item_id;

    UPDATE temp_details
    SET
        sales_tax_id = core.sales_taxes.sales_tax_id
    FROM core.sales_taxes
    WHERE temp_details.sales_tax_code = core.sales_taxes.sales_tax_code;

    UPDATE temp_details
    SET
        unit_name = core.units.unit_name
    FROM core.units
    WHERE temp_details.unit_id = core.units.unit_id;


    FOR this IN
    SELECT * FROM temp_details
    LOOP
        UPDATE temp_details
        SET computed_tax = 
        (
            SELECT COALESCE(SUM(tax), 0) 
            FROM transactions.get_sales_tax
            (
                _tran_book, 
                _store_id, 
                _party_code, 
                '', 
                _price_type_id, 
                this.item_code, 
                this.price, 
                this.quantity, 
                this.discount, 
                0, 
                this.sales_tax_id
            )
        )
        WHERE temp_details.id = this.id;
    END LOOP;
    
    RETURN QUERY
    SELECT * FROM temp_details;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM core.get_compound_item_details('APP', 'MoF-NY-BK-STX', 'Sales', 1, 'JASMI-0002', 1);

