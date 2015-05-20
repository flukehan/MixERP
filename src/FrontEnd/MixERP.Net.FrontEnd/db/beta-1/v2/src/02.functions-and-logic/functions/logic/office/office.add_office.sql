DROP FUNCTION IF EXISTS office.add_office
(
    _office_code            national character varying(12),
    _office_name            national character varying(150),
    _nick_name              national character varying(50),
    _registration_date      date,
    _currency_code          national character varying(12),
    _currency_symbol        national character varying(12),
    _currency_name          national character varying(48),
    _hundredth_name         national character varying(48),
    _admin_name             national character varying(100),
    _user_name              national character varying(50),
    _password               national character varying(48)
);

CREATE FUNCTION office.add_office
(
    _office_code            national character varying(12),
    _office_name            national character varying(150),
    _nick_name              national character varying(50),
    _registration_date      date,
    _currency_code          national character varying(12),
    _currency_symbol        national character varying(12),
    _currency_name          national character varying(48),
    _hundredth_name         national character varying(48),
    _admin_name             national character varying(100),
    _user_name              national character varying(50),
    _password               national character varying(48)
)
RETURNS void 
VOLATILE AS
$$
    DECLARE _office_id      integer;
    DECLARE _user_id		integer;
BEGIN
    IF NOT EXISTS
    (
        SELECT 0 
        FROM core.currencies
        WHERE currency_code=_currency_code
    ) THEN
        INSERT INTO core.currencies(currency_code, currency_symbol, currency_name, hundredth_name)
        SELECT _currency_code, _currency_symbol, _currency_name, _hundredth_name;
    END IF;


    INSERT INTO office.offices(office_code, office_name, nick_name, registration_date, currency_code)
    SELECT _office_code, _office_name, _nick_name, _registration_date, _currency_code
    RETURNING office_id INTO _office_id;

    IF NOT EXISTS(SELECT 0 FROM office.users WHERE user_name='sys') THEN
        INSERT INTO office.users(role_id, department_id, office_id, user_name, password, full_name)
        SELECT office.get_role_id_by_role_code('SYST'), office.get_department_id_by_department_code('SUP'), _office_id, 'sys', '', 'System';
    END IF;
        
    INSERT INTO office.users(role_id, department_id, office_id,user_name,password, full_name, elevated)
    SELECT office.get_role_id_by_role_code('ADMN'), office.get_department_id_by_department_code('SUP'), _office_id, _user_name, _password, _admin_name, true
    RETURNING user_id INTO _user_id;

    INSERT INTO policy.menu_access(office_id, menu_id, user_id)
    SELECT _office_id, core.menus.menu_id, _user_id
    FROM core.menus;

    RETURN;
END;
$$
LANGUAGE plpgsql;