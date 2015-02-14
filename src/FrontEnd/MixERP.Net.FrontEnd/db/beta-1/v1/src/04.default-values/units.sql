DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO core.units(unit_code, unit_name)
        SELECT 'PC', 'Piece'        UNION ALL
        SELECT 'FT', 'Feet'         UNION ALL
        SELECT 'MTR', 'Meter'       UNION ALL
        SELECT 'LTR', 'Liter'       UNION ALL
        SELECT 'GM', 'Gram'         UNION ALL
        SELECT 'KG', 'Kilogram'     UNION ALL
        SELECT 'DZ', 'Dozen'        UNION ALL
        SELECT 'BX', 'Box';


        INSERT INTO core.compound_units(base_unit_id, compare_unit_id, value)
        SELECT core.get_unit_id_by_unit_code('PC'), core.get_unit_id_by_unit_code('DZ'), 12     UNION ALL
        SELECT core.get_unit_id_by_unit_code('DZ'), core.get_unit_id_by_unit_code('BX'), 100    UNION ALL
        SELECT core.get_unit_id_by_unit_code('GM'), core.get_unit_id_by_unit_code('KG'), 1000;
    END IF;
END
$$
LANGUAGE plpgsql;