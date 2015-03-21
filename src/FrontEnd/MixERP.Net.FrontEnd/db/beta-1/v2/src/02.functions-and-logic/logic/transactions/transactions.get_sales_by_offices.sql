DROP FUNCTION IF EXISTS transactions.get_sales_by_offices(office_id integer, divide_by integer);

CREATE FUNCTION transactions.get_sales_by_offices(office_id integer, divide_by integer)
RETURNS TABLE
(
  office text,
  jan numeric,
  feb numeric,
  mar numeric,
  apr numeric,
  may numeric,
  jun numeric,
  jul numeric,
  aug numeric,
  sep numeric,
  oct numeric,
  nov numeric,
  "dec" numeric
)
AS
$$
BEGIN
        IF divide_by <= 0 THEN
                divide_by := 1;
        END IF;
        
        RETURN QUERY
        SELECT * FROM crosstab
        (
                '
                SELECT 
                office.get_office_code_by_id(office_id) AS office,
                date_part(''month'', value_date) AS month_id,
                SUM((price * quantity) - discount + tax)/' || divide_by::text || '::integer AS total
                FROM transactions.verified_stock_transaction_view
                WHERE book IN (''Sales.Direct'', ''Sales.Delivery'')
                AND office_id IN (SELECT * FROM office.get_office_ids(' || quote_literal($1::text) || '))
                GROUP BY office_id,
                date_part(''month'', value_date),
                date_trunc(''month'',value_date)
                ',
                'select m from generate_series(1,12) m'
        )as (
          office text,
          "Jan" numeric,
          "Feb" numeric,
          "Mar" numeric,
          "Apr" numeric,
          "May" numeric,
          "Jun" numeric,
          "Jul" numeric,
          "Aug" numeric,
          "Sep" numeric,
          "Oct" numeric,
          "Nov" numeric,
          "Dec" numeric
        ) ;

END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.get_sales_by_offices(divide_by integer);

CREATE FUNCTION transactions.get_sales_by_offices(divide_by integer)
RETURNS TABLE
(
  office text,
  jan numeric,
  feb numeric,
  mar numeric,
  apr numeric,
  may numeric,
  jun numeric,
  jul numeric,
  aug numeric,
  sep numeric,
  oct numeric,
  nov numeric,
  "dec" numeric
)
AS
$$
    DECLARE root_office_id integer = 0;
BEGIN
    SELECT office.offices.office_id INTO root_office_id
    FROM office.offices
    WHERE parent_office_id IS NULL
    LIMIT 1;

        IF divide_by <= 0 THEN
                divide_by := 1;
        END IF;
        
        RETURN QUERY
        SELECT * FROM transactions.get_sales_by_offices(root_office_id, divide_by);
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_sales_by_offices(1, 1);
--SELECT * FROM transactions.get_sales_by_offices(1000);

