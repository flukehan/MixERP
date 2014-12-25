CREATE VIEW core.shipping_address_scrud_view
AS
SELECT
    core.shipping_addresses.shipping_address_id,
    core.shipping_addresses.shipping_address_code,
    core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
    core.shipping_addresses.zip_code,
    core.shipping_addresses.address_line_1,
    core.shipping_addresses.address_line_2,
    core.shipping_addresses.street,
    core.shipping_addresses.city,
    core.get_state_name_by_state_id(core.shipping_addresses.state_id) AS state,
    core.get_country_name_by_country_id(core.shipping_addresses.country_id) AS country
FROM core.shipping_addresses
INNER JOIN core.parties
ON core.shipping_addresses.party_id=core.parties.party_id;
