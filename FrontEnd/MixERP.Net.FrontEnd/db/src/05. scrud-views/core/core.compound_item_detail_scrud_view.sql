CREATE VIEW core.compound_item_detail_scrud_view
AS
SELECT
        compound_item_detail_id,
        core.compound_item_details.compound_item_id,
        core.compound_items.compound_item_code,
        core.compound_items.compound_item_name,
        item_id,
        core.get_item_name_by_item_id(item_id) AS item,
        core.get_unit_name_by_unit_id(unit_id) AS unit,
        quantity
FROM core.compound_item_details
INNER JOIN core.compound_items
ON core.compound_item_details.compound_item_id = core.compound_items.compound_item_id;
