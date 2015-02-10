DROP VIEW IF EXISTS core.shipping_mail_type_selector_view;

CREATE VIEW core.shipping_mail_type_selector_view
AS
SELECT 
  shipping_mail_types.shipping_mail_type_id, 
  shipping_mail_types.shipping_mail_type_code, 
  shipping_mail_types.shipping_mail_type_name
FROM 
  core.shipping_mail_types;
