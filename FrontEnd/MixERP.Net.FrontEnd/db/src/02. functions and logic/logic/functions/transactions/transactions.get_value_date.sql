CREATE FUNCTION transactions.get_value_date()
RETURNS date
AS
$$
BEGIN
        RETURN NOW()::date;
END
$$
LANGUAGE plpgsql;


