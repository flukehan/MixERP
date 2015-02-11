CREATE FUNCTION core.get_item_tax_rate(_item_id integer)
RETURNS decimal
STABLE
AS
$$
BEGIN
    RETURN
        core.sales_taxes.rate
    FROM core.sales_taxes
    INNER JOIN core.items
    ON core.items.sales_tax_id = core.sales_taxes.sales_tax_id
    AND core.items.item_id = $1;
END
$$
LANGUAGE plpgsql;
