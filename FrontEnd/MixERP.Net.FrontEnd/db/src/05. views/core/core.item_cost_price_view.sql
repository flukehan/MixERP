CREATE VIEW core.item_cost_price_view
AS
SELECT
	core.item_cost_prices.item_cost_price_id,
	core.items.item_code,
	core.items.item_name,
	core.parties.party_code,
	core.parties.party_name,
	core.item_cost_prices.price
FROM 
core.item_cost_prices
INNER JOIN
core.items
ON core.item_cost_prices.item_id = core.items.item_id
LEFT JOIN
core.parties
ON core.item_cost_prices.party_id = core.parties.party_id;

