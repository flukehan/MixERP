CREATE FUNCTION transactions.get_transaction_code(value_date date, office_id integer, user_id integer, login_id bigint)
RETURNS text
AS
$$
    DECLARE _office_id bigint:=$2;
    DECLARE _user_id integer:=$3;
    DECLARE _login_id bigint:=$4;
    DECLARE _ret_val text;  
BEGIN
    _ret_val:= transactions.get_new_transaction_counter($1)::text || '-' || TO_CHAR($1, 'YYYY-MM-DD') || '-' || CAST(_office_id as text) || '-' || CAST(_user_id as text) || '-' || CAST(_login_id as text)   || '-' ||  TO_CHAR(now(), 'HH24-MI-SS');
    RETURN _ret_val;
END
$$
LANGUAGE plpgsql;
