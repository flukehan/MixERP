
/*******************************************************************
    GET UNIQUE EIGHT-TO-TEN DIGIT shipper CODE
    TO IDENTIFY A shipper.
    BASIC FORMULA:
        1. FIRST TWO LETTERS OF FIRST NAME
        2. FIRST LETTER OF MIDDLE NAME (IF AVAILABLE)
        3. FIRST TWO LETTERS OF LAST NAME
        4. shipper NUMBER
*******************************************************************/

CREATE OR REPLACE FUNCTION core.get_shipper_code
(
    text --company name
)
RETURNS text AS
$$
    DECLARE __shipper_code TEXT;
BEGIN
    SELECT INTO 
        __shipper_code 
            shipper_code
    FROM
        core.shippers
    WHERE
        shipper_code LIKE 
            UPPER(left($1, 3) || '%')
    ORDER BY shipper_code desc
    LIMIT 1;

    __shipper_code :=
                    UPPER
                    (
                        left($1,3)
                    ) 
                    || '-' ||
                    CASE
                        WHEN __shipper_code IS NULL 
                        THEN '0001'
                    ELSE 
                        to_char(CAST(right(__shipper_code, 4) AS integer)+1,'FM0000')
                    END;
    RETURN __shipper_code;
END;
$$
LANGUAGE 'plpgsql';


