CREATE VIEW core.supplier_view
AS
SELECT * FROM core.party_view
WHERE is_supplier=true;
