DROP VIEW IF EXISTS core.item_cost_price_scrud_view;

CREATE VIEW core.item_cost_price_scrud_view
AS
SELECT
    core.item_cost_prices.item_cost_price_id,
    core.items.item_code,
    core.items.item_name,
    core.parties.party_code,
    core.parties.party_name,
    unit_code || ' (' || unit_name || ')' AS unit,
    core.item_cost_prices.price
FROM 
core.item_cost_prices
INNER JOIN core.items
ON core.item_cost_prices.item_id = core.items.item_id
INNER JOIN core.units
ON core.item_cost_prices.unit_id = core.units.unit_id
LEFT JOIN core.parties
ON core.item_cost_prices.party_id = core.parties.party_id;

