DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1 FROM core.attachment_lookup
        WHERE book = 'inventory.transfer.request'
    ) THEN
        INSERT INTO core.attachment_lookup(book, resource, resource_key)
        SELECT 'inventory.transfer.request', 'transactions.inventory_transfer_requests', 'inventory_transfer_request_id';
    END IF;

    IF NOT EXISTS
    (
        SELECT 1 FROM core.attachment_lookup
        WHERE book = 'inventory.transfer.delivery'
    ) THEN
        INSERT INTO core.attachment_lookup(book, resource, resource_key)
        SELECT 'inventory.transfer.delivery', 'transactions.inventory_transfer_deliveries', 'inventory_transfer_delivery_id';
    END IF;
END
$$
LANGUAGE plpgsql;