DROP VIEW IF EXISTS core.item_selling_price_scrud_view;

CREATE VIEW core.item_selling_price_scrud_view
AS
SELECT
    core.item_selling_prices.item_selling_price_id,
    core.items.item_code,
    core.items.item_name,
    core.party_types.party_type_code,
    core.party_types.party_type_name,
    unit_code || ' (' || unit_name || ')' AS unit,
    price
FROM
    core.item_selling_prices
INNER JOIN  core.items
ON core.item_selling_prices.item_id = core.items.item_id
INNER JOIN core.units
ON core.item_selling_prices.unit_id = core.units.unit_id
LEFT JOIN core.price_types
ON core.item_selling_prices.price_type_id = core.price_types.price_type_id
LEFT JOIN core.party_types
ON  core.item_selling_prices.party_type_id = core.party_types.party_type_id;

