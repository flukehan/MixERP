-- Function: transactions.get_reorder_view_function(integer)

 DROP FUNCTION transactions.get_reorder_view_function(integer);

CREATE OR REPLACE FUNCTION transactions.get_reorder_view_function(IN office_id integer)
  RETURNS TABLE(item_id integer, item_code character varying, item_name character varying, unit_id integer, unit text, quantity_on_hand numeric, reorder_level integer, reorder_quantity integer, preferred_supplier_id bigint, preferred_supplier text, price public.money_strict2) AS
$BODY$
BEGIN
        RETURN QUERY
        SELECT 
                core.items.item_id,
                core.items.item_code,
                core.items.item_name,
                core.items.reorder_unit_id,
                core.units.unit_name::text AS unit,
                floor(office.count_item_in_stock(core.items.item_id, core.items.reorder_unit_id, $1)) AS quantity_on_hand,
                core.items.reorder_level,
                core.items.reorder_quantity,
                core.items.preferred_supplier_id,
                core.parties.party_code::text AS party,
                core.get_item_cost_price(core.items.item_id, core.items.reorder_unit_id, core.items.preferred_supplier_id)
        FROM core.items
        INNER JOIN core.parties
        ON core.items.preferred_supplier_id = core.parties.party_id
        INNER JOIN core.units
        ON core.items.reorder_unit_id = core.units.unit_id
        WHERE 
        floor
        (
                office.count_item_in_stock(core.items.item_id, core.items.reorder_unit_id, $1)
                +
                core.get_ordered_quantity(core.items.item_id, core.items.reorder_unit_id, $1)
        ) 

        < core.items.reorder_level
        AND core.items.reorder_quantity > 0;
END
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION transactions.get_reorder_view_function(integer)
  OWNER TO postgres;
GRANT EXECUTE ON FUNCTION transactions.get_reorder_view_function(integer) TO public;
GRANT EXECUTE ON FUNCTION transactions.get_reorder_view_function(integer) TO postgres;
GRANT EXECUTE ON FUNCTION transactions.get_reorder_view_function(integer) TO mix_erp;
GRANT EXECUTE ON FUNCTION transactions.get_reorder_view_function(integer) TO report_user;
