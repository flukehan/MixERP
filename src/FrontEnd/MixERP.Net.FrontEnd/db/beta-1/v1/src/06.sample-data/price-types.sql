DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO core.price_types(price_type_code, price_type_name)
        SELECT 'RET', 'Retail'      UNION ALL
        SELECT 'WHO', 'Wholesale';
    END IF;
END
$$
LANGUAGE plpgsql;