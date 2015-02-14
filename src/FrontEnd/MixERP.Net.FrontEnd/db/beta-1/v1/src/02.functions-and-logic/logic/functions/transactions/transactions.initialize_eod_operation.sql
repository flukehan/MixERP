DROP FUNCTION IF EXISTS transactions.initialize_eod_operation(_user_id integer, _office_id integer, _value_date date);

CREATE FUNCTION transactions.initialize_eod_operation(_user_id integer, _office_id integer, _value_date date)
RETURNS void
AS
$$
    DECLARE this            RECORD;    
BEGIN
    IF(_value_date IS NULL) THEN
        RAISE EXCEPTION 'Invalid date.'
        USING ERRCODE='P3008';        
    END IF;

    IF(NOT policy.is_elevated_user(_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9010';
    END IF;

    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    SELECT * FROM transactions.day_operation
    WHERE value_date=_value_date 
    AND office_id = _office_id INTO this;

    IF(this IS NULL) THEN
        INSERT INTO transactions.day_operation(office_id, value_date, started_on, started_by)
        SELECT _office_id, _value_date, NOW(), _user_id;
    ELSE    
        RAISE EXCEPTION 'EOD operation was already initialized.'
        USING ERRCODE='P8101';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;