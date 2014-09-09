CREATE FUNCTION core.get_item_tax_rate(integer)
RETURNS decimal
AS
$$
BEGIN
	RETURN
	COALESCE((
		SELECT core.taxes.rate
		FROM core.taxes
		INNER JOIN core.items
		ON core.taxes.tax_id = core.items.tax_id
		WHERE core.items.item_id=$1
	), 0);
END
$$
LANGUAGE plpgsql;
