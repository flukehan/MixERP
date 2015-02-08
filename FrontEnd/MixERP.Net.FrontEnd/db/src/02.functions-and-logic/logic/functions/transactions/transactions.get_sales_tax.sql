DROP TYPE IF EXISTS transactions.sales_tax_type CASCADE;

CREATE TYPE transactions.sales_tax_type AS
(
    id                          integer,
    sales_tax_detail_id         integer,
    sales_tax_id                integer,
    sales_tax_detail_code       text,
    sales_tax_detail_name       text,
    is_use_tax                  boolean,
    account_id                  integer,
    price                       money_strict,
    quantity                    integer_strict,
    discount                    money_strict2,
    shipping_charge             money_strict2,
    taxable_amount              money_strict2,
    state_sales_tax_id          integer,
    county_sales_tax_id         integer,
    rate                        decimal_strict2,
    base_amount_type            text,
    rate_type                   text,
    rounding_type               text,
    rounding_places             integer,
    tax                         money_strict2
);

DROP FUNCTION IF EXISTS transactions.get_sales_tax
(
    _tran_book                  national character varying(12),
    _store_id                   integer,
    _party_code                 national character varying(12), 
    _shipping_address_code      national character varying(12),
    _price_type_id              integer,
    _item_code                  national character varying(12),
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
    _item_code                  national character varying(12),
    _price                      money_strict2,
    _quantity                   integer_strict2,
    _discount                   money_strict2,
    _shipping_charge            money_strict2,
    _sales_tax_id               integer
)
RETURNS SETOF transactions.sales_tax_type
AS
$$
    DECLARE _has_nexus                      boolean=false;
    DECLARE _party_id                       bigint;
    DECLARE _state_id                       integer;
    DECLARE _state_sales_tax_id             integer;
    DECLARE _state_sales_tax_rate           decimal_strict2;
    DECLARE _tax_base_amount_type_code      text;
    DECLARE _tax                            money_strict2=0;
    DECLARE _cumulative_tax                 money_strict2=0;
    DECLARE _taxable_amount                 money_strict2=0;
    DECLARE this                            RECORD;
