DROP FUNCTION IF EXISTS core.create_payment_card
(
    _payment_card_code      national character varying(12),
    _payment_card_name      national character varying(100),
    _card_type_id           integer
);

CREATE FUNCTION core.create_payment_card
(
    _payment_card_code      national character varying(12),
    _payment_card_name      national character varying(100),
    _card_type_id           integer
)
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.payment_cards
        WHERE payment_card_code = _payment_card_code
    ) THEN
        INSERT INTO core.payment_cards(payment_card_code, payment_card_name, card_type_id)
        SELECT _payment_card_code, _payment_card_name, _card_type_id;
    ELSE
        UPDATE core.payment_cards
        SET 
            payment_card_code =     _payment_card_code, 
            payment_card_name =     _payment_card_name,
            card_type_id =          _card_type_id
        WHERE
            payment_card_code =     _payment_card_code;
    END IF;
END
$$
LANGUAGE plpgsql;