
DROP FUNCTION IF EXISTS transactions.get_product_view
(	
	user_id_				integer,
	book_					text,
	office_id_				integer,
	date_from_				date, 
	date_to_				date, 
	office_					national character varying(12),
	party_					text,	
	price_type_				text,
	user_					national character varying(50),
	reference_number_			national character varying(24),
	statement_reference_			text
 );

CREATE FUNCTION transactions.get_product_view
(
	user_id_				integer,
	book_					text,
	office_id_				integer,
	date_from_				date, 
	date_to_				date, 
	office_					national character varying(12),
	party_					text,	
	price_type_				text,
	user_					national character varying(50),
	reference_number_			national character varying(24),
	statement_reference_			text
 )
RETURNS TABLE
(
	id					bigint,
	value_date				date,
	office					national character varying(12),
	party					text,
	price_type				text,
	amount					decimal(24, 4),
	transaction_ts				TIMESTAMP WITH TIME ZONE,
	"user"					national character varying(50),
	reference_number			national character varying(24),
	statement_reference			text,
	book                                    text,
	agent					text,
	is_credit				boolean,
	shipper					text,
	shipping_address_code			text,
	store					text,	
	flag_background_color			text,
	flag_foreground_color			text
)
AS
$$
BEGIN
        CREATE TEMPORARY TABLE IF NOT EXISTS temp_book(book text) ON COMMIT DROP;

        INSERT INTO temp_book
        SELECT book_;

        IF(book_ = 'Sales.Return') THEN
                TRUNCATE TABLE temp_book;

                INSERT INTO temp_book
                SELECT 'Sales.Direct' UNION SELECT 'Sales.Delivery';                
        END IF;



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
		transactions.stock_master.transaction_master_id AS id,
		transactions.transaction_master.value_date,
		office.offices.office_code AS office,
		core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
		core.price_types.price_type_code || ' (' || core.price_types.price_type_name || ')' AS price_type,
		SUM(transactions.stock_details.price * transactions.stock_details.quantity + tax - discount)::decimal(24, 4) AS amount,
		transactions.transaction_master.transaction_ts,
		office.users.user_name AS user,
		transactions.transaction_master.reference_number,
		transactions.transaction_master.statement_reference,
                transactions.transaction_master.book::text,
		core.get_agent_name_by_agent_id(transactions.stock_master.agent_id),
		transactions.stock_master.is_credit,
		core.get_shipper_name_by_shipper_id(transactions.stock_master.shipper_id),
		core.get_shipping_address_code_by_shipping_address_id(transactions.stock_master.shipping_address_id),
		office.get_store_name_by_store_id(transactions.stock_master.store_id),
		core.get_flag_background_color(core.get_flag_type_id(user_id_, 'transactions.stock_master', 'stock_master_id', transactions.stock_master.stock_master_id)) AS flag_bg,
		core.get_flag_foreground_color(core.get_flag_type_id(user_id_, 'transactions.stock_master', 'stock_master_id', transactions.stock_master.stock_master_id)) AS flag_fg
	FROM transactions.stock_master
	INNER JOIN transactions.stock_details
	ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
	INNER JOIN core.parties
	ON transactions.stock_master.party_id = core.parties.party_id
	INNER JOIN transactions.transaction_master
	ON transactions.transaction_master.transaction_master_id=transactions.stock_master.transaction_master_id
	INNER JOIN office.users
	ON transactions.transaction_master.user_id = office.users.user_id
	INNER JOIN office.offices
	ON transactions.transaction_master.office_id = office.offices.office_id
	LEFT OUTER JOIN core.price_types
	ON transactions.stock_master.price_type_id = core.price_types.price_type_id
	WHERE transactions.transaction_master.book IN (SELECT * FROM temp_book)
	AND NOT
	(
                book_ = 'Sales.Return'
                AND
                transactions.transaction_master.transaction_master_id IN (SELECT transaction_master_id FROM transactions.sales_return)                
	)
	AND transactions.transaction_master.verification_status_id > 0
	AND transactions.transaction_master.value_date BETWEEN date_from_ AND date_to_
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
		transactions.transaction_master.reference_number
	) LIKE '%' || lower(reference_number_) || '%'
	AND 
	lower
	(
		transactions.transaction_master.statement_reference
	) LIKE '%' || lower(statement_reference_) || '%'	
	AND lower
	(
		office.offices.office_code
	) LIKE '%' || lower(office_) || '%'	
	AND office.offices.office_id IN (SELECT office_id FROM office_cte)
	GROUP BY 
		transactions.stock_master.stock_master_id,
		transactions.transaction_master.value_date,
		office.offices.office_code,
		core.parties.party_code,
		core.parties.party_name,
		core.price_types.price_type_code,
		core.price_types.price_type_name,
		transactions.transaction_master.transaction_ts,
		office.users.user_name,
		transactions.transaction_master.reference_number,
		transactions.transaction_master.statement_reference,
		transactions.transaction_master.book	
	LIMIT 100;
END
$$
LANGUAGE plpgsql;

--select * from transactions.get_product_view(1, 'Sales.Return', 1, '1-1-2000',  '1-1-2020', '', '', '', '', '', '');
