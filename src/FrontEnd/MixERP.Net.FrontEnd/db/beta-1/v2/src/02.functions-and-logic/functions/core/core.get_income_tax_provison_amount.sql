DROP FUNCTION IF EXISTS core.get_income_tax_provison_amount(_office_id integer, _profit  decimal(24, 4), _balance  decimal(24, 4));

CREATE FUNCTION core.get_income_tax_provison_amount(_office_id integer, _profit decimal(24, 4), _balance decimal(24, 4))
RETURNS  decimal(24, 4)
AS
$$
    DECLARE _rate real;
BEGIN
    _rate := office.get_income_tax_rate(_office_id);

    RETURN
    (
        (_profit * _rate/100) - _balance
    );
END
$$
LANGUAGE plpgsql;
