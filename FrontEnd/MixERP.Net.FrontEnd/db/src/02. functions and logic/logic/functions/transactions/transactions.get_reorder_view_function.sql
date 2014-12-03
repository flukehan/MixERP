DROP FUNCTION IF EXISTS transactions.get_reorder_view_function(office_id integer);

CREATE FUNCTION transactions.get_reorder_view_function(office_id integer)
RETURNS TABLE
(
        item_id                 integer,
        item_code               national character varying(12),
        item_name               national character varying(150),
        unit_id                 integer,
        unit                    text,
        quantity_on_hand        numeric,
        reorder_level           integer,
        reorder_quantity        integer,
        preferred_supplier_id   bigint,
        preferred_supplier      text,
        price                   money_strict2,
        tax                     national character varying(24)
)
AS
$$
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
                core.parties.party_code || ' (' || core.parties.party_name || ')'::text AS party,
                core.get_item_cost_price(core.items.item_id, core.items.reorder_unit_id, core.items.preferred_supplier_id),
                core.get_sales_tax_code_by_sales_tax_id(core.items.sales_tax_id) as tax
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
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.get_reorder_view_function(2);
