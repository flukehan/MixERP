CREATE VIEW core.supplier_view
AS
SELECT * FROM core.parties
WHERE party_type_id IN
(
        SELECT party_type_id FROM core.party_types
        WHERE is_supplier=true
);