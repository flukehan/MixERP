DROP FUNCTION IF EXISTS add_office
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