DROP FUNCTION IF EXISTS transactions.create_routine(_routine regproc, _order integer);

CREATE FUNCTION transactions.create_routine(_routine regproc, _order integer)
RETURNS void
AS
$$
BEGIN
   INSERT INTO transactions.routines(routine_name, "order")
   SELECT $1, $2;
   
   RETURN;
END
$$
LANGUAGE plpgsql;
