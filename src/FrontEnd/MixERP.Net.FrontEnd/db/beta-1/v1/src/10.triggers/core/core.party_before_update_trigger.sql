CREATE FUNCTION core.party_before_update_trigger()
RETURNS TRIGGER
AS
$$
    DECLARE _parent_currency_code text;
BEGIN
    
    --Get currency code of associated GL head.
    _parent_currency_code := core.get_currency_code_by_party_id(NEW.party_id);


    IF(NEW.currency_code != _parent_currency_code) THEN
        RAISE EXCEPTION 'You cannot have a different currency on the mapped GL account.'
        USING ERRCODE='P8003';
    END IF;

    IF(NEW.account_id IS NULL) THEN
        RAISE EXCEPTION 'The column account_id cannot be null.'
        USING ERRCODE='P3501';
    END IF;
    
    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER party_before_update_trigger
BEFORE UPDATE
ON core.parties
FOR EACH ROW EXECUTE PROCEDURE core.party_before_update_trigger();