BEGIN

    DROP TABLE IF EXISTS temp_sales_tax;
    
    CREATE TEMPORARY TABLE temp_sales_tax
    (
        id                          SERIAL,
        sales_tax_detail_id         integer,
        sales_tax_id                integer,
        sales_tax_detail_code       text,
        sales_tax_detail_name       text,
        is_use_tax                  boolean,
        account_id                  integer,
        price                       money_strict,
        quantity                    integer_strict,
        discount                    money_strict2,
        shipping_charge             money_strict2,
        taxable_amount              money_strict2,
        state_sales_tax_id          integer,
        county_sales_tax_id         integer,
        rate                        decimal_strict2,
        base_amount_type            text,
        rate_type                   text,
        rounding_type               text,
        rounding_places             integer,
        tax                         money_strict2
    ) ON COMMIT DROP;

    IF(COALESCE(_tran_book, '') = '') THEN
        RETURN QUERY SELECT * FROM temp_sales_tax;
    END IF;

    IF(COALESCE(_store_id, 0) = 0) THEN
        RETURN QUERY SELECT * FROM temp_sales_tax;
    END IF;

    IF(COALESCE(_party_code, '') = '') THEN
        RETURN QUERY SELECT * FROM temp_sales_tax;
    END IF;
    
    IF(COALESCE(_price, 0) = 0) THEN
        RETURN QUERY SELECT * FROM temp_sales_tax;
    END IF;
    
    IF(COALESCE(_quantity, 0) = 0) THEN
        RETURN QUERY SELECT * FROM temp_sales_tax;
    END IF;
    
    IF(COALESCE(_sales_tax_id, 0) = 0) THEN
        RETURN QUERY SELECT * FROM temp_sales_tax;
    END IF;

    IF(TRIM(COALESCE(_shipping_address_code, '')) = '') THEN
        _has_nexus                  := false;        
    ELSE
        _state_id                   := core.get_state_id_by_shipping_address_code(_shipping_address_code, _party_id);
        _has_nexus                  := transactions.has_nexus(_state_id);
    END IF;

    IF(_has_nexus) THEN
        SELECT 
            state_sales_tax_id,
            rate
        INTO
            _state_sales_tax_id         
            _state_sales_tax_rate      
        FROM
        core.state_sales_taxes;

    END IF;

    IF(COALESCE(_state_sales_tax_id) = 0) THEN
        _has_nexus                  := false;
    END IF;


    SELECT tax_base_amount_type_code INTO _tax_base_amount_type_code
    FROM core.sales_taxes
    WHERE sales_tax_id=_sales_tax_id;
    


    INSERT INTO temp_sales_tax
    (
        sales_tax_detail_id, 
        sales_tax_id, 
        sales_tax_detail_code, 
        sales_tax_detail_name, 
        price, 
        quantity, 
        discount, 
        shipping_charge, 
        taxable_amount,
        is_use_tax,        
        account_id,
        state_sales_tax_id,
        county_sales_tax_id,
        rate,
        base_amount_type,
        rate_type,
        rounding_type,
        rounding_places,
        tax

    )
    SELECT 
        sales_tax_detail_id, 
        sales_tax_id, 
        sales_tax_detail_code, 
        sales_tax_detail_name,
        _price,
        _quantity,
        _discount,
        _shipping_charge,

        (_price * _quantity) 
        + 
        CASE 
            WHEN applied_on_shipping_charge 
            THEN _shipping_charge 
        ELSE 0 
        END
        - 
        _discount,
        CASE 
            WHEN state_sales_tax_id IS NOT NULL AND based_on_shipping_address AND check_nexus AND _has_nexus
            THEN true
            ELSE false
        END,
        CASE 
            WHEN based_on_shipping_address AND check_nexus AND _has_nexus AND use_tax_collecting_account_id IS NOT NULL
            THEN use_tax_collecting_account_id
            ELSE collecting_account_id
        END,
        CASE 
            WHEN based_on_shipping_address AND check_nexus AND _has_nexus
            THEN _state_sales_tax_id
            ELSE state_sales_tax_id
        END,
        county_sales_tax_id,
        CASE 
            WHEN state_sales_tax_id IS NOT NULL 
            THEN 
                CASE WHEN based_on_shipping_address AND check_nexus AND _has_nexus
                THEN _state_sales_tax_rate
                ELSE
                    core.get_state_sales_tax_rate(state_sales_tax_id)
                END
            WHEN county_sales_tax_id IS NOT NULL
            THEN 
                core.get_county_sales_tax_rate(county_sales_tax_id)
            ELSE
                rate
        END,
        _tax_base_amount_type_code,
        tax_rate_type_code,
        rounding_method_code,
        rounding_decimal_places,
        CASE WHEN state_sales_tax_id IS NULL AND county_sales_tax_id IS NULL AND tax_rate_type_code = 'F'
        THEN
            rate
        ELSE
            NULL
        END
    FROM core.sales_tax_details
    WHERE sales_tax_id=_sales_tax_id;


    IF(_tax_base_amount_type_code = 'L') THEN
        FOR this IN SELECT * FROM temp_sales_tax ORDER BY id
        LOOP
            _taxable_amount     := this.taxable_amount + _cumulative_tax;
            _tax                := 0;
            
            IF(this.rounding_type = 'R') THEN
                _tax            := ROUND((_taxable_amount * this.rate)/100, this.rounding_places);
            ELSIF(this.rounding_type = 'F') THEN
                _tax            := FLOOR((_taxable_amount * this.rate)/100);
            ELSIF(this.rounding_type = 'C') THEN
                _tax            := CEILING((_taxable_amount * this.rate)/100);
            END IF;

            _cumulative_tax     := _cumulative_tax + _tax;

            UPDATE temp_sales_tax SET 
            tax = _tax,
            taxable_amount = _taxable_amount
            WHERE id = this.id;
            
        END LOOP;
    ELSE
        UPDATE temp_sales_tax
        SET tax = 
        CASE WHEN rounding_type = 'R'
        THEN ROUND((taxable_amount * rate)/100, rounding_places)
        WHEN rounding_type = 'F'
        THEN FLOOR((taxable_amount * rate)/100)
        WHEN rounding_type = 'C'
        THEN CEILING((taxable_amount * rate)/100)
        END;
    END IF;


    
    RETURN QUERY SELECT * FROM temp_sales_tax;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_sales_tax('Sales', 1, 'JASMI-0002', 'None', 1, 'RMBP', 225000, 1, 0, 0, 3);

