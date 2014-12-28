
DROP FUNCTION IF EXISTS transactions.get_non_gl_product_view
(   
    user_id_                integer,
    book_                   text,
    office_id_              integer,
    date_from_              date, 
    date_to_                date, 
    office_                 national character varying(12),
    party_                  text,   
    price_type_             text,
    user_                   national character varying(50),
    reference_number_           national character varying(24),
    statement_reference_            text
 );

CREATE FUNCTION transactions.get_non_gl_product_view
(
    user_id_                integer,
    book_                   text,
    office_id_              integer,
    date_from_              date, 
    date_to_                date, 
    office_                 national character varying(12),
    party_                  text,   
    price_type_             text,
    user_                   national character varying(50),
    reference_number_           national character varying(24),
    statement_reference_            text
 )
RETURNS TABLE
(
    id                  bigint,
    value_date              date,
    office                  national character varying(12),
    party                   text,
    price_type              text,
    amount                      decimal(24, 4),
    transaction_ts              TIMESTAMP WITH TIME ZONE,
    "user"                  national character varying(50),
    reference_number            national character varying(24),
    statement_reference         text,
    book                            text,
    flag_background_color           text,
    flag_foreground_color           text
)
AS
$$
BEGIN
    RETURN QUERY 
    WITH RECURSIVE office_cte(office_id) AS 
    (
        SELECT office_id_
        UNION ALL
        SELECT
            c.office_id
        FROM 
        office_cte AS p, 
        office.offices AS c 
        WHERE 
        parent_office_id = p.office_id
    )

    SELECT
        transactions.non_gl_stock_master.non_gl_stock_master_id AS id,
        transactions.non_gl_stock_master.value_date,
        office.offices.office_code AS office,
        core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
        core.price_types.price_type_code || ' (' || core.price_types.price_type_name || ')' AS price_type,
        SUM(transactions.non_gl_stock_details.price * transactions.non_gl_stock_details.quantity + tax - discount)::decimal(24, 4) AS amount,
        transactions.non_gl_stock_master.transaction_ts,
        office.users.user_name AS user,
        transactions.non_gl_stock_master.reference_number,
        transactions.non_gl_stock_master.statement_reference,
        transactions.non_gl_stock_master.book::text,
        core.get_flag_background_color(core.get_flag_type_id(user_id_, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', transactions.non_gl_stock_master.non_gl_stock_master_id::text)) AS flag_bg,
        core.get_flag_foreground_color(core.get_flag_type_id(user_id_, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', transactions.non_gl_stock_master.non_gl_stock_master_id::text)) AS flag_fg
    FROM transactions.non_gl_stock_master
    INNER JOIN transactions.non_gl_stock_details
    ON transactions.non_gl_stock_master.non_gl_stock_master_id = transactions.non_gl_stock_details.non_gl_stock_master_id
    INNER JOIN core.parties
    ON transactions.non_gl_stock_master.party_id = core.parties.party_id
    INNER JOIN office.users
    ON transactions.non_gl_stock_master.user_id = office.users.user_id
    INNER JOIN office.offices
    ON transactions.non_gl_stock_master.office_id = office.offices.office_id
    LEFT OUTER JOIN core.price_types
    ON transactions.non_gl_stock_master.price_type_id = core.price_types.price_type_id
    WHERE transactions.non_gl_stock_master.book = book_
    AND transactions.non_gl_stock_master.value_date BETWEEN date_from_ AND date_to_
    AND 
    lower
    (
        core.parties.party_code || ' (' || core.parties.party_name || ')'
    ) LIKE '%' || lower(party_) || '%'
    AND
    lower
    (
        COALESCE(core.price_types.price_type_code, '') || ' (' || COALESCE(core.price_types.price_type_name, '') || ')'
    ) LIKE '%' || lower(price_type_) || '%'
    AND 
    lower
    (
        office.users.user_name
    )  LIKE '%' || lower(user_) || '%'
    AND 
    lower
    (
        COALESCE(transactions.non_gl_stock_master.reference_number, '')
    ) LIKE '%' || lower(reference_number_) || '%'
    AND 
    lower
    (
        COALESCE(transactions.non_gl_stock_master.statement_reference, '')
    ) LIKE '%' || lower(statement_reference_) || '%'    
    AND lower
    (
        office.offices.office_code
    ) LIKE '%' || lower(office_) || '%' 
    AND office.offices.office_id IN (SELECT office_id FROM office_cte)
    GROUP BY 
        transactions.non_gl_stock_master.non_gl_stock_master_id,
        transactions.non_gl_stock_master.value_date,
        office.offices.office_code,
        core.parties.party_code,
        core.parties.party_name,
        core.price_types.price_type_code,
        core.price_types.price_type_name,
        transactions.non_gl_stock_master.transaction_ts,
        office.users.user_name,
        transactions.non_gl_stock_master.reference_number,
        transactions.non_gl_stock_master.statement_reference,
        transactions.non_gl_stock_master.book
    LIMIT 100;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_non_gl_product_view(1,'Purchase.Order',1, '1-1-2000', '1-1-2050', '', '', '', '', '', '');


