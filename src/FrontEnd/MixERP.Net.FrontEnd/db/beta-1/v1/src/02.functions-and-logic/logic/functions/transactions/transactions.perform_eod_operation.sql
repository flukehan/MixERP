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
    DECLARE _notice             text;
    DECLARE _office_code        text;
BEGIN
    IF(_value_date IS NULL) THEN
        RAISE EXCEPTION 'Invalid date.'
        USING ERRCODE='P3008';
    END IF;

    IF(NOT policy.is_elevated_user(_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9001';
    END IF;

    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    SELECT * FROM transactions.day_operation
    WHERE value_date=_value_date 
    AND office_id = _office_id INTO this;

    IF(this IS NULL) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    ELSE    
        IF(this.completed OR this.completed_on IS NOT NULL) THEN
            RAISE WARNING 'EOD operation was already performed.';
            _is_error        := true;
        END IF;
    END IF;
    
    IF(NOT _is_error) THEN
        _office_code        := office.get_office_code_by_id(_office_id);
        _notice             := 'EOD started.'::text;
        RAISE INFO  '%', _notice;

        FOR this IN
        SELECT routine_id, routine_name 
        FROM transactions.routines 
        WHERE status 
        ORDER BY "order" ASC
        LOOP
            _routine_id             := this.routine_id;
            _routine                := this.routine_name;
            _sql                    := format('SELECT * FROM %1$s($1);', _routine);

            RAISE NOTICE '%', _sql;

            _notice             := 'Performing ' || _routine::text || '.';
            RAISE INFO '%', _notice;

            PERFORM pg_sleep(5);
            EXECUTE _sql USING _office_id;

            _notice             := 'Completed  ' || _routine::text || '.';
            RAISE INFO '%', _notice;
            
            PERFORM pg_sleep(5);            
        END LOOP;


        UPDATE transactions.day_operation SET 
            completed_on = NOW(), 
            completed_by = _user_id,
            completed = true
        WHERE value_date=_value_date
        AND office_id = _office_id;

        _notice             := 'EOD of ' || _office_code || ' for ' || _value_date::text || ' completed without errors.'::text;
        RAISE INFO '%', _notice;

        _notice             := 'OK'::text;
        RAISE INFO '%', _notice;

        RETURN true;
    END IF;

    RETURN false;    
END;
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.perform_eod_operation(_login_id bigint);

CREATE FUNCTION transactions.perform_eod_operation(_login_id bigint)
RETURNS boolean
AS
$$
    DECLARE _user_id    integer;
    DECLARE _office_id integer;
    DECLARE _value_date date;
BEGIN
    SELECT 
        user_id,
        office_id,
        transactions.get_value_date(office_id)
    INTO
        _user_id,
        _office_id,
        _value_date
    FROM audit.logins
    WHERE login_id=$1;

    RETURN transactions.perform_eod_operation(_user_id, _office_id, _value_date);
END
$$
LANGUAGE plpgsql;