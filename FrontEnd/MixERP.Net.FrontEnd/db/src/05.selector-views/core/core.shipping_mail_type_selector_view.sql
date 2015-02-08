DROP VIEW IF EXISTS core.shipping_mail_type_selector_view;

CREATE VIEW core.shipping_mail_type_selector_view
AS
SELECT * FROM core.shipping_mail_types;