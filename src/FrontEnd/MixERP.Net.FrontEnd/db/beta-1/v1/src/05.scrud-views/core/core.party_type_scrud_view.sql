CREATE VIEW core.party_type_scrud_view
AS
SELECT 
        party_type_id,
        party_type_code,
        party_type_name,
        is_supplier
FROM core.party_types;