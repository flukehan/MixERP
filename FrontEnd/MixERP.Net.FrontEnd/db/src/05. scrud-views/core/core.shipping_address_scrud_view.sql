CREATE VIEW core.shipping_address_scrud_view
AS
SELECT
    core.shipping_addresses.shipping_address_id,
    core.shipping_addresses.shipping_address_code,
    core.shipping_addresses.party_id,
    core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
    core.shipping_addresses.po_box,
    core.shipping_addresses.address_line_1,
    core.shipping_addresses.address_line_2,
    core.shipping_addresses.street,
    core.shipping_addresses.city,
    core.shipping_addresses.state,
    core.shipping_addresses.country
FROM core.shipping_addresses
INNER JOIN core.parties
ON core.shipping_addresses.party_id=core.parties.party_id;
