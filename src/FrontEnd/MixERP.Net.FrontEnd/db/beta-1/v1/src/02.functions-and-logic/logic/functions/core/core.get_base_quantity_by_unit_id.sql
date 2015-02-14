DROP FUNCTION IF EXISTS core.get_base_quantity_by_unit_id(integer, integer);

CREATE FUNCTION core.get_base_quantity_by_unit_id(integer, integer)
RETURNS decimal
AS
$$
DECLARE _root_unit_id integer;
DECLARE _factor decimal;
BEGIN
    _root_unit_id = core.get_root_unit_id($1);
    _factor = core.convert_unit($1, _root_unit_id);

    RETURN _factor * $2;
END
$$
LANGUAGE plpgsql;



/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/


DROP FUNCTION IF EXISTS unit_tests.get_base_quantity_by_unit_id_test();

CREATE FUNCTION unit_tests.get_base_quantity_by_unit_id_test()
RETURNS public.test_result
AS
$$
    DECLARE message test_result;
    DECLARE result boolean;
    DECLARE actual decimal;
    DECLARE expected decimal=345;
BEGIN

        INSERT INTO core.units(unit_code, unit_name)
        SELECT 'TUC-0000001', 'Test Unit 1' UNION ALL 
        SELECT 'TUC-0000002', 'Test Unit 2';


        INSERT INTO core.compound_units(base_unit_id, compare_unit_id, value)
        SELECT core.get_unit_id_by_unit_code('TUC-0000001'), core.get_unit_id_by_unit_code('TUC-0000002'), 345;

        SELECT core.get_base_quantity_by_unit_id(core.get_unit_id_by_unit_code('TUC-0000002'), 1) INTO actual;

        DELETE FROM core.compound_units WHERE base_unit_id = core.get_unit_id_by_unit_code('TUC-0000001');
        DELETE FROM core.units WHERE unit_code IN('TUC-0000001', 'TUC-0000002');

        RAISE NOTICE '%', actual;

        SELECT * FROM assert.is_equal(actual, expected) INTO message, result;        

        IF(result = false) THEN
                RETURN message;
        END IF;

        SELECT assert.ok('End of test.') INTO message;  
        RETURN message;
END
$$
LANGUAGE plpgsql;




