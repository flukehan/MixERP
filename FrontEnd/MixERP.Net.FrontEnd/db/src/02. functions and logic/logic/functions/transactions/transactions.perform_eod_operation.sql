DROP FUNCTION IF EXISTS transactions.perform_eod_operation(_user_id integer, _office_id integer, _value_date date);

CREATE FUNCTION transactions.perform_eod_operation(_user_id integer, _office_id integer, _value_date date)
RETURNS boolean
AS
$$
    DECLARE _routine            regproc;
    DECLARE _routine_id         integer;
    DECLARE this                RECORD;
    DECLARE _sql                text;
    DECLARE _is_error           boolean=false;
BEGIN
    IF(NOT policy.is_elevated_user(_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.';
    END IF;
    
    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.';
    END IF;

    IF(_value_date IS NULL) THEN
        RAISE EXCEPTION 'Value date error.';
    END IF;

    SELECT * FROM transactions.day_operation
    WHERE value_date=_value_date 
    AND office_id = _office_id INTO this;

    IF(this IS NULL) THEN
        INSERT INTO transactions.day_operation(office_id, value_date, started_on)
        SELECT _office_id, _value_date, NOW();
    ELSE    
        IF(this.completed OR this.completed_on IS NOT NULL) THEN
            RAISE WARNING 'EOD operation was already performed.';
            _is_error        := true;
        END IF;
    END IF;
    
    IF(NOT _is_error) THEN
        FOR this IN
        SELECT routine_id, routine_name 
        FROM transactions.routines 
        WHERE status 
        ORDER BY "order" ASC
        LOOP
            _routine_id             := this.routine_id;
            _routine                := this.routine_name;
            _sql                    := format('SELECT * FROM %1$s($1);', _routine);

            RAISE INFO '%', _sql;

            EXECUTE _sql USING _office_id;
        END LOOP;


        UPDATE transactions.day_operation SET completed_on = NOW(), completed = true
        WHERE value_date=_value_date 
        AND office_id = _office_id;

        RETURN true;
    END IF;

    RETURN false;    
END;
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.perform_eod_operation(1, 1, '2014-12-14');



