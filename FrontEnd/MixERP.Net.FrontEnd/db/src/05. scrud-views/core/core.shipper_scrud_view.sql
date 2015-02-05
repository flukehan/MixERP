CREATE VIEW core.shipper_scrud_view
AS
SELECT
        shipper_id,
        shipper_code,
        company_name,
        shipper_name,
        po_box,
        address_line_1,
        address_line_2,
        street,
        city,
        state,
        country,
        phone,
        fax,
        cell,
        email,
        url,
        contact_person,
        contact_po_box,
        contact_address_line_1,
        contact_address_line_2,
        contact_street,
        contact_city,
        contact_state,
        contact_country,
        contact_email,
        contact_phone,
        contact_cell,
        factory_address,
        pan_number,
        sst_number,
        cst_number,
        account_number || ' (' || account_name || ')' AS account
FROM core.shippers
INNER JOIN core.accounts
ON core.shippers.account_id = core.accounts.account_id;

