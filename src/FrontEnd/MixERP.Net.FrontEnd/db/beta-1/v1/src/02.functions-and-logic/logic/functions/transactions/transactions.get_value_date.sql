DROP FUNCTION IF EXISTS transactions.get_value_date(_office_id integer);

CREATE FUNCTION transactions.get_value_date(_office_id integer)
RETURNS date
AS
$$
    DECLARE this            RECORD;
    DECLARE _value_date     date=NOW();
BEGIN
    SELECT * FROM transactions.day_operation
    WHERE office_id = _office_id
    AND value_date =
    (
        SELECT MAX(value_date)
        FROM transactions.day_operation
        WHERE office_id = _office_id
    ) INTO this;

    IF(this.day_id IS NOT NULL) THEN
        IF(this.completed) THEN
            _value_date  := this.value_date + interval '1' day;
        ELSE
            _value_date  := this.value_date;    
        END IF;
    END IF;
    
    RETURN _value_date;
END
$$
LANGUAGE plpgsql;


--select transactions.get_value_date(2);