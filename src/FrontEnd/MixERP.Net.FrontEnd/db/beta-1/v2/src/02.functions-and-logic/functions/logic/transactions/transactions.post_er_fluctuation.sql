DROP FUNCTION IF EXISTS transactions.post_er_fluctuation(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_er_fluctuation(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
AS
$$
BEGIN

END
$$
LANGUAGE plpgsql;
