DROP VIEW IF EXISTS office.office_scrud_view;
CREATE VIEW office.office_scrud_view
AS
SELECT 
  office.offices.office_id, 
  office.offices.office_code, 
  office.offices.office_name, 
  office.offices.nick_name, 
  office.offices.registration_date, 
  core.currencies.currency_code || '('|| core.currencies.currency_name||')' AS currency, 
  office.offices.po_box, 
  office.offices.address_line_1, 
  office.offices.address_line_2, 
  office.offices.street, 
  office.offices.city, 
  office.offices.state, 
  office.offices.zip_code, 
  office.offices.country, 
  office.offices.phone, 
  office.offices.fax, 
  office.offices.email, 
  office.offices.url, 
  office.offices.registration_number, 
  parent_office.office_code || '('|| parent_office.office_name||')' AS parent_office
FROM 
  office.offices
INNER JOIN core.currencies
ON office.offices.currency_code = core.currencies.currency_code
LEFT JOIN office.offices AS parent_office
ON  office.offices.office_id = parent_office.office_id;

