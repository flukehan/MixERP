DROP FUNCTION IF EXISTS transactions.get_party_transaction_summary
(
    office_id integer, 
    party_id bigint
);

CREATE FUNCTION transactions.get_party_transaction_summary
(
    office_id integer, 
    party_id bigint, 
    OUT currency_code text, 
    OUT currency_symbol text, 
    OUT total_due_amount decimal(24, 4), 
    OUT office_due_amount decimal(24, 4), 
    OUT last_receipt_date date, 
    OUT transaction_value decimal(24, 4)
)
AS
$$
    DECLARE root_office_id integer = 0;
BEGIN
    currency_code := core.get_currency_code_by_party_id(party_id);

    SELECT core.currencies.currency_symbol into $4
    FROM core.currencies
    WHERE core.currencies.currency_code = $3;

    SELECT office.offices.office_id INTO root_office_id
    FROM office.offices
    WHERE parent_office_id IS NULL;

    total_due_amount := transactions.get_total_due(root_office_id, party_id);
    office_due_amount := transactions.get_total_due(office_id, party_id);
    last_receipt_date := transactions.get_last_receipt_date(office_id, party_id);
    transaction_value := transactions.get_average_party_transaction(party_id, office_id);

    RETURN;
END
$$
LANGUAGE plpgsql;
