CREATE OR REPLACE FUNCTION core.get_party_code
(
    text, --First Name
    text, --Middle Name
    text  --Last Name
)
RETURNS text AS
$$
    DECLARE _party_code TEXT;
BEGIN
    SELECT INTO 
        _party_code 
            party_code
    FROM
        core.parties
    WHERE
        party_code LIKE 
            UPPER(left($1,2) ||
            CASE
                WHEN $2 IS NULL or $2 = '' 
                THEN left($3,3)
            ELSE 
                left($2,1) || left($3,2)
            END 
            || '%')
    ORDER BY party_code desc
    LIMIT 1;

    _party_code :=
                    UPPER
                    (
                        left($1,2)||
                        CASE
                            WHEN $2 IS NULL or $2 = '' 
                            THEN left($3,3)
                        ELSE 
                            left($2,1)||left($3,2)
                        END
                    ) 
                    || '-' ||
                    CASE
                        WHEN _party_code IS NULL 
                        THEN '0001'
                    ELSE 
                        to_char(right(_party_code,4)::national character varying::int +1,'FM0000')
                    END;
    RETURN _party_code;
END;
$$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION core.get_party_code
(
    text --Company Name
)
RETURNS text AS
$$
    DECLARE _party_code TEXT;
BEGIN
    SELECT INTO 
        _party_code 
            party_code
    FROM
        core.parties
    WHERE
        party_code LIKE 
            UPPER(left($1,5) || '%')
    ORDER BY party_code desc
    LIMIT 1;

    
    _party_code :=
                    UPPER
                    (left($1,5)) 
                    || '-' ||
                    CASE
                        WHEN _party_code IS NULL 
                        THEN '0001'
                    ELSE 
                        to_char(right(_party_code, 4)::national character varying::int +1,'FM0000')
                    END;
    RETURN _party_code;
END;
$$
LANGUAGE 'plpgsql';