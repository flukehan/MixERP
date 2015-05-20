ALTER TABLE core.compound_item_details
DROP CONSTRAINT IF EXISTS compound_item_details_unit_chk;

ALTER TABLE core.compound_item_details
ADD CONSTRAINT compound_item_details_unit_chk
CHECK(core.is_valid_unit(item_id, unit_id));

ALTER TABLE core.item_cost_prices
DROP CONSTRAINT IF EXISTS item_cost_prices_unit_chk;

ALTER TABLE core.item_cost_prices
ADD CONSTRAINT item_cost_prices_unit_chk
CHECK(core.is_valid_unit(item_id, unit_id));


ALTER TABLE core.item_selling_prices
DROP CONSTRAINT IF EXISTS item_selling_prices_unit_chk;

ALTER TABLE core.item_selling_prices
ADD CONSTRAINT item_selling_prices_unit_chk
CHECK(core.is_valid_unit(item_id, unit_id));

ALTER TABLE core.items
DROP CONSTRAINT IF EXISTS items_reorder_quantity_chk;

ALTER TABLE core.items
ADD CONSTRAINT items_reorder_quantity_chk
CHECK
(
core.convert_unit(reorder_unit_id, unit_id) * reorder_quantity >= reorder_level
);
