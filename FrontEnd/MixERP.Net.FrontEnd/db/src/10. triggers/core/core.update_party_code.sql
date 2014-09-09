CREATE FUNCTION core.update_party_code()
RETURNS TRIGGER
AS
$$
BEGIN
	UPDATE core.parties
	SET 
		party_code=core.get_party_code(NEW.first_name, NEW.middle_name, NEW.last_name)
	WHERE core.parties.party_id=NEW.party_id;
	
	RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER update_party_code
AFTER INSERT
ON core.parties
FOR EACH ROW EXECUTE PROCEDURE core.update_party_code();
