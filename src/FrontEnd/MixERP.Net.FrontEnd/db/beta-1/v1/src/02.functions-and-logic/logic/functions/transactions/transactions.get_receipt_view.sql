DROP FUNCTION IF EXISTS transactions.get_receipt_view
(
    _user_id                integer,
    _office_id              integer,
    _date_from              date, 
    _date_to                date, 
    _office                 national character varying(12),
    _party                  text,   
    _user                   national character varying(50),
    _reference_number           national character varying(24),
    _statement_reference            text
);

CREATE FUNCTION transactions.get_receipt_view
(
    _user_id                integer,
    _office_id              integer,
    _date_from              date, 
    _date_to                date, 
    _office                 national character varying(12),
    _party                  text,   
    _user                   national character varying(50),
    _reference_number           national character varying(24),
    _statement_reference            text
)
RETURNS TABLE
(
        id                                      bigint,
        value_date                              date,
        reference_number                        text,
        statement_reference                     text,
        office                                  text,
        party                                   text,
        "user"                                    text,
        currency_code                           text,
        amount                                  money_strict,
    transaction_ts              TIMESTAMP WITH TIME ZONE,
    flag_background_color           text,
    flag_foreground_color           text
)
AS
$$
BEGIN
    RETURN QUERY 
        SELECT
                transactions.transaction_master.transaction_master_id,
                transactions.transaction_master.value_date,
                transactions.transaction_master.reference_number::text,
                transactions.transaction_master.statement_reference::text,
                office.offices.office_code || ' (' || office.offices.office_name || ')' as office,
                core.parties.party_code || ' (' || core.parties.party_name || ')' as party,
                office.users.user_name::text,
                transactions.customer_receipts.currency_code::text,
                transactions.customer_receipts.amount,
        transactions.transaction_master.transaction_ts,
        core.get_flag_background_color(core.get_flag_type_id(_user_id, 'transactions.transaction_master', 'transaction_master_id', transactions.transaction_master.transaction_master_id::text)) AS flag_bg,
        core.get_flag_foreground_color(core.get_flag_type_id(_user_id, 'transactions.transaction_master', 'transaction_master_id', transactions.transaction_master.transaction_master_id::text)) AS flag_fg                
        FROM transactions.customer_receipts
        INNER JOIN core.parties
        ON transactions.customer_receipts.party_id = core.parties.party_id
        INNER JOIN transactions.transaction_master
        ON transactions.customer_receipts.transaction_master_id = transactions.transaction_master.transaction_master_id
        INNER JOIN office.offices
        ON transactions.transaction_master.office_id = office.offices.office_id
        INNER JOIN office.users
        ON transactions.transaction_master.user_id = office.users.user_id
        WHERE transactions.transaction_master.verification_status_id > 0
        AND transactions.transaction_master.office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND transactions.transaction_master.value_date BETWEEN _date_from AND _date_to
        AND
    lower
    (
        core.parties.party_code || ' (' || core.parties.party_name || ')'
    ) LIKE '%' || lower(_party) || '%'
    AND 
    lower
    (
        office.users.user_name
    )  LIKE '%' || lower(_user) || '%'
    AND 
    lower
    (
        transactions.transaction_master.reference_number
    ) LIKE '%' || lower(_reference_number) || '%'
    AND 
    lower
    (
        transactions.transaction_master.statement_reference
    ) LIKE '%' || lower(_statement_reference) || '%'    
    AND lower
    (
        office.offices.office_code
    ) LIKE '%' || lower(_office) || '%'
    LIMIT 100;
END
$$
LANGUAGE plpgsql;




--SELECT * FROM transactions.get_receipt_view(1, 1,'1-1-2000','1-1-2020','','','','','');

