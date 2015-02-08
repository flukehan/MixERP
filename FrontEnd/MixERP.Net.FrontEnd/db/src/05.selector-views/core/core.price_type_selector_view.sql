DROP VIEW IF EXISTS core.price_type_selector_view;

CREATE VIEW core.price_type_selector_view
AS
SELECT * FROM core.price_types;