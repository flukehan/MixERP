DROP VIEW IF EXISTS core.payment_card_scrud_view;

CREATE VIEW core.payment_card_scrud_view
AS
SELECT 
    core.payment_cards.payment_card_id,
    core.payment_cards.payment_card_code,
    core.payment_cards.payment_card_name,
    core.card_types.card_type_code || ' (' || core.card_types.card_type_name || ')' AS card_type
FROM core.payment_cards
INNER JOIN core.card_types
ON core.payment_cards.card_type_id = core.card_types.card_type_id;