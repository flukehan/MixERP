CREATE FUNCTION add_role(rolename text, password text)
RETURNS void AS
$$
    DECLARE dynamic     text;
BEGIN
    IF EXISTS(SELECT * FROM pg_roles WHERE rolname=$1) THEN
        dynamic := 'ALTER ROLE ' || quote_ident($1) || ' WITH PASSWORD ' || quote_literal($2) || ';';
        EXECUTE dynamic;
        RETURN;
    END IF;

    dynamic := 'CREATE ROLE ' || quote_ident($1) || ' WITH PASSWORD ' || quote_literal($2) || ';';
    EXECUTE dynamic;    
END
$$
LANGUAGE plpgsql;
