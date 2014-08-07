/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

/********************************************************************************
	NOTE: ALL RANDOM INDEXES ARE REMOVED FROM THE SCRIPT.
	TODO : NEED TO CREATE INDEXES.
***********************************************************************************/

DO 
$$
BEGIN
   EXECUTE 'ALTER DATABASE ' || current_database() || ' SET timezone TO ''UTC''';
END;
$$
LANGUAGE plpgsql;


DROP SCHEMA IF EXISTS audit CASCADE;
DROP SCHEMA IF EXISTS core CASCADE;
DROP SCHEMA IF EXISTS office CASCADE;
DROP SCHEMA IF EXISTS policy CASCADE;
DROP SCHEMA IF EXISTS transactions CASCADE;
DROP SCHEMA IF EXISTS crm CASCADE;
DROP SCHEMA IF EXISTS mrp CASCADE;


CREATE SCHEMA audit;
CREATE SCHEMA core;
CREATE SCHEMA office;
CREATE SCHEMA policy;
CREATE SCHEMA transactions;
CREATE SCHEMA crm;
CREATE SCHEMA mrp;

DO
$$
BEGIN
	IF NOT EXISTS (SELECT * FROM pg_catalog.pg_user WHERE  usename = 'mix_erp') THEN
		CREATE ROLE mix_erp WITH LOGIN PASSWORD 'change-on-deloyment';
	END IF;


	GRANT USAGE ON SCHEMA public TO mix_erp;
	GRANT USAGE ON SCHEMA information_schema TO mix_erp;
	GRANT USAGE ON SCHEMA audit TO mix_erp;
	GRANT USAGE ON SCHEMA core TO mix_erp;
	GRANT USAGE ON SCHEMA office TO mix_erp;
	GRANT USAGE ON SCHEMA policy TO mix_erp;
	GRANT USAGE ON SCHEMA transactions TO mix_erp;
	GRANT USAGE ON SCHEMA crm TO mix_erp;
	GRANT USAGE ON SCHEMA mrp TO mix_erp;

	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT SELECT ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;

	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT ALL ON SEQUENCES TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT ALL ON SEQUENCES TO mix_erp;




	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT EXECUTE ON FUNCTIONS TO mix_erp;
	ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT EXECUTE ON FUNCTIONS TO mix_erp;
   
END
$$
LANGUAGE plpgsql;




DO
$$
BEGIN
	IF NOT EXISTS (SELECT * FROM pg_catalog.pg_user WHERE  usename = 'report_user') THEN
		CREATE ROLE report_user WITH LOGIN PASSWORD 'change-on-deloyment';
	END IF;

	GRANT USAGE ON SCHEMA public TO report_user;
	GRANT USAGE ON SCHEMA information_schema TO report_user;
	GRANT USAGE ON SCHEMA audit TO report_user;
	GRANT USAGE ON SCHEMA core TO report_user;
	GRANT USAGE ON SCHEMA office TO report_user;
	GRANT USAGE ON SCHEMA policy TO report_user;
	GRANT USAGE ON SCHEMA transactions TO report_user;
	GRANT USAGE ON SCHEMA crm TO report_user;
	GRANT USAGE ON SCHEMA mrp TO report_user;

	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT SELECT ON TABLES TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT SELECT ON TABLES TO report_user;


	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT EXECUTE ON FUNCTIONS TO report_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT EXECUTE ON FUNCTIONS TO report_user;
END
$$
LANGUAGE plpgsql;




CREATE TABLE core.verification_statuses
(
	verification_status_id			smallint NOT NULL PRIMARY KEY,
	verification_status_name		national character varying(128) NOT NULL
);

CREATE UNIQUE INDEX verification_statuses_verification_status_name_uix
ON core.verification_statuses(UPPER(verification_status_name));


--These are hardcoded values and therefore the meanings should always remain intact
--regardless of the language.
INSERT INTO core.verification_statuses
SELECT -3, 'Rejected' UNION ALL
SELECT -2, 'Closed' UNION ALL
SELECT -1, 'Withdrawn' UNION ALL
SELECT 0, 'Unverified' UNION ALL
SELECT 1, 'Automatically Approved by Workflow' UNION ALL
SELECT 2, 'Approved';

DROP DOMAIN IF EXISTS transaction_type;
CREATE DOMAIN transaction_type
AS char(2)
CHECK
(
	VALUE IN
	(
		'Dr', --Debit
		'Cr' --Credit
	)
);

/*******************************************************************
	MIXERP STRICT Data Types: NEGATIVES ARE NOT ALLOWED
*******************************************************************/

DROP DOMAIN IF EXISTS money_strict;
CREATE DOMAIN money_strict
AS money
CHECK
(
	VALUE > '0'
);


DROP DOMAIN IF EXISTS money_strict2;
CREATE DOMAIN money_strict2
AS money
CHECK
(
	VALUE >= '0'
);

DROP DOMAIN IF EXISTS integer_strict;
CREATE DOMAIN integer_strict
AS integer
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS integer_strict2;
CREATE DOMAIN integer_strict2
AS integer
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS smallint_strict;
CREATE DOMAIN smallint_strict
AS smallint
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS smallint_strict2;
CREATE DOMAIN smallint_strict2
AS smallint
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS decimal_strict;
CREATE DOMAIN decimal_strict
AS decimal
CHECK
(
	VALUE > 0
);

DROP VIEW IF EXISTS db_stat;
CREATE VIEW db_stat
AS
SELECT
	relname,
	last_vacuum,
	last_autovacuum,
	last_analyze,
	last_autoanalyze,
	vacuum_count,
	autovacuum_count,
	analyze_count,
	autoanalyze_count
FROM
   pg_stat_user_tables;

DROP DOMAIN IF EXISTS decimal_strict2;
CREATE DOMAIN decimal_strict2
AS decimal
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS image_path;
CREATE DOMAIN image_path
AS text;

DROP DOMAIN IF EXISTS color;
CREATE DOMAIN color
AS text;


CREATE TABLE office.users
(
	user_id 				SERIAL NOT NULL PRIMARY KEY,
	role_id 				smallint NOT NULL,
	office_id 				integer NOT NULL,
	user_name 				national character varying(50) NOT NULL,
	full_name 				national character varying(100) NOT NULL,
	password 				text NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE TABLE core.flag_types
(
	flag_type_id				SERIAL NOT NULL PRIMARY KEY,
	flag_type_name				national character varying(24) NOT NULL,
	background_color			color NOT NULL,
	foreground_color			color NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

INSERT INTO core.flag_types(flag_type_name, background_color, foreground_color)
SELECT 'Critical', 		'#CC0404', '#FFFFFF' UNION ALL
SELECT 'Important',		'#A3159E', '#FFFFFF' UNION ALL
SELECT 'Review', 		'#142F82', '#FFFFFF' UNION ALL
SELECT 'Todo', 			'#F9FF4F', '#000000' UNION ALL
SELECT 'OK', 			'#95F75C', '#000000';

CREATE TABLE core.flags
(
	flag_id					BIGSERIAL NOT NULL PRIMARY KEY,
	user_id					integer NOT NULL REFERENCES office.users(user_id),
	flag_type_id				integer NOT NULL REFERENCES core.flag_types(flag_type_id),
	resource				text, --Fully qualified resource name. Example: transactions.non_gl_stock_master.
	resource_key				text, --The unique idenfier for lookup. Example: non_gl_stock_master_id,
	resource_id				integer, --The value of the unique identifier to lookup for,
	flagged_on				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX flags_user_id_resource_resource_id_uix
ON core.flags(user_id, UPPER(resource), UPPER(resource_key), resource_id);


CREATE FUNCTION core.create_flag
(
	user_id_ 	integer,
	flag_type_id_	integer,
	resource_	text,
	resource_key_	text,
	resource_id_	integer
)
RETURNS void
AS
$$
BEGIN
	IF NOT EXISTS(SELECT * FROM core.flags WHERE user_id=user_id_ AND resource=resource_ AND resource_key=resource_key_ AND resource_id=resource_id_) THEN
		INSERT INTO core.flags(user_id, flag_type_id, resource, resource_key, resource_id)
		SELECT user_id_, flag_type_id_, resource_, resource_key_, resource_id_;
	ELSE
		UPDATE core.flags
		SET
			flag_type_id=flag_type_id_
		WHERE 
			user_id=user_id_ 
		AND 
			resource=resource_ 
		AND 
			resource_key=resource_key_ 
		AND 
			resource_id=resource_id_;
	END IF;
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION core.get_flag_type_id
(
	user_id_ integer,
	resource_ text,
	resource_key_ text,
	resource_id_ bigint
)
RETURNS integer
AS
$$
BEGIN
	RETURN 
	(
		SELECT flag_type_id
		FROM core.flags
		WHERE user_id=$1
		AND resource=$2
		AND resource_key=$3
		AND resource_id=$4
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_flag_type_id
(
	user_id_ integer,
	resource_ text,
	resource_id_ bigint
)
RETURNS integer
AS
$$
BEGIN
	RETURN core.get_flag_type_id($1, $2, $3::text);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_flag_type_id
(
	user_id_ integer,
	resource_ text,
	resource_id_ integer
)
RETURNS integer
AS
$$
BEGIN
	RETURN core.get_flag_type_id($1, $2, $3::text);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_flag_background_color(flag_type_id_ integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT background_color
		FROM core.flag_types
		WHERE core.flag_types.flag_type_id=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_flag_foreground_color(flag_type_id_ integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT foreground_color
		FROM core.flag_types
		WHERE core.flag_types.flag_type_id=$1
	);
END
$$
LANGUAGE plpgsql;


CREATE TABLE transactions.attachments
(
	attachment_id				BIGSERIAL NOT NULL PRIMARY KEY,
	user_id					integer NOT NULL CONSTRAINT attachments_users_fk REFERENCES office.users(user_id),
	resource				text, --Fully qualified resource name. Example: transactions.non_gl_stock_master.
	resource_key				text, --The unique idenfier for lookup. Example: non_gl_stock_master_id,
	resource_id				integer, --The value of the unique identifier to lookup for,
	original_file_name			text NOT NULL,
	file_extension				national character varying(12) NOT NULL,
	file_size				integer NOT NULL,
	file_path				text NOT NULL,
	comment					national character varying(96) NOT NULL CONSTRAINT attachments_comment_df DEFAULT(''),
	added_on				TIMESTAMP WITH TIME ZONE NOT NULL CONSTRAINT attachments_added_on_df DEFAULT(NOW())
);

CREATE UNIQUE INDEX attachments_file_path_uix
ON transactions.attachments(UPPER(file_path));


CREATE TABLE core.currencies
(
	currency_code				national character varying(12) NOT NULL PRIMARY KEY,
	currency_symbol				national character varying(12) NOT NULL,
	currency_name				national character varying(48) NOT NULL UNIQUE,
	hundredth_name				national character varying(48) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

INSERT INTO core.currencies
SELECT 'NPR', 'Rs.', 'Nepali Rupees', 'paisa' UNION ALL
SELECT 'USD', '$ ', 'United States Dollar', 'cents';

CREATE FUNCTION office.is_parent_office(parent integer_strict, child integer_strict)
RETURNS boolean
AS
$$		
BEGIN
	IF $1!=$2 THEN
		IF EXISTS
		(
			WITH RECURSIVE office_cte(office_id, path) AS (
			 SELECT
				tn.office_id,  tn.office_id::TEXT AS path
				FROM office.offices AS tn WHERE tn.parent_office_id IS NULL
			UNION ALL
			 SELECT
				c.office_id, (p.path || '->' || c.office_id::TEXT)
				FROM office_cte AS p, office.offices AS c WHERE parent_office_id = p.office_id
			)
			SELECT * FROM
			(
				SELECT regexp_split_to_table(path, '->')
				FROM office_cte AS n WHERE n.office_id = $2
			) AS items
			WHERE regexp_split_to_table=$1::text
		) THEN
			RETURN TRUE;
		END IF;
	END IF;
	RETURN false;
END
$$
LANGUAGE plpgsql;

CREATE TABLE office.offices
(
	office_id				SERIAL NOT NULL PRIMARY KEY,
	office_code 				national character varying(12) NOT NULL,
	office_name 				national character varying(150) NOT NULL,
	nick_name 				national character varying(50) NULL,
	registration_date 			date NOT NULL,
	currency_code 				national character varying(12) NOT NULL 
						CONSTRAINT offices_currencies_fk REFERENCES core.currencies(currency_code)
						CONSTRAINT offices_currency_code_df DEFAULT('NPR'),
	address_line_1				national character varying(128) NULL,	
	address_line_2				national character varying(128) NULL,
	street 					national character varying(50) NULL,
	city 					national character varying(50) NULL,
	state 					national character varying(50) NULL,
	zip_code				national character varying(24) NULL,
	country 				national character varying(50) NULL,
	phone 					national character varying(24) NULL,
	fax 					national character varying(24) NULL,
	email 					national character varying(128) NULL,
	url 					national character varying(50) NULL,
	registration_number 			national character varying(24) NULL,
	pan_number 				national character varying(24) NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
	parent_office_id 			integer NULL REFERENCES office.offices(office_id)
		CHECK
		(
			office.is_parent_office(office_id, parent_office_id) = FALSE
			AND
			parent_office_id != office_id
		)
);

ALTER TABLE office.users
ADD FOREIGN KEY(office_id) REFERENCES office.offices(office_id);

CREATE UNIQUE INDEX offices_office_code_uix
ON office.offices(UPPER(office_code));

CREATE UNIQUE INDEX offices_office_name_uix
ON office.offices(UPPER(office_name));

CREATE UNIQUE INDEX offices_nick_name_uix
ON office.offices(UPPER(nick_name));


/*******************************************************************
	SAMPLE DATA FEED
	TODO: REMOVE THE BELOW BEFORE RELEASE
*******************************************************************/

INSERT INTO office.offices(office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number)
SELECT 'PES','Planet Earth Solutions', 'PES Technologies', '06/06/1989', 'Brooklyn','NY','','US','','','','info@mixof.org','http://mixof.org','0','0';


INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,parent_office_id)
SELECT 'PES-NY-BK','Brooklyn Branch', 'PES Brooklyn', '06/06/1989', 'Brooklyn','NY','12345555','','','','','info@mixof.org','http://mixof.org','0','0',(SELECT office_id FROM office.offices WHERE office_code='PES');

INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,parent_office_id)
SELECT 'PES-NY-MEM','Memphis Branch', 'PES Memphis', '06/06/1989', 'Memphis', 'NY','','','','64464554','','info@mixof.org','http://mixof.org','0','0',(SELECT office_id FROM office.offices WHERE office_code='PES');


/*******************************************************************
	RETURNS MINI OFFICE TABLE
*******************************************************************/

CREATE TYPE office.office_type AS
(
	office_id				integer_strict,
	office_code 				national character varying(12),
	office_name 				national character varying(150),
	address text
);

CREATE FUNCTION office.get_offices()
RETURNS setof office.office_type
AS
$$
DECLARE "@record" office.office_type%rowtype;
BEGIN
	FOR "@record" IN SELECT office_id, office_code,office_name,street || ' ' || city AS Address FROM office.offices WHERE parent_office_id IS NOT NULL
	LOOP
		RETURN NEXT "@record";
	END LOOP;

	IF NOT FOUND THEN
		FOR "@record" IN SELECT office_id, office_code,office_name,street || ' ' || city AS Address FROM office.offices WHERE parent_office_id IS NULL
		LOOP
			RETURN NEXT "@record";
		END LOOP;
	END IF;

	RETURN;
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION office.get_office_name_by_id(office_id integer_strict)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT office.offices.office_name FROM office.offices
		WHERE office.offices.office_id=$1
	);
END
$$
LANGUAGE plpgsql;


--TODO
CREATE VIEW office.office_view
AS
SELECT * FROM office.offices;

CREATE TABLE office.departments
(
	department_id SERIAL			NOT NULL PRIMARY KEY,
	department_code				national character varying(12) NOT NULL,
	department_name				national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX departments_department_code_uix
ON office.departments(UPPER(department_code));

CREATE UNIQUE INDEX departments_department_name_uix
ON office.departments(UPPER(department_name));


INSERT INTO office.departments(department_code, department_name)
SELECT 'SAL', 'Sales & Billing' UNION ALL
SELECT 'MKT', 'Marketing & Promotion' UNION ALL
SELECT 'SUP', 'Support' UNION ALL
SELECT 'CC', 'Customer Care';


CREATE TABLE office.roles
(
	role_id SERIAL				NOT NULL PRIMARY KEY,
	role_code				national character varying(12) NOT NULL,
	role_name				national character varying(50) NOT NULL,
	is_admin 				boolean NOT NULL CONSTRAINT roles_is_admin_df DEFAULT(false),
	is_system 				boolean NOT NULL CONSTRAINT roles_is_system_df DEFAULT(false),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

ALTER TABLE office.users
ADD FOREIGN KEY(role_id) REFERENCES office.roles(role_id);


CREATE UNIQUE INDEX roles_role_code_uix
ON office.roles(UPPER(role_code));

CREATE UNIQUE INDEX roles_role_name_uix
ON office.roles(UPPER(role_name));

INSERT INTO office.roles(role_code,role_name, is_system)
SELECT 'SYST', 'System', true;

INSERT INTO office.roles(role_code,role_name, is_admin)
SELECT 'ADMN', 'Administrators', true;

INSERT INTO office.roles(role_code,role_name)
SELECT 'USER', 'Users' UNION ALL
SELECT 'EXEC', 'Executive' UNION ALL
SELECT 'MNGR', 'Manager' UNION ALL
SELECT 'SALE', 'Sales' UNION ALL
SELECT 'MARK', 'Marketing' UNION ALL
SELECT 'LEGL', 'Legal & Compliance' UNION ALL
SELECT 'FINC', 'Finance' UNION ALL
SELECT 'HUMR', 'Human Resources' UNION ALL
SELECT 'INFO', 'Information Technology' UNION ALL
SELECT 'CUST', 'Customer Service';



CREATE FUNCTION office.get_office_id_by_user_id(user_id integer_strict)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT office.users.office_id FROM office.users
		WHERE office.users.user_id=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION office.get_office_id_by_office_code(office_code text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT office.offices.office_id FROM office.offices
		WHERE office.offices.office_code=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION office.get_user_id_by_user_name(user_name text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT office.users.user_id FROM office.users
		WHERE office.users.user_name=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION office.get_user_name_by_user_id(user_id integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT office.users.user_name FROM office.users
		WHERE office.users.user_id=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION office.get_role_id_by_use_id(user_id integer_strict)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT office.users.role_id FROM office.users
		WHERE office.users.user_id=$1
	);
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION office.get_role_code_by_user_name(user_name text)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT office.roles.role_code FROM office.roles, office.users
		WHERE office.roles.role_id=office.users.role_id
		AND office.users.user_name=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE VIEW office.user_view
AS
SELECT
	office.users.user_id,
	office.users.user_name,
	office.users.full_name,
	office.roles.role_name,
	office.offices.office_name
FROM
	office.users
INNER JOIN office.roles
ON office.users.role_id = office.roles.role_id
INNER JOIN office.offices
ON office.users.office_id = office.offices.office_id;

CREATE FUNCTION office.get_sys_user_id()
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT office.users.user_id 
		FROM office.roles, office.users
		WHERE office.roles.role_id = office.users.role_id
		AND office.roles.is_system=true LIMIT 1
	);
END
$$
LANGUAGE plpgsql;



CREATE FUNCTION office.create_user
(
	role_id integer_strict,
	office_id integer_strict,
	user_name text,
	password text,
	full_name text
)
RETURNS VOID
AS
$$
BEGIN
	INSERT INTO office.users(role_id,office_id,user_name,password, full_name)
	SELECT $1, $2, $3, $4,$5;
	RETURN;
END
$$
LANGUAGE plpgsql;


SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='SYST'),(SELECT office_id FROM office.offices WHERE office_code='PES'),'sys','','System');

/*******************************************************************
	TODO: REMOVE THIS USER ON DEPLOYMENT
*******************************************************************/
SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='ADMN'),(SELECT office_id FROM office.offices WHERE office_code='PES'),'binod','+qJ9AMyGgrX/AOF4GmwmBa4SrA3+InlErVkJYmAopVZh+WFJD7k2ZO9dxox6XiqT38dSoM72jLoXNzwvY7JAQA==','Binod Nepal');

CREATE FUNCTION office.validate_login
(
	user_name text,
	password text
)
RETURNS boolean
AS
$$
BEGIN
	IF EXISTS
	(
		SELECT 1 FROM office.users 
		WHERE office.users.user_name=$1 
		AND office.users.password=$2 
		--The system user should not be allowed to login.
		AND office.users.role_id != 
		(
			SELECT office.roles.role_id 
			FROM office.roles 
			WHERE office.roles.role_code='SYST'
		)
	) THEN
		RETURN true;
	END IF;
	RETURN false;
END
$$
LANGUAGE plpgsql;



CREATE UNIQUE INDEX users_user_name_uix
ON office.users(UPPER(user_name));


CREATE TABLE audit.logins
(
	login_id 				BIGSERIAL NOT NULL PRIMARY KEY,
	user_id 				integer NOT NULL REFERENCES office.users(user_id),
	office_id 				integer NOT NULL REFERENCES office.offices(office_id),
	browser 				national character varying(500) NOT NULL,
	ip_address 				national character varying(50) NOT NULL,
	login_date_time 			TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(now()),
	remote_user 				national character varying(50) NOT NULL,
	culture					national character varying(12) NOT NULL
);

CREATE FUNCTION office.get_login_id(_user_id integer)
RETURNS bigint
AS
$$
BEGIN
	RETURN
	(
		SELECT login_id
		FROM audit.logins
		WHERE user_id=$1
		AND login_date_time = 
		(
			SELECT MAX(login_date_time)
			FROM audit.logins
			WHERE user_id=$1
		)
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION office.get_logged_in_office_id(_user_id integer)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT office_id
		FROM audit.logins
		WHERE user_id=$1
		AND login_date_time = 
		(
			SELECT MAX(login_date_time)
			FROM audit.logins
			WHERE user_id=$1
		)
	);
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION office.get_logged_in_culture(_user_id integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT culture
		FROM audit.logins
		WHERE user_id=$1
		AND login_date_time = 
		(
			SELECT MAX(login_date_time)
			FROM audit.logins
			WHERE user_id=$1
		)
	);
END
$$
LANGUAGE plpgsql;

CREATE TABLE audit.failed_logins
(
	failed_login_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	user_id 				integer NULL REFERENCES office.users(user_id),
	user_name 				national character varying(50) NOT NULL,
	office_id 				integer NULL REFERENCES office.offices(office_id),
	browser 				national character varying(500) NOT NULL,
	ip_address 				national character varying(50) NOT NULL,
	failed_date_time 			TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(now()),
	remote_user 				national character varying(50) NOT NULL,
	details 				national character varying(250) NULL
);


CREATE TABLE policy.lock_outs
(
	lock_out_id 				BIGSERIAL NOT NULL PRIMARY KEY,
	user_id 				integer NOT NULL REFERENCES office.users(user_id),
	lock_out_time 				TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	lock_out_till 				TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW() + '5 minutes'::interval)
);

--TODO: Create a lockout policy.
CREATE FUNCTION policy.perform_lock_out()
RETURNS TRIGGER
AS
$$
BEGIN
	IF(
		SELECT COUNT(*) FROM audit.failed_logins
		WHERE audit.failed_logins.user_id=NEW.user_id
		AND audit.failed_logins.failed_date_time 
		BETWEEN NOW()-'5minutes'::interval 
		AND NOW()
	)::integer>5 THEN

	INSERT INTO policy.lock_outs(user_id)SELECT NEW.user_id;
END IF;
RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER lockout_user
AFTER INSERT
ON audit.failed_logins
FOR EACH ROW EXECUTE PROCEDURE policy.perform_lock_out();

CREATE FUNCTION policy.is_locked_out_till(user_id integer_strict)
RETURNS TIMESTAMP
AS
$$
BEGIN
	RETURN
	(
		SELECT MAX(policy.lock_outs.lock_out_till)::TIMESTAMP WITHOUT TIME ZONE FROM policy.lock_outs
		WHERE policy.lock_outs.user_id=$1
	);
END
$$
LANGUAGE plpgsql;



CREATE TABLE core.price_types
(
	price_type_id 				SERIAL  NOT NULL PRIMARY KEY,
	price_type_code 			national character varying(12) NOT NULL,
	price_type_name 			national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX price_types_price_type_code_uix
ON core.price_types(UPPER(price_type_code));

CREATE UNIQUE INDEX price_types_price_type_name_uix
ON core.price_types(UPPER(price_type_name));


INSERT INTO core.price_types(price_type_code, price_type_name)
SELECT 'RET', 'Retail' UNION ALL
SELECT 'WHO', 'Wholesale';



CREATE TABLE core.menus
(
	menu_id 				SERIAL NOT NULL PRIMARY KEY,
	menu_text 				national character varying(250) NOT NULL,
	url 					national character varying(250) NULL,
	menu_code 				national character varying(12) NOT NULL,
	level 					smallint NOT NULL,
	parent_menu_id 				integer NULL REFERENCES core.menus(menu_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX menus_menu_code_uix
ON core.menus(UPPER(menu_code));

CREATE TABLE core.menu_locale
(
	menu_locale_id				SERIAL NOT NULL PRIMARY KEY,
	menu_id 				integer NOT NULL REFERENCES core.menus(menu_id),
	culture					national character varying(12) NOT NULL,
	menu_text 				national character varying(250) NOT NULL
);

CREATE UNIQUE INDEX menu_locale_menu_id_culture_uix
ON core.menu_locale(menu_id, LOWER(culture));

CREATE TABLE policy.menu_policy
(
	policy_id				SERIAL NOT NULL PRIMARY KEY,
	menu_id					integer NOT NULL REFERENCES core.menus(menu_id),
	office_id				integer NULL REFERENCES office.offices(office_id),
	inherit_in_child_offices		boolean NOT NULL DEFAULT(false),
	role_id					integer NULL REFERENCES office.roles(role_id),
	user_id					integer NULL REFERENCES office.users(user_id),
	scope					national character varying(12) NOT NULL
						CONSTRAINT menu_policy_scope_chk
						CHECK(scope IN('Allow','Deny'))
	
);

CREATE TABLE policy.menu_access
(
	access_id				BIGSERIAL NOT NULL PRIMARY KEY,
	office_id				integer NOT NULL REFERENCES office.offices(office_id),
	menu_id					integer NOT NULL REFERENCES core.menus(menu_id),
	user_id					integer NULL REFERENCES office.users(user_id)	
);


CREATE FUNCTION core.get_menu_id(menu_code text)
RETURNS INTEGER
AS
$$
BEGIN
	RETURN
	(
		SELECT core.menus.menu_id
		FROM core.menus
		WHERE core.menus.menu_code=$1
	);
END
$$
LANGUAGE plpgsql;



CREATE FUNCTION core.get_root_parent_menu_id(text)
RETURNS integer
AS
$$
	DECLARE retVal integer;
BEGIN
	WITH RECURSIVE find_parent(menu_id_group, parent, parent_menu_id, recentness) AS
	(
			SELECT menu_id, menu_id, parent_menu_id, 0
			FROM core.menus
			WHERE url=$1
			UNION ALL
			SELECT fp.menu_id_group, i.menu_id, i.parent_menu_id, fp.recentness + 1
			FROM core.menus i
			JOIN find_parent fp ON i.menu_id = fp.parent_menu_id
	)

		SELECT parent INTO retVal
		FROM find_parent q 
		JOIN
		(
				SELECT menu_id_group, MAX(recentness) AS answer
				FROM find_parent
				GROUP BY menu_id_group 
		) AS ans ON q.menu_id_group = ans.menu_id_group AND q.recentness = ans.answer 
		ORDER BY q.menu_id_group;

	RETURN retVal;
END
$$
LANGUAGE plpgsql;


INSERT INTO core.menus(menu_text, url, menu_code, level)
SELECT 'Dashboard', '~/Dashboard/Index.aspx', 'DB', 0 UNION ALL
SELECT 'Sales', '~/Sales/Index.aspx', 'SA', 0 UNION ALL
SELECT 'Purchase', '~/Purchase/Index.aspx', 'PU', 0 UNION ALL
SELECT 'Products & Items', '~/Inventory/Index.aspx', 'ITM', 0 UNION ALL
SELECT 'Finance', '~/Finance/Index.aspx', 'FI', 0 UNION ALL
SELECT 'Manufacturing', '~/Manufacturing/Index.aspx', 'MF', 0 UNION ALL
SELECT 'CRM', '~/CRM/Index.aspx', 'CRM', 0 UNION ALL
SELECT 'Setup Parameters', '~/Setup/Index.aspx', 'SE', 0 UNION ALL
SELECT 'POS', '~/POS/Index.aspx', 'POS', 0;


INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
		  SELECT 'Sales & Quotation', NULL, 'SAQ', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Direct Sales', '~/Sales/DirectSales.aspx', 'DRS', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Quotation', '~/Sales/Quotation.aspx', 'SQ', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Order', '~/Sales/Order.aspx', 'SO', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Delivery for Sales Order', '~/Sales/DeliveryForOrder.aspx', 'DSO', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Delivery Without Sales Order', '~/Sales/DeliveryWithoutOrder.aspx', 'DWO', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Invoice for Sales Delivery', '~/Sales/Invoice.aspx', 'ISD', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Receipt from Customer', '~/Sales/Receipt.aspx', 'RFC', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Return', '~/Sales/Return.aspx', 'SR', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'SSM', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Bonus Slab for Agents', '~/Sales/Setup/AgentBonusSlabs.aspx', 'ABS', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Bonus Slab Details', '~/Sales/Setup/AgentBonusSlabDetails.aspx', 'BSD', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Agents', '~/Sales/Setup/Agents.aspx', 'SSA', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Bonus Slab Assignment', '~/Sales/Setup/BonusSlabAssignment.aspx', 'BSA', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Reports', NULL, 'SAR', 1, core.get_menu_id('SA')
UNION ALL SELECT 'View Sales Inovice', '~/Reports/Sales.View.Sales.Invoice.xml', 'SAR-SVSI', 2, core.get_menu_id('SAR')
UNION ALL SELECT 'Cashier Management', NULL, 'CM', 1, core.get_menu_id('POS')
UNION ALL SELECT 'Assign Cashier', '~/POS/AssignCashier.aspx', 'ASC', 2, core.get_menu_id('CM')
UNION ALL SELECT 'POS Setup', NULL, 'POSS', 1, core.get_menu_id('POS')
UNION ALL SELECT 'Store Types', '~/POS/Setup/StoreTypes.aspx', 'STT', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Stores', '~/POS/Setup/Stores.aspx', 'STO', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Cash Repository Setup', '~/Setup/CashRepositories.aspx', 'SCR', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Counter Setup', '~/Setup/Counters.aspx', 'SCS', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Purchase & Quotation', NULL, 'PUQ', 1, core.get_menu_id('PU')
UNION ALL SELECT 'Direct Purchase', '~/Purchase/DirectPurchase.aspx', 'DRP', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Order', '~/Purchase/Order.aspx', 'PO', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'GRN against PO', '~/Purchase/GRN.aspx', 'GRN', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Invoice Against GRN', '~/Purchase/Invoice.aspx', 'PAY', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Payment to Supplier', '~/Purchase/Payment.aspx', 'PAS', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Return', '~/Purchase/Return.aspx', 'PR', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Reports', NULL, 'PUR', 1, core.get_menu_id('PU')
UNION ALL SELECT 'Inventory Movements', NULL, 'IIM', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Stock Transfer Journal', '~/Inventory/Transfer.aspx', 'STJ', 2, core.get_menu_id('IIM')
UNION ALL SELECT 'Stock Adjustments', '~/Inventory/Adjustment.aspx', 'STA', 2, core.get_menu_id('IIM')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'ISM', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Party Types', '~/Inventory/Setup/PartyTypes.aspx', 'PT', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Party Accounts', '~/Inventory/Setup/Parties.aspx', 'PA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipping Addresses', '~/Inventory/Setup/ShippingAddresses.aspx', 'PSA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Maintenance', '~/Inventory/Setup/Items.aspx', 'SSI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Cost Prices', '~/Inventory/Setup/CostPrices.aspx', 'ICP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Selling Prices', '~/Inventory/Setup/SellingPrices.aspx', 'ISP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Groups', '~/Inventory/Setup/ItemGroups.aspx', 'SSG', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Brands', '~/Inventory/Setup/Brands.aspx', 'SSB', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Units of Measure', '~/Inventory/Setup/UOM.aspx', 'UOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Units of Measure', '~/Inventory/Setup/CUOM.aspx', 'CUOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipper Information', '~/Inventory/Setup/Shipper.aspx', 'SHI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Transactions & Templates', NULL, 'FTT', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Journal Voucher Entry', '~/Finance/JournalVoucher.aspx', 'JVN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Template Transaction', '~/Finance/TemplateTransaction.aspx', 'TTR', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Standing Instructions', '~/Finance/StandingInstructions.aspx', 'STN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Update Exchange Rates', '~/Finance/UpdateExchangeRates.aspx', 'UER', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Reconcile Bank Account', '~/Finance/BankReconciliation.aspx', 'RBA', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Voucher Verification', '~/Finance/VoucherVerification.aspx', 'FVV', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Transaction Document Manager', '~/Finance/TransactionDocumentManager.aspx', 'FTDM', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'FSM', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Chart of Accounts', '~/Finance/Setup/COA.aspx', 'COA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Currency Management', '~/Finance/Setup/Currencies.aspx', 'CUR', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Bank Accounts', '~/Finance/Setup/BankAccounts.aspx', 'CBA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Product GL Mapping', '~/Finance/Setup/ProductGLMapping.aspx', 'PGM', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Budgets & Targets', '~/Finance/Setup/BudgetAndTarget.aspx', 'BT', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Ageing Slabs', '~/Finance/Setup/AgeingSlabs.aspx', 'AGS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Tax Types', '~/Finance/Setup/TaxTypes.aspx', 'TTY', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Tax Setup', '~/Finance/Setup/TaxSetup.aspx', 'TS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Cost Centers', '~/Finance/Setup/CostCenters.aspx', 'CC', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Manufacturing Workflow', NULL, 'MFW', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Sales Forecast', '~/Manufacturing/Workflow/SalesForecast.aspx', 'MFWSF', 2, core.get_menu_id('MFW')
UNION ALL SELECT 'Master Production Schedule', '~/Manufacturing/Workflow/MasterProductionSchedule.aspx', 'MFWMPS', 2, core.get_menu_id('MFW')
UNION ALL SELECT 'Manufacturing Setup', NULL, 'MFS', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Work Centers', '~/Manufacturing/Setup/WorkCenters.aspx', 'MFSWC', 2, core.get_menu_id('MFS')
UNION ALL SELECT 'Bills of Material', '~/Manufacturing/Setup/BillsOfMaterial.aspx', 'MFSBOM', 2, core.get_menu_id('MFS')
UNION ALL SELECT 'Manufacturing Reports', NULL, 'MFR', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Gross & Net Requirements', '~/Manufacturing/Reports/GrossAndNetRequirements.aspx', 'MFRGNR', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Capacity vs Lead', '~/Manufacturing/Reports/CapacityVersusLead.aspx', 'MFRCVSL', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Shop Floor Planning', '~/Manufacturing/Reports/ShopFloorPlanning.aspx', 'MFRSFP', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Production Order Status', '~/Manufacturing/Reports/ProductionOrderStatus.aspx', 'MFRPOS', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'CRM Main', NULL, 'CRMM', 1, core.get_menu_id('CRM')
UNION ALL SELECT 'Add a New Lead', '~/CRM/Lead.aspx', 'CRML', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Add a New Opportunity', '~/CRM/Opportunity.aspx', 'CRMO', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Convert Lead to Opportunity', '~/CRM/ConvertLeadToOpportunity.aspx', 'CRMC', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Lead Follow Up', '~/CRM/LeadFollowUp.aspx', 'CRMFL', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Opportunity Follow Up', '~/CRM/OpportunityFollowUp.aspx', 'CRMFO', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'CSM', 1, core.get_menu_id('CRM')
UNION ALL SELECT 'Lead Sources Setup', '~/CRM/Setup/LeadSources.aspx', 'CRMLS', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Lead Status Setup', '~/CRM/Setup/LeadStatuses.aspx', 'CRMLST', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Opportunity Stages Setup', '~/CRM/Setup/OpportunityStages.aspx', 'CRMOS', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Miscellaneous Parameters', NULL, 'SMP', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Flags', '~/Setup/Flags.aspx', 'TRF', 2, core.get_menu_id('SMP')
UNION ALL SELECT 'Audit Reports', NULL, 'SEAR', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Login View', '~/Reports/Office.Login.xml', 'SEAR-LV', 2, core.get_menu_id('SEAR')
UNION ALL SELECT 'Office Setup', NULL, 'SOS', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Office & Branch Setup', '~/Setup/Offices.aspx', 'SOB', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Department Setup', '~/Setup/Departments.aspx', 'SDS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Role Management', '~/Setup/Roles.aspx', 'SRM', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'User Management', '~/Setup/Users.aspx', 'SUM', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Fiscal Year Information', '~/Setup/FiscalYear.aspx', 'SFY', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Frequency & Fiscal Year Management', '~/Setup/Frequency.aspx', 'SFR', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Policy Management', NULL, 'SPM', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Voucher Verification Policy', '~/Setup/Policy/VoucherVerification.aspx', 'SVV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Automatic Verification Policy', '~/Setup/Policy/AutoVerification.aspx', 'SAV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Menu Access Policy', '~/Setup/Policy/MenuAccess.aspx', 'SMA', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'GL Access Policy', '~/Setup/Policy/GLAccess.aspx', 'SAP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Store Policy', '~/Setup/Policy/Store.aspx', 'SSP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Switches', '~/Setup/Policy/Switches.aspx', 'SWI', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Admin Tools', NULL, 'SAT', 1, core.get_menu_id('SE')
UNION ALL SELECT 'SQL Query Tool', '~/Setup/Admin/Query.aspx', 'SQL', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Database Statistics', '~/Setup/Admin/DatabaseStatistics.aspx', 'DBSTAT', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Backup Database', '~/Setup/Admin/Backup.aspx', 'BAK', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Restore Database', '~/Setup/Admin/Restore.aspx', 'RES', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Change User Password', '~/Setup/Admin/ChangePassword.aspx', 'PWD', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'New Company', '~/Setup/Admin/NewCompany.aspx', 'NEW', 2, core.get_menu_id('SAT');


INSERT INTO policy.menu_access(office_id, menu_id, user_id)
SELECT office.get_office_id_by_office_code('PES-NY-BK'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus

UNION ALL

SELECT office.get_office_id_by_office_code('PES-NY-MEM'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus;


CREATE VIEW office.sign_in_view
AS
SELECT
	users.user_id, 
	roles.role_code || ' (' || roles.role_name || ')' AS role, 
	roles.is_admin,
	roles.is_system,
	users.user_name, 
	users.full_name,
	office.get_login_id(office.users.user_id) AS login_id,
	office.get_logged_in_office_id(office.users.user_id) AS office_id,
	office.get_logged_in_culture(office.users.user_id) AS culture,
	logged_in_office.office_code || ' (' || logged_in_office.office_name || ')' AS office,
	logged_in_office.office_code,
	logged_in_office.office_name,
	logged_in_office.nick_name,
	logged_in_office.registration_date,
	logged_in_office.registration_number,
	logged_in_office.pan_number,
	logged_in_office.address_line_1,
	logged_in_office.address_line_2,
	logged_in_office.street,
	logged_in_office.city,
	logged_in_office.state,
	logged_in_office.country,
	logged_in_office.zip_code,
	logged_in_office.phone,
	logged_in_office.fax,
	logged_in_office.email,
	logged_in_office.url
FROM 
	office.users
INNER JOIN
	office.roles
ON
	users.role_id = roles.role_id 
INNER JOIN
	office.offices
ON
	users.office_id = offices.office_id
LEFT JOIN
	office.offices AS logged_in_office
ON
	logged_in_office.office_id = office.get_logged_in_office_id(office.users.user_id);


CREATE OR REPLACE VIEW office.role_view
AS
SELECT 
  roles.role_id, 
  roles.role_code, 
  roles.role_name
FROM 
  office.roles;
	
	
CREATE TABLE core.frequencies
(
	frequency_id 				SERIAL NOT NULL PRIMARY KEY,
	frequency_code 				national character varying(12) NOT NULL,
	frequency_name 				national character varying(50) NOT NULL
);


CREATE UNIQUE INDEX frequencies_frequency_code_uix
ON core.frequencies(UPPER(frequency_code));

CREATE UNIQUE INDEX frequencies_frequency_name_uix
ON core.frequencies(UPPER(frequency_name));

INSERT INTO core.frequencies
SELECT 2, 'EOM', 'End of Month' UNION ALL
SELECT 3, 'EOQ', 'End of Quarter' UNION ALL
SELECT 4, 'EOH', 'End of Half' UNION ALL
SELECT 5, 'EOY', 'End of Year';


CREATE TABLE core.fiscal_year
(
	fiscal_year_code 			national character varying(12) NOT NULL PRIMARY KEY,
	fiscal_year_name 			national character varying(50) NOT NULL,
	starts_from 				date NOT NULL,
	ends_on 				date NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX fiscal_year_fiscal_year_name_uix
ON core.fiscal_year(UPPER(fiscal_year_name));

CREATE UNIQUE INDEX fiscal_year_starts_from_uix
ON core.fiscal_year(starts_from);

CREATE UNIQUE INDEX fiscal_year_ends_on_uix
ON core.fiscal_year(ends_on);


CREATE TABLE core.frequency_setups
(
	frequency_setup_id			SERIAL NOT NULL PRIMARY KEY,
	fiscal_year_code 			national character varying(12) NOT NULL REFERENCES core.fiscal_year(fiscal_year_code),
	value_date 				date NOT NULL UNIQUE,
	frequency_id 				integer NOT NULL REFERENCES core.frequencies(frequency_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

--TODO: Validation constraints for core.frequency_setups

CREATE TABLE core.units
(
	unit_id 				SERIAL NOT NULL PRIMARY KEY,
	unit_code 				national character varying(12) NOT NULL,
	unit_name 				national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX units_unit_code_uix
ON core.units(UPPER(unit_code));

CREATE UNIQUE INDEX "units_unit_name_uix"
ON core.units(UPPER(unit_name));

INSERT INTO core.units(unit_code, unit_name)
SELECT 'PC', 'Piece' UNION ALL
SELECT 'FT', 'Feet' UNION ALL
SELECT 'MTR', 'Meter' UNION ALL
SELECT 'LTR', 'Liter' UNION ALL
SELECT 'GM', 'Gram' UNION ALL
SELECT 'KG', 'Kilogram' UNION ALL
SELECT 'DZ', 'Dozen' UNION ALL
SELECT 'BX', 'Box';

CREATE FUNCTION core.get_unit_id_by_unit_code(text)
RETURNS smallint
AS
$$
BEGIN
	RETURN
	(
		SELECT
			core.units.unit_id
		FROM
			core.units
		WHERE
			core.units.unit_code=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_unit_id_by_unit_name(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT
			core.units.unit_id
		FROM
			core.units
		WHERE
			core.units.unit_name=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE TABLE core.compound_units
(
	compound_unit_id 			SERIAL NOT NULL PRIMARY KEY,
	base_unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	value 					smallint NOT NULL,
	compare_unit_id 			integer NOT NULL REFERENCES core.units(unit_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
						CONSTRAINT compound_units_check CHECK(base_unit_id != compare_unit_id)
);

CREATE UNIQUE INDEX compound_units_info_uix
ON core.compound_units(base_unit_id, compare_unit_id);

INSERT INTO core.compound_units(base_unit_id, compare_unit_id, value)
SELECT core.get_unit_id_by_unit_code('PC'), core.get_unit_id_by_unit_code('DZ'), 12 UNION ALL
SELECT core.get_unit_id_by_unit_code('DZ'), core.get_unit_id_by_unit_code('BX'), 100 UNION ALL
SELECT core.get_unit_id_by_unit_code('GM'), core.get_unit_id_by_unit_code('KG'), 1000;

CREATE FUNCTION core.get_root_unit_id(integer)
RETURNS integer
AS
$$
	DECLARE root_unit_id integer;
BEGIN
	SELECT base_unit_id INTO root_unit_id
	FROM core.compound_units
	WHERE compare_unit_id=$1;

	IF(root_unit_id IS NULL) THEN
		RETURN $1;
	ELSE
		RETURN core.get_root_unit_id(root_unit_id);
	END IF;	
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.is_parent_unit(parent integer, child integer)
RETURNS boolean
AS
$$		
BEGIN
	IF $1!=$2 THEN
		IF EXISTS
		(
			WITH RECURSIVE unit_cte(unit_id) AS 
			(
			 SELECT tn.compare_unit_id
				FROM core.compound_units AS tn WHERE tn.base_unit_id = $1
			UNION ALL
			 SELECT
				c.compare_unit_id
				FROM unit_cte AS p, 
			  core.compound_units AS c 
				WHERE base_unit_id = p.unit_id
			)

			SELECT * FROM unit_cte
			WHERE unit_id=$2
		) THEN
			RETURN TRUE;
		END IF;
	END IF;
	RETURN false;
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.convert_unit(integer, integer)
RETURNS decimal
AS
$$
	DECLARE _factor decimal;
BEGIN
	IF(core.get_root_unit_id($1) != core.get_root_unit_id($2)) THEN
		RETURN 0;
	END IF;

	IF($1 = $2) THEN
		RETURN 1.00;
	END IF;
	
	IF(core.is_parent_unit($1, $2)) THEN
			WITH RECURSIVE unit_cte(unit_id, value) AS 
			(
				SELECT tn.compare_unit_id, tn.value
				FROM core.compound_units AS tn WHERE tn.base_unit_id = $1

				UNION ALL

				SELECT 
				c.compare_unit_id, c.value * p.value
				FROM unit_cte AS p, 
				core.compound_units AS c 
				WHERE base_unit_id = p.unit_id
			)
		SELECT 1.00/value INTO _factor
		FROM unit_cte
		WHERE unit_id=$2;
	ELSE
			WITH RECURSIVE unit_cte(unit_id, value) AS 
			(
			 SELECT tn.compare_unit_id, tn.value
				FROM core.compound_units AS tn WHERE tn.base_unit_id = $2
			UNION ALL
			 SELECT 
				c.compare_unit_id, c.value * p.value
				FROM unit_cte AS p, 
			  core.compound_units AS c 
				WHERE base_unit_id = p.unit_id
			)

		SELECT value INTO _factor
		FROM unit_cte
		WHERE unit_id=$1;
	END IF;

	RETURN _factor;
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION core.get_associated_units(integer)
RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
AS
$$
	DECLARE root_unit_id integer;
BEGIN
	CREATE TEMPORARY TABLE IF NOT EXISTS temp_unit(unit_id integer) ON COMMIT DROP;	
	
	SELECT core.get_root_unit_id($1) INTO root_unit_id;
	
	INSERT INTO temp_unit(unit_id) 
	SELECT root_unit_id
	WHERE NOT EXISTS
	(
		SELECT * FROM temp_unit
		WHERE temp_unit.unit_id=root_unit_id
	);
	
	WITH RECURSIVE cte(unit_id)
	AS
	(
		 SELECT 
			compare_unit_id
		 FROM 
			core.compound_units
		 WHERE 
			base_unit_id = root_unit_id

		UNION ALL

		 SELECT
			units.compare_unit_id
		 FROM 
			core.compound_units units
		 INNER JOIN cte 
		 ON cte.unit_id = units.base_unit_id
	)
	
	INSERT INTO temp_unit(unit_id)
	SELECT cte.unit_id FROM cte;
	
	DELETE FROM temp_unit
	WHERE temp_unit.unit_id IS NULL;
	
	RETURN QUERY 
	SELECT 
		core.units.unit_id,
		core.units.unit_code::text,
		core.units.unit_name::text
	FROM
		core.units
	WHERE
		core.units.unit_id 
	IN
	(
		SELECT temp_unit.unit_id FROM temp_unit
	);
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION core.get_associated_units_from_item_id(integer)
RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
AS
$$
DECLARE _unit_id integer;
BEGIN
	SELECT core.items.unit_id INTO _unit_id
	FROM core.items
	WHERE core.items.item_id=$1;

	RETURN QUERY
	SELECT ret.unit_id, ret.unit_code, ret.unit_name
	FROM core.get_associated_units(_unit_id) AS ret;

END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_associated_units_from_item_code(text)
RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
AS
$$
DECLARE _unit_id integer;
BEGIN
	SELECT core.items.unit_id INTO _unit_id
	FROM core.items
	WHERE core.items.item_code=$1;

	RETURN QUERY
	SELECT ret.unit_id, ret.unit_code, ret.unit_name
	FROM core.get_associated_units(_unit_id) AS ret;

END
$$
LANGUAGE plpgsql;


CREATE VIEW core.compound_unit_view
AS
SELECT
	compound_unit_id,
	base_unit.unit_name base_unit_name,
	value,
	compare_unit.unit_name compare_unit_name
FROM
	core.compound_units,
	core.units base_unit,
	core.units compare_unit
WHERE
	core.compound_units.base_unit_id = base_unit.unit_id
AND
	core.compound_units.compare_unit_id = compare_unit.unit_id;


--TODO
CREATE VIEW core.unit_view
AS
SELECT * FROM core.units;

CREATE FUNCTION core.get_base_quantity_by_unit_name(text, integer)
RETURNS decimal
AS
$$
DECLARE _unit_id integer;
DECLARE _root_unit_id integer;
DECLARE _factor decimal;
BEGIN
	_unit_id := core.get_unit_id_by_unit_name($1);
	_root_unit_id = core.get_root_unit_id(_unit_id);
	_factor = core.convert_unit(_unit_id, _root_unit_id);

	RETURN _factor * $2;
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_base_unit_id_by_unit_name(text)
RETURNS integer
AS
$$
DECLARE _unit_id integer;
BEGIN
	_unit_id := core.get_unit_id_by_unit_name($1);

	RETURN
	(
		core.get_root_unit_id(_unit_id)
	);
END
$$
LANGUAGE plpgsql;

CREATE TABLE core.account_masters
(
	account_master_id 			SERIAL NOT NULL PRIMARY KEY,
	account_master_code 			national character varying(3) NOT NULL,
	account_master_name 			national character varying(40) NOT NULL	
);

CREATE UNIQUE INDEX account_master_code_uix
ON core.account_masters(UPPER(account_master_code));

CREATE UNIQUE INDEX account_master_name_uix
ON core.account_masters(UPPER(account_master_name));



CREATE TABLE core.accounts
(
	account_id				SERIAL NOT NULL PRIMARY KEY,
	account_master_id 			integer NOT NULL REFERENCES core.account_masters(account_master_id),
	account_code      			national character varying(12) NOT NULL,
	external_code     			national character varying(12) NULL CONSTRAINT accounts_external_code_df DEFAULT(''),
	confidential      			boolean NOT NULL CONSTRAINT accounts_confidential_df DEFAULT(false),
	account_name      			national character varying(100) NOT NULL,
	description	  			national character varying(200) NULL,
	sys_type 	  			boolean NOT NULL CONSTRAINT accounts_sys_type_df DEFAULT(false),
	is_cash		  			boolean NOT NULL CONSTRAINT accounts_is_cash_df DEFAULT(false),
	parent_account_id 			integer NULL REFERENCES core.accounts(account_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX accountsCode_uix
ON core.accounts(UPPER(account_code));

CREATE UNIQUE INDEX accounts_Name_uix
ON core.accounts(UPPER(account_name));

CREATE FUNCTION core.has_child_accounts(integer)
RETURNS boolean
AS
$$
BEGIN
	IF EXISTS(SELECT 0 FROM core.accounts WHERE parent_account_id=$1 LIMIT 1) THEN
		RETURN true;
	END IF;

	RETURN false;
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION core.get_cash_account_id()
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT account_id
		FROM core.accounts
		WHERE is_cash=true
		LIMIT 1
	);
END
$$
LANGUAGE plpgsql;

CREATE VIEW core.account_view
AS
SELECT
	core.accounts.account_id,
	core.account_masters.account_master_code,
	core.accounts.account_code,
	core.accounts.external_code,
	core.accounts.account_name,
	core.accounts.confidential,
	core.accounts.description,
	core.accounts.sys_type,
	core.accounts.is_cash,
	parent_account.account_code || ' (' || parent_account.account_name || ')' AS parent,
	core.has_child_accounts(core.accounts.account_id) AS has_child
FROM core.accounts
INNER JOIN core.account_masters
ON core.account_masters.account_master_id=core.accounts.account_master_id
LEFT JOIN core.accounts parent_account
ON parent_account.account_id=core.accounts.parent_account_id;

INSERT INTO core.account_masters(account_master_code, account_master_name) SELECT 'BSA', 'Balance Sheet A/C';
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10000', 'Assets', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Balance Sheet A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10001', 'Current Assets', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10100', 'Cash at Bank A/C', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10110', 'Regular Checking Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10120', 'Payroll Checking Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10130', 'Savings Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10140', 'Special Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id, is_cash) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10200', 'Cash in Hand A/C', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets'), true;
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10300', 'Investments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10310', 'Short Term Investment', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Investments');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10320', 'Other Investments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Investments');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10321', 'Investments-Money Market', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Investments');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10322', 'Bank Deposit Contract (Fixed Deposit)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Investments');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10323', 'Investments-Certificates of Deposit', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Investments');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10400', 'Accounts Receivable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10500', 'Other Receivables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10600', 'Allowance for Doubtful Accounts', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10700', 'Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10720', 'Raw Materials Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10730', 'Supplies Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10740', 'Work in Progress Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10750', 'Finished Goods Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10800', 'Prepaid Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '10900', 'Employee Advances', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '11000', 'Notes Receivable-Current', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '11100', 'Prepaid Interest', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '11200', 'Accrued Incomes (Assets)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '11300', 'Other Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '11400', 'Other Current Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12001', 'Noncurrent Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12100', 'Furniture and Fixtures', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12200', 'Plants & Equipments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12300', 'Rental Property', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12400', 'Vehicles', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12500', 'Intangibles', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12600', 'Other Depreciable Properties', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12700', 'Leasehold Improvements', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12800', 'Buildings', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '12900', 'Building Improvements', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13000', 'Interior Decorations', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13100', 'Land', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13200', 'Long Term Investments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13300', 'Trade Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13400', 'Rental Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13500', 'Staff Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13600', 'Other Noncurrent Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13700', 'Other Financial Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13710', 'Deposits Held', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Financial Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13800', 'Accumulated Depreciations', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13810', 'Accumulated Depreciation-Furniture and Fixtures', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13820', 'Accumulated Depreciation-Equipment', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13830', 'Accumulated Depreciation-Vehicles', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13840', 'Accumulated Depreciation-Other Depreciable Properties', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13850', 'Accumulated Depreciation-Leasehold', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13860', 'Accumulated Depreciation-Buildings', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13870', 'Accumulated Depreciation-Building Improvements', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '13880', 'Accumulated Depreciation-Interior Decorations', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14001', 'Other Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14100', 'Other Assets-Deposits', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14200', 'Other Assets-Organization Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14300', 'Other Assets-Accumulated Amortization-Organization Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14400', 'Notes Receivable-Non-current', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14500', 'Other Non-current Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '14600', 'Nonfinancial Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20000', 'Liabilities', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Balance Sheet A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20001', 'Current Liabilities', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20100', 'Accounts Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20110', 'Shipping Charge Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20200', 'Accrued Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20300', 'Wages Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20400', 'Deductions Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20500', 'Health Insurance Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20600', 'Superannutation Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20700', 'Tax Payables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20710', 'Sales Tax Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20720', 'Federal Payroll Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20730', 'FUTA Tax Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20740', 'State Payroll Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20750', 'SUTA Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20760', 'Local Payroll Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20770', 'Income Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20780', 'Other Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20800', 'Employee Benefits Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20810', 'Provision for Annual Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20820', 'Provision for Long Service Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20830', 'Provision for Personal Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20840', 'Provision for Health Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '20900', 'Current Portion of Long-term Debt', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21000', 'Advance Incomes', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21010', 'Advance Sales Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Advance Incomes');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21020', 'Grant Received in Advance', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Advance Incomes');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21100', 'Deposits from Customers', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21200', 'Other Current Liabilities', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21210', 'Short Term Loan Payables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21220', 'Short Term Hirepurchase Payables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21230', 'Short Term Lease Liability', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '21240', 'Grants Repayable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24001', 'Noncurrent Liabilities', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24100', 'Notes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24200', 'Land Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24300', 'Equipment Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24400', 'Vehicles Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24500', 'Lease Liability', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24600', 'Loan Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24700', 'Hirepurchase Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24800', 'Bank Loans Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '24900', 'Deferred Revenue', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '25000', 'Other Long-term Liabilities', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '25010', 'Long Term Employee Benefit Provision', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Long-term Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28001', 'Equity', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Liabilities');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28100', 'Stated Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28110', 'Founder Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Stated Capital');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28120', 'Promoter Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Stated Capital');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28130', 'Member Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Stated Capital');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28200', 'Capital Surplus', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28210', 'Share Premium', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28220', 'Capital Redemption Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28230', 'Statutory Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28240', 'Asset Revaluation Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28250', 'Exchange Rate Fluctuation Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28260', 'Capital Reserves Arising From Merger', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28270', 'Capital Reserves Arising From Acuisition', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28300', 'Retained Surplus', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28310', 'Accumulated Profits', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Retained Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28320', 'Accumulated Losses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Retained Surplus');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28400', 'Treasury Stock', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28500', 'Current Year Surplus', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28600', 'General Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '28700', 'Other Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
INSERT INTO core.account_masters(account_master_code, account_master_name) SELECT 'PLA', 'Profit and Loss A/C';
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30000', 'Revenues', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Profit and Loss A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30100', 'Sales A/C', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30200', 'Interest Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30300', 'Other Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30400', 'Finance Charge Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30500', 'Shipping Charges Reimbursed', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30600', 'Sales Returns and Allowances', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '30700', 'Sales Discounts', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40000', 'Expenses', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Profit and Loss A/C');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40100', 'Purchase A/C', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40200', 'Cost of GoodS Sold', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40205', 'Product Cost', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40210', 'Raw Material Purchases', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40215', 'Direct Labor Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40220', 'Indirect Labor Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40225', 'Heat and Power', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40230', 'Commissions', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40235', 'Miscellaneous Factory Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40240', 'Cost of Goods Sold-Salaries and Wages', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40245', 'Cost of Goods Sold-Contract Labor', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40250', 'Cost of Goods Sold-Freight', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40255', 'Cost of Goods Sold-Other', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40260', 'Inventory Adjustments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40265', 'Purchase Returns and Allowances', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40270', 'Purchase Discounts', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of GoodS Sold');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40300', 'General Purchase Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40400', 'Advertising Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40500', 'Amortization Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40600', 'Auto Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40700', 'Bad Debt Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40800', 'Bank Fees', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '40900', 'Cash Over and Short', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41000', 'Charitable Contributions Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41100', 'Commissions and Fees Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41200', 'Depreciation Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41300', 'Dues and Subscriptions Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41400', 'Employee Benefit Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41410', 'Employee Benefit Expenses-Health Insurance', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41420', 'Employee Benefit Expenses-Pension Plans', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41430', 'Employee Benefit Expenses-Profit Sharing Plan', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41440', 'Employee Benefit Expenses-Other', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41500', 'Freight Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41600', 'Gifts Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41700', 'Income Tax Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41710', 'Income Tax Expenses-Federal', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Income Tax Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41720', 'Income Tax Expenses-State', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Income Tax Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41730', 'Income Tax Expenses-Local', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Income Tax Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41800', 'Insurance Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41810', 'Insurance Expenses-Product Liability', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Insurance Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41820', 'Insurance Expenses-Vehicle', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Insurance Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '41900', 'Interest Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42000', 'Laundry and Dry Cleaning Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42100', 'Legal and Professional Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42200', 'Licenses Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42300', 'Loss on NSF Checks', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42400', 'Maintenance Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42500', 'Meals and Entertainment Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42600', 'Office Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42700', 'Payroll Tax Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42800', 'Penalties and Fines Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '42900', 'Other Taxe Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43000', 'Postage Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43100', 'Rent or Lease Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43200', 'Repair and Maintenance Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43210', 'Repair and Maintenance Expenses-Office', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Repair and Maintenance Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43220', 'Repair and Maintenance Expenses-Vehicle', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Repair and Maintenance Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43300', 'Supplies Expenses-Office', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43400', 'Telephone Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43500', 'Training Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43600', 'Travel Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43700', 'Salary Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43800', 'Wages Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '43900', 'Utilities Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '44000', 'Other Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.accounts(account_master_id,account_code,account_name, sys_type, parent_account_id) SELECT (SELECT account_master_id FROM core.account_masters WHERE account_master_code='BSA'), '44100', 'Gain/Loss on Sale of Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
INSERT INTO core.account_masters(account_master_code, account_master_name) SELECT 'OBS', 'Off Balance Sheet A/C';

CREATE FUNCTION core.disable_editing_sys_type()
RETURNS TRIGGER
AS
$$
BEGIN
	IF TG_OP='UPDATE' OR TG_OP='DELETE' THEN
		IF EXISTS
		(
			SELECT *
			FROM core.accounts
			WHERE (sys_type=true OR is_cash=true)
			AND account_id=OLD.account_id
		) THEN
			RAISE EXCEPTION 'You are not allowed to change system accounts.';
		END IF;
	END IF;
	
	IF TG_OP='INSERT' THEN
		IF (NEW.sys_type=true OR NEW.is_cash=true) THEN
			RAISE EXCEPTION 'You are not allowed to add system accounts.';
		END IF;
	END IF;

	IF TG_OP='DELETE' THEN
		RETURN OLD;
	END IF;

	RETURN NEW;	
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER restrict_delete_sys_type_trigger
BEFORE DELETE
ON core.accounts
FOR EACH ROW EXECUTE PROCEDURE core.disable_editing_sys_type();

CREATE TRIGGER restrict_update_sys_type_trigger
BEFORE UPDATE
ON core.accounts
FOR EACH ROW EXECUTE PROCEDURE core.disable_editing_sys_type();

CREATE TRIGGER restrict_insert_sys_type_trigger
BEFORE INSERT
ON core.accounts
FOR EACH ROW EXECUTE PROCEDURE core.disable_editing_sys_type();

CREATE VIEW core.accounts_view
AS
SELECT
	core.accounts.account_id,
	core.accounts.account_code,
	core.accounts.account_name,
	core.accounts.description,
	core.accounts.sys_type,
	core.accounts.parent_account_id,
	parent_accounts.account_code AS parent_account_code,
	parent_accounts.account_name AS parent_account_name,
	core.account_masters.account_master_id,
	core.account_masters.account_master_code,
	core.account_masters.account_master_name
FROM
	core.account_masters
	INNER JOIN core.accounts 
	ON core.account_masters.account_master_id = core.accounts.account_master_id
	LEFT OUTER JOIN core.accounts AS parent_accounts 
	ON core.accounts.parent_account_id = parent_accounts.account_id;


CREATE FUNCTION core.get_account_id_by_account_code(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT account_id
		FROM core.accounts
		WHERE account_code=$1
	);
END
$$
LANGUAGE plpgsql;


CREATE TABLE core.account_parameters
(
	account_parameter_id 			SERIAL NOT NULL CONSTRAINT account_parameters_pk PRIMARY KEY,
	parameter_name 				national character varying(128) NOT NULL,
	account_id 				integer NOT NULL REFERENCES core.accounts(account_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX account_parameters_parameter_name_uix
ON core.account_parameters(UPPER(parameter_name));

INSERT INTO core.account_parameters(parameter_name, account_id)
SELECT 'Sales', core.get_account_id_by_account_code('30100') UNION ALL
SELECT 'Sales.Receivables', core.get_account_id_by_account_code('10400') UNION ALL
SELECT 'Sales.Discount', core.get_account_id_by_account_code('30700') UNION ALL
SELECT 'Sales.Tax', core.get_account_id_by_account_code('20700') UNION ALL
SELECT 'Purchase', core.get_account_id_by_account_code('40100') UNION ALL
SELECT 'Purchase.Payables', core.get_account_id_by_account_code('20100') UNION ALL
SELECT 'Purchase.Discount', core.get_account_id_by_account_code('40270') UNION ALL
SELECT 'Purchase.Tax', core.get_account_id_by_account_code('20700') UNION ALL
SELECT 'Inventory', core.get_account_id_by_account_code('10700') UNION ALL
SELECT 'COGS', core.get_account_id_by_account_code('40200') UNION ALL
SELECT 'Tax.Payable', core.get_account_id_by_account_code('20700');

CREATE FUNCTION core.get_account_id_by_parameter(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT
			account_id
		FROM	
			core.account_parameters
		WHERE
			parameter_name=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_account_name(integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT
			account_name
		FROM	
			core.accounts
		WHERE
			account_id=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE TABLE core.bank_accounts
(
	account_id 				integer NOT NULL CONSTRAINT bank_accounts_pk PRIMARY KEY
								CONSTRAINT bank_accounts_accounts_fk REFERENCES core.accounts(account_id),
	maintained_by_user_id 			integer NOT NULL CONSTRAINT bank_accounts_users_fk REFERENCES office.users(user_id),
	bank_name 				national character varying(128) NOT NULL,
	bank_branch 				national character varying(128) NOT NULL,
	bank_contact_number 			national character varying(128) NULL,
	bank_address 				text NULL,
	bank_account_code 			national character varying(128) NULL,
	bank_account_type 			national character varying(128) NULL,
	relationship_officer_name		national character varying(128) NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE VIEW core.bank_account_view
AS
SELECT
	core.accounts.account_id,
	core.accounts.account_code,
	core.accounts.account_name,
	office.users.user_name AS maintained_by,
	core.bank_accounts.bank_name,
	core.bank_accounts.bank_branch,
	core.bank_accounts.bank_contact_number,
	core.bank_accounts.bank_address,
	core.bank_accounts.bank_account_code,
	core.bank_accounts.bank_account_type,
	core.bank_accounts.relationship_officer_name AS relation_officer
FROM
	core.bank_accounts
INNER JOIN core.accounts ON core.accounts.account_id = core.bank_accounts.account_id
INNER JOIN office.users ON core.bank_accounts.maintained_by_user_id = office.users.user_id;

CREATE TABLE core.agents
(
	agent_id				SERIAL NOT NULL PRIMARY KEY,
	agent_code				national character varying(12) NOT NULL,
	agent_name 				national character varying(100) NOT NULL,
	address 				national character varying(100) NOT NULL,
	contact_number 				national character varying(50) NOT NULL,
	commission_rate 			decimal_strict2 NOT NULL DEFAULT(0),
	account_id 				integer NOT NULL REFERENCES core.accounts(account_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX agents_agent_name_uix
ON core.agents(UPPER(agent_name));

INSERT INTO core.agents(agent_code, agent_name, address, contact_number, commission_rate, account_id)
SELECT 'OFF', 'Office', 'Office', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100');

CREATE VIEW core.agent_view
AS
SELECT
	agent_id,
	agent_code,
	agent_name,
	address,
	contact_number,
	commission_rate,
	account_name
FROM
	core.agents,
	core.accounts
WHERE
	core.agents.account_id = core.accounts.account_id;

CREATE TABLE core.bonus_slabs
(
	bonus_slab_id 				SERIAL NOT NULL PRIMARY KEY,
	bonus_slab_code 			national character varying(12) NOT NULL,
	bonus_slab_name 			national character varying(50) NOT NULL,
	checking_frequency_id 			integer NOT NULL REFERENCES core.frequencies(frequency_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX bonus_slabs_bonus_slab_code_uix
ON core.bonus_slabs(UPPER(bonus_slab_code));


CREATE UNIQUE INDEX bonus_slabs_bonus_slab_name_uix
ON core.bonus_slabs(UPPER(bonus_slab_name));


CREATE VIEW core.bonus_slab_view
AS
SELECT
	bonus_slab_id,
	bonus_slab_code,
	bonus_slab_name,
	checking_frequency_id,
	frequency_name
FROM
core.bonus_slabs, core.frequencies
WHERE
core.bonus_slabs.checking_frequency_id = core.frequencies.frequency_id;

CREATE TABLE core.bonus_slab_details
(
	bonus_slab_detail_id 			SERIAL NOT NULL PRIMARY KEY,
	bonus_slab_id 				integer NOT NULL REFERENCES core.bonus_slabs(bonus_slab_id),
	amount_from 				money_strict NOT NULL,
	amount_to 				money_strict NOT NULL,
	bonus_rate 				decimal_strict NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
						CONSTRAINT bonus_slab_details_amounts_chk CHECK(amount_to>amount_from)
);


CREATE VIEW core.bonus_slab_detail_view
AS
SELECT
	bonus_slab_detail_id,
	core.bonus_slab_details.bonus_slab_id,
	core.bonus_slabs.bonus_slab_name AS slab_name,
	amount_from,
	amount_to,
	bonus_rate
FROM
	core.bonus_slab_details,
	core.bonus_slabs
WHERE
	core.bonus_slab_details.bonus_slab_id = core.bonus_slabs.bonus_slab_id;

CREATE TABLE core.agent_bonus_setups
(
	agent_bonus_setup_id SERIAL NOT NULL PRIMARY KEY,
	agent_id integer NOT NULL REFERENCES core.agents(agent_id),
	bonus_slab_id integer NOT NULL REFERENCES core.bonus_slabs(bonus_slab_id)
);

CREATE UNIQUE INDEX agent_bonus_setups_uix
ON core.agent_bonus_setups(agent_id, bonus_slab_id);


CREATE VIEW core.agent_bonus_setup_view
AS
SELECT
	agent_bonus_setup_id,
	agent_name,
	bonus_slab_name
FROM
	core.agent_bonus_setups,
	core.agents,
	core.bonus_slabs
WHERE
	core.agent_bonus_setups.agent_id = core.agents.agent_id
AND
	core.agent_bonus_setups.bonus_slab_id = core.bonus_slabs.bonus_slab_id;

CREATE TABLE core.ageing_slabs
(
	ageing_slab_id SERIAL NOT NULL PRIMARY KEY,
	ageing_slab_name national character varying(24) NOT NULL,
	from_days integer NOT NULL,
	to_days integer NOT NULL CHECK(to_days > 0)
);

CREATE UNIQUE INDEX ageing_slabs_ageing_slab_name_uix
ON core.ageing_slabs(UPPER(ageing_slab_name));

INSERT INTO core.ageing_slabs(ageing_slab_name,from_days,to_days)
SELECT 'SLAB 1',0, 30 UNION ALL
SELECT 'SLAB 2',31, 60 UNION ALL
SELECT 'SLAB 3',61, 90 UNION ALL
SELECT 'SLAB 4',91, 365 UNION ALL
SELECT 'SLAB 5',366, 999999;


CREATE TABLE core.party_types
(
	party_type_id 				SERIAL NOT NULL PRIMARY KEY,
	party_type_code 			national character varying(12) NOT NULL, 
	party_type_name 			national character varying(50) NOT NULL,
	is_supplier 				boolean NOT NULL CONSTRAINT party_types_is_supplier_df DEFAULT(false),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

INSERT INTO core.party_types(party_type_code, party_type_name) SELECT 'A', 'Agent';
INSERT INTO core.party_types(party_type_code, party_type_name) SELECT 'C', 'Customer';
INSERT INTO core.party_types(party_type_code, party_type_name) SELECT 'D', 'Dealer';
INSERT INTO core.party_types(party_type_code, party_type_name, is_supplier) SELECT 'S', 'Supplier', true;

CREATE TABLE core.parties
(
	party_id BIGSERIAL			NOT NULL PRIMARY KEY,
	party_type_id				smallint NOT NULL REFERENCES core.party_types(party_type_id),
	party_code				national character varying(12) NULL,
	first_name				national character varying(50) NOT NULL,
	middle_name				national character varying(50) NULL,
	last_name				national character varying(50) NOT NULL,
	party_name				text NULL,
	date_of_birth				date NULL,
	address_line_1				national character varying(128) NULL,	
	address_line_2				national character varying(128) NULL,
	street 					national character varying(50) NULL,
	city 					national character varying(50) NULL,
	state 					national character varying(50) NULL,
	country 				national character varying(50) NULL,
	phone 					national character varying(24) NULL,
	fax 					national character varying(24) NULL,
	cell 					national character varying(24) NULL,
	email 					national character varying(128) NULL,
	url 					national character varying(50) NULL,
	pan_number 				national character varying(50) NULL,
	sst_number 				national character varying(50) NULL,
	cst_number 				national character varying(50) NULL,
	allow_credit 				boolean NULL,
	maximum_credit_period 			smallint NULL,
	maximum_credit_amount 			money_strict2 NULL,
	charge_interest 			boolean NULL,
	interest_rate 				decimal NULL,
	interest_compounding_frequency_id	smallint NULL REFERENCES core.frequencies(frequency_id),
	account_id 				integer NOT NULL REFERENCES core.accounts(account_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX parties_party_code_uix
ON core.parties(UPPER(party_code));

/*******************************************************************
	GET UNIQUE EIGHT-TO-TEN DIGIT CUSTOMER CODE
	TO IDENTIFY A PARTY.
	BASIC FORMULA:
		1. FIRST TWO LETTERS OF FIRST NAME
		2. FIRST LETTER OF MIDDLE NAME (IF AVAILABLE)
		3. FIRST TWO LETTERS OF LAST NAME
		4. CUSTOMER NUMBER
*******************************************************************/

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
						to_char(CAST(right(_party_code,4) AS integer)+1,'FM0000')
					END;
	RETURN _party_code;
END;
$$
LANGUAGE 'plpgsql';


CREATE FUNCTION core.update_party_code()
RETURNS trigger
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


CREATE FUNCTION core.get_party_type_id_by_party_code(text)
RETURNS smallint
AS
$$
BEGIN
	RETURN
	(
		SELECT
			party_type_id
		FROM
			core.parties
		WHERE 
			core.parties.party_code=$1
	);
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION core.get_party_id_by_party_code(text)
RETURNS smallint
AS
$$
BEGIN
	RETURN
	(
		SELECT
			party_id
		FROM
			core.parties
		WHERE 
			core.parties.party_code=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE VIEW core.party_view
AS
SELECT
	core.parties.party_id,
	core.party_types.party_type_id,
	core.party_types.is_supplier,
	core.party_types.party_type_code || ' (' || core.party_types.party_type_name || ')' AS party_type,
	core.parties.party_code,
	core.parties.first_name,
	core.parties.middle_name,
	core.parties.last_name,
	core.parties.party_name,
	core.parties.address_line_1,
	core.parties.address_line_2,
	core.parties.street,
	core.parties.city,
	core.parties.state,
	core.parties.country,
	core.parties.allow_credit,
	core.parties.maximum_credit_period,
	core.parties.maximum_credit_amount,
	core.parties.charge_interest,
	core.parties.interest_rate,
	core.parties.pan_number,
	core.parties.sst_number,
	core.parties.cst_number,
	core.parties.phone,
	core.parties.fax,
	core.parties.cell,
	core.parties.email,
	core.parties.url
FROM
core.parties
INNER JOIN
core.party_types
ON core.parties.party_type_id = core.party_types.party_type_id;

CREATE FUNCTION core.is_supplier(int)
RETURNS boolean
AS
$$
BEGIN
	IF EXISTS
	(
		SELECT 1 FROM core.parties 
		INNER JOIN core.party_types 
		ON core.parties.party_type_id=core.party_types.party_type_id
		WHERE core.parties.party_id=$1
		AND core.party_types.is_supplier=true
	) THEN
		RETURN true;
	END IF;
	
	RETURN false;
END
$$
LANGUAGE plpgsql;

CREATE VIEW core.supplier_view
AS
SELECT * FROM core.party_view
WHERE is_supplier=true;


CREATE TABLE core.shipping_addresses
(
	shipping_address_id			BIGSERIAL NOT NULL PRIMARY KEY,
	shipping_address_code			national character varying(24) NOT NULL,
	party_id				bigint NOT NULL REFERENCES core.parties(party_id),
	address_line_1				national character varying(128) NULL,	
	address_line_2				national character varying(128) NULL,
	street					national character varying(128) NULL,
	city					national character varying(128) NOT NULL,
	state					national character varying(128) NOT NULL,
	country					national character varying(128) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX shipping_addresses_shipping_address_code_uix
ON core.shipping_addresses(UPPER(shipping_address_code), party_id);

CREATE FUNCTION core.get_shipping_address_id_by_shipping_address_code(text)
RETURNS smallint
AS
$$
BEGIN
	RETURN
	(
		SELECT
			shipping_address_id
		FROM
			core.shipping_addresses
		WHERE 
			core.shipping_addresses.shipping_address_code=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.update_shipping_address_code_trigger()
RETURNS TRIGGER
AS
$$
DECLARE _counter integer;
BEGIN
	IF TG_OP='INSERT' THEN

		SELECT COALESCE(MAX(shipping_address_code::integer), 0) + 1
		INTO _counter
		FROM core.shipping_addresses
		WHERE party_id=NEW.party_id;

		NEW.shipping_address_code := trim(to_char(_counter, '000'));
		
		RETURN NEW;
	END IF;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER update_shipping_address_code_trigger
BEFORE INSERT
ON core.shipping_addresses
FOR EACH ROW EXECUTE PROCEDURE core.update_shipping_address_code_trigger();

CREATE VIEW core.shipping_address_view
AS
SELECT
	core.shipping_addresses.shipping_address_id,
	core.shipping_addresses.shipping_address_code,
	core.shipping_addresses.party_id,
	core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
	core.shipping_addresses.address_line_1,
	core.shipping_addresses.address_line_2,
	core.shipping_addresses.street,
	core.shipping_addresses.city,
	core.shipping_addresses.state,
	core.shipping_addresses.country
FROM core.shipping_addresses
INNER JOIN core.parties
ON core.shipping_addresses.party_id=core.parties.party_id;

CREATE TABLE core.brands
(
	brand_id SERIAL NOT NULL PRIMARY KEY,
	brand_code national character varying(12) NOT NULL,
	brand_name national character varying(150) NOT NULL
);

CREATE UNIQUE INDEX brands_brand_code_uix
ON core.brands(UPPER(brand_code));

CREATE UNIQUE INDEX brands_brand_name_uix
ON core.brands(UPPER(brand_name));

INSERT INTO core.brands(brand_code, brand_name)
SELECT 'DEF', 'Default';


CREATE TABLE core.shippers
(
	shipper_id				BIGSERIAL NOT NULL PRIMARY KEY,
	shipper_code				national character varying(12) NULL,
	company_name				national character varying(128) NOT NULL,
	shipper_name				national character varying(150) NULL,
	address_line_1				national character varying(128) NULL,	
	address_line_2				national character varying(128) NULL,
	street					national character varying(50) NULL,
	city					national character varying(50) NULL,
	state 					national character varying(50) NULL,
	country 				national character varying(50) NULL,
	phone 					national character varying(50) NULL,
	fax 					national character varying(50) NULL,
	cell 					national character varying(50) NULL,
	email 					national character varying(128) NULL,
	url 					national character varying(50) NULL,
	contact_person 				national character varying(50) NULL,
	contact_address_line_1			national character varying(128) NULL,	
	contact_address_line_2			national character varying(128) NULL,
	contact_street 				national character varying(50) NULL,
	contact_city 				national character varying(50) NULL,
	contact_state 				national character varying(50) NULL,
	contact_country 			national character varying(50) NULL,
	contact_email 				national character varying(128) NULL,
	contact_phone 				national character varying(50) NULL,
	contact_cell 				national character varying(50) NULL,
	factory_address 			national character varying(250) NULL,
	pan_number 				national character varying(50) NULL,
	sst_number 				national character varying(50) NULL,
	cst_number 				national character varying(50) NULL,
	account_id 				integer NOT NULL REFERENCES core.accounts(account_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX shippers_shipper_code_uix
ON core.shippers(UPPER(shipper_code));


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

CREATE FUNCTION core.update_shipper_code()
RETURNS trigger
AS
$$
BEGIN
	UPDATE core.shippers
	SET 
		shipper_code=core.get_shipper_code(NEW.company_name)
	WHERE core.shippers.shipper_id=NEW.shipper_id;
	
	RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER update_shipper_code
AFTER INSERT
ON core.shippers
FOR EACH ROW EXECUTE PROCEDURE core.update_shipper_code();


CREATE FUNCTION core.get_account_id_by_shipper_id(integer)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT
			core.shippers.account_id
		FROM
			core.shippers
		WHERE
			core.shippers.shipper_id=$1
	);
END
$$
LANGUAGE plpgsql;


CREATE TABLE core.tax_types
(
	tax_type_id 				SERIAL  NOT NULL PRIMARY KEY,
	tax_type_code 				national character varying(12) NOT NULL,
	tax_type_name 				national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX tax_types_tax_type_code_uix
ON core.tax_types(UPPER(tax_type_code));

CREATE UNIQUE INDEX tax_types_tax_type_name_uix
ON core.tax_types(UPPER(tax_type_name));

INSERT INTO core.tax_types(tax_type_code, tax_type_name)
SELECT 'DEF', 'Default';

CREATE TABLE core.taxes
(
	tax_id SERIAL  				NOT NULL PRIMARY KEY,
	tax_type_id 				smallint NOT NULL REFERENCES core.tax_types(tax_type_id),
	tax_code 				national character varying(12) NOT NULL,
	tax_name 				national character varying(50) NOT NULL,
	rate 					decimal NOT NULL,
	account_id 				integer NOT NULL REFERENCES core.accounts(account_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);



CREATE UNIQUE INDEX taxes_tax_code_uix
ON core.taxes(UPPER(tax_code));

CREATE UNIQUE INDEX taxes_tax_name_uix
ON core.taxes(UPPER(tax_name));

INSERT INTO core.taxes(tax_type_id, tax_code, tax_name, rate, account_id)
SELECT 1, 'VAT', 'Value Added Tax', 13, (SELECT account_id FROM core.accounts WHERE account_name='Sales Tax Payable') UNION ALL
SELECT 1, 'SAT', 'Sales Tax', 5, (SELECT account_id FROM core.accounts WHERE account_name='Sales Tax Payable');

CREATE VIEW core.tax_view
AS
SELECT
	tax_id,
	tax_code,
	tax_name,
	rate,
	tax_type_code,
	tax_type_name,
	account_code,
	account_name
FROM
	core.taxes,
	core.accounts,
	core.tax_types
WHERE
	core.taxes.account_id = core.accounts.account_id
AND
	core.taxes.tax_type_id = core.tax_types.tax_type_id;

CREATE TABLE core.item_groups
(
	item_group_id 				SERIAL NOT NULL PRIMARY KEY,
	item_group_code 			national character varying(12) NOT NULL,
	item_group_name 			national character varying(50) NOT NULL,
	exclude_from_purchase 			boolean NOT NULL CONSTRAINT item_groups_exclude_from_purchase_df DEFAULT('No'),
	exclude_from_sales 			boolean NOT NULL CONSTRAINT item_groups_exclude_from_sales_df DEFAULT('No'),
	tax_id 					smallint NOT NULL REFERENCES core.taxes(tax_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX item_groups_item_group_code_uix
ON core.item_groups(UPPER(item_group_code));

CREATE UNIQUE INDEX item_groups_item_group_name_uix
ON core.item_groups(UPPER(item_group_name));

INSERT INTO core.item_groups(item_group_code, item_group_name, tax_id)
SELECT 'DEF', 'Default', 1;


CREATE TABLE core.items
(
	item_id 				SERIAL NOT NULL PRIMARY KEY,
	item_code 				national character varying(12) NOT NULL,
	item_name 				national character varying(150) NOT NULL,
	item_group_id 				integer NOT NULL REFERENCES core.item_groups(item_group_id),
	brand_id 				integer NOT NULL REFERENCES core.brands(brand_id),
	preferred_supplier_id 			integer NOT NULL REFERENCES core.parties(party_id) 
						CONSTRAINT items_preferred_supplier_id_chk CHECK(core.is_supplier(preferred_supplier_id) = true),
	lead_time_in_days 			integer NOT NULL DEFAULT(0),
	unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	hot_item 				boolean NOT NULL,
	cost_price 				money_strict NOT NULL,
	cost_price_includes_tax 		boolean NOT NULL CONSTRAINT items_cost_price_includes_tax_df DEFAULT('No'),
	selling_price 				money_strict NOT NULL,
	selling_price_includes_tax 		boolean NOT NULL CONSTRAINT items_selling_price_includes_tax_df DEFAULT('No'),
	tax_id 					integer NOT NULL REFERENCES core.taxes(tax_id),
	reorder_level 				integer NOT NULL,
	item_image 				image_path NULL,
	maintain_stock 				boolean NOT NULL DEFAULT(true),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX items_item_name_uix
ON core.items(UPPER(item_name));

CREATE FUNCTION core.get_item_id_by_item_code(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT
			item_id
		FROM
			core.items
		WHERE 
			core.items.item_code=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.get_item_tax_rate(integer)
RETURNS decimal
AS
$$
BEGIN
	RETURN
	COALESCE((
		SELECT core.taxes.rate
		FROM core.taxes
		INNER JOIN core.items
		ON core.taxes.tax_id = core.items.tax_id
		WHERE core.items.item_id=$1
	), 0);
END
$$
LANGUAGE plpgsql;

--TODO
CREATE VIEW core.item_view
AS
SELECT * FROM core.items;


/*******************************************************************
	PLEASE NOTE :

	THESE ARE THE MOST EFFECTIVE STOCK ITEM PRICES.
	THE PRICE IN THIS CATALOG IS ACTUALLY
	PICKED UP AT THE TIME OF PURCHASE AND SALES.

	A STOCK ITEM PRICE MAY BE DIFFERENT FOR DIFFERENT units.
	FURTHER, A STOCK ITEM WOULD BE SOLD AT A HIGHER PRICE
	WHEN SOLD LOOSE THAN WHAT IT WOULD ACTUALLY COST IN A
	COMPOUND UNIT.

	EXAMPLE, ONE CARTOON (20 BOTTLES) OF BEER BOUGHT AS A UNIT
	WOULD COST 25% LESS FROM THE SAME STORE.

*******************************************************************/

CREATE TABLE core.item_selling_prices
(	
	item_selling_price_id			BIGSERIAL NOT NULL PRIMARY KEY,
	item_id 				integer NOT NULL REFERENCES core.items(item_id),
	unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	party_type_id 				smallint NULL REFERENCES core.party_types(party_type_id), 
	price_type_id 				smallint NULL REFERENCES core.price_types(price_type_id),
	includes_tax 				boolean NOT NULL CONSTRAINT item_selling_prices_includes_tax_df DEFAULT('No'),
	price 					money_strict NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE VIEW core.item_selling_price_view
AS
SELECT
	core.item_selling_prices.item_selling_price_id,
	core.items.item_code,
	core.items.item_name,
	core.party_types.party_type_code,
	core.party_types.party_type_name,
	price
FROM
	core.item_selling_prices
INNER JOIN 	core.items
ON 
	core.item_selling_prices.item_id = core.items.item_id
LEFT JOIN
	core.price_types
ON
	core.item_selling_prices.price_type_id = core.price_types.price_type_id
LEFT JOIN
	core.party_types
ON	core.item_selling_prices.party_type_id = core.party_types.party_type_id;



CREATE TABLE core.item_cost_prices
(	
	item_cost_price_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	item_id 				integer NOT NULL REFERENCES core.items(item_id),
	entry_ts 				TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(now()),
	unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	party_id 				bigint NULL REFERENCES core.parties(party_id),
	lead_time_in_days 			integer NOT NULL DEFAULT(0),
	includes_tax 				boolean NOT NULL CONSTRAINT item_cost_prices_includes_tax_df DEFAULT('No'),
	price 					money_strict NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE VIEW core.item_cost_price_view
AS
SELECT
	core.item_cost_prices.item_cost_price_id,
	core.items.item_code,
	core.items.item_name,
	core.parties.party_code,
	core.parties.party_name,
	core.item_cost_prices.price
FROM 
core.item_cost_prices
INNER JOIN
core.items
ON core.item_cost_prices.item_id = core.items.item_id
LEFT JOIN
core.parties
ON core.item_cost_prices.party_id = core.parties.party_id;

CREATE FUNCTION core.get_item_cost_price(item_id_ integer, unit_id_ integer, party_id_ bigint)
RETURNS money
AS
$$
	DECLARE _price money;
	DECLARE _unit_id integer;
	DECLARE _factor decimal;
	DECLARE _tax_rate decimal;
	DECLARE _includes_tax boolean;
	DECLARE _tax money;
BEGIN
	--Fist pick the catalog price which matches all these fields:
	--Item, Unit, and Supplier.
	--This is the most effective price.
	SELECT 
		item_cost_prices.price, 
		item_cost_prices.unit_id,
		item_cost_prices.includes_tax
	INTO 
		_price, 
		_unit_id,
		_includes_tax		
	FROM core.item_cost_prices
	WHERE item_cost_prices.item_id = $1
	AND item_cost_prices.unit_id = $2
	AND item_cost_prices.party_id =$3;

	IF(_unit_id IS NULL) THEN
		--We do not have a cost price of this item for the unit supplied.
		--Let's see if this item has a price for other units.
		SELECT 
			item_cost_prices.price, 
			item_cost_prices.unit_id,
			item_cost_prices.includes_tax
		INTO 
			_price, 
			_unit_id,
			_includes_tax
		FROM core.item_cost_prices
		WHERE item_cost_prices.item_id=$1
		AND item_cost_prices.party_id =$3;
	END IF;

	
	IF(_price IS NULL) THEN
		--This item does not have cost price defined in the catalog.
		--Therefore, getting the default cost price from the item definition.
		SELECT 
			cost_price, 
			unit_id,
			cost_price_includes_tax
		INTO 
			_price, 
			_unit_id,
			_includes_tax
		FROM core.items
		WHERE core.items.item_id = $1;
	END IF;

	IF(_includes_tax) THEN
		_tax_rate := core.get_item_tax_rate($1);
		_price := _price / ((100 + _tax_rate)/ 100);
	END IF;

	--Get the unitary conversion factor if the requested unit does not match with the price defition.
	_factor := core.convert_unit($2, _unit_id);

	RETURN _price * _factor;
END
$$
LANGUAGE plpgsql;


CREATE TABLE office.store_types
(
	store_type_id 				SERIAL NOT NULL PRIMARY KEY,
	store_type_code 			national character varying(12) NOT NULL,
	store_type_name 			national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX store_types_Code_uix
ON office.store_types(UPPER(store_type_code));


CREATE UNIQUE INDEX store_types_Name_uix
ON office.store_types(UPPER(store_type_name));

INSERT INTO office.store_types(store_type_code,store_type_name)
SELECT 'GOD', 'Godown' UNION ALL
SELECT 'SAL', 'Sales Center' UNION ALL
SELECT 'WAR', 'Warehouse' UNION ALL
SELECT 'PRO', 'Production';


CREATE TABLE office.stores
(
	store_id SERIAL 			NOT NULL PRIMARY KEY,
	office_id 				integer NOT NULL REFERENCES office.offices(office_id),
	store_code 				national character varying(12) NOT NULL,
	store_name 				national character varying(50) NOT NULL,
	address 				national character varying(50) NULL,
	store_type_id 				integer NOT NULL REFERENCES office.store_types(store_type_id),
	allow_sales 				boolean NOT NULL DEFAULT(true),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX stores_store_code_uix
ON office.stores(UPPER(store_code));

CREATE UNIQUE INDEX stores_store_name_uix
ON office.stores(UPPER(store_name));


--TODO
CREATE VIEW office.store_view
AS
SELECT * FROM office.stores;


CREATE TABLE office.cash_repositories
(
	cash_repository_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	office_id 				integer NOT NULL REFERENCES office.offices(office_id),
	cash_repository_code 			national character varying(12) NOT NULL,
	cash_repository_name 			national character varying(50) NOT NULL,
	parent_cash_repository_id 		integer NULL REFERENCES office.cash_repositories(cash_repository_id),
	description 				national character varying(100) NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX cash_repositories_cash_repository_code_uix
ON office.cash_repositories(UPPER(cash_repository_code));

CREATE UNIQUE INDEX cash_repositories_cash_repository_name_uix
ON office.cash_repositories(UPPER(cash_repository_name));

CREATE FUNCTION office.get_cash_repository_id_by_cash_repository_code(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT cash_repository_id
		FROM office.cash_repositories
		WHERE cash_repository_code=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION office.get_cash_repository_id_by_cash_repository_name(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT cash_repository_id
		FROM office.cash_repositories
		WHERE cash_repository_name=$1
	);
END
$$
LANGUAGE plpgsql;



CREATE VIEW office.cash_repository_view
AS
SELECT
	office.cash_repositories.cash_repository_id,
	office.cash_repositories.cash_repository_code,
	office.cash_repositories.cash_repository_name,
	parent_cash_repositories.cash_repository_code parent_cr_code,
	parent_cash_repositories.cash_repository_name parent_cr_name,
	office.cash_repositories.description
FROM
	office.cash_repositories
LEFT OUTER JOIN
	office.cash_repositories AS parent_cash_repositories
ON
	office.cash_repositories.parent_cash_repository_id=parent_cash_repositories.cash_repository_id;
 
CREATE TABLE office.counters
(
	counter_id 				SERIAL NOT NULL PRIMARY KEY,
	store_id 				smallint NOT NULL REFERENCES office.stores(store_id),
	cash_repository_id 			integer NOT NULL REFERENCES office.cash_repositories(cash_repository_id),
	counter_code 				national character varying(12) NOT NULL,
	counter_name 				national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX counters_counter_code_uix
ON office.counters(UPPER(counter_code));

CREATE UNIQUE INDEX counters_counter_name_uix
ON office.counters(UPPER(counter_name));


CREATE TABLE office.cost_centers
(
	cost_center_id 				SERIAL NOT NULL PRIMARY KEY,
	cost_center_code 			national character varying(24) NOT NULL,
	cost_center_name 			national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX cost_centers_cost_center_code_uix
ON office.cost_centers(UPPER(cost_center_code));

CREATE UNIQUE INDEX cost_centers_cost_center_name_uix
ON office.cost_centers(UPPER(cost_center_name));

INSERT INTO office.cost_centers(cost_center_code, cost_center_name)
SELECT 'DEF', 'Default' UNION ALL
SELECT 'GEN', 'General Administration' UNION ALL
SELECT 'HUM', 'Human Resources' UNION ALL
SELECT 'SCC', 'Support & Customer Care' UNION ALL
SELECT 'GAE', 'Guest Accomodation & Entertainment' UNION ALL
SELECT 'MKT', 'Marketing & Promotion' UNION ALL
SELECT 'SAL', 'Sales & Billing' UNION ALL
SELECT 'FIN', 'Finance & Accounting';

CREATE VIEW office.cost_center_view
AS
SELECT
	office.cost_centers.cost_center_id,
	office.cost_centers.cost_center_code,
	office.cost_centers.cost_center_name
FROM
	office.cost_centers;


CREATE TABLE office.cashiers
(
	cashier_id BIGSERIAL NOT NULL PRIMARY KEY,
	counter_id integer NOT NULL REFERENCES office.counters(counter_id),
	user_id integer NOT NULL REFERENCES office.users(user_id),
	assigned_by_user_id integer NOT NULL REFERENCES office.users(user_id),
	transaction_date date NOT NULL,
	closed boolean NOT NULL
);

CREATE UNIQUE INDEX Cashiers_user_id_TDate_uix
ON office.cashiers(user_id ASC, transaction_date DESC);


/*******************************************************************
	STORE policy DEFINES THE RIGHT OF USERS TO ACCESS A STORE.
	AN ADMINISTRATOR CAN ACCESS ALL THE stores, BY DEFAULT.
*******************************************************************/


CREATE TABLE policy.store_policies
(
	store_policy_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	written_by_user_id 			integer NOT NULL REFERENCES office.users(user_id),
	status 					boolean NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE TABLE policy.store_policy_details
(
	store_policy_detail_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	store_policy_id 			integer NOT NULL REFERENCES policy.store_policies(store_policy_id),
	user_id 				integer NOT NULL REFERENCES office.users(user_id),
	store_id 				smallint NOT NULL REFERENCES office.stores(store_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE TABLE core.item_opening_inventory
(
	item_opening_inventory_id 		BIGSERIAL NOT NULL PRIMARY KEY,
	entry_ts 				TIMESTAMP WITH TIME ZONE NOT NULL,
	item_id 				integer NOT NULL REFERENCES core.items(item_id),
	store_id 				smallint NOT NULL REFERENCES office.stores(store_id),
	unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	quantity 				integer NOT NULL,
	amount 					money_strict NOT NULL,
	base_unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	base_quantity 				decimal NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE TABLE audit.history
(
	activity_id				BIGSERIAL NOT NULL PRIMARY KEY,
	event_ts 				TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	principal_user 				national character varying(50) NOT NULL DEFAULT(current_user),
	user_id 				integer /*NOT*/ NULL REFERENCES office.users(user_id),
	type 					national character varying(50) NOT NULL,
	table_schema 				national character varying(50) NOT NULL,
	table_name 				national character varying(50) NOT NULL,
	primary_key_id 				national character varying(50) NOT NULL,
	column_name 				national character varying(50) NOT NULL,
	old_val 				text NULL,
	new_val 				text NULL,
						CONSTRAINT audit_history_val_chk 
							CHECK
							(
									(old_val IS NULL AND new_val IS NOT NULL) OR
									(old_val IS NOT NULL AND new_val IS NULL) OR
									(old_val IS NOT NULL AND new_val IS NOT NULL)
							)
);


CREATE FUNCTION office.is_sys_user(integer)
RETURNS boolean
AS
$$
BEGIN
	IF EXISTS
	(
		SELECT * FROM office.users
		WHERE user_id=$1
		AND role_id IN
		(
			SELECT office.roles.role_id FROM office.roles WHERE office.roles.role_code='SYST'
		)
	) THEN
		RETURN true;
	END IF;

	RETURN false;
END
$$
LANGUAGE plpgsql;


/*******************************************************************
	THIS FUNCTION RETURNS A NEW INCREMENTAL COUNTER SUBJECT 
	TO BE USED TO GENERATE TRANSACTION CODES
*******************************************************************/

CREATE FUNCTION transactions.get_new_transaction_counter(date)
RETURNS integer
AS
$$
	DECLARE _ret_val integer;
BEGIN
	SELECT INTO _ret_val
		COALESCE(MAX(transaction_counter),0)
	FROM transactions.transaction_master
	WHERE value_date=$1;

	IF _ret_val IS NULL THEN
		RETURN 1::integer;
	ELSE
		RETURN (_ret_val + 1)::integer;
	END IF;
END;
$$
LANGUAGE plpgsql;

CREATE FUNCTION transactions.get_transaction_code(value_date date, office_id integer, user_id integer, login_id bigint)
RETURNS text
AS
$$
	DECLARE _office_id bigint:=$2;
	DECLARE _user_id integer:=$3;
	DECLARE _login_id bigint:=$4;
	DECLARE _ret_val text;	
BEGIN
	_ret_val:= transactions.get_new_transaction_counter($1)::text || '-' || TO_CHAR($1, 'YYYY-MM-DD') || '-' || CAST(_office_id as text) || '-' || CAST(_user_id as text) || '-' || CAST(_login_id as text)   || '-' ||  TO_CHAR(now(), 'HH24-MI-SS');
	RETURN _ret_val;
END
$$
LANGUAGE plpgsql;


CREATE TABLE transactions.transaction_master
(
	transaction_master_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	transaction_counter 			integer NOT NULL, --Sequence of transactions of a date
	transaction_code 			national character varying(50) NOT NULL,
	book 					national character varying(50) NOT NULL, --Transaction book. Ex. Sales, Purchase, Journal
	value_date 				date NOT NULL,
	transaction_ts 				TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(now()),
	login_id 				bigint NOT NULL REFERENCES audit.logins(login_id),
	user_id 				integer NOT NULL REFERENCES office.users(user_id),
	sys_user_id 				integer NULL REFERENCES office.users(user_id)
						CONSTRAINT transaction_master_sys_user_id_chk CHECK(sys_user_id IS NULL OR office.is_sys_user(sys_user_id)=true),
	office_id 				integer NOT NULL REFERENCES office.offices(office_id),
	cost_center_id 				integer NULL REFERENCES office.cost_centers(cost_center_id),
	reference_number 			national character varying(24) NULL,
	statement_reference			text NULL,
	last_verified_on 			TIMESTAMP WITH TIME ZONE NULL, 
	verified_by_user_id 			integer NULL REFERENCES office.users(user_id),
	verification_status_id 			smallint NOT NULL REFERENCES core.verification_statuses(verification_status_id) DEFAULT(0/*Awaiting verification*/),
	verification_reason 			national character varying(128) NOT NULL CONSTRAINT transaction_master_verification_reason_df DEFAULT(''),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
						CONSTRAINT transaction_master_login_id_sys_user_id_chk
							CHECK
							(
								(
									login_id IS NULL AND sys_user_id IS NOT NULL
								)

								OR

								(
									login_id IS NOT NULL AND sys_user_id IS NULL
								)
							)
);

CREATE UNIQUE INDEX transaction_master_transaction_code_uix
ON transactions.transaction_master(UPPER(transaction_code));



CREATE TABLE transactions.transaction_details
(
	transaction_detail_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	transaction_master_id 			bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
	tran_type 				transaction_type NOT NULL,
	account_id 				integer NOT NULL REFERENCES core.accounts(account_id),
	statement_reference 			text NULL,
	cash_repository_id 			integer NULL REFERENCES office.cash_repositories(cash_repository_id),
	amount 					money_strict NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE TABLE transactions.stock_master
(
	stock_master_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	transaction_master_id 			bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
	party_id 				bigint NULL REFERENCES core.parties(party_id),
	agent_id 				integer NULL REFERENCES core.agents(agent_id),
	price_type_id 				integer NULL REFERENCES core.price_types(price_type_id),
	is_credit 				boolean NOT NULL CONSTRAINT stock_master_is_credit_df DEFAULT(false),
	shipper_id 				integer NULL REFERENCES core.shippers(shipper_id),
	shipping_address_id 			integer NULL REFERENCES core.shipping_addresses(shipping_address_id),
	shipping_charge 			money NOT NULL CONSTRAINT stock_master_shipping_charge_df DEFAULT(0),
	store_id 				integer NULL REFERENCES office.stores(store_id),
	cash_repository_id 			integer NULL REFERENCES office.cash_repositories(cash_repository_id),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE TABLE transactions.stock_details
(
	stock_master_detail_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	stock_master_id 			bigint NOT NULL REFERENCES transactions.stock_master(stock_master_id),
	tran_type 				transaction_type NOT NULL,
	store_id 				integer NULL REFERENCES office.stores(store_id),
	item_id 				integer NOT NULL REFERENCES core.items(item_id),
	quantity 				integer NOT NULL,
	unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	base_quantity 				decimal NOT NULL,
	base_unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	price 					money_strict NOT NULL,
	discount money 				NOT NULL CONSTRAINT stock_details_discount_df DEFAULT(0),
	tax_rate 				decimal NOT NULL CONSTRAINT stock_details_tax_rate_df DEFAULT(0),
	tax money 				NOT NULL CONSTRAINT stock_details_tax_df DEFAULT(0),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE MATERIALIZED VIEW transactions.trial_balance_view
AS
SELECT core.get_account_name(account_id), 
	SUM(CASE transactions.transaction_details.tran_type WHEN 'Dr' THEN amount ELSE NULL END) AS debit,
	SUM(CASE transactions.transaction_details.tran_type WHEN 'Cr' THEN amount ELSE NULL END) AS Credit
FROM transactions.transaction_details
GROUP BY account_id;


--TODO
CREATE TABLE transactions.non_gl_stock_master
(
	non_gl_stock_master_id			BIGSERIAL NOT NULL PRIMARY KEY,
	value_date 				date NOT NULL,
	book					national character varying(48) NOT NULL,
	party_id 				bigint NULL REFERENCES core.parties(party_id),
	price_type_id 				integer NULL REFERENCES core.price_types(price_type_id),
	transaction_ts 				TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(now()),
	login_id 				bigint NOT NULL REFERENCES audit.logins(login_id),
	user_id 				integer NOT NULL REFERENCES office.users(user_id),
	office_id 				integer NOT NULL REFERENCES office.offices(office_id),
	reference_number			national character varying(24) NULL,
	statement_reference 			text NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE TABLE transactions.non_gl_stock_details
(
	non_gl_stock_detail_id 			BIGSERIAL NOT NULL PRIMARY KEY,
	non_gl_stock_master_id 			bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id),
	item_id 				integer NOT NULL REFERENCES core.items(item_id),
	quantity 				integer NOT NULL,
	unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	base_quantity 				decimal NOT NULL,
	base_unit_id 				integer NOT NULL REFERENCES core.units(unit_id),
	price 					money_strict NOT NULL,
	discount 				money NOT NULL CONSTRAINT non_gl_stock_details_discount_df DEFAULT(0),
	tax_rate 				decimal NOT NULL CONSTRAINT non_gl_stock_details_tax_rate_df DEFAULT(0),
	tax 					money NOT NULL CONSTRAINT non_gl_stock_details_tax_df DEFAULT(0),
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


--This table stores information of quotations
--which were upgraded to order(s).
CREATE TABLE transactions.non_gl_stock_master_relations
(
	non_gl_stock_master_relation_id		BIGSERIAL NOT NULL PRIMARY KEY,	
	order_non_gl_stock_master_id		bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id),
	quotation_non_gl_stock_master_id	bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id)
);


--This table stores information of Non GL Stock Transactions such as orders and quotations
--which were upgraded to deliveries or invoices.
CREATE TABLE transactions.stock_master_non_gl_relations
(
	stock_master_non_gl_relation_id		BIGSERIAL NOT NULL PRIMARY KEY,	
	stock_master_id				bigint NOT NULL REFERENCES transactions.stock_master(stock_master_id),
	non_gl_stock_master_id			bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id)
);

CREATE FUNCTION transactions.are_sales_quotations_already_merged(VARIADIC arr bigint[])
RETURNS boolean
AS
$$
BEGIN
	IF
	(
		SELECT 
		COUNT(*) 
		FROM transactions.non_gl_stock_master_relations 
		WHERE quotation_non_gl_stock_master_id = any($1)
	) > 0 THEN
		RETURN true;
	END IF;

	IF
	(
		SELECT 
		COUNT(*) 
		FROM transactions.stock_master_non_gl_relations
		WHERE non_gl_stock_master_id = any($1)
	) > 0 THEN
		RETURN true;
	END IF;

	RETURN false;
END
$$
LANGUAGE plpgsql;	

CREATE FUNCTION transactions.are_sales_orders_already_merged(VARIADIC arr bigint[])
RETURNS boolean
AS
$$
BEGIN
	IF
	(
		SELECT 
		COUNT(*) 
		FROM transactions.stock_master_non_gl_relations
		WHERE non_gl_stock_master_id = any($1)
	) > 0 THEN
		RETURN true;
	END IF;

	RETURN false;
END
$$
LANGUAGE plpgsql;	


CREATE TABLE crm.lead_sources
(
	lead_source_id				SERIAL NOT NULL PRIMARY KEY,
	lead_source_code 			national character varying(12) NOT NULL,
	lead_source_name 			national character varying(128) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX lead_sources_lead_source_code_uix
ON crm.lead_sources(UPPER(lead_source_code));


CREATE UNIQUE INDEX lead_sources_lead_source_name_uix
ON crm.lead_sources(UPPER(lead_source_name));

INSERT INTO crm.lead_sources(lead_source_code, lead_source_name)
SELECT 'AG', 'Agent' UNION ALL
SELECT 'CC', 'Cold Call' UNION ALL
SELECT 'CR', 'Customer Reference' UNION ALL
SELECT 'DI', 'Direct Inquiry' UNION ALL
SELECT 'EV', 'Events' UNION ALL
SELECT 'PR', 'Partner';

CREATE TABLE crm.lead_statuses
(
	lead_status_id				SERIAL NOT NULL PRIMARY KEY,
	lead_status_code 			national character varying(12) NOT NULL,
	lead_status_name 			national character varying(128) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX lead_statuses_lead_status_code_uix
ON crm.lead_statuses(UPPER(lead_status_code));


CREATE UNIQUE INDEX lead_statuses_lead_status_name_uix
ON crm.lead_statuses(UPPER(lead_status_name));

INSERT INTO crm.lead_statuses(lead_status_code, lead_status_name)
SELECT 'CL', 'Cool' UNION ALL
SELECT 'CF', 'Contact in Future' UNION ALL
SELECT 'LO', 'Lost' UNION ALL
SELECT 'IP', 'In Prgress' UNION ALL
SELECT 'QF', 'Qualified';

CREATE TABLE crm.opportunity_stages
(
	opportunity_stage_id 			SERIAL  NOT NULL PRIMARY KEY,
	opportunity_stage_code 			national character varying(12) NOT NULL,
	opportunity_stage_name 			national character varying(50) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);


CREATE UNIQUE INDEX opportunity_stages_opportunity_stage_code_uix
ON crm.opportunity_stages(UPPER(opportunity_stage_code));

CREATE UNIQUE INDEX opportunity_stages_opportunity_stage_name_uix
ON crm.opportunity_stages(UPPER(opportunity_stage_name));


INSERT INTO crm.opportunity_stages(opportunity_stage_code, opportunity_stage_name)
SELECT 'PRO', 'Prospecting' UNION ALL
SELECT 'QUA', 'Qualification' UNION ALL
SELECT 'NEG', 'Negotiating' UNION ALL
SELECT 'VER', 'Verbal' UNION ALL
SELECT 'CLW', 'Closed Won' UNION ALL
SELECT 'CLL', 'Closed Lost';

CREATE FUNCTION transactions.get_invoice_amount(transaction_master_id_ bigint)
RETURNS money
AS
$$
DECLARE _shipping_charge money;
DECLARE _stock_total money;
BEGIN
	SELECT SUM((quantity * price) + tax - discount) INTO _stock_total
	FROM transactions.stock_details
	WHERE transactions.stock_details.stock_master_id =
	(
		SELECT transactions.stock_master.stock_master_id
		FROM transactions.stock_master WHERE transactions.stock_master.transaction_master_id= $1
	);

	SELECT shipping_charge INTO _shipping_charge
	FROM transactions.stock_master
	WHERE transactions.stock_master.transaction_master_id=$1;

	RETURN COALESCE(_stock_total + _shipping_charge, 0::money);	
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION core.count_item_in_stock(item_id_ integer, unit_id_ integer, store_id_ integer)
RETURNS decimal
AS
$$
	DECLARE _base_unit_id integer;
	DECLARE _debit decimal;
	DECLARE _credit decimal;
	DECLARE _balance decimal;
	DECLARE _factor decimal;
BEGIN

	--Get the base item unit
	SELECT 
		core.get_root_unit_id(core.items.unit_id) 
	INTO _base_unit_id
	FROM core.items
	WHERE core.items.item_id=$1;

	--Get the sum of debit stock quantity from approved transactions
	SELECT 
		COALESCE(SUM(base_quantity), 0)
	INTO _debit
	FROM transactions.stock_details
	INNER JOIN transactions.stock_master
	ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
	INNER JOIN transactions.transaction_master
	ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
	WHERE transactions.transaction_master.verification_status_id > 0
	AND transactions.stock_details.item_id=$1
	AND transactions.stock_details.store_id=$3
	AND transactions.stock_details.tran_type='Dr';
	
	--Get the sum of credit stock quantity from approved transactions
	SELECT 
		COALESCE(SUM(base_quantity), 0)
	INTO _credit
	FROM transactions.stock_details
	INNER JOIN transactions.stock_master
	ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
	INNER JOIN transactions.transaction_master
	ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
	WHERE transactions.transaction_master.verification_status_id > 0
	AND transactions.stock_details.item_id=$1
	AND transactions.stock_details.store_id=$3
	AND transactions.stock_details.tran_type='Cr';
	
	_balance:= _debit - _credit;

	
	_factor = core.convert_unit($2, _base_unit_id);

	return _balance / _factor;	
END
$$
LANGUAGE plpgsql;

CREATE TABLE core.switch_categories
(
	switch_category_id 			SERIAL NOT NULL PRIMARY KEY,
	switch_category_name			national character varying(128) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX switch_categories_switch_category_name_uix
ON core.switch_categories(UPPER(switch_category_name));

INSERT INTO core.switch_categories(switch_category_name)
SELECT 'General';

CREATE FUNCTION core.get_switch_category_id_by_name(text)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT switch_category_id
		FROM core.switch_categories
		WHERE core.switch_categories.switch_category_name=$1
	);
END
$$
LANGUAGE plpgsql;

CREATE TABLE office.work_centers
(
	work_center_id				SERIAL NOT NULL PRIMARY KEY,
	office_id				integer NOT NULL REFERENCES office.offices(office_id),
	work_center_code			national character varying(12) NOT NULL,
	work_center_name			national character varying(128) NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE UNIQUE INDEX work_centers_work_center_code_uix
ON office.work_centers(UPPER(work_center_code));

CREATE UNIQUE INDEX work_centers_work_center_name_uix
ON office.work_centers(UPPER(work_center_name));

CREATE VIEW office.work_center_view
AS
SELECT
	office.work_centers.work_center_id,
	office.offices.office_code || ' (' || office.offices.office_name || ')' AS office,
	office.work_centers.work_center_code,
	office.work_centers.work_center_name
FROM office.work_centers
INNER JOIN office.offices
ON office.work_centers.office_id = office.offices.office_id;



CREATE FUNCTION office.is_admin(integer)
RETURNS boolean
AS
$$
BEGIN
	RETURN
	(
		SELECT office.roles.is_admin FROM office.users
		INNER JOIN office.roles
		ON office.users.role_id = office.roles.role_id
		WHERE office.users.user_id=$1
	);
END
$$
LANGUAGE PLPGSQL;


CREATE FUNCTION office.is_sys(integer)
RETURNS boolean
AS
$$
BEGIN
	RETURN
	(
		SELECT office.roles.is_system FROM office.users
		INNER JOIN office.roles
		ON office.users.role_id = office.roles.role_id
		WHERE office.users.user_id=$1
	);
END
$$
LANGUAGE PLPGSQL;



CREATE TABLE policy.voucher_verification_policy
(
	user_id					integer NOT NULL PRIMARY KEY REFERENCES office.users(user_id),
	can_verify_sales_transactions		boolean NOT NULL CONSTRAINT voucher_verification_policy_verify_sales_df DEFAULT(false),
	sales_verification_limit		money NOT NULL CONSTRAINT voucher_verification_policy_sales_verification_limit_df DEFAULT(0),
	can_verify_purchase_transactions	boolean NOT NULL CONSTRAINT voucher_verification_policy_verify_purchase_df DEFAULT(false),
	purchase_verification_limit		money NOT NULL CONSTRAINT voucher_verification_policy_purchase_verification_limit_df DEFAULT(0),
	can_verify_gl_transactions		boolean NOT NULL CONSTRAINT voucher_verification_policy_verify_gl_df DEFAULT(false),
	gl_verification_limit			money NOT NULL CONSTRAINT voucher_verification_policy_gl_verification_limit_df DEFAULT(0),
	can_self_verify				boolean NOT NULL CONSTRAINT voucher_verification_policy_verify_self_df DEFAULT(false),
	self_verification_limit			money NOT NULL CONSTRAINT voucher_verification_policy_self_verification_limit_df DEFAULT(0),
	effective_from				date NOT NULL,
	ends_on					date NOT NULL,
	is_active				boolean NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE VIEW policy.voucher_verification_policy_view
AS
SELECT
	policy.voucher_verification_policy.user_id,
	office.users.user_name,
	policy.voucher_verification_policy.can_verify_sales_transactions,
	policy.voucher_verification_policy.sales_verification_limit,
	policy.voucher_verification_policy.can_verify_purchase_transactions,
	policy.voucher_verification_policy.purchase_verification_limit,
	policy.voucher_verification_policy.can_verify_gl_transactions,
	policy.voucher_verification_policy.gl_verification_limit,
	policy.voucher_verification_policy.can_self_verify,
	policy.voucher_verification_policy.self_verification_limit,
	policy.voucher_verification_policy.effective_from,
	policy.voucher_verification_policy.ends_on,
	policy.voucher_verification_policy.is_active
FROM policy.voucher_verification_policy
INNER JOIN office.users
ON policy.voucher_verification_policy.user_id=office.users.user_id;

CREATE TABLE policy.auto_verification_policy
(
	user_id					integer NOT NULL PRIMARY KEY REFERENCES office.users(user_id),
	verify_sales_transactions		boolean NOT NULL CONSTRAINT auto_verification_policy_verify_sales_df DEFAULT(false),
	sales_verification_limit		money NOT NULL CONSTRAINT auto_verification_policy_sales_verification_limit_df DEFAULT(0),
	verify_purchase_transactions		boolean NOT NULL CONSTRAINT auto_verification_policy_verify_purchase_df DEFAULT(false),
	purchase_verification_limit		money NOT NULL CONSTRAINT auto_verification_policy_purchase_verification_limit_df DEFAULT(0),
	verify_gl_transactions			boolean NOT NULL CONSTRAINT auto_verification_policy_verify_gl_df DEFAULT(false),
	gl_verification_limit			money NOT NULL CONSTRAINT auto_verification_policy_gl_verification_limit_df DEFAULT(0),
	effective_from				date NOT NULL,
	ends_on					date NOT NULL,
	is_active				boolean NOT NULL,
	audit_user_id				integer NULL REFERENCES office.users(user_id),
	audit_ts				TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW())
);

CREATE VIEW policy.auto_verification_policy_view
AS
SELECT
	policy.auto_verification_policy.user_id,
	office.users.user_name,
	policy.auto_verification_policy.verify_sales_transactions,
	policy.auto_verification_policy.sales_verification_limit,
	policy.auto_verification_policy.verify_purchase_transactions,
	policy.auto_verification_policy.purchase_verification_limit,
	policy.auto_verification_policy.verify_gl_transactions,
	policy.auto_verification_policy.gl_verification_limit,
	policy.auto_verification_policy.effective_from,
	policy.auto_verification_policy.ends_on,
	policy.auto_verification_policy.is_active
FROM policy.auto_verification_policy
INNER JOIN office.users
ON policy.auto_verification_policy.user_id=office.users.user_id;

DROP FUNCTION IF EXISTS core.is_leap_year(integer);
CREATE FUNCTION core.is_leap_year(integer)
RETURNS boolean
AS
$$
BEGIN
	RETURN (SELECT date_part('day', (($1::text || '-02-01')::date + '1 month'::interval - '1 day'::interval)) = 29);
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS core.get_current_year();
CREATE FUNCTION core.get_current_year()
RETURNS integer
AS
$$
BEGIN
	RETURN(SELECT EXTRACT(year FROM current_date)::integer);
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS core.is_leap_year();
CREATE FUNCTION core.is_leap_year()
RETURNS boolean
AS
$$
BEGIN
	RETURN core.is_leap_year(core.get_current_year());
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS core.calculate_interest(principal numeric, rate numeric, days integer, num_of_days_in_year integer, round_up integer);
CREATE FUNCTION core.calculate_interest(principal numeric, rate numeric, days integer, round_up integer, num_of_days_in_year integer)
RETURNS numeric
AS
$$
	DECLARE interest numeric;
BEGIN
	IF num_of_days_in_year = 0 OR num_of_days_in_year IS NULL THEN
		RAISE EXCEPTION 'Cannot calculate interest. The number of days in a year was not provided.';
	END IF;
	
	interest := ROUND(principal * rate * days / (num_of_days_in_year * 100), round_up);

	RETURN interest;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;


DROP FUNCTION IF EXISTS core.calculate_interest(principal numeric, rate numeric, days integer, round_up integer);
CREATE FUNCTION core.calculate_interest(principal numeric, rate numeric, days integer, round_up integer)
RETURNS numeric
AS
$$
	DECLARE num_of_days_in_year integer = 365;
BEGIN
	IF core.is_leap_year() THEN
		num_of_days_in_year = 366;
	END IF;
	
	RETURN core.calculate_interest(principal, rate, days, round_up, num_of_days_in_year);
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS office.can_login(user_id integer_strict, office_id integer_strict);
CREATE FUNCTION office.can_login(user_id integer_strict, office_id integer_strict)
RETURNS boolean
AS
$$
DECLARE _office_id integer;
BEGIN
	_office_id:=office.get_office_id_by_user_id($1);

	IF $1 = office.get_sys_user_id() THEN
		RETURN false;
	END IF;

	IF $2=_office_id THEN
		RETURN true;
	ELSE
		IF office.is_parent_office(_office_id,$2) THEN
			RETURN true;
		END IF;
	END IF;
	RETURN false;
END;
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS office.sign_in(office_id integer_strict, user_name text, password text, browser text, ip_address text, remote_user text, culture text);
CREATE FUNCTION office.sign_in(office_id integer_strict, user_name text, password text, browser text, ip_address text, remote_user text, culture text)
RETURNS integer
AS
$$
	DECLARE _user_id integer;
	DECLARE _lock_out_till TIMESTAMP;
BEGIN
	_user_id:=office.get_user_id_by_user_name($2);

	IF _user_id IS NULL THEN
		INSERT INTO audit.failed_logins(user_name,browser,ip_address,remote_user,details)
		SELECT $2, $4, $5, $6, 'Invalid user name.';
	ELSE
		_lock_out_till:=policy.is_locked_out_till(_user_id);
		IF NOT ((_lock_out_till IS NOT NULL) AND (_lock_out_till>NOW())) THEN
			IF office.validate_login($2,$3) THEN
				IF office.can_login(_user_id,$1) THEN
					INSERT INTO audit.logins(office_id,user_id,browser,ip_address,remote_user, culture)
					SELECT $1, _user_id, $4, $5, $6, $7;

					RETURN CAST(currval('audit.logins_login_id_seq') AS integer);
				ELSE
					INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
					SELECT $1, _user_id, $2, $4, $5, $6, 'User from ' || office.get_office_name_by_id(office.get_office_id_by_user_id(_user_id)) || ' cannot login to ' || office.get_office_name_by_id($1) || '.';
				END IF;
			ELSE
				INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
				SELECT $1, _user_id, $2, $4, $5, $6, 'Invalid login attempt.';
			END IF;
		END IF;
	END IF;

	RETURN 0;
END
$$
LANGUAGE plpgsql;




DROP FUNCTION IF EXISTS core.get_item_cost_price(item_id_ integer, party_id_ integer, unit_id_ integer);
CREATE FUNCTION core.get_item_cost_price(item_id_ integer, party_id_ integer, unit_id_ integer)
RETURNS money
AS
$$
	DECLARE _price money;
	DECLARE _unit_id integer;
	DECLARE _factor decimal;
	DECLARE _tax_rate decimal;
	DECLARE _includes_tax boolean;
	DECLARE _tax money;
BEGIN

	--Fist pick the catalog price which matches all these fields:
	--Item, Customer Type, Price Type, and Unit.
	--This is the most effective price.
	SELECT 
		item_cost_prices.price, 
		item_cost_prices.unit_id,
		item_cost_prices.includes_tax
	INTO 
		_price, 
		_unit_id,
		_includes_tax		
	FROM core.item_cost_prices
	WHERE item_cost_prices.item_id=$1
	AND item_cost_prices.party_id=$2
	AND item_cost_prices.unit_id = $3;

	IF(_unit_id IS NULL) THEN
		--We do not have a cost price of this item for the unit supplied.
		--Let's see if this item has a price for other units.
		SELECT 
			item_cost_prices.price, 
			item_cost_prices.unit_id,
			item_cost_prices.includes_tax
		INTO 
			_price, 
			_unit_id,
			_includes_tax
		FROM core.item_cost_prices
		WHERE item_cost_prices.item_id=$1
		AND item_cost_prices.party_id=$2;
	END IF;

	
	IF(_price IS NULL) THEN
		--This item does not have cost price defined in the catalog.
		--Therefore, getting the default cost price from the item definition.
		SELECT 
			cost_price, 
			unit_id,
			cost_price_includes_tax
		INTO 
			_price, 
			_unit_id,
			_includes_tax
		FROM core.items
		WHERE core.items.item_id = $1;
	END IF;

	IF(_includes_tax) THEN
		_tax_rate := core.get_item_tax_rate($1);
		_price := _price / ((100 + _tax_rate)/ 100);
	END IF;

	--Get the unitary conversion factor if the requested unit does not match with the price defition.
	_factor := core.convert_unit($3, _unit_id);

	RETURN _price * _factor;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS core.get_item_selling_price(item_id_ integer, party_type_id_ integer, price_type_id_ integer, unit_id_ integer);
CREATE FUNCTION core.get_item_selling_price(item_id_ integer, party_type_id_ integer, price_type_id_ integer, unit_id_ integer)
RETURNS money
AS
$$
	DECLARE _price money;
	DECLARE _unit_id integer;
	DECLARE _factor decimal;
	DECLARE _tax_rate decimal;
	DECLARE _includes_tax boolean;
	DECLARE _tax money;
BEGIN

	--Fist pick the catalog price which matches all these fields:
	--Item, Customer Type, Price Type, and Unit.
	--This is the most effective price.
	SELECT 
		item_selling_prices.price, 
		item_selling_prices.unit_id,
		item_selling_prices.includes_tax
	INTO 
		_price, 
		_unit_id,
		_includes_tax		
	FROM core.item_selling_prices
	WHERE item_selling_prices.item_id=$1
	AND item_selling_prices.party_type_id=$2
	AND item_selling_prices.price_type_id =$3
	AND item_selling_prices.unit_id = $4;

	IF(_unit_id IS NULL) THEN
		--We do not have a selling price of this item for the unit supplied.
		--Let's see if this item has a price for other units.
		SELECT 
			item_selling_prices.price, 
			item_selling_prices.unit_id,
			item_selling_prices.includes_tax
		INTO 
			_price, 
			_unit_id,
			_includes_tax
		FROM core.item_selling_prices
		WHERE item_selling_prices.item_id=$1
		AND item_selling_prices.party_type_id=$2
		AND item_selling_prices.price_type_id =$3;
	END IF;

	
	IF(_price IS NULL) THEN
		--This item does not have selling price defined in the catalog.
		--Therefore, getting the default selling price from the item definition.
		SELECT 
			selling_price, 
			unit_id,
			selling_price_includes_tax
		INTO 
			_price, 
			_unit_id,
			_includes_tax
		FROM core.items
		WHERE core.items.item_id = $1;
	END IF;

	IF(_includes_tax) THEN
		_tax_rate := core.get_item_tax_rate($1);
		_price := _price / ((100 + _tax_rate)/ 100);
	END IF;

	--Get the unitary conversion factor if the requested unit does not match with the price defition.
	_factor := core.convert_unit($4, _unit_id);

	RETURN _price * _factor;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.verification_trigger() CASCADE;
CREATE FUNCTION transactions.verification_trigger()
RETURNS TRIGGER
AS
$$
	DECLARE _transaction_master_id bigint;
	DECLARE _transaction_posted_by integer;
	DECLARE _old_verifier integer;
	DECLARE _old_status integer;
	DECLARE _old_reason national character varying(128);
	DECLARE _verifier integer;
	DECLARE _status integer;
	DECLARE _reason national character varying(128);
	DECLARE _has_policy boolean;
	DECLARE _is_sys boolean;
	DECLARE _rejected smallint=-3;
	DECLARE _closed smallint=-2;
	DECLARE _withdrawn smallint=-1;
	DECLARE _unapproved smallint = 0;
	DECLARE _auto_approved smallint = 1;
	DECLARE _approved smallint=2;
	DECLARE _book text;
	DECLARE _can_verify_sales_transactions boolean;
	DECLARE _sales_verification_limit money;
	DECLARE _can_verify_purchase_transactions boolean;
	DECLARE _purchase_verification_limit money;
	DECLARE _can_verify_gl_transactions boolean;
	DECLARE _gl_verification_limit money;
	DECLARE _can_verify_self boolean;
	DECLARE _self_verification_limit money;
	DECLARE _posted_amount money;
BEGIN
	IF TG_OP='DELETE' THEN
		RAISE EXCEPTION 'Deleting a transaction is not allowed. Mark the transaction as rejected instead.';
	END IF;

	IF TG_OP='UPDATE' THEN
		RAISE NOTICE 'Columns except the following will be ignored for this update: verified_by_user_id, verification_status_id, verification_reason.';

		IF(OLD.transaction_master_id IS DISTINCT FROM NEW.transaction_master_id) THEN
			RAISE EXCEPTION 'Cannot update the column "transaction_master_id".';
		END IF;

		IF(OLD.transaction_counter IS DISTINCT FROM NEW.transaction_counter) THEN
			RAISE EXCEPTION 'Cannot update the column "transaction_counter".';
		END IF;

		IF(OLD.transaction_code IS DISTINCT FROM NEW.transaction_code) THEN
			RAISE EXCEPTION 'Cannot update the column "transaction_code".';
		END IF;

		IF(OLD.book IS DISTINCT FROM NEW.book) THEN
			RAISE EXCEPTION 'Cannot update the column "book".';
		END IF;

		IF(OLD.value_date IS DISTINCT FROM NEW.value_date) THEN
			RAISE EXCEPTION 'Cannot update the column "value_date".';
		END IF;

		IF(OLD.transaction_ts IS DISTINCT FROM NEW.transaction_ts) THEN
			RAISE EXCEPTION 'Cannot update the column "transaction_ts".';
		END IF;

		IF(OLD.login_id IS DISTINCT FROM NEW.login_id) THEN
			RAISE EXCEPTION 'Cannot update the column "login_id".';
		END IF;

		IF(OLD.user_id IS DISTINCT FROM NEW.user_id) THEN
			RAISE EXCEPTION 'Cannot update the column "user_id".';
		END IF;

		IF(OLD.sys_user_id IS DISTINCT FROM NEW.sys_user_id) THEN
			RAISE EXCEPTION 'Cannot update the column "sys_user_id".';
		END IF;

		IF(OLD.office_id IS DISTINCT FROM NEW.office_id) THEN
			RAISE EXCEPTION 'Cannot update the column "office_id".';
		END IF;

		IF(OLD.cost_center_id IS DISTINCT FROM NEW.cost_center_id) THEN
			RAISE EXCEPTION 'Cannot update the column "cost_center_id".';
		END IF;

		_transaction_master_id := OLD.transaction_master_id;
		_book := OLD.book;
		_old_verifier := OLD.verified_by_user_id;
		_old_status := OLD.verification_status_id;
		_old_reason := OLD.verification_reason;
		_transaction_posted_by := OLD.user_id;		
		_verifier := NEW.verified_by_user_id;
		_status := NEW.verification_status_id;
		_reason := NEW.verification_reason;
		_is_sys := office.is_sys(_verifier);


		SELECT
			SUM(amount)
		INTO
			_posted_amount
		FROM
			transactions.transaction_details
		WHERE transactions.transaction_details.transaction_master_id = _transaction_master_id
		AND transactions.transaction_details.tran_type='Cr';


		SELECT
			true,
			can_verify_sales_transactions,
			sales_verification_limit,
			can_verify_purchase_transactions,
			purchase_verification_limit,
			can_verify_gl_transactions,
			gl_verification_limit,
			can_self_verify,
			self_verification_limit
		INTO
			_has_policy,
			_can_verify_sales_transactions,
			_sales_verification_limit,
			_can_verify_purchase_transactions,
			_purchase_verification_limit,
			_can_verify_gl_transactions,
			_gl_verification_limit,
			_can_verify_self,
			_self_verification_limit
		FROM
		policy.voucher_verification_policy
		WHERE user_id=_verifier
		AND is_active=true
		AND now() >= effective_from
		AND now() <= ends_on;

		IF(_verifier IS NULL) THEN
			RAISE EXCEPTION 'Access is denied.';
		END IF;		
		
		IF(_status != _withdrawn AND _has_policy = false) THEN
			RAISE EXCEPTION 'Access is denied. You don''t have the right to verify the transaction.';
		END IF;

		IF(_status = _withdrawn AND _has_policy = false) THEN
			IF(_transaction_posted_by != _verifier) THEN
				RAISE EXCEPTION 'Access is denied. You don''t have the right to withdraw the transaction.';
			END IF;
		END IF;

		IF(_status = _auto_approved AND _is_sys = false) THEN
			RAISE EXCEPTION 'Access is denied.';
		END IF;


		IF(_has_policy = false) THEN
			RAISE EXCEPTION 'Access is denied.';
		END IF;


		--Is trying verify self transaction.
		IF(NEW.verified_by_user_id = NEW.user_id) THEN
			IF(_can_verify_self = false) THEN
				RAISE EXCEPTION 'Please ask someone else to verify the transaction you posted.';
			END IF;
			IF(_can_verify_self = true) THEN
				IF(_posted_amount > _self_verification_limit AND _self_verification_limit > 0::money) THEN
					RAISE EXCEPTION 'Self verfication limit exceeded. The transaction was not verified.';
				END IF;
			END IF;
		END IF;

		IF(lower(_book) LIKE '%sales%') THEN
			IF(_can_verify_sales_transactions = false) THEN
				RAISE EXCEPTION 'Access is denied.';
			END IF;
			IF(_can_verify_sales_transactions = true) THEN
				IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::money) THEN
					RAISE EXCEPTION 'Sales verfication limit exceeded. The transaction was not verified.';
				END IF;
			END IF;			
		END IF;


		IF(lower(_book) LIKE '%purchase%') THEN
			IF(_can_verify_purchase_transactions = false) THEN
				RAISE EXCEPTION 'Access is denied.';
			END IF;
			IF(_can_verify_purchase_transactions = true) THEN
				IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::money) THEN
					RAISE EXCEPTION 'Purchase verfication limit exceeded. The transaction was not verified.';
				END IF;
			END IF;			
		END IF;


		IF(lower(_book) LIKE 'journal%') THEN
			IF(_can_verify_gl_transactions = false) THEN
				RAISE EXCEPTION 'Access is denied.';
			END IF;
			IF(_can_verify_gl_transactions = true) THEN
				IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::money) THEN
					RAISE EXCEPTION 'GL verfication limit exceeded. The transaction was not verified.';
				END IF;
			END IF;			
		END IF;

		NEW.last_verified_on := now();

	END IF;	
	RETURN NEW;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER verification_update_trigger
AFTER UPDATE
ON transactions.transaction_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verification_trigger();

CREATE TRIGGER verification_delete_trigger
BEFORE DELETE
ON transactions.transaction_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verification_trigger();


DROP FUNCTION IF EXISTS transactions.auto_verify(bigint) CASCADE;

CREATE FUNCTION transactions.auto_verify(bigint)
RETURNS VOID
AS
$$
	DECLARE _transaction_master_id bigint;
	DECLARE _transaction_posted_by integer;
	DECLARE _verifier integer;
	DECLARE _status integer;
	DECLARE _reason national character varying(128);
	DECLARE _rejected smallint=-3;
	DECLARE _closed smallint=-2;
	DECLARE _withdrawn smallint=-1;
	DECLARE _unapproved smallint = 0;
	DECLARE _auto_approved smallint = 1;
	DECLARE _approved smallint=2;
	DECLARE _book text;
	DECLARE _auto_verify_sales boolean;
	DECLARE _sales_verification_limit money;
	DECLARE _auto_verify_purchase boolean;
	DECLARE _purchase_verification_limit money;
	DECLARE _auto_verify_gl boolean;
	DECLARE _gl_verification_limit money;
	DECLARE _posted_amount money;
	DECLARE _auto_verification boolean=true;
	DECLARE _has_policy boolean=false;
BEGIN
	_transaction_master_id := $1;

	SELECT
		transactions.transaction_master.book,
		transactions.transaction_master.user_id
	INTO
		_book,
		_transaction_posted_by 	
	FROM
	transactions.transaction_master
	WHERE transactions.transaction_master.transaction_master_id=_transaction_master_id;
	

	_verifier := office.get_sys_user_id();
	_status := 2;
	_reason := 'Automatically verified by workflow.';

	SELECT
		SUM(amount)
	INTO
		_posted_amount
	FROM
		transactions.transaction_details
	WHERE transactions.transaction_details.transaction_master_id = _transaction_master_id
	AND transactions.transaction_details.tran_type='Cr';


	SELECT
		true,
		verify_sales_transactions,
		sales_verification_limit,
		verify_purchase_transactions,
		purchase_verification_limit,
		verify_gl_transactions,
		gl_verification_limit
	INTO
		_has_policy,
		_auto_verify_sales,
		_sales_verification_limit,
		_auto_verify_purchase,
		_purchase_verification_limit,
		_auto_verify_gl,
		_gl_verification_limit
	FROM
	policy.auto_verification_policy
	WHERE user_id=_transaction_posted_by
	AND is_active=true
	AND now() >= effective_from
	AND now() <= ends_on;



	IF(lower(_book) LIKE 'sales%') THEN
		IF(_auto_verify_sales = false) THEN
			_auto_verification := false;
		END IF;
		IF(_auto_verify_sales = true) THEN
			IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::money) THEN
				_auto_verification := false;
			END IF;
		END IF;			
	END IF;


	IF(lower(_book) LIKE 'purchase%') THEN
		IF(_auto_verify_purchase = false) THEN
			_auto_verification := false;
		END IF;
		IF(_auto_verify_purchase = true) THEN
			IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::money) THEN
				_auto_verification := false;
			END IF;
		END IF;			
	END IF;


	IF(lower(_book) LIKE 'journal%') THEN
		IF(_auto_verify_gl = false) THEN
			_auto_verification := false;
		END IF;
		IF(_auto_verify_gl = true) THEN
			IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::money) THEN
				_auto_verification := false;
			END IF;
		END IF;			
	END IF;

	IF(_has_policy=true) THEN
		IF(_auto_verification = true) THEN
			UPDATE transactions.transaction_master
			SET 
				last_verified_on = now(),
				verified_by_user_id=_verifier,
				verification_status_id=_status,
				verification_reason=_reason
			WHERE
				transactions.transaction_master.transaction_master_id=_transaction_master_id;
		END IF;
	ELSE
		RAISE NOTICE 'No auto verification policy found for this user.';
	END IF;
RETURN;
END
$$
LANGUAGE plpgsql;

DROP VIEW IF EXISTS transactions.verified_transactions_view;
DROP VIEW IF EXISTS transactions.transaction_view;
CREATE VIEW transactions.transaction_view
AS
SELECT
	transactions.transaction_master.transaction_master_id,
	transactions.transaction_master.transaction_counter,
	transactions.transaction_master.transaction_code,
	transactions.transaction_master.book,
	transactions.transaction_master.value_date,
	transactions.transaction_master.transaction_ts,
	transactions.transaction_master.login_id,
	transactions.transaction_master.user_id,
	transactions.transaction_master.sys_user_id,
	transactions.transaction_master.office_id,
	transactions.transaction_master.cost_center_id,
	transactions.transaction_master.reference_number,
	transactions.transaction_master.statement_reference AS master_statement_reference,
	transactions.transaction_master.last_verified_on,
	transactions.transaction_master.verified_by_user_id,
	transactions.transaction_master.verification_status_id,
	transactions.transaction_master.verification_reason,
	transactions.transaction_details.transaction_detail_id,
	transactions.transaction_details.tran_type,
	transactions.transaction_details.account_id,
	transactions.transaction_details.statement_reference,
	transactions.transaction_details.cash_repository_id,
	transactions.transaction_details.amount
FROM
transactions.transaction_master
INNER JOIN transactions.transaction_details
ON transactions.transaction_master.transaction_master_id = transactions.transaction_details.transaction_master_id;

CREATE VIEW transactions.verified_transactions_view
AS
SELECT * FROM transactions.transaction_view
WHERE verification_status_id > 0;


DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(integer);
CREATE FUNCTION transactions.get_cash_repository_balance(integer)
RETURNS money
AS
$$
	DECLARE _debit money;
	DECLARE _credit money;
BEGIN
	SELECT COALESCE(SUM(amount), 0::money) INTO _debit
	FROM transactions.verified_transactions_view
	WHERE cash_repository_id=$1
	AND tran_type='Dr';

	SELECT COALESCE(SUM(amount), 0::money) INTO _credit
	FROM transactions.verified_transactions_view
	WHERE cash_repository_id=$1
	AND tran_type='Cr';

	RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS transactions.get_product_view
(
	text,
	date, 
	date, 
	national character varying(12),
	text,	
	text,
	national character varying(50),
	national character varying(24),
	text
);

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
	amount					money_strict,
	transaction_ts				TIMESTAMP WITH TIME ZONE,
	"user"					national character varying(50),
	reference_number			national character varying(24),
	statement_reference			text,
	flag_background_color			text,
	flag_foreground_color			text
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
		SUM(transactions.non_gl_stock_details.price * transactions.non_gl_stock_details.quantity + tax - discount)::money_strict AS amount,
		transactions.non_gl_stock_master.transaction_ts,
		office.users.user_name AS user,
		transactions.non_gl_stock_master.reference_number,
		transactions.non_gl_stock_master.statement_reference,
		core.get_flag_background_color(core.get_flag_type_id(user_id_, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', transactions.non_gl_stock_master.non_gl_stock_master_id)) AS flag_bg,
		core.get_flag_foreground_color(core.get_flag_type_id(user_id_, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', transactions.non_gl_stock_master.non_gl_stock_master_id)) AS flag_fg
	FROM transactions.non_gl_stock_master
	INNER JOIN transactions.non_gl_stock_details
	ON transactions.non_gl_stock_master.non_gl_stock_master_id = transactions.non_gl_stock_details.non_gl_stock_master_id
	INNER JOIN core.parties
	ON transactions.non_gl_stock_master.party_id = core.parties.party_id
	INNER JOIN core.price_types
	ON transactions.non_gl_stock_master.price_type_id = core.price_types.price_type_id
	INNER JOIN office.users
	ON transactions.non_gl_stock_master.user_id = office.users.user_id
	INNER JOIN office.offices
	ON transactions.non_gl_stock_master.office_id = office.offices.office_id
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
		core.price_types.price_type_code || ' (' || core.price_types.price_type_name || ')'
	) LIKE '%' || lower(price_type_) || '%'
	AND 
	lower
	(
		office.users.user_name
	)  LIKE '%' || lower(user_) || '%'
	AND 
	lower
	(
		transactions.non_gl_stock_master.reference_number
	) LIKE '%' || lower(reference_number_) || '%'
	AND 
	lower
	(
		transactions.non_gl_stock_master.statement_reference
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
		transactions.non_gl_stock_master.statement_reference		
	LIMIT 100;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS policy.check_menu_policy_trigger() CASCADE;


CREATE FUNCTION policy.check_menu_policy_trigger()
RETURNS trigger
AS
$$
	DECLARE count integer=0;
BEGIN
	IF NEW.office_id IS NOT NULL THEN
		count := count + 1;
	END IF;

	IF NEW.role_id IS NOT NULL THEN
		count := count + 1;
	END IF;
	
	IF NEW.user_id IS NOT NULL THEN
		count := count + 1;
	END IF;

	IF count <> 1 THEN
		RAISE EXCEPTION 'Only one of the following columns is required : office_id, role_id, user_id.';
	END IF;

	RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER check_menu_policy_trigger BEFORE INSERT
ON policy.menu_policy
FOR EACH ROW EXECUTE PROCEDURE policy.check_menu_policy_trigger();

DROP FUNCTION IF EXISTS policy.get_menu(user_id_ integer, office_id_ integer, culture_ text);
CREATE FUNCTION policy.get_menu(user_id_ integer, office_id_ integer, culture_ text)
RETURNS TABLE
(
	menu_id			integer,
	menu_text		national character varying(250),
	url			national character varying(250),
	menu_code		character varying(12),
	level			smallint,
	parent_menu_id		integer
)
AS
$$
DECLARE culture_exists boolean = false;
BEGIN
	IF EXISTS(SELECT * FROM core.menu_locale WHERE culture=$3) THEN
		culture_exists := true;
	END IF;

	IF culture_exists THEN
		RETURN QUERY 
		SELECT
			core.menus.menu_id,
			core.menu_locale.menu_text,
			core.menus.url,
			core.menus.menu_code,
			core.menus.level,
			core.menus.parent_menu_id	
		FROM core.menus
		INNER JOIN policy.menu_access
		ON core.menus.menu_id = policy.menu_access.menu_id
		INNER JOIN core.menu_locale
		ON core.menus.menu_id = core.menu_locale.menu_id
		WHERE policy.menu_access.user_id=$1
		AND policy.menu_access.office_id=$2
		AND core.menu_locale.culture=$3;
	ELSE
		RETURN QUERY 
		SELECT
			core.menus.menu_id,
			core.menus.menu_text,
			core.menus.url,
			core.menus.menu_code,
			core.menus.level,
			core.menus.parent_menu_id	
		FROM core.menus
		INNER JOIN policy.menu_access
		ON core.menus.menu_id = policy.menu_access.menu_id
		WHERE policy.menu_access.user_id=$1
		AND policy.menu_access.office_id=$2;
	END IF;

END
$$
LANGUAGE plpgsql;

DROP SCHEMA IF EXISTS scrud CASCADE;
CREATE SCHEMA scrud;

CREATE VIEW scrud.constraint_column_usage AS
    SELECT CAST(current_database() AS text) AS table_catalog,
           CAST(tblschema AS text) AS table_schema,
           CAST(tblname AS text) AS table_name,
           CAST(colname AS text) AS column_name,
           CAST(current_database() AS text) AS constraint_catalog,
           CAST(cstrschema AS text) AS constraint_schema,
           CAST(cstrname AS text) AS constraint_name

    FROM (
        /* check constraints */
        SELECT DISTINCT nr.nspname, r.relname, r.relowner, a.attname, nc.nspname, c.conname
          FROM pg_namespace nr, pg_class r, pg_attribute a, pg_depend d, pg_namespace nc, pg_constraint c
          WHERE nr.oid = r.relnamespace
            AND r.oid = a.attrelid
            AND d.refclassid = 'pg_catalog.pg_class'::regclass
            AND d.refobjid = r.oid
            AND d.refobjsubid = a.attnum
            AND d.classid = 'pg_catalog.pg_constraint'::regclass
            AND d.objid = c.oid
            AND c.connamespace = nc.oid
            AND c.contype = 'c'
            AND r.relkind = 'r'
            AND NOT a.attisdropped

        UNION ALL

        /* unique/primary key/foreign key constraints */
        SELECT nr.nspname, r.relname, r.relowner, a.attname, nc.nspname, c.conname
          FROM pg_namespace nr, pg_class r, pg_attribute a, pg_namespace nc,
               pg_constraint c
          WHERE nr.oid = r.relnamespace
            AND r.oid = a.attrelid
            AND nc.oid = c.connamespace
            AND (CASE WHEN c.contype = 'f' THEN r.oid = c.confrelid AND a.attnum = ANY (c.confkey)
                      ELSE r.oid = c.conrelid AND a.attnum = ANY (c.conkey) END)
            AND NOT a.attisdropped
            AND c.contype IN ('p', 'u', 'f')
            AND r.relkind = 'r'

      ) AS x (tblschema, tblname, tblowner, colname, cstrschema, cstrname);


CREATE VIEW scrud.relationship_view
AS
SELECT
	tc.table_schema,
	tc.table_name,
	kcu.column_name,
	ccu.table_schema AS references_schema,
	ccu.table_name AS references_table,
	ccu.column_name AS references_field  
FROM
	information_schema.table_constraints tc  
LEFT JOIN
	information_schema.key_column_usage kcu  
		ON tc.constraint_catalog = kcu.constraint_catalog  
		AND tc.constraint_schema = kcu.constraint_schema  
		AND tc.constraint_name = kcu.constraint_name  
LEFT JOIN
	information_schema.referential_constraints rc  
		ON tc.constraint_catalog = rc.constraint_catalog  
		AND tc.constraint_schema = rc.constraint_schema  
		AND tc.constraint_name = rc.constraint_name	
LEFT JOIN
	scrud.constraint_column_usage ccu  
		ON rc.unique_constraint_catalog = ccu.constraint_catalog  
		AND rc.unique_constraint_schema = ccu.constraint_schema  
		AND rc.unique_constraint_name = ccu.constraint_name  
WHERE
	lower(tc.constraint_type) in ('foreign key');

CREATE FUNCTION scrud.parse_default(text)
RETURNS text
AS
$$
DECLARE _sql text;
DECLARE _val text;
BEGIN
	IF($1 LIKE '%::%' AND $1 NOT LIKE 'nextval%') THEN
		_sql := 'SELECT ' || $1;
		EXECUTE _sql INTO _val;
		RETURN _val;
	END IF;

	RETURN $1;
END
$$
LANGUAGE plpgsql;


CREATE VIEW scrud.mixerp_table_view
AS
SELECT information_schema.columns.table_schema, 
	   information_schema.columns.table_name, 
	   information_schema.columns.column_name, 
	   references_schema, 
	   references_table, 
	   references_field, 
	   ordinal_position,
	   is_nullable,
	   scrud.parse_default(column_default) AS column_default, 
	   data_type, 
	   domain_name,
	   character_maximum_length, 
	   character_octet_length, 
	   numeric_precision, 
	   numeric_precision_radix, 
	   numeric_scale, 
	   datetime_precision, 
	   udt_name 
FROM   information_schema.columns 
	   LEFT JOIN scrud.relationship_view 
			  ON information_schema.columns.table_schema = 
				 scrud.relationship_view.table_schema 
				 AND information_schema.columns.table_name = 
					 scrud.relationship_view.table_name 
				 AND information_schema.columns.column_name = 
					 scrud.relationship_view.column_name 
WHERE  information_schema.columns.table_schema 
NOT IN 
	( 
		'pg_catalog', 'information_schema'
	)
AND 	   information_schema.columns.column_name 
NOT IN
	(
		'audit_user_id', 'audit_ts'
	)
;
	


INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('DB'), 'fr', 'tableau de bord' UNION ALL
SELECT core.get_menu_id('SA'), 'fr', 'ventes' UNION ALL
SELECT core.get_menu_id('PU'), 'fr', 'acheter' UNION ALL
SELECT core.get_menu_id('ITM'), 'fr', 'Produits et Articles' UNION ALL
SELECT core.get_menu_id('FI'), 'fr', 'Finances' UNION ALL
SELECT core.get_menu_id('MF'), 'fr', 'fabrication' UNION ALL
SELECT core.get_menu_id('CRM'), 'fr', 'CRM' UNION ALL
SELECT core.get_menu_id('SE'), 'fr', 'Paramètres de configuration' UNION ALL
SELECT core.get_menu_id('POS'), 'fr', 'POS' UNION ALL
SELECT core.get_menu_id('SAQ'), 'fr', 'Ventes & Devis' UNION ALL
SELECT core.get_menu_id('DRS'), 'fr', 'vente directe' UNION ALL
SELECT core.get_menu_id('SQ'), 'fr', 'Offre de vente' UNION ALL
SELECT core.get_menu_id('SO'), 'fr', 'commande' UNION ALL
SELECT core.get_menu_id('DSO'), 'fr', 'Livraison de la commande client' UNION ALL
SELECT core.get_menu_id('DWO'), 'fr', 'Livraison Sans Commande' UNION ALL
SELECT core.get_menu_id('ISD'), 'fr', 'Facture pour les ventes Livraison' UNION ALL
SELECT core.get_menu_id('RFC'), 'fr', 'Réception de la clientèle' UNION ALL
SELECT core.get_menu_id('SR'), 'fr', 'ventes Retour' UNION ALL
SELECT core.get_menu_id('SSM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('ABS'), 'fr', 'Bonus dalle pour les agents' UNION ALL
SELECT core.get_menu_id('BSD'), 'fr', 'Bonus Slab Détails' UNION ALL
SELECT core.get_menu_id('SSA'), 'fr', 'Agents de vente' UNION ALL
SELECT core.get_menu_id('BSA'), 'fr', 'Bonus dalle Affectation' UNION ALL
SELECT core.get_menu_id('SAR'), 'fr', 'Rapports de vente' UNION ALL
SELECT core.get_menu_id('SAR-SVSI'), 'fr', 'Voir la facture de vente' UNION ALL
SELECT core.get_menu_id('CM'), 'fr', 'Gestion de la Caisse' UNION ALL
SELECT core.get_menu_id('ASC'), 'fr', 'attribuer Caissier' UNION ALL
SELECT core.get_menu_id('POSS'), 'fr', 'Configuration de POS' UNION ALL
SELECT core.get_menu_id('STT'), 'fr', 'Types de magasins' UNION ALL
SELECT core.get_menu_id('STO'), 'fr', 'magasins' UNION ALL
SELECT core.get_menu_id('SCR'), 'fr', 'Configuration espace d''archivage automatique' UNION ALL
SELECT core.get_menu_id('SCS'), 'fr', 'Configuration du compteur' UNION ALL
SELECT core.get_menu_id('PUQ'), 'fr', 'Achat & Devis' UNION ALL
SELECT core.get_menu_id('DRP'), 'fr', 'Achat direct' UNION ALL
SELECT core.get_menu_id('PO'), 'fr', 'Bon de commande' UNION ALL
SELECT core.get_menu_id('GRN'), 'fr', 'GRN contre PO' UNION ALL
SELECT core.get_menu_id('PAY'), 'fr', 'Facture d''achat contre GRN' UNION ALL
SELECT core.get_menu_id('PAS'), 'fr', 'Paiement à Fournisseur' UNION ALL
SELECT core.get_menu_id('PR'), 'fr', 'achat de retour' UNION ALL
SELECT core.get_menu_id('PUR'), 'fr', 'Rapports d''achat' UNION ALL
SELECT core.get_menu_id('IIM'), 'fr', 'Les mouvements des stocks' UNION ALL
SELECT core.get_menu_id('STJ'), 'fr', 'Journal de transfert de stock' UNION ALL
SELECT core.get_menu_id('STA'), 'fr', 'Ajustements de stock' UNION ALL
SELECT core.get_menu_id('ISM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('PT'), 'fr', 'Types de fête' UNION ALL
SELECT core.get_menu_id('PA'), 'fr', 'Les comptes des partis' UNION ALL
SELECT core.get_menu_id('PSA'), 'fr', 'Adresse de livraison' UNION ALL
SELECT core.get_menu_id('SSI'), 'fr', 'Point de maintenance' UNION ALL
SELECT core.get_menu_id('ICP'), 'fr', 'coût prix' UNION ALL
SELECT core.get_menu_id('ISP'), 'fr', 'prix de vente' UNION ALL
SELECT core.get_menu_id('SSG'), 'fr', 'Groupes des ouvrages' UNION ALL
SELECT core.get_menu_id('SSB'), 'fr', 'marques' UNION ALL
SELECT core.get_menu_id('UOM'), 'fr', 'Unités de mesure' UNION ALL
SELECT core.get_menu_id('CUOM'), 'fr', 'Unités composées de mesure' UNION ALL
SELECT core.get_menu_id('SHI'), 'fr', 'Renseignements sur l''expéditeur' UNION ALL
SELECT core.get_menu_id('FTT'), 'fr', 'Transactions et Modèles' UNION ALL
SELECT core.get_menu_id('JVN'), 'fr', 'Journal Chèques Entrée' UNION ALL
SELECT core.get_menu_id('TTR'), 'fr', 'Transaction modèle' UNION ALL
SELECT core.get_menu_id('STN'), 'fr', 'Instructions permanentes' UNION ALL
SELECT core.get_menu_id('UER'), 'fr', 'Taux de change mise à jour' UNION ALL
SELECT core.get_menu_id('RBA'), 'fr', 'Concilier compte bancaire' UNION ALL
SELECT core.get_menu_id('FVV'), 'fr', 'Vérification bon' UNION ALL
SELECT core.get_menu_id('FTDM'), 'fr', 'Transaction Document Manager' UNION ALL
SELECT core.get_menu_id('FSM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('COA'), 'fr', 'Tableau des comptes' UNION ALL
SELECT core.get_menu_id('CUR'), 'fr', 'Gestion des devises' UNION ALL
SELECT core.get_menu_id('CBA'), 'fr', 'Comptes bancaires' UNION ALL
SELECT core.get_menu_id('PGM'), 'fr', 'Cartographie de GL produit' UNION ALL
SELECT core.get_menu_id('BT'), 'fr', 'Budgets et objectifs' UNION ALL
SELECT core.get_menu_id('AGS'), 'fr', 'Vieillissement Dalles' UNION ALL
SELECT core.get_menu_id('TTY'), 'fr', 'Types d''impôt' UNION ALL
SELECT core.get_menu_id('TS'), 'fr', 'Configuration de l''impôt' UNION ALL
SELECT core.get_menu_id('CC'), 'fr', 'Centres de coûts' UNION ALL
SELECT core.get_menu_id('MFW'), 'fr', 'Flux de travail de fabrication' UNION ALL
SELECT core.get_menu_id('MFWSF'), 'fr', 'Prévisions de ventes' UNION ALL
SELECT core.get_menu_id('MFWMPS'), 'fr', 'Le calendrier de production de Maître' UNION ALL
SELECT core.get_menu_id('MFS'), 'fr', 'Configuration de fabrication' UNION ALL
SELECT core.get_menu_id('MFSWC'), 'fr', 'Centres de travail' UNION ALL
SELECT core.get_menu_id('MFSBOM'), 'fr', 'Nomenclatures' UNION ALL
SELECT core.get_menu_id('MFR'), 'fr', 'Rapports de fabrication' UNION ALL
SELECT core.get_menu_id('MFRGNR'), 'fr', 'Exigences bruts et nets' UNION ALL
SELECT core.get_menu_id('MFRCVSL'), 'fr', 'Capacité vs plomb' UNION ALL
SELECT core.get_menu_id('MFRSFP'), 'fr', 'Planification d''atelier' UNION ALL
SELECT core.get_menu_id('MFRPOS'), 'fr', 'Suivi de commande de production' UNION ALL
SELECT core.get_menu_id('CRMM'), 'fr', 'CRM principal' UNION ALL
SELECT core.get_menu_id('CRML'), 'fr', 'Ajouter un nouveau chef' UNION ALL
SELECT core.get_menu_id('CRMO'), 'fr', 'Ajouter une nouvelle opportunité' UNION ALL
SELECT core.get_menu_id('CRMC'), 'fr', 'Autre plomb à la relance' UNION ALL
SELECT core.get_menu_id('CRMFL'), 'fr', 'Suivi plomb' UNION ALL
SELECT core.get_menu_id('CRMFO'), 'fr', 'possibilité de Suivi' UNION ALL
SELECT core.get_menu_id('CSM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('CRMLS'), 'fr', 'Sources plomb Configuration' UNION ALL
SELECT core.get_menu_id('CRMLST'), 'fr', 'Configuration de l''état de plomb' UNION ALL
SELECT core.get_menu_id('CRMOS'), 'fr', 'Possibilité de configuration Etapes' UNION ALL
SELECT core.get_menu_id('SMP'), 'fr', 'Paramètres divers' UNION ALL
SELECT core.get_menu_id('TRF'), 'fr', 'drapeaux' UNION ALL
SELECT core.get_menu_id('SEAR'), 'fr', 'Rapports de vérification' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'fr', 'Connectez-vous Voir' UNION ALL
SELECT core.get_menu_id('SOS'), 'fr', 'Installation d''Office' UNION ALL
SELECT core.get_menu_id('SOB'), 'fr', 'Bureau & Direction installation' UNION ALL
SELECT core.get_menu_id('SDS'), 'fr', 'Département installation' UNION ALL
SELECT core.get_menu_id('SRM'), 'fr', 'Gestion des rôles' UNION ALL
SELECT core.get_menu_id('SUM'), 'fr', 'Gestion des utilisateurs' UNION ALL
SELECT core.get_menu_id('SFY'), 'fr', 'Exercice information' UNION ALL
SELECT core.get_menu_id('SFR'), 'fr', 'Fréquence et gestion Exercice' UNION ALL
SELECT core.get_menu_id('SPM'), 'fr', 'Gestion des politiques' UNION ALL
SELECT core.get_menu_id('SVV'), 'fr', 'Politique sur la vérification Bon' UNION ALL
SELECT core.get_menu_id('SAV'), 'fr', 'Politique sur la vérification automatique' UNION ALL
SELECT core.get_menu_id('SMA'), 'fr', 'Politique Accès au menu' UNION ALL
SELECT core.get_menu_id('SAP'), 'fr', 'GL Politique d''accès' UNION ALL
SELECT core.get_menu_id('SSP'), 'fr', 'Politique de magasin' UNION ALL
SELECT core.get_menu_id('SWI'), 'fr', 'commutateurs' UNION ALL
SELECT core.get_menu_id('SAT'), 'fr', 'Outils d''administration' UNION ALL
SELECT core.get_menu_id('SQL'), 'fr', 'SQL Query Tool' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'fr', 'Statistiques de base de données' UNION ALL
SELECT core.get_menu_id('BAK'), 'fr', 'Base de données de sauvegarde' UNION ALL
SELECT core.get_menu_id('RES'), 'fr', 'restaurer la base de données' UNION ALL
SELECT core.get_menu_id('PWD'), 'fr', 'Changer d''utilisateur Mot de passe' UNION ALL
SELECT core.get_menu_id('NEW'), 'fr', 'nouvelle entreprise';
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
ALTER TABLE core.parties
ADD 	shipping_address national character varying(250) NULL;

INSERT INTO core.parties(party_type_id, first_name, last_name, date_of_birth, city, state, country,shipping_address, phone, fax, cell, email, url, pan_number, sst_number, cst_number, allow_credit, maximum_credit_period, maximum_credit_amount, charge_interest, interest_rate, interest_compounding_frequency_id, account_id)
SELECT  4, 'Jacob', 'Smith', '1970-01-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741510', '1-5478450', '9812345670', 'jacob_smith@gmail.com', 'www.jacob.com', '5412541', '12457841','4578420','t'::boolean,1,500000,'t'::boolean,5,3,67 UNION ALL
SELECT  1, 'Michael', 'Johnson', '1970-01-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741511', '1-5478451', '9812345671', 'michael_johnson@gmail.com', 'www.michael.com', '5412542', '12457842','4578421','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Joshua', 'Williams', '1970-01-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741512', '1-5478452', '9812345672', 'joshua_williams@gmail.com', 'www.joshua.com', '5412543', '12457843','4578422','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Matthew', 'Jones', '1970-01-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741513', '1-5478453', '9812345673', 'matthew_jones@gmail.com', 'www.matthew.com', '5412544', '12457844','4578423','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  4, 'Ethan', 'Brown', '1970-01-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741514', '1-5478454', '9812345674', 'ethan_brown@gmail.com', 'www.ethan.com', '5412545', '12457845','4578424','t'::boolean,1,500000,'t'::boolean,5,3,67 UNION ALL
SELECT  4, 'Andrew', 'Davis', '1970-01-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741515', '1-5478455', '9812345675', 'andrew_davis@gmail.com', 'www.andrew.com', '5412546', '12457846','4578425','f'::boolean,0,0,'t'::boolean,5,3,67 UNION ALL
SELECT  1, 'Daniel', 'Miller', '1970-01-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741516', '1-5478456', '9812345676', 'daniel_miller@gmail.com', 'www.daniel.com', '5412547', '12457847','4578426','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Anthony', 'Wilson', '1970-01-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741517', '1-5478457', '9812345677', 'anthony_wilson@gmail.com', 'www.anthony.com', '5412548', '12457848','4578427','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Christopher', 'Moore', '1970-01-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741518', '1-5478458', '9812345678', 'christopher_moore@gmail.com', 'www.christopher.com', '5412549', '12457849','4578428','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Joseph', 'Taylor', '1970-01-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741519', '1-5478459', '9812345679', 'joseph_taylor@gmail.com', 'www.joseph.com', '5412550', '12457850','4578429','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'William', 'Anderson', '1970-01-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741520', '1-5478460', '9812345680', 'william_anderson@gmail.com', 'www.william.com', '5412551', '12457851','4578430','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexander', 'Thomas', '1970-01-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741521', '1-5478461', '9812345681', 'alexander_thomas@gmail.com', 'www.alexander.com', '5412552', '12457852','4578431','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ryan', 'Jackson', '1970-01-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741522', '1-5478462', '9812345682', 'ryan_jackson@gmail.com', 'www.ryan.com', '5412553', '12457853','4578432','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'David', 'White', '1970-01-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741523', '1-5478463', '9812345683', 'david_white@gmail.com', 'www.david.com', '5412554', '12457854','4578433','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nicholas', 'Harris', '1970-01-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741524', '1-5478464', '9812345684', 'nicholas_harris@gmail.com', 'www.nicholas.com', '5412555', '12457855','4578434','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyler', 'Martin', '1970-01-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741525', '1-5478465', '9812345685', 'tyler_martin@gmail.com', 'www.tyler.com', '5412556', '12457856','4578435','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'James', 'Thompson', '1970-01-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741526', '1-5478466', '9812345686', 'james_thompson@gmail.com', 'www.james.com', '5412557', '12457857','4578436','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'John', 'Garcia', '1970-01-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741527', '1-5478467', '9812345687', 'john_garcia@gmail.com', 'www.john.com', '5412558', '12457858','4578437','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jonathan', 'Martinez', '1970-01-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741528', '1-5478468', '9812345688', 'jonathan_martinez@gmail.com', 'www.jonathan.com', '5412559', '12457859','4578438','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nathan', 'Robinson', '1970-01-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741529', '1-5478469', '9812345689', 'nathan_robinson@gmail.com', 'www.nathan.com', '5412560', '12457860','4578439','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Samuel', 'Clark', '1970-01-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741530', '1-5478470', '9812345690', 'samuel_clark@gmail.com', 'www.samuel.com', '5412561', '12457861','4578440','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Christian', 'Rodriguez', '1970-01-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741531', '1-5478471', '9812345691', 'christian_rodriguez@gmail.com', 'www.christian.com', '5412562', '12457862','4578441','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Noah', 'Lewis', '1970-01-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741532', '1-5478472', '9812345692', 'noah_lewis@gmail.com', 'www.noah.com', '5412563', '12457863','4578442','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dylan', 'Lee', '1970-01-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741533', '1-5478473', '9812345693', 'dylan_lee@gmail.com', 'www.dylan.com', '5412564', '12457864','4578443','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Benjamin', 'Walker', '1970-01-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741534', '1-5478474', '9812345694', 'benjamin_walker@gmail.com', 'www.benjamin.com', '5412565', '12457865','4578444','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Logan', 'Hall', '1970-01-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741535', '1-5478475', '9812345695', 'logan_hall@gmail.com', 'www.logan.com', '5412566', '12457866','4578445','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brandon', 'Allen', '1970-01-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741536', '1-5478476', '9812345696', 'brandon_allen@gmail.com', 'www.brandon.com', '5412567', '12457867','4578446','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gabriel', 'Young', '1970-01-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741537', '1-5478477', '9812345697', 'gabriel_young@gmail.com', 'www.gabriel.com', '5412568', '12457868','4578447','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zachary', 'Hernandez', '1970-01-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741538', '1-5478478', '9812345698', 'zachary_hernandez@gmail.com', 'www.zachary.com', '5412569', '12457869','4578448','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jose', 'King', '1970-01-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741539', '1-5478479', '9812345699', 'jose_king@gmail.com', 'www.jose.com', '5412570', '12457870','4578449','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elijah', 'Wright', '1970-01-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741540', '1-5478480', '9812345700', 'elijah_wright@gmail.com', 'www.elijah.com', '5412571', '12457871','4578450','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Angel', 'Lopez', '1970-02-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741541', '1-5478481', '9812345701', 'angel_lopez@gmail.com', 'www.angel.com', '5412572', '12457872','4578451','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kevin', 'Hill', '1970-02-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741542', '1-5478482', '9812345702', 'kevin_hill@gmail.com', 'www.kevin.com', '5412573', '12457873','4578452','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jack', 'Scott', '1970-02-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741543', '1-5478483', '9812345703', 'jack_scott@gmail.com', 'www.jack.com', '5412574', '12457874','4578453','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Caleb', 'Green', '1970-02-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741544', '1-5478484', '9812345704', 'caleb_green@gmail.com', 'www.caleb.com', '5412575', '12457875','4578454','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Justin', 'Adams', '1970-02-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741545', '1-5478485', '9812345705', 'justin_adams@gmail.com', 'www.justin.com', '5412576', '12457876','4578455','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Austin', 'Baker', '1970-02-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741546', '1-5478486', '9812345706', 'austin_baker@gmail.com', 'www.austin.com', '5412577', '12457877','4578456','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Evan', 'Gonzalez', '1970-02-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741547', '1-5478487', '9812345707', 'evan_gonzalez@gmail.com', 'www.evan.com', '5412578', '12457878','4578457','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Robert', 'Nelson', '1970-02-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741548', '1-5478488', '9812345708', 'robert_nelson@gmail.com', 'www.robert.com', '5412579', '12457879','4578458','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Thomas', 'Carter', '1970-02-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741549', '1-5478489', '9812345709', 'thomas_carter@gmail.com', 'www.thomas.com', '5412580', '12457880','4578459','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Luke', 'Mitchell', '1970-02-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741550', '1-5478490', '9812345710', 'luke_mitchell@gmail.com', 'www.luke.com', '5412581', '12457881','4578460','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mason', 'Perez', '1970-02-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741551', '1-5478491', '9812345711', 'mason_perez@gmail.com', 'www.mason.com', '5412582', '12457882','4578461','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aidan', 'Roberts', '1970-02-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741552', '1-5478492', '9812345712', 'aidan_roberts@gmail.com', 'www.aidan.com', '5412583', '12457883','4578462','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jackson', 'Turner', '1970-02-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741553', '1-5478493', '9812345713', 'jackson_turner@gmail.com', 'www.jackson.com', '5412584', '12457884','4578463','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isaiah', 'Phillips', '1970-02-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741554', '1-5478494', '9812345714', 'isaiah_phillips@gmail.com', 'www.isaiah.com', '5412585', '12457885','4578464','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jordan', 'Campbell', '1970-02-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741555', '1-5478495', '9812345715', 'jordan_campbell@gmail.com', 'www.jordan.com', '5412586', '12457886','4578465','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gavin', 'Parker', '1970-02-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741556', '1-5478496', '9812345716', 'gavin_parker@gmail.com', 'www.gavin.com', '5412587', '12457887','4578466','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Connor', 'Evans', '1970-02-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741557', '1-5478497', '9812345717', 'connor_evans@gmail.com', 'www.connor.com', '5412588', '12457888','4578467','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aiden', 'Edwards', '1970-02-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741558', '1-5478498', '9812345718', 'aiden_edwards@gmail.com', 'www.aiden.com', '5412589', '12457889','4578468','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isaac', 'Collins', '1970-02-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741559', '1-5478499', '9812345719', 'isaac_collins@gmail.com', 'www.isaac.com', '5412590', '12457890','4578469','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jason', 'Stewart', '1970-02-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741560', '1-5478500', '9812345720', 'jason_stewart@gmail.com', 'www.jason.com', '5412591', '12457891','4578470','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cameron', 'Sanchez', '1970-02-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741561', '1-5478501', '9812345721', 'cameron_sanchez@gmail.com', 'www.cameron.com', '5412592', '12457892','4578471','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hunter', 'Morris', '1970-02-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741562', '1-5478502', '9812345722', 'hunter_morris@gmail.com', 'www.hunter.com', '5412593', '12457893','4578472','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  4, 'Jayden', 'Rogers', '1970-02-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741563', '1-5478503', '9812345723', 'jayden_rogers@gmail.com', 'www.jayden.com', '5412594', '12457894','4578473','t'::boolean,1,500000,'t'::boolean,5,3,67 UNION ALL
SELECT  1, 'Juan', 'Reed', '1970-02-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741564', '1-5478504', '9812345724', 'juan_reed@gmail.com', 'www.juan.com', '5412595', '12457895','4578474','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Charles', 'Cook', '1970-02-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741565', '1-5478505', '9812345725', 'charles_cook@gmail.com', 'www.charles.com', '5412596', '12457896','4578475','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aaron', 'Morgan', '1970-02-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741566', '1-5478506', '9812345726', 'aaron_morgan@gmail.com', 'www.aaron.com', '5412597', '12457897','4578476','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lucas', 'Bell', '1970-02-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741567', '1-5478507', '9812345727', 'lucas_bell@gmail.com', 'www.lucas.com', '5412598', '12457898','4578477','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Luis', 'Murphy', '1970-02-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741568', '1-5478508', '9812345728', 'luis_murphy@gmail.com', 'www.luis.com', '5412599', '12457899','4578478','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Owen', 'Bailey', '1970-03-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741569', '1-5478509', '9812345729', 'owen_bailey@gmail.com', 'www.owen.com', '5412600', '12457900','4578479','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Landon', 'Rivera', '1970-03-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741570', '1-5478510', '9812345730', 'landon_rivera@gmail.com', 'www.landon.com', '5412601', '12457901','4578480','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Diego', 'Cooper', '1970-03-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741571', '1-5478511', '9812345731', 'diego_cooper@gmail.com', 'www.diego.com', '5412602', '12457902','4578481','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brian', 'Richardson', '1970-03-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741572', '1-5478512', '9812345732', 'brian_richardson@gmail.com', 'www.brian.com', '5412603', '12457903','4578482','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adam', 'Cox', '1970-03-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741573', '1-5478513', '9812345733', 'adam_cox@gmail.com', 'www.adam.com', '5412604', '12457904','4578483','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adrian', 'Howard', '1970-03-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741574', '1-5478514', '9812345734', 'adrian_howard@gmail.com', 'www.adrian.com', '5412605', '12457905','4578484','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kyle', 'Ward', '1970-03-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741575', '1-5478515', '9812345735', 'kyle_ward@gmail.com', 'www.kyle.com', '5412606', '12457906','4578485','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Eric', 'Torres', '1970-03-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741576', '1-5478516', '9812345736', 'eric_torres@gmail.com', 'www.eric.com', '5412607', '12457907','4578486','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ian', 'Peterson', '1970-03-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741577', '1-5478517', '9812345737', 'ian_peterson@gmail.com', 'www.ian.com', '5412608', '12457908','4578487','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nathaniel', 'Gray', '1970-03-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741578', '1-5478518', '9812345738', 'nathaniel_gray@gmail.com', 'www.nathaniel.com', '5412609', '12457909','4578488','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carlos', 'Ramirez', '1970-03-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741579', '1-5478519', '9812345739', 'carlos_ramirez@gmail.com', 'www.carlos.com', '5412610', '12457910','4578489','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alex', 'James', '1970-03-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741580', '1-5478520', '9812345740', 'alex_james@gmail.com', 'www.alex.com', '5412611', '12457911','4578490','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bryan', 'Watson', '1970-03-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741581', '1-5478521', '9812345741', 'bryan_watson@gmail.com', 'www.bryan.com', '5412612', '12457912','4578491','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jesus', 'Brooks', '1970-03-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741582', '1-5478522', '9812345742', 'jesus_brooks@gmail.com', 'www.jesus.com', '5412613', '12457913','4578492','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  4, 'Julian', 'Kelly', '1970-03-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741583', '1-5478523', '9812345743', 'julian_kelly@gmail.com', 'www.julian.com', '5412614', '12457914','4578493','t'::boolean,1,500000,'t'::boolean,5,3,67 UNION ALL
SELECT  1, 'Sean', 'Sanders', '1970-03-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741584', '1-5478524', '9812345744', 'sean_sanders@gmail.com', 'www.sean.com', '5412615', '12457915','4578494','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  4, 'Carter', 'Price', '1970-03-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741585', '1-5478525', '9812345745', 'carter_price@gmail.com', 'www.carter.com', '5412616', '12457916','4578495','t'::boolean,1,500000,'t'::boolean,5,3,67 UNION ALL
SELECT  1, 'Hayden', 'Bennett', '1970-03-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741586', '1-5478526', '9812345746', 'hayden_bennett@gmail.com', 'www.hayden.com', '5412617', '12457917','4578496','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  4, 'Jeremiah', 'Wood', '1970-03-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741587', '1-5478527', '9812345747', 'jeremiah_wood@gmail.com', 'www.jeremiah.com', '5412618', '12457918','4578497','t'::boolean,1,500000,'t'::boolean,5,3,67 UNION ALL
SELECT  1, 'Cole', 'Barnes', '1970-03-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741588', '1-5478528', '9812345748', 'cole_barnes@gmail.com', 'www.cole.com', '5412619', '12457919','4578498','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brayden', 'Ross', '1970-03-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741589', '1-5478529', '9812345749', 'brayden_ross@gmail.com', 'www.brayden.com', '5412620', '12457920','4578499','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Wyatt', 'Henderson', '1970-03-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741590', '1-5478530', '9812345750', 'wyatt_henderson@gmail.com', 'www.wyatt.com', '5412621', '12457921','4578500','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chase', 'Coleman', '1970-03-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741591', '1-5478531', '9812345751', 'chase_coleman@gmail.com', 'www.chase.com', '5412622', '12457922','4578501','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Steven', 'Jenkins', '1970-03-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741592', '1-5478532', '9812345752', 'steven_jenkins@gmail.com', 'www.steven.com', '5412623', '12457923','4578502','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Timothy', 'Perry', '1970-03-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741593', '1-5478533', '9812345753', 'timothy_perry@gmail.com', 'www.timothy.com', '5412624', '12457924','4578503','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dominic', 'Powell', '1970-03-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741594', '1-5478534', '9812345754', 'dominic_powell@gmail.com', 'www.dominic.com', '5412625', '12457925','4578504','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sebastian', 'Long', '1970-03-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741595', '1-5478535', '9812345755', 'sebastian_long@gmail.com', 'www.sebastian.com', '5412626', '12457926','4578505','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Xavier', 'Patterson', '1970-03-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741596', '1-5478536', '9812345756', 'xavier_patterson@gmail.com', 'www.xavier.com', '5412627', '12457927','4578506','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaden', 'Hughes', '1970-03-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741597', '1-5478537', '9812345757', 'jaden_hughes@gmail.com', 'www.jaden.com', '5412628', '12457928','4578507','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jesse', 'Flores', '1970-03-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741598', '1-5478538', '9812345758', 'jesse_flores@gmail.com', 'www.jesse.com', '5412629', '12457929','4578508','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Devin', 'Washington', '1970-03-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741599', '1-5478539', '9812345759', 'devin_washington@gmail.com', 'www.devin.com', '5412630', '12457930','4578509','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Seth', 'Butler', '1970-04-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741600', '1-5478540', '9812345760', 'seth_butler@gmail.com', 'www.seth.com', '5412631', '12457931','4578510','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Antonio', 'Simmons', '1970-04-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741601', '1-5478541', '9812345761', 'antonio_simmons@gmail.com', 'www.antonio.com', '5412632', '12457932','4578511','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Richard', 'Foster', '1970-04-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741602', '1-5478542', '9812345762', 'richard_foster@gmail.com', 'www.richard.com', '5412633', '12457933','4578512','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Miguel', 'Gonzales', '1970-04-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741603', '1-5478543', '9812345763', 'miguel_gonzales@gmail.com', 'www.miguel.com', '5412634', '12457934','4578513','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Colin', 'Bryant', '1970-04-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741604', '1-5478544', '9812345764', 'colin_bryant@gmail.com', 'www.colin.com', '5412635', '12457935','4578514','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cody', 'Alexander', '1970-04-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741605', '1-5478545', '9812345765', 'cody_alexander@gmail.com', 'www.cody.com', '5412636', '12457936','4578515','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alejandro', 'Russell', '1970-04-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741606', '1-5478546', '9812345766', 'alejandro_russell@gmail.com', 'www.alejandro.com', '5412637', '12457937','4578516','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Caden', 'Griffin', '1970-04-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741607', '1-5478547', '9812345767', 'caden_griffin@gmail.com', 'www.caden.com', '5412638', '12457938','4578517','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Blake', 'Diaz', '1970-04-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741608', '1-5478548', '9812345768', 'blake_diaz@gmail.com', 'www.blake.com', '5412639', '12457939','4578518','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carson', 'Hayes', '1970-04-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741609', '1-5478549', '9812345769', 'carson_hayes@gmail.com', 'www.carson.com', '5412640', '12457940','4578519','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kaden', 'Myers', '1970-04-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741610', '1-5478550', '9812345770', 'kaden_myers@gmail.com', 'www.kaden.com', '5412641', '12457941','4578520','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jake', 'Ford', '1970-04-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741611', '1-5478551', '9812345771', 'jake_ford@gmail.com', 'www.jake.com', '5412642', '12457942','4578521','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Henry', 'Hamilton', '1970-04-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741612', '1-5478552', '9812345772', 'henry_hamilton@gmail.com', 'www.henry.com', '5412643', '12457943','4578522','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Liam', 'Graham', '1970-04-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741613', '1-5478553', '9812345773', 'liam_graham@gmail.com', 'www.liam.com', '5412644', '12457944','4578523','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Victor', 'Sullivan', '1970-04-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741614', '1-5478554', '9812345774', 'victor_sullivan@gmail.com', 'www.victor.com', '5412645', '12457945','4578524','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Riley', 'Wallace', '1970-04-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741615', '1-5478555', '9812345775', 'riley_wallace@gmail.com', 'www.riley.com', '5412646', '12457946','4578525','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ashton', 'Woods', '1970-04-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741616', '1-5478556', '9812345776', 'ashton_woods@gmail.com', 'www.ashton.com', '5412647', '12457947','4578526','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Patrick', 'Cole', '1970-04-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741617', '1-5478557', '9812345777', 'patrick_cole@gmail.com', 'www.patrick.com', '5412648', '12457948','4578527','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bryce', 'West', '1970-04-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741618', '1-5478558', '9812345778', 'bryce_west@gmail.com', 'www.bryce.com', '5412649', '12457949','4578528','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brady', 'Jordan', '1970-04-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741619', '1-5478559', '9812345779', 'brady_jordan@gmail.com', 'www.brady.com', '5412650', '12457950','4578529','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Vincent', 'Owens', '1970-04-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741620', '1-5478560', '9812345780', 'vincent_owens@gmail.com', 'www.vincent.com', '5412651', '12457951','4578530','t'::boolean,1,500000,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trevor', 'Reynolds', '1970-04-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741621', '1-5478561', '9812345781', 'trevor_reynolds@gmail.com', 'www.trevor.com', '5412652', '12457952','4578531','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tristan', 'Fisher', '1970-04-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741622', '1-5478562', '9812345782', 'tristan_fisher@gmail.com', 'www.tristan.com', '5412653', '12457953','4578532','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mark', 'Ellis', '1970-04-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741623', '1-5478563', '9812345783', 'mark_ellis@gmail.com', 'www.mark.com', '5412654', '12457954','4578533','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jeremy', 'Harrison', '1970-04-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741624', '1-5478564', '9812345784', 'jeremy_harrison@gmail.com', 'www.jeremy.com', '5412655', '12457955','4578534','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Oscar', 'Gibson', '1970-04-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741625', '1-5478565', '9812345785', 'oscar_gibson@gmail.com', 'www.oscar.com', '5412656', '12457956','4578535','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marcus', 'Mcdonald', '1970-04-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741626', '1-5478566', '9812345786', 'marcus_mcdonald@gmail.com', 'www.marcus.com', '5412657', '12457957','4578536','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jorge', 'Cruz', '1970-04-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741627', '1-5478567', '9812345787', 'jorge_cruz@gmail.com', 'www.jorge.com', '5412658', '12457958','4578537','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Parker', 'Marshall', '1970-04-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741628', '1-5478568', '9812345788', 'parker_marshall@gmail.com', 'www.parker.com', '5412659', '12457959','4578538','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kaleb', 'Ortiz', '1970-04-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741629', '1-5478569', '9812345789', 'kaleb_ortiz@gmail.com', 'www.kaleb.com', '5412660', '12457960','4578539','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cooper', 'Gomez', '1970-05-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741630', '1-5478570', '9812345790', 'cooper_gomez@gmail.com', 'www.cooper.com', '5412661', '12457961','4578540','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kenneth', 'Murray', '1970-05-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741631', '1-5478571', '9812345791', 'kenneth_murray@gmail.com', 'www.kenneth.com', '5412662', '12457962','4578541','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Garrett', 'Freeman', '1970-05-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741632', '1-5478572', '9812345792', 'garrett_freeman@gmail.com', 'www.garrett.com', '5412663', '12457963','4578542','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Joel', 'Wells', '1970-05-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741633', '1-5478573', '9812345793', 'joel_wells@gmail.com', 'www.joel.com', '5412664', '12457964','4578543','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ivan', 'Webb', '1970-05-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741634', '1-5478574', '9812345794', 'ivan_webb@gmail.com', 'www.ivan.com', '5412665', '12457965','4578544','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Josiah', 'Simpson', '1970-05-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741635', '1-5478575', '9812345795', 'josiah_simpson@gmail.com', 'www.josiah.com', '5412666', '12457966','4578545','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alan', 'Stevens', '1970-05-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741636', '1-5478576', '9812345796', 'alan_stevens@gmail.com', 'www.alan.com', '5412667', '12457967','4578546','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Conner', 'Tucker', '1970-05-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741637', '1-5478577', '9812345797', 'conner_tucker@gmail.com', 'www.conner.com', '5412668', '12457968','4578547','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Eduardo', 'Porter', '1970-05-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741638', '1-5478578', '9812345798', 'eduardo_porter@gmail.com', 'www.eduardo.com', '5412669', '12457969','4578548','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Paul', 'Hunter', '1970-05-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741639', '1-5478579', '9812345799', 'paul_hunter@gmail.com', 'www.paul.com', '5412670', '12457970','4578549','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tanner', 'Hicks', '1970-05-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741640', '1-5478580', '9812345800', 'tanner_hicks@gmail.com', 'www.tanner.com', '5412671', '12457971','4578550','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braden', 'Crawford', '1970-05-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741641', '1-5478581', '9812345801', 'braden_crawford@gmail.com', 'www.braden.com', '5412672', '12457972','4578551','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexis', 'Henry', '1970-05-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741642', '1-5478582', '9812345802', 'alexis_henry@gmail.com', 'www.alexis.com', '5412673', '12457973','4578552','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Edward', 'Boyd', '1970-05-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741643', '1-5478583', '9812345803', 'edward_boyd@gmail.com', 'www.edward.com', '5412674', '12457974','4578553','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Omar', 'Mason', '1970-05-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741644', '1-5478584', '9812345804', 'omar_mason@gmail.com', 'www.omar.com', '5412675', '12457975','4578554','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nicolas', 'Morales', '1970-05-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741645', '1-5478585', '9812345805', 'nicolas_morales@gmail.com', 'www.nicolas.com', '5412676', '12457976','4578555','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jared', 'Kennedy', '1970-05-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741646', '1-5478586', '9812345806', 'jared_kennedy@gmail.com', 'www.jared.com', '5412677', '12457977','4578556','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Peyton', 'Warren', '1970-05-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741647', '1-5478587', '9812345807', 'peyton_warren@gmail.com', 'www.peyton.com', '5412678', '12457978','4578557','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'George', 'Dixon', '1970-05-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741648', '1-5478588', '9812345808', 'george_dixon@gmail.com', 'www.george.com', '5412679', '12457979','4578558','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maxwell', 'Ramos', '1970-05-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741649', '1-5478589', '9812345809', 'maxwell_ramos@gmail.com', 'www.maxwell.com', '5412680', '12457980','4578559','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cristian', 'Reyes', '1970-05-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741650', '1-5478590', '9812345810', 'cristian_reyes@gmail.com', 'www.cristian.com', '5412681', '12457981','4578560','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Francisco', 'Burns', '1970-05-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741651', '1-5478591', '9812345811', 'francisco_burns@gmail.com', 'www.francisco.com', '5412682', '12457982','4578561','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Collin', 'Gordon', '1970-05-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741652', '1-5478592', '9812345812', 'collin_gordon@gmail.com', 'www.collin.com', '5412683', '12457983','4578562','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nolan', 'Shaw', '1970-05-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741653', '1-5478593', '9812345813', 'nolan_shaw@gmail.com', 'www.nolan.com', '5412684', '12457984','4578563','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Preston', 'Holmes', '1970-05-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741654', '1-5478594', '9812345814', 'preston_holmes@gmail.com', 'www.preston.com', '5412685', '12457985','4578564','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Stephen', 'Rice', '1970-05-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741655', '1-5478595', '9812345815', 'stephen_rice@gmail.com', 'www.stephen.com', '5412686', '12457986','4578565','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ayden', 'Robertson', '1970-05-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741656', '1-5478596', '9812345816', 'ayden_robertson@gmail.com', 'www.ayden.com', '5412687', '12457987','4578566','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gage', 'Hunt', '1970-05-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741657', '1-5478597', '9812345817', 'gage_hunt@gmail.com', 'www.gage.com', '5412688', '12457988','4578567','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Levi', 'Black', '1970-05-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741658', '1-5478598', '9812345818', 'levi_black@gmail.com', 'www.levi.com', '5412689', '12457989','4578568','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dakota', 'Daniels', '1970-05-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741659', '1-5478599', '9812345819', 'dakota_daniels@gmail.com', 'www.dakota.com', '5412690', '12457990','4578569','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Micah', 'Palmer', '1970-05-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741660', '1-5478600', '9812345820', 'micah_palmer@gmail.com', 'www.micah.com', '5412691', '12457991','4578570','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Eli', 'Mills', '1970-06-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741661', '1-5478601', '9812345821', 'eli_mills@gmail.com', 'www.eli.com', '5412692', '12457992','4578571','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Manuel', 'Nichols', '1970-06-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741662', '1-5478602', '9812345822', 'manuel_nichols@gmail.com', 'www.manuel.com', '5412693', '12457993','4578572','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Grant', 'Grant', '1970-06-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741663', '1-5478603', '9812345823', 'grant_grant@gmail.com', 'www.grant.com', '5412694', '12457994','4578573','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Colton', 'Knight', '1970-06-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741664', '1-5478604', '9812345824', 'colton_knight@gmail.com', 'www.colton.com', '5412695', '12457995','4578574','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Damian', 'Ferguson', '1970-06-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741665', '1-5478605', '9812345825', 'damian_ferguson@gmail.com', 'www.damian.com', '5412696', '12457996','4578575','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ricardo', 'Rose', '1970-06-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741666', '1-5478606', '9812345826', 'ricardo_rose@gmail.com', 'www.ricardo.com', '5412697', '12457997','4578576','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Giovanni', 'Stone', '1970-06-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741667', '1-5478607', '9812345827', 'giovanni_stone@gmail.com', 'www.giovanni.com', '5412698', '12457998','4578577','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Andres', 'Hawkins', '1970-06-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741668', '1-5478608', '9812345828', 'andres_hawkins@gmail.com', 'www.andres.com', '5412699', '12457999','4578578','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emmanuel', 'Dunn', '1970-06-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741669', '1-5478609', '9812345829', 'emmanuel_dunn@gmail.com', 'www.emmanuel.com', '5412700', '12458000','4578579','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Peter', 'Perkins', '1970-06-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741670', '1-5478610', '9812345830', 'peter_perkins@gmail.com', 'www.peter.com', '5412701', '12458001','4578580','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Malachi', 'Hudson', '1970-06-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741671', '1-5478611', '9812345831', 'malachi_hudson@gmail.com', 'www.malachi.com', '5412702', '12458002','4578581','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cesar', 'Spencer', '1970-06-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741672', '1-5478612', '9812345832', 'cesar_spencer@gmail.com', 'www.cesar.com', '5412703', '12458003','4578582','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Javier', 'Gardner', '1970-06-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741673', '1-5478613', '9812345833', 'javier_gardner@gmail.com', 'www.javier.com', '5412704', '12458004','4578583','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Max', 'Stephens', '1970-06-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741674', '1-5478614', '9812345834', 'maximum_stephens@gmail.com', 'www.max.com', '5412705', '12458005','4578584','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hector', 'Payne', '1970-06-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741675', '1-5478615', '9812345835', 'hector_payne@gmail.com', 'www.hector.com', '5412706', '12458006','4578585','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Edgar', 'Pierce', '1970-06-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741676', '1-5478616', '9812345836', 'edgar_pierce@gmail.com', 'www.edgar.com', '5412707', '12458007','4578586','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shane', 'Berry', '1970-06-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741677', '1-5478617', '9812345837', 'shane_berry@gmail.com', 'www.shane.com', '5412708', '12458008','4578587','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Fernando', 'Matthews', '1970-06-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741678', '1-5478618', '9812345838', 'fernando_matthews@gmail.com', 'www.fernando.com', '5412709', '12458009','4578588','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ty', 'Arnold', '1970-06-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741679', '1-5478619', '9812345839', 'ty_arnold@gmail.com', 'www.ty.com', '5412710', '12458010','4578589','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jeffrey', 'Wagner', '1970-06-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741680', '1-5478620', '9812345840', 'jeffrey_wagner@gmail.com', 'www.jeffrey.com', '5412711', '12458011','4578590','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bradley', 'Willis', '1970-06-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741681', '1-5478621', '9812345841', 'bradley_willis@gmail.com', 'www.bradley.com', '5412712', '12458012','4578591','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Derek', 'Ray', '1970-06-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741682', '1-5478622', '9812345842', 'derek_ray@gmail.com', 'www.derek.com', '5412713', '12458013','4578592','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Travis', 'Watkins', '1970-06-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741683', '1-5478623', '9812345843', 'travis_watkins@gmail.com', 'www.travis.com', '5412714', '12458014','4578593','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brendan', 'Olson', '1970-06-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741684', '1-5478624', '9812345844', 'brendan_olson@gmail.com', 'www.brendan.com', '5412715', '12458015','4578594','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shawn', 'Carroll', '1970-06-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741685', '1-5478625', '9812345845', 'shawn_carroll@gmail.com', 'www.shawn.com', '5412716', '12458016','4578595','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Edwin', 'Duncan', '1970-06-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741686', '1-5478626', '9812345846', 'edwin_duncan@gmail.com', 'www.edwin.com', '5412717', '12458017','4578596','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Spencer', 'Snyder', '1970-06-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741687', '1-5478627', '9812345847', 'spencer_snyder@gmail.com', 'www.spencer.com', '5412718', '12458018','4578597','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mario', 'Hart', '1970-06-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741688', '1-5478628', '9812345848', 'mario_hart@gmail.com', 'www.mario.com', '5412719', '12458019','4578598','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dalton', 'Cunningham', '1970-06-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741689', '1-5478629', '9812345849', 'dalton_cunningham@gmail.com', 'www.dalton.com', '5412720', '12458020','4578599','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Erick', 'Bradley', '1970-06-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741690', '1-5478630', '9812345850', 'erick_bradley@gmail.com', 'www.erick.com', '5412721', '12458021','4578600','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Johnathan', 'Lane', '1970-07-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741691', '1-5478631', '9812345851', 'johnathan_lane@gmail.com', 'www.johnathan.com', '5412722', '12458022','4578601','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Erik', 'Andrews', '1970-07-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741692', '1-5478632', '9812345852', 'erik_andrews@gmail.com', 'www.erik.com', '5412723', '12458023','4578602','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jonah', 'Ruiz', '1970-07-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741693', '1-5478633', '9812345853', 'jonah_ruiz@gmail.com', 'www.jonah.com', '5412724', '12458024','4578603','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Donovan', 'Harper', '1970-07-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741694', '1-5478634', '9812345854', 'donovan_harper@gmail.com', 'www.donovan.com', '5412725', '12458025','4578604','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leonardo', 'Fox', '1970-07-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741695', '1-5478635', '9812345855', 'leonardo_fox@gmail.com', 'www.leonardo.com', '5412726', '12458026','4578605','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Wesley', 'Riley', '1970-07-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741696', '1-5478636', '9812345856', 'wesley_riley@gmail.com', 'www.wesley.com', '5412727', '12458027','4578606','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elias', 'Armstrong', '1970-07-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741697', '1-5478637', '9812345857', 'elias_armstrong@gmail.com', 'www.elias.com', '5412728', '12458028','4578607','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marco', 'Carpenter', '1970-07-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741698', '1-5478638', '9812345858', 'marco_carpenter@gmail.com', 'www.marco.com', '5412729', '12458029','4578608','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trenton', 'Weaver', '1970-07-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741699', '1-5478639', '9812345859', 'trenton_weaver@gmail.com', 'www.trenton.com', '5412730', '12458030','4578609','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Devon', 'Greene', '1970-07-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741700', '1-5478640', '9812345860', 'devon_greene@gmail.com', 'www.devon.com', '5412731', '12458031','4578610','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brody', 'Lawrence', '1970-07-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741701', '1-5478641', '9812345861', 'brody_lawrence@gmail.com', 'www.brody.com', '5412732', '12458032','4578611','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Abraham', 'Elliott', '1970-07-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741702', '1-5478642', '9812345862', 'abraham_elliott@gmail.com', 'www.abraham.com', '5412733', '12458033','4578612','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaylen', 'Chavez', '1970-07-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741703', '1-5478643', '9812345863', 'jaylen_chavez@gmail.com', 'www.jaylen.com', '5412734', '12458034','4578613','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bryson', 'Sims', '1970-07-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741704', '1-5478644', '9812345864', 'bryson_sims@gmail.com', 'www.bryson.com', '5412735', '12458035','4578614','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Josue', 'Austin', '1970-07-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741705', '1-5478645', '9812345865', 'josue_austin@gmail.com', 'www.josue.com', '5412736', '12458036','4578615','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sergio', 'Peters', '1970-07-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741706', '1-5478646', '9812345866', 'sergio_peters@gmail.com', 'www.sergio.com', '5412737', '12458037','4578616','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Drew', 'Kelley', '1970-07-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741707', '1-5478647', '9812345867', 'drew_kelley@gmail.com', 'www.drew.com', '5412738', '12458038','4578617','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Damien', 'Franklin', '1970-07-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741708', '1-5478648', '9812345868', 'damien_franklin@gmail.com', 'www.damien.com', '5412739', '12458039','4578618','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Raymond', 'Lawson', '1970-07-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741709', '1-5478649', '9812345869', 'raymond_lawson@gmail.com', 'www.raymond.com', '5412740', '12458040','4578619','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Andy', 'Fields', '1970-07-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741710', '1-5478650', '9812345870', 'andy_fields@gmail.com', 'www.andy.com', '5412741', '12458041','4578620','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dillon', 'Gutierrez', '1970-07-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741711', '1-5478651', '9812345871', 'dillon_gutierrez@gmail.com', 'www.dillon.com', '5412742', '12458042','4578621','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gregory', 'Ryan', '1970-07-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741712', '1-5478652', '9812345872', 'gregory_ryan@gmail.com', 'www.gregory.com', '5412743', '12458043','4578622','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Roberto', 'Schmidt', '1970-07-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741713', '1-5478653', '9812345873', 'roberto_schmidt@gmail.com', 'www.roberto.com', '5412744', '12458044','4578623','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Roman', 'Carr', '1970-07-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741714', '1-5478654', '9812345874', 'roman_carr@gmail.com', 'www.roman.com', '5412745', '12458045','4578624','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Martin', 'Vasquez', '1970-07-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741715', '1-5478655', '9812345875', 'martin_vasquez@gmail.com', 'www.martin.com', '5412746', '12458046','4578625','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Andre', 'Castillo', '1970-07-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741716', '1-5478656', '9812345876', 'andre_castillo@gmail.com', 'www.andre.com', '5412747', '12458047','4578626','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jace', 'Wheeler', '1970-07-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741717', '1-5478657', '9812345877', 'jace_wheeler@gmail.com', 'www.jace.com', '5412748', '12458048','4578627','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Oliver', 'Chapman', '1970-07-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741718', '1-5478658', '9812345878', 'oliver_chapman@gmail.com', 'www.oliver.com', '5412749', '12458049','4578628','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Miles', 'Oliver', '1970-07-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741719', '1-5478659', '9812345879', 'miles_oliver@gmail.com', 'www.miles.com', '5412750', '12458050','4578629','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Harrison', 'Montgomery', '1970-07-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741720', '1-5478660', '9812345880', 'harrison_montgomery@gmail.com', 'www.harrison.com', '5412751', '12458051','4578630','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jalen', 'Richards', '1970-07-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741721', '1-5478661', '9812345881', 'jalen_richards@gmail.com', 'www.jalen.com', '5412752', '12458052','4578631','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'corey', 'Williamson', '1970-08-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741722', '1-5478662', '9812345882', 'corey_williamson@gmail.com', 'www.corey.com', '5412753', '12458053','4578632','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dominick', 'Johnston', '1970-08-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741723', '1-5478663', '9812345883', 'dominick_johnston@gmail.com', 'www.dominick.com', '5412754', '12458054','4578633','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Avery', 'Banks', '1970-08-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741724', '1-5478664', '9812345884', 'avery_banks@gmail.com', 'www.avery.com', '5412755', '12458055','4578634','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Clayton', 'Meyer', '1970-08-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741725', '1-5478665', '9812345885', 'clayton_meyer@gmail.com', 'www.clayton.com', '5412756', '12458056','4578635','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Pedro', 'Bishop', '1970-08-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741726', '1-5478666', '9812345886', 'pedro_bishop@gmail.com', 'www.pedro.com', '5412757', '12458057','4578636','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Israel', 'Mccoy', '1970-08-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741727', '1-5478667', '9812345887', 'israel_mccoy@gmail.com', 'www.israel.com', '5412758', '12458058','4578637','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Calvin', 'Howell', '1970-08-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741728', '1-5478668', '9812345888', 'calvin_howell@gmail.com', 'www.calvin.com', '5412759', '12458059','4578638','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Colby', 'Alvarez', '1970-08-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741729', '1-5478669', '9812345889', 'colby_alvarez@gmail.com', 'www.colby.com', '5412760', '12458060','4578639','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dawson', 'Morrison', '1970-08-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741730', '1-5478670', '9812345890', 'dawson_morrison@gmail.com', 'www.dawson.com', '5412761', '12458061','4578640','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cayden', 'Hansen', '1970-08-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741731', '1-5478671', '9812345891', 'cayden_hansen@gmail.com', 'www.cayden.com', '5412762', '12458062','4578641','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaiden', 'Fernandez', '1970-08-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741732', '1-5478672', '9812345892', 'jaiden_fernandez@gmail.com', 'www.jaiden.com', '5412763', '12458063','4578642','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Taylor', 'Garza', '1970-08-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741733', '1-5478673', '9812345893', 'taylor_garza@gmail.com', 'www.taylor.com', '5412764', '12458064','4578643','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Landen', 'Harvey', '1970-08-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741734', '1-5478674', '9812345894', 'landen_harvey@gmail.com', 'www.landen.com', '5412765', '12458065','4578644','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Troy', 'Little', '1970-08-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741735', '1-5478675', '9812345895', 'troy_little@gmail.com', 'www.troy.com', '5412766', '12458066','4578645','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Julio', 'Burton', '1970-08-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741736', '1-5478676', '9812345896', 'julio_burton@gmail.com', 'www.julio.com', '5412767', '12458067','4578646','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trey', 'Stanley', '1970-08-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741737', '1-5478677', '9812345897', 'trey_stanley@gmail.com', 'www.trey.com', '5412768', '12458068','4578647','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaxon', 'Nguyen', '1970-08-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741738', '1-5478678', '9812345898', 'jaxon_nguyen@gmail.com', 'www.jaxon.com', '5412769', '12458069','4578648','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rafael', 'George', '1970-08-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741739', '1-5478679', '9812345899', 'rafael_george@gmail.com', 'www.rafael.com', '5412770', '12458070','4578649','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dustin', 'Jacobs', '1970-08-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741740', '1-5478680', '9812345900', 'dustin_jacobs@gmail.com', 'www.dustin.com', '5412771', '12458071','4578650','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ruben', 'Reid', '1970-08-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741741', '1-5478681', '9812345901', 'ruben_reid@gmail.com', 'www.ruben.com', '5412772', '12458072','4578651','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Camden', 'Kim', '1970-08-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741742', '1-5478682', '9812345902', 'camden_kim@gmail.com', 'www.camden.com', '5412773', '12458073','4578652','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Frank', 'Fuller', '1970-08-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741743', '1-5478683', '9812345903', 'frank_fuller@gmail.com', 'www.frank.com', '5412774', '12458074','4578653','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Scott', 'Lynch', '1970-08-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741744', '1-5478684', '9812345904', 'scott_lynch@gmail.com', 'www.scott.com', '5412775', '12458075','4578654','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mitchell', 'Dean', '1970-08-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741745', '1-5478685', '9812345905', 'mitchell_dean@gmail.com', 'www.mitchell.com', '5412776', '12458076','4578655','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zane', 'Gilbert', '1970-08-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741746', '1-5478686', '9812345906', 'zane_gilbert@gmail.com', 'www.zane.com', '5412777', '12458077','4578656','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Payton', 'Garrett', '1970-08-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741747', '1-5478687', '9812345907', 'payton_garrett@gmail.com', 'www.payton.com', '5412778', '12458078','4578657','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kai', 'Romero', '1970-08-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741748', '1-5478688', '9812345908', 'kai_romero@gmail.com', 'www.kai.com', '5412779', '12458079','4578658','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keegan', 'Welch', '1970-08-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741749', '1-5478689', '9812345909', 'keegan_welch@gmail.com', 'www.keegan.com', '5412780', '12458080','4578659','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Skyler', 'Larson', '1970-08-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741750', '1-5478690', '9812345910', 'skyler_larson@gmail.com', 'www.skyler.com', '5412781', '12458081','4578660','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brett', 'Frazier', '1970-08-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741751', '1-5478691', '9812345911', 'brett_frazier@gmail.com', 'www.brett.com', '5412782', '12458082','4578661','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Johnny', 'Burke', '1970-08-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741752', '1-5478692', '9812345912', 'johnny_burke@gmail.com', 'www.johnny.com', '5412783', '12458083','4578662','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Griffin', 'Hanson', '1970-09-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741753', '1-5478693', '9812345913', 'griffin_hanson@gmail.com', 'www.griffin.com', '5412784', '12458084','4578663','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marcos', 'Day', '1970-09-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741754', '1-5478694', '9812345914', 'marcos_day@gmail.com', 'www.marcos.com', '5412785', '12458085','4578664','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Beau', 'Stokes', '1971-02-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741927', '1-5478867', '9812346087', 'beau_stokes@gmail.com', 'www.beau.com', '5412958', '12458258','4578837','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Derrick', 'Mendoza', '1970-09-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741755', '1-5478695', '9812345915', 'derrick_mendoza@gmail.com', 'www.derrick.com', '5412786', '12458086','4578665','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Drake', 'Moreno', '1970-09-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741756', '1-5478696', '9812345916', 'drake_moreno@gmail.com', 'www.drake.com', '5412787', '12458087','4578666','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Raul', 'Bowman', '1970-09-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741757', '1-5478697', '9812345917', 'raul_bowman@gmail.com', 'www.raul.com', '5412788', '12458088','4578667','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kaiden', 'Medina', '1970-09-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741758', '1-5478698', '9812345918', 'kaiden_medina@gmail.com', 'www.kaiden.com', '5412789', '12458089','4578668','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gerardo', 'Fowler', '1970-09-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741759', '1-5478699', '9812345919', 'gerardo_fowler@gmail.com', 'www.gerardo.com', '5412790', '12458090','4578669','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braxton', 'Brewer', '1970-09-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741760', '1-5478700', '9812345920', 'braxton_brewer@gmail.com', 'www.braxton.com', '5412791', '12458091','4578670','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Armando', 'Hoffman', '1970-09-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741761', '1-5478701', '9812345921', 'armando_hoffman@gmail.com', 'www.armando.com', '5412792', '12458092','4578671','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Grayson', 'Carlson', '1970-09-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741762', '1-5478702', '9812345922', 'grayson_carlson@gmail.com', 'www.grayson.com', '5412793', '12458093','4578672','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Simon', 'Silva', '1970-09-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741763', '1-5478703', '9812345923', 'simon_silva@gmail.com', 'www.simon.com', '5412794', '12458094','4578673','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kayden', 'Pearson', '1970-09-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741764', '1-5478704', '9812345924', 'kayden_pearson@gmail.com', 'www.kayden.com', '5412795', '12458095','4578674','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ronald', 'Holland', '1970-09-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741765', '1-5478705', '9812345925', 'ronald_holland@gmail.com', 'www.ronald.com', '5412796', '12458096','4578675','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Angelo', 'Douglas', '1970-09-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741766', '1-5478706', '9812345926', 'angelo_douglas@gmail.com', 'www.angelo.com', '5412797', '12458097','4578676','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leo', 'Fleming', '1970-09-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741767', '1-5478707', '9812345927', 'leo_fleming@gmail.com', 'www.leo.com', '5412798', '12458098','4578677','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chance', 'Jensen', '1970-09-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741768', '1-5478708', '9812345928', 'chance_jensen@gmail.com', 'www.chance.com', '5412799', '12458099','4578678','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brock', 'Vargas', '1970-09-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741769', '1-5478709', '9812345929', 'brock_vargas@gmail.com', 'www.brock.com', '5412800', '12458100','4578679','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lukas', 'Byrd', '1970-09-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741770', '1-5478710', '9812345930', 'lukas_byrd@gmail.com', 'www.lukas.com', '5412801', '12458101','4578680','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaime', 'Davidson', '1970-09-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741771', '1-5478711', '9812345931', 'jaime_davidson@gmail.com', 'www.jaime.com', '5412802', '12458102','4578681','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lance', 'Hopkins', '1970-09-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741772', '1-5478712', '9812345932', 'lance_hopkins@gmail.com', 'www.lance.com', '5412803', '12458103','4578682','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Enrique', 'May', '1970-09-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741773', '1-5478713', '9812345933', 'enrique_may@gmail.com', 'www.enrique.com', '5412804', '12458104','4578683','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dante', 'Terry', '1970-09-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741774', '1-5478714', '9812345934', 'dante_terry@gmail.com', 'www.dante.com', '5412805', '12458105','4578684','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Malik', 'Herrera', '1970-09-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741775', '1-5478715', '9812345935', 'malik_herrera@gmail.com', 'www.malik.com', '5412806', '12458106','4578685','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyson', 'Wade', '1970-09-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741776', '1-5478716', '9812345936', 'tyson_wade@gmail.com', 'www.tyson.com', '5412807', '12458107','4578686','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emanuel', 'Soto', '1970-09-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741777', '1-5478717', '9812345937', 'emanuel_soto@gmail.com', 'www.emanuel.com', '5412808', '12458108','4578687','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Phillip', 'Walters', '1970-09-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741778', '1-5478718', '9812345938', 'phillip_walters@gmail.com', 'www.phillip.com', '5412809', '12458109','4578688','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Fabian', 'Curtis', '1970-09-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741779', '1-5478719', '9812345939', 'fabian_curtis@gmail.com', 'www.fabian.com', '5412810', '12458110','4578689','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tucker', 'Neal', '1970-09-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741780', '1-5478720', '9812345940', 'tucker_neal@gmail.com', 'www.tucker.com', '5412811', '12458111','4578690','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trent', 'Caldwell', '1970-09-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741781', '1-5478721', '9812345941', 'trent_caldwell@gmail.com', 'www.trent.com', '5412812', '12458112','4578691','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Allen', 'Lowe', '1970-09-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741782', '1-5478722', '9812345942', 'allen_lowe@gmail.com', 'www.allen.com', '5412813', '12458113','4578692','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jakob', 'Jennings', '1970-10-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741783', '1-5478723', '9812345943', 'jakob_jennings@gmail.com', 'www.jakob.com', '5412814', '12458114','4578693','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hudson', 'Barnett', '1970-10-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741784', '1-5478724', '9812345944', 'hudson_barnett@gmail.com', 'www.hudson.com', '5412815', '12458115','4578694','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emilio', 'Graves', '1970-10-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741785', '1-5478725', '9812345945', 'emilio_graves@gmail.com', 'www.emilio.com', '5412816', '12458116','4578695','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maddox', 'Jimenez', '1970-10-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741786', '1-5478726', '9812345946', 'maddox_jimenez@gmail.com', 'www.maddox.com', '5412817', '12458117','4578696','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Santiago', 'Horton', '1970-10-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741787', '1-5478727', '9812345947', 'santiago_horton@gmail.com', 'www.santiago.com', '5412818', '12458118','4578697','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Xander', 'Shelton', '1970-10-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741788', '1-5478728', '9812345948', 'xander_shelton@gmail.com', 'www.xander.com', '5412819', '12458119','4578698','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aden', 'Barrett', '1970-10-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741789', '1-5478729', '9812345949', 'aden_barrett@gmail.com', 'www.aden.com', '5412820', '12458120','4578699','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rylan', 'Obrien', '1970-10-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741790', '1-5478730', '9812345950', 'rylan_obrien@gmail.com', 'www.rylan.com', '5412821', '12458121','4578700','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kyler', 'Castro', '1970-10-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741791', '1-5478731', '9812345951', 'kyler_castro@gmail.com', 'www.kyler.com', '5412822', '12458122','4578701','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kameron', 'Sutton', '1970-10-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741792', '1-5478732', '9812345952', 'kameron_sutton@gmail.com', 'www.kameron.com', '5412823', '12458123','4578702','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Pablo', 'Gregory', '1970-10-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741793', '1-5478733', '9812345953', 'pablo_gregory@gmail.com', 'www.pablo.com', '5412824', '12458124','4578703','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cade', 'Mckinney', '1970-10-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741794', '1-5478734', '9812345954', 'cade_mckinney@gmail.com', 'www.cade.com', '5412825', '12458125','4578704','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adan', 'Lucas', '1970-10-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741795', '1-5478735', '9812345955', 'adan_lucas@gmail.com', 'www.adan.com', '5412826', '12458126','4578705','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keith', 'Miles', '1970-10-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741796', '1-5478736', '9812345956', 'keith_miles@gmail.com', 'www.keith.com', '5412827', '12458127','4578706','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Asher', 'Craig', '1970-10-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741797', '1-5478737', '9812345957', 'asher_craig@gmail.com', 'www.asher.com', '5412828', '12458128','4578707','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Donald', 'Rodriquez', '1970-10-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741798', '1-5478738', '9812345958', 'donald_rodriquez@gmail.com', 'www.donald.com', '5412829', '12458129','4578708','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alberto', 'Chambers', '1970-10-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741799', '1-5478739', '9812345959', 'alberto_chambers@gmail.com', 'www.alberto.com', '5412830', '12458130','4578709','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alec', 'Holt', '1970-10-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741800', '1-5478740', '9812345960', 'alec_holt@gmail.com', 'www.alec.com', '5412831', '12458131','4578710','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darius', 'Lambert', '1970-10-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741801', '1-5478741', '9812345961', 'darius_lambert@gmail.com', 'www.darius.com', '5412832', '12458132','4578711','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gustavo', 'Fletcher', '1970-10-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741802', '1-5478742', '9812345962', 'gustavo_fletcher@gmail.com', 'www.gustavo.com', '5412833', '12458133','4578712','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Saul', 'Watts', '1970-10-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741803', '1-5478743', '9812345963', 'saul_watts@gmail.com', 'www.saul.com', '5412834', '12458134','4578713','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ryder', 'Bates', '1970-10-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741804', '1-5478744', '9812345964', 'ryder_bates@gmail.com', 'www.ryder.com', '5412835', '12458135','4578714','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zion', 'Hale', '1970-10-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741805', '1-5478745', '9812345965', 'zion_hale@gmail.com', 'www.zion.com', '5412836', '12458136','4578715','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Casey', 'Rhodes', '1970-10-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741806', '1-5478746', '9812345966', 'casey_rhodes@gmail.com', 'www.casey.com', '5412837', '12458137','4578716','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gael', 'Pena', '1970-10-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741807', '1-5478747', '9812345967', 'gael_pena@gmail.com', 'www.gael.com', '5412838', '12458138','4578717','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mathew', 'Beck', '1970-10-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741808', '1-5478748', '9812345968', 'mathew_beck@gmail.com', 'www.mathew.com', '5412839', '12458139','4578718','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Arturo', 'Newman', '1970-10-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741809', '1-5478749', '9812345969', 'arturo_newman@gmail.com', 'www.arturo.com', '5412840', '12458140','4578719','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Randy', 'Haynes', '1970-10-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741810', '1-5478750', '9812345970', 'randy_haynes@gmail.com', 'www.randy.com', '5412841', '12458141','4578720','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mateo', 'Mcdaniel', '1970-10-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741811', '1-5478751', '9812345971', 'mateo_mcdaniel@gmail.com', 'www.mateo.com', '5412842', '12458142','4578721','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Quinn', 'Mendez', '1970-10-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741812', '1-5478752', '9812345972', 'quinn_mendez@gmail.com', 'www.quinn.com', '5412843', '12458143','4578722','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jimmy', 'Bush', '1970-10-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741813', '1-5478753', '9812345973', 'jimmy_bush@gmail.com', 'www.jimmy.com', '5412844', '12458144','4578723','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Theodore', 'Vaughn', '1970-11-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741814', '1-5478754', '9812345974', 'theodore_vaughn@gmail.com', 'www.theodore.com', '5412845', '12458145','4578724','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jude', 'Parks', '1970-11-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741815', '1-5478755', '9812345975', 'jude_parks@gmail.com', 'www.jude.com', '5412846', '12458146','4578725','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sawyer', 'Dawson', '1970-11-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741816', '1-5478756', '9812345976', 'sawyer_dawson@gmail.com', 'www.sawyer.com', '5412847', '12458147','4578726','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zackary', 'Santiago', '1970-11-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741817', '1-5478757', '9812345977', 'zackary_santiago@gmail.com', 'www.zackary.com', '5412848', '12458148','4578727','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ezekiel', 'Norris', '1970-11-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741818', '1-5478758', '9812345978', 'ezekiel_norris@gmail.com', 'www.ezekiel.com', '5412849', '12458149','4578728','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Myles', 'Hardy', '1970-11-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741819', '1-5478759', '9812345979', 'myles_hardy@gmail.com', 'www.myles.com', '5412850', '12458150','4578729','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Corbin', 'Love', '1970-11-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741820', '1-5478760', '9812345980', 'corbin_love@gmail.com', 'www.corbin.com', '5412851', '12458151','4578730','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Danny', 'Steele', '1970-11-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741821', '1-5478761', '9812345981', 'danny_steele@gmail.com', 'www.danny.com', '5412852', '12458152','4578731','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Axel', 'Curry', '1970-11-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741822', '1-5478762', '9812345982', 'axel_curry@gmail.com', 'www.axel.com', '5412853', '12458153','4578732','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brennan', 'Powers', '1970-11-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741823', '1-5478763', '9812345983', 'brennan_powers@gmail.com', 'www.brennan.com', '5412854', '12458154','4578733','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lane', 'Schultz', '1970-11-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741824', '1-5478764', '9812345984', 'lane_schultz@gmail.com', 'www.lane.com', '5412855', '12458155','4578734','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jerry', 'Barker', '1970-11-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741825', '1-5478765', '9812345985', 'jerry_barker@gmail.com', 'www.jerry.com', '5412856', '12458156','4578735','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dennis', 'Guzman', '1970-11-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741826', '1-5478766', '9812345986', 'dennis_guzman@gmail.com', 'www.dennis.com', '5412857', '12458157','4578736','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lorenzo', 'Page', '1970-11-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741827', '1-5478767', '9812345987', 'lorenzo_page@gmail.com', 'www.lorenzo.com', '5412858', '12458158','4578737','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Esteban', 'Munoz', '1970-11-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741828', '1-5478768', '9812345988', 'esteban_munoz@gmail.com', 'www.esteban.com', '5412859', '12458159','4578738','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tony', 'Ball', '1970-11-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741829', '1-5478769', '9812345989', 'tony_ball@gmail.com', 'www.tony.com', '5412860', '12458160','4578739','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brenden', 'Keller', '1970-11-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741830', '1-5478770', '9812345990', 'brenden_keller@gmail.com', 'www.brenden.com', '5412861', '12458161','4578740','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Damon', 'Chandler', '1970-11-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741831', '1-5478771', '9812345991', 'damon_chandler@gmail.com', 'www.damon.com', '5412862', '12458162','4578741','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braeden', 'Weber', '1970-11-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741832', '1-5478772', '9812345992', 'braeden_weber@gmail.com', 'www.braeden.com', '5412863', '12458163','4578742','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Louis', 'Leonard', '1970-11-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741833', '1-5478773', '9812345993', 'louis_leonard@gmail.com', 'www.louis.com', '5412864', '12458164','4578743','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Philip', 'Walsh', '1970-11-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741834', '1-5478774', '9812345994', 'philip_walsh@gmail.com', 'www.philip.com', '5412865', '12458165','4578744','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brayan', 'Lyons', '1970-11-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741835', '1-5478775', '9812345995', 'brayan_lyons@gmail.com', 'www.brayan.com', '5412866', '12458166','4578745','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Curtis', 'Ramsey', '1970-11-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741836', '1-5478776', '9812345996', 'curtis_ramsey@gmail.com', 'www.curtis.com', '5412867', '12458167','4578746','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Charlie', 'Wolfe', '1970-11-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741837', '1-5478777', '9812345997', 'charlie_wolfe@gmail.com', 'www.charlie.com', '5412868', '12458168','4578747','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nickolas', 'Schneider', '1970-11-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741838', '1-5478778', '9812345998', 'nickolas_schneider@gmail.com', 'www.nickolas.com', '5412869', '12458169','4578748','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jayson', 'Mullins', '1970-11-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741839', '1-5478779', '9812345999', 'jayson_mullins@gmail.com', 'www.jayson.com', '5412870', '12458170','4578749','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jonathon', 'Benson', '1970-11-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741840', '1-5478780', '9812346000', 'jonathon_benson@gmail.com', 'www.jonathon.com', '5412871', '12458171','4578750','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zander', 'Sharp', '1970-11-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741841', '1-5478781', '9812346001', 'zander_sharp@gmail.com', 'www.zander.com', '5412872', '12458172','4578751','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nikolas', 'Bowen', '1970-11-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741842', '1-5478782', '9812346002', 'nikolas_bowen@gmail.com', 'www.nikolas.com', '5412873', '12458173','4578752','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Quentin', 'Daniel', '1970-11-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741843', '1-5478783', '9812346003', 'quentin_daniel@gmail.com', 'www.quentin.com', '5412874', '12458174','4578753','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Morgan', 'Barber', '1970-12-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741844', '1-5478784', '9812346004', 'morgan_barber@gmail.com', 'www.morgan.com', '5412875', '12458175','4578754','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ismael', 'Cummings', '1970-12-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741845', '1-5478785', '9812346005', 'ismael_cummings@gmail.com', 'www.ismael.com', '5412876', '12458176','4578755','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emiliano', 'Hines', '1970-12-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741846', '1-5478786', '9812346006', 'emiliano_hines@gmail.com', 'www.emiliano.com', '5412877', '12458177','4578756','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gary', 'Baldwin', '1970-12-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741847', '1-5478787', '9812346007', 'gary_baldwin@gmail.com', 'www.gary.com', '5412878', '12458178','4578757','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tristen', 'Griffith', '1970-12-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741848', '1-5478788', '9812346008', 'tristen_griffith@gmail.com', 'www.tristen.com', '5412879', '12458179','4578758','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chandler', 'Valdez', '1970-12-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741849', '1-5478789', '9812346009', 'chandler_valdez@gmail.com', 'www.chandler.com', '5412880', '12458180','4578759','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Amir', 'Hubbard', '1970-12-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741850', '1-5478790', '9812346010', 'amir_hubbard@gmail.com', 'www.amir.com', '5412881', '12458181','4578760','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darren', 'Salazar', '1970-12-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741851', '1-5478791', '9812346011', 'darren_salazar@gmail.com', 'www.darren.com', '5412882', '12458182','4578761','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Albert', 'Reeves', '1970-12-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741852', '1-5478792', '9812346012', 'albert_reeves@gmail.com', 'www.albert.com', '5412883', '12458183','4578762','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Salvador', 'Warner', '1970-12-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741853', '1-5478793', '9812346013', 'salvador_warner@gmail.com', 'www.salvador.com', '5412884', '12458184','4578763','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mekhi', 'Stevenson', '1970-12-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741854', '1-5478794', '9812346014', 'mekhi_stevenson@gmail.com', 'www.mekhi.com', '5412885', '12458185','4578764','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Abel', 'Burgess', '1970-12-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741855', '1-5478795', '9812346015', 'abel_burgess@gmail.com', 'www.abel.com', '5412886', '12458186','4578765','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Joaquin', 'Santos', '1970-12-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741856', '1-5478796', '9812346016', 'joaquin_santos@gmail.com', 'www.joaquin.com', '5412887', '12458187','4578766','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Caiden', 'Tate', '1970-12-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741857', '1-5478797', '9812346017', 'caiden_tate@gmail.com', 'www.caiden.com', '5412888', '12458188','4578767','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jay', 'Cross', '1970-12-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741858', '1-5478798', '9812346018', 'jay_cross@gmail.com', 'www.jay.com', '5412889', '12458189','4578768','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Declan', 'Garner', '1970-12-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741859', '1-5478799', '9812346019', 'declan_garner@gmail.com', 'www.declan.com', '5412890', '12458190','4578769','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Julius', 'Mann', '1970-12-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741860', '1-5478800', '9812346020', 'julius_mann@gmail.com', 'www.julius.com', '5412891', '12458191','4578770','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alfredo', 'Mack', '1970-12-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741861', '1-5478801', '9812346021', 'alfredo_mack@gmail.com', 'www.alfredo.com', '5412892', '12458192','4578771','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Camron', 'Moss', '1970-12-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741862', '1-5478802', '9812346022', 'camron_moss@gmail.com', 'www.camron.com', '5412893', '12458193','4578772','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maximilian', 'Thornton', '1970-12-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741863', '1-5478803', '9812346023', 'maximilian_thornton@gmail.com', 'www.maximilian.com', '5412894', '12458194','4578773','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Arthur', 'Dennis', '1970-12-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741864', '1-5478804', '9812346024', 'arthur_dennis@gmail.com', 'www.arthur.com', '5412895', '12458195','4578774','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Holden', 'Mcgee', '1970-12-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741865', '1-5478805', '9812346025', 'holden_mcgee@gmail.com', 'www.holden.com', '5412896', '12458196','4578775','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Larry', 'Farmer', '1970-12-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741866', '1-5478806', '9812346026', 'larry_farmer@gmail.com', 'www.larry.com', '5412897', '12458197','4578776','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ezra', 'Delgado', '1970-12-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741867', '1-5478807', '9812346027', 'ezra_delgado@gmail.com', 'www.ezra.com', '5412898', '12458198','4578777','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Moises', 'Aguilar', '1970-12-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741868', '1-5478808', '9812346028', 'moises_aguilar@gmail.com', 'www.moises.com', '5412899', '12458199','4578778','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Douglas', 'Vega', '1970-12-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741869', '1-5478809', '9812346029', 'douglas_vega@gmail.com', 'www.douglas.com', '5412900', '12458200','4578779','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Orlando', 'Glover', '1970-12-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741870', '1-5478810', '9812346030', 'orlando_glover@gmail.com', 'www.orlando.com', '5412901', '12458201','4578780','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keaton', 'Manning', '1970-12-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741871', '1-5478811', '9812346031', 'keaton_manning@gmail.com', 'www.keaton.com', '5412902', '12458202','4578781','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braylon', 'Cohen', '1970-12-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741872', '1-5478812', '9812346032', 'braylon_cohen@gmail.com', 'www.braylon.com', '5412903', '12458203','4578782','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ramon', 'Harmon', '1970-12-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741873', '1-5478813', '9812346033', 'ramon_harmon@gmail.com', 'www.ramon.com', '5412904', '12458204','4578783','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bryant', 'Rodgers', '1970-12-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741874', '1-5478814', '9812346034', 'bryant_rodgers@gmail.com', 'www.bryant.com', '5412905', '12458205','4578784','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dallas', 'Robbins', '1971-01-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741875', '1-5478815', '9812346035', 'dallas_robbins@gmail.com', 'www.dallas.com', '5412906', '12458206','4578785','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Walker', 'Newton', '1971-01-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741876', '1-5478816', '9812346036', 'walker_newton@gmail.com', 'www.walker.com', '5412907', '12458207','4578786','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mauricio', 'Todd', '1971-01-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741877', '1-5478817', '9812346037', 'mauricio_todd@gmail.com', 'www.mauricio.com', '5412908', '12458208','4578787','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marvin', 'Blair', '1971-01-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741878', '1-5478818', '9812346038', 'marvin_blair@gmail.com', 'www.marvin.com', '5412909', '12458209','4578788','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ernesto', 'Higgins', '1971-01-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741879', '1-5478819', '9812346039', 'ernesto_higgins@gmail.com', 'www.ernesto.com', '5412910', '12458210','4578789','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hugo', 'Ingram', '1971-01-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741880', '1-5478820', '9812346040', 'hugo_ingram@gmail.com', 'www.hugo.com', '5412911', '12458211','4578790','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Joe', 'Reese', '1971-01-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741881', '1-5478821', '9812346041', 'joe_reese@gmail.com', 'www.joe.com', '5412912', '12458212','4578791','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reece', 'Cannon', '1971-01-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741882', '1-5478822', '9812346042', 'reece_cannon@gmail.com', 'www.reece.com', '5412913', '12458213','4578792','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Felix', 'Strickland', '1971-01-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741883', '1-5478823', '9812346043', 'felix_strickland@gmail.com', 'www.felix.com', '5412914', '12458214','4578793','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Yahir', 'Townsend', '1971-01-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741884', '1-5478824', '9812346044', 'yahir_townsend@gmail.com', 'www.yahir.com', '5412915', '12458215','4578794','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Walter', 'Potter', '1971-01-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741885', '1-5478825', '9812346045', 'walter_potter@gmail.com', 'www.walter.com', '5412916', '12458216','4578795','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cory', 'Goodwin', '1971-01-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741886', '1-5478826', '9812346046', 'cory_goodwin@gmail.com', 'www.cory.com', '5412917', '12458217','4578796','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tate', 'Walton', '1971-01-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741887', '1-5478827', '9812346047', 'tate_walton@gmail.com', 'www.tate.com', '5412918', '12458218','4578797','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ricky', 'Rowe', '1971-01-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741888', '1-5478828', '9812346048', 'ricky_rowe@gmail.com', 'www.ricky.com', '5412919', '12458219','4578798','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chad', 'Hampton', '1971-01-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741889', '1-5478829', '9812346049', 'chad_hampton@gmail.com', 'www.chad.com', '5412920', '12458220','4578799','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maximus', 'Ortega', '1971-01-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741890', '1-5478830', '9812346050', 'maximus_ortega@gmail.com', 'www.maximus.com', '5412921', '12458221','4578800','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dean', 'Patton', '1971-01-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741891', '1-5478831', '9812346051', 'dean_patton@gmail.com', 'www.dean.com', '5412922', '12458222','4578801','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marc', 'Swanson', '1971-01-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741892', '1-5478832', '9812346052', 'marc_swanson@gmail.com', 'www.marc.com', '5412923', '12458223','4578802','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braydon', 'Joseph', '1971-01-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741893', '1-5478833', '9812346053', 'braydon_joseph@gmail.com', 'www.braydon.com', '5412924', '12458224','4578803','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ali', 'Francis', '1971-01-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741894', '1-5478834', '9812346054', 'ali_francis@gmail.com', 'www.ali.com', '5412925', '12458225','4578804','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elliot', 'Goodman', '1971-01-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741895', '1-5478835', '9812346055', 'elliot_goodman@gmail.com', 'www.elliot.com', '5412926', '12458226','4578805','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jonas', 'Maldonado', '1971-01-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741896', '1-5478836', '9812346056', 'jonas_maldonado@gmail.com', 'www.jonas.com', '5412927', '12458227','4578806','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Weston', 'Yates', '1971-01-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741897', '1-5478837', '9812346057', 'weston_yates@gmail.com', 'www.weston.com', '5412928', '12458228','4578807','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaxson', 'Becker', '1971-01-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741898', '1-5478838', '9812346058', 'jaxson_becker@gmail.com', 'www.jaxson.com', '5412929', '12458229','4578808','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isiah', 'Erickson', '1971-01-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741899', '1-5478839', '9812346059', 'isiah_erickson@gmail.com', 'www.isiah.com', '5412930', '12458230','4578809','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rodrigo', 'Hodges', '1971-01-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741900', '1-5478840', '9812346060', 'rodrigo_hodges@gmail.com', 'www.rodrigo.com', '5412931', '12458231','4578810','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Davis', 'Rios', '1971-01-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741901', '1-5478841', '9812346061', 'davis_rios@gmail.com', 'www.davis.com', '5412932', '12458232','4578811','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Easton', 'Conner', '1971-01-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741902', '1-5478842', '9812346062', 'easton_conner@gmail.com', 'www.easton.com', '5412933', '12458233','4578812','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Russell', 'Adkins', '1971-01-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741903', '1-5478843', '9812346063', 'russell_adkins@gmail.com', 'www.russell.com', '5412934', '12458234','4578813','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bennett', 'Webster', '1971-01-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741904', '1-5478844', '9812346064', 'bennett_webster@gmail.com', 'www.bennett.com', '5412935', '12458235','4578814','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lawrence', 'Norman', '1971-01-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741905', '1-5478845', '9812346065', 'lawrence_norman@gmail.com', 'www.lawrence.com', '5412936', '12458236','4578815','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chris', 'Malone', '1971-02-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741906', '1-5478846', '9812346066', 'chris_malone@gmail.com', 'www.chris.com', '5412937', '12458237','4578816','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shaun', 'Hammond', '1971-02-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741907', '1-5478847', '9812346067', 'shaun_hammond@gmail.com', 'www.shaun.com', '5412938', '12458238','4578817','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nasir', 'Flowers', '1971-02-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741908', '1-5478848', '9812346068', 'nasir_flowers@gmail.com', 'www.nasir.com', '5412939', '12458239','4578818','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kristopher', 'Cobb', '1971-02-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741909', '1-5478849', '9812346069', 'kristopher_cobb@gmail.com', 'www.kristopher.com', '5412940', '12458240','4578819','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Luca', 'Moody', '1971-02-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741910', '1-5478850', '9812346070', 'luca_moody@gmail.com', 'www.luca.com', '5412941', '12458241','4578820','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Uriel', 'Quinn', '1971-02-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741911', '1-5478851', '9812346071', 'uriel_quinn@gmail.com', 'www.uriel.com', '5412942', '12458242','4578821','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Eddie', 'Blake', '1971-02-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741912', '1-5478852', '9812346072', 'eddie_blake@gmail.com', 'www.eddie.com', '5412943', '12458243','4578822','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Javon', 'Maxwell', '1971-02-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741913', '1-5478853', '9812346073', 'javon_maxwell@gmail.com', 'www.javon.com', '5412944', '12458244','4578823','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Issac', 'Pope', '1971-02-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741914', '1-5478854', '9812346074', 'issac_pope@gmail.com', 'www.issac.com', '5412945', '12458245','4578824','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reese', 'Floyd', '1971-02-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741915', '1-5478855', '9812346075', 'reese_floyd@gmail.com', 'www.reese.com', '5412946', '12458246','4578825','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Terry', 'Osborne', '1971-02-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741916', '1-5478856', '9812346076', 'terry_osborne@gmail.com', 'www.terry.com', '5412947', '12458247','4578826','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Micheal', 'Paul', '1971-02-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741917', '1-5478857', '9812346077', 'micheal_paul@gmail.com', 'www.micheal.com', '5412948', '12458248','4578827','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Graham', 'Mccarthy', '1971-02-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741918', '1-5478858', '9812346078', 'graham_mccarthy@gmail.com', 'www.graham.com', '5412949', '12458249','4578828','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Amari', 'Guerrero', '1971-02-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741919', '1-5478859', '9812346079', 'amari_guerrero@gmail.com', 'www.amari.com', '5412950', '12458250','4578829','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zachariah', 'Lindsey', '1971-02-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741920', '1-5478860', '9812346080', 'zachariah_lindsey@gmail.com', 'www.zachariah.com', '5412951', '12458251','4578830','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Silas', 'Estrada', '1971-02-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741921', '1-5478861', '9812346081', 'silas_estrada@gmail.com', 'www.silas.com', '5412952', '12458252','4578831','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carl', 'Sandoval', '1971-02-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741922', '1-5478862', '9812346082', 'carl_sandoval@gmail.com', 'www.carl.com', '5412953', '12458253','4578832','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maurice', 'Gibbs', '1971-02-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741923', '1-5478863', '9812346083', 'maurice_gibbs@gmail.com', 'www.maurice.com', '5412954', '12458254','4578833','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kade', 'Tyler', '1971-02-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741924', '1-5478864', '9812346084', 'kade_tyler@gmail.com', 'www.kade.com', '5412955', '12458255','4578834','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elliott', 'Gross', '1971-02-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741925', '1-5478865', '9812346085', 'elliott_gross@gmail.com', 'www.elliott.com', '5412956', '12458256','4578835','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Roger', 'Fitzgerald', '1971-02-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741926', '1-5478866', '9812346086', 'roger_fitzgerald@gmail.com', 'www.roger.com', '5412957', '12458257','4578836','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamarion', 'Doyle', '1971-02-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741928', '1-5478868', '9812346088', 'jamarion_doyle@gmail.com', 'www.jamarion.com', '5412959', '12458259','4578838','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Omarion', 'Sherman', '1971-02-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741929', '1-5478869', '9812346089', 'omarion_sherman@gmail.com', 'www.omarion.com', '5412960', '12458260','4578839','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leonel', 'Saunders', '1971-02-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741930', '1-5478870', '9812346090', 'leonel_saunders@gmail.com', 'www.leonel.com', '5412961', '12458261','4578840','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marshall', 'Wise', '1971-02-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741931', '1-5478871', '9812346091', 'marshall_wise@gmail.com', 'www.marshall.com', '5412962', '12458262','4578841','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reid', 'Colon', '1971-02-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741932', '1-5478872', '9812346092', 'reid_colon@gmail.com', 'www.reid.com', '5412963', '12458263','4578842','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jadon', 'Gill', '1971-02-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741933', '1-5478873', '9812346093', 'jadon_gill@gmail.com', 'www.jadon.com', '5412964', '12458264','4578843','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamari', 'Alvarado', '1971-03-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741934', '1-5478874', '9812346094', 'jamari_alvarado@gmail.com', 'www.jamari.com', '5412965', '12458265','4578844','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dorian', 'Greer', '1971-03-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741935', '1-5478875', '9812346095', 'dorian_greer@gmail.com', 'www.dorian.com', '5412966', '12458266','4578845','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Noe', 'Padilla', '1971-03-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741936', '1-5478876', '9812346096', 'noe_padilla@gmail.com', 'www.noe.com', '5412967', '12458267','4578846','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tommy', 'Simon', '1971-03-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741937', '1-5478877', '9812346097', 'tommy_simon@gmail.com', 'www.tommy.com', '5412968', '12458268','4578847','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Todd', 'Kemp', '1971-08-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742097', '1-5479037', '9812346257', 'todd_kemp@gmail.com', 'www.todd.com', '5413128', '12458428','4579007','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zachery', 'Waters', '1971-03-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741938', '1-5478878', '9812346098', 'zachery_waters@gmail.com', 'www.zachery.com', '5412969', '12458269','4578848','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Davion', 'Nunez', '1971-03-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741939', '1-5478879', '9812346099', 'davion_nunez@gmail.com', 'www.davion.com', '5412970', '12458270','4578849','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kelvin', 'Ballard', '1971-03-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741940', '1-5478880', '9812346100', 'kelvin_ballard@gmail.com', 'www.kelvin.com', '5412971', '12458271','4578850','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cohen', 'Schwartz', '1971-03-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741941', '1-5478881', '9812346101', 'cohen_schwartz@gmail.com', 'www.cohen.com', '5412972', '12458272','4578851','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jon', 'Mcbride', '1971-03-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741942', '1-5478882', '9812346102', 'jon_mcbride@gmail.com', 'www.jon.com', '5412973', '12458273','4578852','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Melvin', 'Houston', '1971-03-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741943', '1-5478883', '9812346103', 'melvin_houston@gmail.com', 'www.melvin.com', '5412974', '12458274','4578853','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Guillermo', 'Christensen', '1971-03-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741944', '1-5478884', '9812346104', 'guillermo_christensen@gmail.com', 'www.guillermo.com', '5412975', '12458275','4578854','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaylin', 'Klein', '1971-03-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741945', '1-5478885', '9812346105', 'jaylin_klein@gmail.com', 'www.jaylin.com', '5412976', '12458276','4578855','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jeffery', 'Pratt', '1971-03-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741946', '1-5478886', '9812346106', 'jeffery_pratt@gmail.com', 'www.jeffery.com', '5412977', '12458277','4578856','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaydon', 'Briggs', '1971-03-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741947', '1-5478887', '9812346107', 'jaydon_briggs@gmail.com', 'www.jaydon.com', '5412978', '12458278','4578857','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nelson', 'Parsons', '1971-03-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741948', '1-5478888', '9812346108', 'nelson_parsons@gmail.com', 'www.nelson.com', '5412979', '12458279','4578858','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deandre', 'Mclaughlin', '1971-03-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741949', '1-5478889', '9812346109', 'deandre_mclaughlin@gmail.com', 'www.deandre.com', '5412980', '12458280','4578859','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rowan', 'Zimmerman', '1971-03-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741950', '1-5478890', '9812346110', 'rowan_zimmerman@gmail.com', 'www.rowan.com', '5412981', '12458281','4578860','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Noel', 'French', '1971-03-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741951', '1-5478891', '9812346111', 'noel_french@gmail.com', 'www.noel.com', '5412982', '12458282','4578861','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Justice', 'Buchanan', '1971-03-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741952', '1-5478892', '9812346112', 'justice_buchanan@gmail.com', 'www.justice.com', '5412983', '12458283','4578862','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Branden', 'Moran', '1971-03-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741953', '1-5478893', '9812346113', 'branden_moran@gmail.com', 'www.branden.com', '5412984', '12458284','4578863','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Felipe', 'Copeland', '1971-03-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741954', '1-5478894', '9812346114', 'felipe_copeland@gmail.com', 'www.felipe.com', '5412985', '12458285','4578864','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jessie', 'Roy', '1971-03-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741955', '1-5478895', '9812346115', 'jessie_roy@gmail.com', 'www.jessie.com', '5412986', '12458286','4578865','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kristian', 'Pittman', '1971-03-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741956', '1-5478896', '9812346116', 'kristian_pittman@gmail.com', 'www.kristian.com', '5412987', '12458287','4578866','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rodney', 'Brady', '1971-03-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741957', '1-5478897', '9812346117', 'rodney_brady@gmail.com', 'www.rodney.com', '5412988', '12458288','4578867','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jermaine', 'Mccormick', '1971-03-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741958', '1-5478898', '9812346118', 'jermaine_mccormick@gmail.com', 'www.jermaine.com', '5412989', '12458289','4578868','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Frederick', 'Holloway', '1971-03-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741959', '1-5478899', '9812346119', 'frederick_holloway@gmail.com', 'www.frederick.com', '5412990', '12458290','4578869','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nathanael', 'Brock', '1971-03-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741960', '1-5478900', '9812346120', 'nathanael_brock@gmail.com', 'www.nathanael.com', '5412991', '12458291','4578870','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Franklin', 'Poole', '1971-03-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741961', '1-5478901', '9812346121', 'franklin_poole@gmail.com', 'www.franklin.com', '5412992', '12458292','4578871','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dane', 'Frank', '1971-03-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741962', '1-5478902', '9812346122', 'dane_frank@gmail.com', 'www.dane.com', '5412993', '12458293','4578872','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Khalil', 'Logan', '1971-03-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741963', '1-5478903', '9812346123', 'khalil_logan@gmail.com', 'www.khalil.com', '5412994', '12458294','4578873','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brent', 'Owen', '1971-03-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741964', '1-5478904', '9812346124', 'brent_owen@gmail.com', 'www.brent.com', '5412995', '12458295','4578874','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Billy', 'Bass', '1971-04-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741965', '1-5478905', '9812346125', 'billy_bass@gmail.com', 'www.billy.com', '5412996', '12458296','4578875','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jayce', 'Marsh', '1971-04-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741966', '1-5478906', '9812346126', 'jayce_marsh@gmail.com', 'www.jayce.com', '5412997', '12458297','4578876','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Terrance', 'Drake', '1971-04-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741967', '1-5478907', '9812346127', 'terrance_drake@gmail.com', 'www.terrance.com', '5412998', '12458298','4578877','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kenny', 'Wong', '1971-04-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741968', '1-5478908', '9812346128', 'kenny_wong@gmail.com', 'www.kenny.com', '5412999', '12458299','4578878','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Quinton', 'Jefferson', '1971-04-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741969', '1-5478909', '9812346129', 'quinton_jefferson@gmail.com', 'www.quinton.com', '5413000', '12458300','4578879','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Allan', 'Park', '1971-04-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741970', '1-5478910', '9812346130', 'allan_park@gmail.com', 'www.allan.com', '5413001', '12458301','4578880','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Skylar', 'Morton', '1971-04-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741971', '1-5478911', '9812346131', 'skylar_morton@gmail.com', 'www.skylar.com', '5413002', '12458302','4578881','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sam', 'Abbott', '1971-04-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741972', '1-5478912', '9812346132', 'sam_abbott@gmail.com', 'www.sam.com', '5413003', '12458303','4578882','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamal', 'Sparks', '1971-04-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741973', '1-5478913', '9812346133', 'jamal_sparks@gmail.com', 'www.jamal.com', '5413004', '12458304','4578883','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rogelio', 'Patrick', '1971-04-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741974', '1-5478914', '9812346134', 'rogelio_patrick@gmail.com', 'www.rogelio.com', '5413005', '12458305','4578884','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nehemiah', 'Norton', '1971-04-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741975', '1-5478915', '9812346135', 'nehemiah_norton@gmail.com', 'www.nehemiah.com', '5413006', '12458306','4578885','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Quincy', 'Huff', '1971-04-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741976', '1-5478916', '9812346136', 'quincy_huff@gmail.com', 'www.quincy.com', '5413007', '12458307','4578886','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Izaiah', 'Clayton', '1971-04-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741977', '1-5478917', '9812346137', 'izaiah_clayton@gmail.com', 'www.izaiah.com', '5413008', '12458308','4578887','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ahmad', 'Massey', '1971-04-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741978', '1-5478918', '9812346138', 'ahmad_massey@gmail.com', 'www.ahmad.com', '5413009', '12458309','4578888','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reed', 'Lloyd', '1971-04-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741979', '1-5478919', '9812346139', 'reed_lloyd@gmail.com', 'www.reed.com', '5413010', '12458310','4578889','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Roy', 'Figueroa', '1971-04-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741980', '1-5478920', '9812346140', 'roy_figueroa@gmail.com', 'www.roy.com', '5413011', '12458311','4578890','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brendon', 'Carson', '1971-04-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741981', '1-5478921', '9812346141', 'brendon_carson@gmail.com', 'www.brendon.com', '5413012', '12458312','4578891','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Desmond', 'Bowers', '1971-04-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741982', '1-5478922', '9812346142', 'desmond_bowers@gmail.com', 'www.desmond.com', '5413013', '12458313','4578892','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rene', 'Roberson', '1971-04-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741983', '1-5478923', '9812346143', 'rene_roberson@gmail.com', 'www.rene.com', '5413014', '12458314','4578893','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mohamed', 'Barton', '1971-04-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741984', '1-5478924', '9812346144', 'mohamed_barton@gmail.com', 'www.mohamed.com', '5413015', '12458315','4578894','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kody', 'Tran', '1971-04-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741985', '1-5478925', '9812346145', 'kody_tran@gmail.com', 'www.kody.com', '5413016', '12458316','4578895','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Osvaldo', 'Lamb', '1971-04-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741986', '1-5478926', '9812346146', 'osvaldo_lamb@gmail.com', 'www.osvaldo.com', '5413017', '12458317','4578896','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Phoenix', 'Harrington', '1971-04-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741987', '1-5478927', '9812346147', 'phoenix_harrington@gmail.com', 'www.phoenix.com', '5413018', '12458318','4578897','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Toby', 'Casey', '1971-04-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741988', '1-5478928', '9812346148', 'toby_casey@gmail.com', 'www.toby.com', '5413019', '12458319','4578898','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaylon', 'Boone', '1971-04-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741989', '1-5478929', '9812346149', 'jaylon_boone@gmail.com', 'www.jaylon.com', '5413020', '12458320','4578899','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Wilson', 'Cortez', '1971-04-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741990', '1-5478930', '9812346150', 'wilson_cortez@gmail.com', 'www.wilson.com', '5413021', '12458321','4578900','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Terrell', 'Clarke', '1971-04-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741991', '1-5478931', '9812346151', 'terrell_clarke@gmail.com', 'www.terrell.com', '5413022', '12458322','4578901','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jameson', 'Mathis', '1971-04-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741992', '1-5478932', '9812346152', 'jameson_mathis@gmail.com', 'www.jameson.com', '5413023', '12458323','4578902','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Conor', 'Singleton', '1971-04-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741993', '1-5478933', '9812346153', 'conor_singleton@gmail.com', 'www.conor.com', '5413024', '12458324','4578903','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alvin', 'Wilkins', '1971-04-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741994', '1-5478934', '9812346154', 'alvin_wilkins@gmail.com', 'www.alvin.com', '5413025', '12458325','4578904','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Solomon', 'Cain', '1971-05-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741995', '1-5478935', '9812346155', 'solomon_cain@gmail.com', 'www.solomon.com', '5413026', '12458326','4578905','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tomas', 'Bryan', '1971-05-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741996', '1-5478936', '9812346156', 'tomas_bryan@gmail.com', 'www.tomas.com', '5413027', '12458327','4578906','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tobias', 'Underwood', '1971-05-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741997', '1-5478937', '9812346157', 'tobias_underwood@gmail.com', 'www.tobias.com', '5413028', '12458328','4578907','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Triston', 'Hogan', '1971-05-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741998', '1-5478938', '9812346158', 'triston_hogan@gmail.com', 'www.triston.com', '5413029', '12458329','4578908','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bobby', 'Mckenzie', '1971-05-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5741999', '1-5478939', '9812346159', 'bobby_mckenzie@gmail.com', 'www.bobby.com', '5413030', '12458330','4578909','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Pierce', 'Collier', '1971-05-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742000', '1-5478940', '9812346160', 'pierce_collier@gmail.com', 'www.pierce.com', '5413031', '12458331','4578910','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lincoln', 'Luna', '1971-05-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742001', '1-5478941', '9812346161', 'lincoln_luna@gmail.com', 'www.lincoln.com', '5413032', '12458332','4578911','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Byron', 'Phelps', '1971-05-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742002', '1-5478942', '9812346162', 'byron_phelps@gmail.com', 'www.byron.com', '5413033', '12458333','4578912','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cyrus', 'Mcguire', '1971-05-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742003', '1-5478943', '9812346163', 'cyrus_mcguire@gmail.com', 'www.cyrus.com', '5413034', '12458334','4578913','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rodolfo', 'Allison', '1971-05-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742004', '1-5478944', '9812346164', 'rodolfo_allison@gmail.com', 'www.rodolfo.com', '5413035', '12458335','4578914','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trevon', 'Bridges', '1971-05-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742005', '1-5478945', '9812346165', 'trevon_bridges@gmail.com', 'www.trevon.com', '5413036', '12458336','4578915','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Will', 'Wilkerson', '1971-05-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742006', '1-5478946', '9812346166', 'will_wilkerson@gmail.com', 'www.will.com', '5413037', '12458337','4578916','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rohan', 'Nash', '1971-05-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742007', '1-5478947', '9812346167', 'rohan_nash@gmail.com', 'www.rohan.com', '5413038', '12458338','4578917','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Demetrius', 'Summers', '1971-05-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742008', '1-5478948', '9812346168', 'demetrius_summers@gmail.com', 'www.demetrius.com', '5413039', '12458339','4578918','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Craig', 'Atkins', '1971-05-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742009', '1-5478949', '9812346169', 'craig_atkins@gmail.com', 'www.craig.com', '5413040', '12458340','4578919','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Anderson', 'Wilcox', '1971-05-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742010', '1-5478950', '9812346170', 'anderson_wilcox@gmail.com', 'www.anderson.com', '5413041', '12458341','4578920','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zackery', 'Pitts', '1971-05-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742011', '1-5478951', '9812346171', 'zackery_pitts@gmail.com', 'www.zackery.com', '5413042', '12458342','4578921','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bruce', 'Conley', '1971-05-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742012', '1-5478952', '9812346172', 'bruce_conley@gmail.com', 'www.bruce.com', '5413043', '12458343','4578922','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reginald', 'Marquez', '1971-05-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742013', '1-5478953', '9812346173', 'reginald_marquez@gmail.com', 'www.reginald.com', '5413044', '12458344','4578923','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adolfo', 'Burnett', '1971-05-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742014', '1-5478954', '9812346174', 'adolfo_burnett@gmail.com', 'www.adolfo.com', '5413045', '12458345','4578924','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Damion', 'Richard', '1971-05-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742015', '1-5478955', '9812346175', 'damion_richard@gmail.com', 'www.damion.com', '5413046', '12458346','4578925','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Wade', 'Cochran', '1971-05-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742016', '1-5478956', '9812346176', 'wade_cochran@gmail.com', 'www.wade.com', '5413047', '12458347','4578926','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jett', 'Chase', '1971-05-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742017', '1-5478957', '9812346177', 'jett_chase@gmail.com', 'www.jett.com', '5413048', '12458348','4578927','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Harley', 'Davenport', '1971-05-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742018', '1-5478958', '9812346178', 'harley_davenport@gmail.com', 'www.harley.com', '5413049', '12458349','4578928','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Joey', 'Hood', '1971-05-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742019', '1-5478959', '9812346179', 'joey_hood@gmail.com', 'www.joey.com', '5413050', '12458350','4578929','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marlon', 'Gates', '1971-05-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742020', '1-5478960', '9812346180', 'marlon_gates@gmail.com', 'www.marlon.com', '5413051', '12458351','4578930','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bailey', 'Clay', '1971-05-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742021', '1-5478961', '9812346181', 'bailey_clay@gmail.com', 'www.bailey.com', '5413052', '12458352','4578931','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isaias', 'Ayala', '1971-05-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742022', '1-5478962', '9812346182', 'isaias_ayala@gmail.com', 'www.isaias.com', '5413053', '12458353','4578932','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Karl', 'Berg', '1972-01-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742268', '1-5479208', '9812346428', 'karl_berg@gmail.com', 'www.karl.com', '5413299', '12458599','4579178','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Steve', 'Sawyer', '1971-05-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742023', '1-5478963', '9812346183', 'steve_sawyer@gmail.com', 'www.steve.com', '5413054', '12458354','4578933','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cruz', 'Roman', '1971-05-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742024', '1-5478964', '9812346184', 'cruz_roman@gmail.com', 'www.cruz.com', '5413055', '12458355','4578934','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'River', 'Vazquez', '1971-05-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742025', '1-5478965', '9812346185', 'river_vazquez@gmail.com', 'www.river.com', '5413056', '12458356','4578935','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Willie', 'Dickerson', '1971-06-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742026', '1-5478966', '9812346186', 'willie_dickerson@gmail.com', 'www.willie.com', '5413057', '12458357','4578936','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kellen', 'Hodge', '1971-06-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742027', '1-5478967', '9812346187', 'kellen_hodge@gmail.com', 'www.kellen.com', '5413058', '12458358','4578937','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gerald', 'Acosta', '1971-06-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742028', '1-5478968', '9812346188', 'gerald_acosta@gmail.com', 'www.gerald.com', '5413059', '12458359','4578938','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Grady', 'Flynn', '1971-06-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742029', '1-5478969', '9812346189', 'grady_flynn@gmail.com', 'www.grady.com', '5413060', '12458360','4578939','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Blaine', 'Espinoza', '1971-06-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742030', '1-5478970', '9812346190', 'blaine_espinoza@gmail.com', 'www.blaine.com', '5413061', '12458361','4578940','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kendall', 'Nicholson', '1971-06-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742031', '1-5478971', '9812346191', 'kendall_nicholson@gmail.com', 'www.kendall.com', '5413062', '12458362','4578941','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Judah', 'Monroe', '1971-06-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742032', '1-5478972', '9812346192', 'judah_monroe@gmail.com', 'www.judah.com', '5413063', '12458363','4578942','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leon', 'Wolf', '1971-06-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742033', '1-5478973', '9812346193', 'leon_wolf@gmail.com', 'www.leon.com', '5413064', '12458364','4578943','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marquis', 'Morrow', '1971-06-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742034', '1-5478974', '9812346194', 'marquis_morrow@gmail.com', 'www.marquis.com', '5413065', '12458365','4578944','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Harry', 'Kirk', '1971-06-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742035', '1-5478975', '9812346195', 'harry_kirk@gmail.com', 'www.harry.com', '5413066', '12458366','4578945','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Francis', 'Randall', '1971-06-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742036', '1-5478976', '9812346196', 'francis_randall@gmail.com', 'www.francis.com', '5413067', '12458367','4578946','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deven', 'Anthony', '1971-06-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742037', '1-5478977', '9812346197', 'deven_anthony@gmail.com', 'www.deven.com', '5413068', '12458368','4578947','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gilberto', 'Whitaker', '1971-06-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742038', '1-5478978', '9812346198', 'gilberto_whitaker@gmail.com', 'www.gilberto.com', '5413069', '12458369','4578948','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alfonso', 'Oconnor', '1971-06-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742039', '1-5478979', '9812346199', 'alfonso_oconnor@gmail.com', 'www.alfonso.com', '5413070', '12458370','4578949','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Everett', 'Skinner', '1971-06-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742040', '1-5478980', '9812346200', 'everett_skinner@gmail.com', 'www.everett.com', '5413071', '12458371','4578950','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dayton', 'Ware', '1971-06-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742041', '1-5478981', '9812346201', 'dayton_ware@gmail.com', 'www.dayton.com', '5413072', '12458372','4578951','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Johnathon', 'Molina', '1971-06-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742042', '1-5478982', '9812346202', 'johnathon_molina@gmail.com', 'www.johnathon.com', '5413073', '12458373','4578952','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alonzo', 'Kirby', '1971-06-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742043', '1-5478983', '9812346203', 'alonzo_kirby@gmail.com', 'www.alonzo.com', '5413074', '12458374','4578953','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Malcolm', 'Huffman', '1971-06-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742044', '1-5478984', '9812346204', 'malcolm_huffman@gmail.com', 'www.malcolm.com', '5413075', '12458375','4578954','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Moses', 'Bradford', '1971-06-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742045', '1-5478985', '9812346205', 'moses_bradford@gmail.com', 'www.moses.com', '5413076', '12458376','4578955','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Finn', 'Charles', '1971-06-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742046', '1-5478986', '9812346206', 'finn_charles@gmail.com', 'www.finn.com', '5413077', '12458377','4578956','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gunnar', 'Gilmore', '1971-06-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742047', '1-5478987', '9812346207', 'gunnar_gilmore@gmail.com', 'www.gunnar.com', '5413078', '12458378','4578957','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jasper', 'Dominguez', '1971-06-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742048', '1-5478988', '9812346208', 'jasper_dominguez@gmail.com', 'www.jasper.com', '5413079', '12458379','4578958','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kobe', 'Oneal', '1971-06-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742049', '1-5478989', '9812346209', 'kobe_oneal@gmail.com', 'www.kobe.com', '5413080', '12458380','4578959','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Johan', 'Bruce', '1971-06-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742050', '1-5478990', '9812346210', 'johan_bruce@gmail.com', 'www.johan.com', '5413081', '12458381','4578960','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Talan', 'Lang', '1971-06-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742051', '1-5478991', '9812346211', 'talan_lang@gmail.com', 'www.talan.com', '5413082', '12458382','4578961','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ben', 'Combs', '1971-06-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742052', '1-5478992', '9812346212', 'ben_combs@gmail.com', 'www.ben.com', '5413083', '12458383','4578962','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trace', 'Kramer', '1971-06-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742053', '1-5478993', '9812346213', 'trace_kramer@gmail.com', 'www.trace.com', '5413084', '12458384','4578963','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ulises', 'Heath', '1971-06-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742054', '1-5478994', '9812346214', 'ulises_heath@gmail.com', 'www.ulises.com', '5413085', '12458385','4578964','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ezequiel', 'Hancock', '1971-06-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742055', '1-5478995', '9812346215', 'ezequiel_hancock@gmail.com', 'www.ezequiel.com', '5413086', '12458386','4578965','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Titus', 'Gallagher', '1971-07-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742056', '1-5478996', '9812346216', 'titus_gallagher@gmail.com', 'www.titus.com', '5413087', '12458387','4578966','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rocco', 'Gaines', '1971-07-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742057', '1-5478997', '9812346217', 'rocco_gaines@gmail.com', 'www.rocco.com', '5413088', '12458388','4578967','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ariel', 'Shaffer', '1971-07-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742058', '1-5478998', '9812346218', 'ariel_shaffer@gmail.com', 'www.ariel.com', '5413089', '12458389','4578968','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamie', 'Short', '1971-07-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742059', '1-5478999', '9812346219', 'jamie_short@gmail.com', 'www.jamie.com', '5413090', '12458390','4578969','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rolando', 'Wiggins', '1971-07-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742060', '1-5479000', '9812346220', 'rolando_wiggins@gmail.com', 'www.rolando.com', '5413091', '12458391','4578970','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Warren', 'Mathews', '1971-07-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742061', '1-5479001', '9812346221', 'warren_mathews@gmail.com', 'www.warren.com', '5413092', '12458392','4578971','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kendrick', 'Mcclain', '1971-07-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742062', '1-5479002', '9812346222', 'kendrick_mcclain@gmail.com', 'www.kendrick.com', '5413093', '12458393','4578972','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tristin', 'Fischer', '1971-07-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742063', '1-5479003', '9812346223', 'tristin_fischer@gmail.com', 'www.tristin.com', '5413094', '12458394','4578973','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamison', 'Wall', '1971-07-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742064', '1-5479004', '9812346224', 'jamison_wall@gmail.com', 'www.jamison.com', '5413095', '12458395','4578974','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Abram', 'Small', '1971-07-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742065', '1-5479005', '9812346225', 'abram_small@gmail.com', 'www.abram.com', '5413096', '12458396','4578975','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ahmed', 'Melton', '1971-07-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742066', '1-5479006', '9812346226', 'ahmed_melton@gmail.com', 'www.ahmed.com', '5413097', '12458397','4578976','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jairo', 'Hensley', '1971-07-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742067', '1-5479007', '9812346227', 'jairo_hensley@gmail.com', 'www.jairo.com', '5413098', '12458398','4578977','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Devan', 'Bond', '1971-07-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742068', '1-5479008', '9812346228', 'devan_bond@gmail.com', 'www.devan.com', '5413099', '12458399','4578978','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jerome', 'Dyer', '1971-07-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742069', '1-5479009', '9812346229', 'jerome_dyer@gmail.com', 'www.jerome.com', '5413100', '12458400','4578979','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Orion', 'Cameron', '1971-07-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742070', '1-5479010', '9812346230', 'orion_cameron@gmail.com', 'www.orion.com', '5413101', '12458401','4578980','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Vicente', 'Grimes', '1971-07-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742071', '1-5479011', '9812346231', 'vicente_grimes@gmail.com', 'www.vicente.com', '5413102', '12458402','4578981','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Damarion', 'Contreras', '1971-07-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742072', '1-5479012', '9812346232', 'damarion_contreras@gmail.com', 'www.damarion.com', '5413103', '12458403','4578982','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Greyson', 'Christian', '1971-07-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742073', '1-5479013', '9812346233', 'greyson_christian@gmail.com', 'www.greyson.com', '5413104', '12458404','4578983','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ray', 'Wyatt', '1971-07-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742074', '1-5479014', '9812346234', 'ray_wyatt@gmail.com', 'www.ray.com', '5413105', '12458405','4578984','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gianni', 'Baxter', '1971-07-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742075', '1-5479015', '9812346235', 'gianni_baxter@gmail.com', 'www.gianni.com', '5413106', '12458406','4578985','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kadin', 'Snow', '1971-07-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742076', '1-5479016', '9812346236', 'kadin_snow@gmail.com', 'www.kadin.com', '5413107', '12458407','4578986','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ramiro', 'Mosley', '1971-07-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742077', '1-5479017', '9812346237', 'ramiro_mosley@gmail.com', 'www.ramiro.com', '5413108', '12458408','4578987','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ronnie', 'Shepherd', '1971-07-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742078', '1-5479018', '9812346238', 'ronnie_shepherd@gmail.com', 'www.ronnie.com', '5413109', '12458409','4578988','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brodie', 'Larsen', '1971-07-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742079', '1-5479019', '9812346239', 'brodie_larsen@gmail.com', 'www.brodie.com', '5413110', '12458410','4578989','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Stanley', 'Hoover', '1971-07-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742080', '1-5479020', '9812346240', 'stanley_hoover@gmail.com', 'www.stanley.com', '5413111', '12458411','4578990','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jase', 'Beasley', '1971-07-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742081', '1-5479021', '9812346241', 'jase_beasley@gmail.com', 'www.jase.com', '5413112', '12458412','4578991','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kieran', 'Glenn', '1971-07-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742082', '1-5479022', '9812346242', 'kieran_glenn@gmail.com', 'www.kieran.com', '5413113', '12458413','4578992','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Porter', 'Petersen', '1971-07-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742083', '1-5479023', '9812346243', 'porter_petersen@gmail.com', 'www.porter.com', '5413114', '12458414','4578993','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Colten', 'Whitehead', '1971-07-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742084', '1-5479024', '9812346244', 'colten_whitehead@gmail.com', 'www.colten.com', '5413115', '12458415','4578994','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyrone', 'Meyers', '1971-07-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742085', '1-5479025', '9812346245', 'tyrone_meyers@gmail.com', 'www.tyrone.com', '5413116', '12458416','4578995','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Terrence', 'Keith', '1971-07-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742086', '1-5479026', '9812346246', 'terrence_keith@gmail.com', 'www.terrence.com', '5413117', '12458417','4578996','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darrell', 'Garrison', '1971-08-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742087', '1-5479027', '9812346247', 'darrell_garrison@gmail.com', 'www.darrell.com', '5413118', '12458418','4578997','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jarrett', 'Vincent', '1971-08-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742088', '1-5479028', '9812346248', 'jarrett_vincent@gmail.com', 'www.jarrett.com', '5413119', '12458419','4578998','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alvaro', 'Shields', '1971-08-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742089', '1-5479029', '9812346249', 'alvaro_shields@gmail.com', 'www.alvaro.com', '5413120', '12458420','4578999','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braiden', 'Horn', '1971-08-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742090', '1-5479030', '9812346250', 'braiden_horn@gmail.com', 'www.braiden.com', '5413121', '12458421','4579000','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kolby', 'Savage', '1971-08-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742091', '1-5479031', '9812346251', 'kolby_savage@gmail.com', 'www.kolby.com', '5413122', '12458422','4579001','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Addison', 'Olsen', '1971-08-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742092', '1-5479032', '9812346252', 'addison_olsen@gmail.com', 'www.addison.com', '5413123', '12458423','4579002','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emerson', 'Schroeder', '1971-08-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742093', '1-5479033', '9812346253', 'emerson_schroeder@gmail.com', 'www.emerson.com', '5413124', '12458424','4579003','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ibrahim', 'Hartman', '1971-08-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742094', '1-5479034', '9812346254', 'ibrahim_hartman@gmail.com', 'www.ibrahim.com', '5413125', '12458425','4579004','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cedric', 'Woodard', '1971-08-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742095', '1-5479035', '9812346255', 'cedric_woodard@gmail.com', 'www.cedric.com', '5413126', '12458426','4579005','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lee', 'Mueller', '1971-08-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742096', '1-5479036', '9812346256', 'lee_mueller@gmail.com', 'www.lee.com', '5413127', '12458427','4579006','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emmett', 'Deleon', '1971-08-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742098', '1-5479038', '9812346258', 'emmett_deleon@gmail.com', 'www.emmett.com', '5413129', '12458429','4579008','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keenan', 'Booth', '1971-08-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742099', '1-5479039', '9812346259', 'keenan_booth@gmail.com', 'www.keenan.com', '5413130', '12458430','4579009','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leonard', 'Patel', '1971-08-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742100', '1-5479040', '9812346260', 'leonard_patel@gmail.com', 'www.leonard.com', '5413131', '12458431','4579010','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alijah', 'Calhoun', '1971-08-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742101', '1-5479041', '9812346261', 'alijah_calhoun@gmail.com', 'www.alijah.com', '5413132', '12458432','4579011','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Davin', 'Wiley', '1971-08-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742102', '1-5479042', '9812346262', 'davin_wiley@gmail.com', 'www.davin.com', '5413133', '12458433','4579012','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gilbert', 'Eaton', '1971-08-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742103', '1-5479043', '9812346263', 'gilbert_eaton@gmail.com', 'www.gilbert.com', '5413134', '12458434','4579013','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Karson', 'Cline', '1971-08-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742104', '1-5479044', '9812346264', 'karson_cline@gmail.com', 'www.karson.com', '5413135', '12458435','4579014','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kole', 'Navarro', '1971-08-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742105', '1-5479045', '9812346265', 'kole_navarro@gmail.com', 'www.kole.com', '5413136', '12458436','4579015','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Quintin', 'Harrell', '1971-08-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742106', '1-5479046', '9812346266', 'quintin_harrell@gmail.com', 'www.quintin.com', '5413137', '12458437','4579016','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rudy', 'Lester', '1971-08-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742107', '1-5479047', '9812346267', 'rudy_lester@gmail.com', 'www.rudy.com', '5413138', '12458438','4579017','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darian', 'Humphrey', '1971-08-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742108', '1-5479048', '9812346268', 'darian_humphrey@gmail.com', 'www.darian.com', '5413139', '12458439','4579018','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deshawn', 'Parrish', '1971-08-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742109', '1-5479049', '9812346269', 'deshawn_parrish@gmail.com', 'www.deshawn.com', '5413140', '12458440','4579019','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aldo', 'Duran', '1971-08-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742110', '1-5479050', '9812346270', 'aldo_duran@gmail.com', 'www.aldo.com', '5413141', '12458441','4579020','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Neil', 'Hutchinson', '1971-08-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742111', '1-5479051', '9812346271', 'neil_hutchinson@gmail.com', 'www.neil.com', '5413142', '12458442','4579021','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Randall', 'Hess', '1971-08-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742112', '1-5479052', '9812346272', 'randall_hess@gmail.com', 'www.randall.com', '5413143', '12458443','4579022','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cristopher', 'Dorsey', '1971-08-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742113', '1-5479053', '9812346273', 'cristopher_dorsey@gmail.com', 'www.cristopher.com', '5413144', '12458444','4579023','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elisha', 'Bullock', '1971-08-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742114', '1-5479054', '9812346274', 'elisha_bullock@gmail.com', 'www.elisha.com', '5413145', '12458445','4579024','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ronan', 'Robles', '1971-08-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742115', '1-5479055', '9812346275', 'ronan_robles@gmail.com', 'www.ronan.com', '5413146', '12458446','4579025','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Efrain', 'Beard', '1971-08-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742116', '1-5479056', '9812346276', 'efrain_beard@gmail.com', 'www.efrain.com', '5413147', '12458447','4579026','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leland', 'Dalton', '1971-08-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742117', '1-5479057', '9812346277', 'leland_dalton@gmail.com', 'www.leland.com', '5413148', '12458448','4579027','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Davon', 'Avila', '1971-09-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742118', '1-5479058', '9812346278', 'davon_avila@gmail.com', 'www.davon.com', '5413149', '12458449','4579028','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Junior', 'Vance', '1971-09-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742119', '1-5479059', '9812346279', 'junior_vance@gmail.com', 'www.junior.com', '5413150', '12458450','4579029','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Waylon', 'Rich', '1971-09-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742120', '1-5479060', '9812346280', 'waylon_rich@gmail.com', 'www.waylon.com', '5413151', '12458451','4579030','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Irvin', 'Blackwell', '1971-09-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742121', '1-5479061', '9812346281', 'irvin_blackwell@gmail.com', 'www.irvin.com', '5413152', '12458452','4579031','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Coleman', 'York', '1971-09-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742122', '1-5479062', '9812346282', 'coleman_york@gmail.com', 'www.coleman.com', '5413153', '12458453','4579032','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Romeo', 'Johns', '1971-09-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742123', '1-5479063', '9812346283', 'romeo_johns@gmail.com', 'www.romeo.com', '5413154', '12458454','4579033','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Antoine', 'Blankenship', '1971-09-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742124', '1-5479064', '9812346284', 'antoine_blankenship@gmail.com', 'www.antoine.com', '5413155', '12458455','4579034','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaquan', 'Trevino', '1971-09-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742125', '1-5479065', '9812346285', 'jaquan_trevino@gmail.com', 'www.jaquan.com', '5413156', '12458456','4579035','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Camren', 'Salinas', '1971-09-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742126', '1-5479066', '9812346286', 'camren_salinas@gmail.com', 'www.camren.com', '5413157', '12458457','4579036','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dominik', 'Campos', '1971-09-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742127', '1-5479067', '9812346287', 'dominik_campos@gmail.com', 'www.dominik.com', '5413158', '12458458','4579037','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Talon', 'Pruitt', '1971-09-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742128', '1-5479068', '9812346288', 'talon_pruitt@gmail.com', 'www.talon.com', '5413159', '12458459','4579038','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gunner', 'Moses', '1971-09-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742129', '1-5479069', '9812346289', 'gunner_moses@gmail.com', 'www.gunner.com', '5413160', '12458460','4579039','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kolton', 'Callahan', '1971-09-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742130', '1-5479070', '9812346290', 'kolton_callahan@gmail.com', 'www.kolton.com', '5413161', '12458461','4579040','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mohammed', 'Golden', '1971-09-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742131', '1-5479071', '9812346291', 'mohammed_golden@gmail.com', 'www.mohammed.com', '5413162', '12458462','4579041','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexzander', 'Montoya', '1971-09-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742132', '1-5479072', '9812346292', 'alexzander_montoya@gmail.com', 'www.alexzander.com', '5413163', '12458463','4579042','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Duncan', 'Hardin', '1971-09-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742133', '1-5479073', '9812346293', 'duncan_hardin@gmail.com', 'www.duncan.com', '5413164', '12458464','4579043','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jabari', 'Guerra', '1971-09-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742134', '1-5479074', '9812346294', 'jabari_guerra@gmail.com', 'www.jabari.com', '5413165', '12458465','4579044','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Amare', 'Mcdowell', '1971-09-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742135', '1-5479075', '9812346295', 'amare_mcdowell@gmail.com', 'www.amare.com', '5413166', '12458466','4579045','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Amarion', 'Carey', '1971-09-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742136', '1-5479076', '9812346296', 'amarion_carey@gmail.com', 'www.amarion.com', '5413167', '12458467','4579046','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jefferson', 'Stafford', '1971-09-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742137', '1-5479077', '9812346297', 'jefferson_stafford@gmail.com', 'www.jefferson.com', '5413168', '12458468','4579047','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mohammad', 'Gallegos', '1971-09-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742138', '1-5479078', '9812346298', 'mohammad_gallegos@gmail.com', 'www.mohammad.com', '5413169', '12458469','4579048','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kasey', 'Henson', '1971-09-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742139', '1-5479079', '9812346299', 'kasey_henson@gmail.com', 'www.kasey.com', '5413170', '12458470','4579049','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Misael', 'Wilkinson', '1971-09-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742140', '1-5479080', '9812346300', 'misael_wilkinson@gmail.com', 'www.misael.com', '5413171', '12458471','4579050','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brice', 'Booker', '1971-09-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742141', '1-5479081', '9812346301', 'brice_booker@gmail.com', 'www.brice.com', '5413172', '12458472','4579051','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Harold', 'Merritt', '1971-09-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742142', '1-5479082', '9812346302', 'harold_merritt@gmail.com', 'www.harold.com', '5413173', '12458473','4579052','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'August', 'Miranda', '1971-09-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742143', '1-5479083', '9812346303', 'august_miranda@gmail.com', 'www.august.com', '5413174', '12458474','4579053','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brycen', 'Atkinson', '1971-09-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742144', '1-5479084', '9812346304', 'brycen_atkinson@gmail.com', 'www.brycen.com', '5413175', '12458475','4579054','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Draven', 'Orr', '1971-09-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742145', '1-5479085', '9812346305', 'draven_orr@gmail.com', 'www.draven.com', '5413176', '12458476','4579055','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kamron', 'Decker', '1971-09-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742146', '1-5479086', '9812346306', 'kamron_decker@gmail.com', 'www.kamron.com', '5413177', '12458477','4579056','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Asa', 'Hobbs', '1971-09-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742147', '1-5479087', '9812346307', 'asa_hobbs@gmail.com', 'www.asa.com', '5413178', '12458478','4579057','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Eugene', 'Preston', '1971-10-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742148', '1-5479088', '9812346308', 'eugene_preston@gmail.com', 'www.eugene.com', '5413179', '12458479','4579058','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aron', 'Tanner', '1971-10-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742149', '1-5479089', '9812346309', 'aron_tanner@gmail.com', 'www.aron.com', '5413180', '12458480','4579059','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Freddy', 'Knox', '1971-10-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742150', '1-5479090', '9812346310', 'freddy_knox@gmail.com', 'www.freddy.com', '5413181', '12458481','4579060','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Julien', 'Pacheco', '1971-10-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742151', '1-5479091', '9812346311', 'julien_pacheco@gmail.com', 'www.julien.com', '5413182', '12458482','4579061','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zechariah', 'Stephenson', '1971-10-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742152', '1-5479092', '9812346312', 'zechariah_stephenson@gmail.com', 'www.zechariah.com', '5413183', '12458483','4579062','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sage', 'Glass', '1971-10-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742153', '1-5479093', '9812346313', 'sage_glass@gmail.com', 'www.sage.com', '5413184', '12458484','4579063','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brooks', 'Rojas', '1971-10-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742154', '1-5479094', '9812346314', 'brooks_rojas@gmail.com', 'www.brooks.com', '5413185', '12458485','4579064','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dwayne', 'Serrano', '1971-10-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742155', '1-5479095', '9812346315', 'dwayne_serrano@gmail.com', 'www.dwayne.com', '5413186', '12458486','4579065','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alonso', 'Marks', '1971-10-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742156', '1-5479096', '9812346316', 'alonso_marks@gmail.com', 'www.alonso.com', '5413187', '12458487','4579066','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maverick', 'Hickman', '1971-10-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742157', '1-5479097', '9812346317', 'maverick_hickman@gmail.com', 'www.maverick.com', '5413188', '12458488','4579067','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dashawn', 'English', '1971-10-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742158', '1-5479098', '9812346318', 'dashawn_english@gmail.com', 'www.dashawn.com', '5413189', '12458489','4579068','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aydan', 'Sweeney', '1971-10-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742159', '1-5479099', '9812346319', 'aydan_sweeney@gmail.com', 'www.aydan.com', '5413190', '12458490','4579069','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Donte', 'Strong', '1971-10-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742160', '1-5479100', '9812346320', 'donte_strong@gmail.com', 'www.donte.com', '5413191', '12458491','4579070','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyrell', 'Prince', '1971-10-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742161', '1-5479101', '9812346321', 'tyrell_prince@gmail.com', 'www.tyrell.com', '5413192', '12458492','4579071','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keagan', 'Mcclure', '1971-10-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742162', '1-5479102', '9812346322', 'keagan_mcclure@gmail.com', 'www.keagan.com', '5413193', '12458493','4579072','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Clay', 'Conway', '1971-10-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742163', '1-5479103', '9812346323', 'clay_conway@gmail.com', 'www.clay.com', '5413194', '12458494','4579073','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ernest', 'Walter', '1971-10-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742164', '1-5479104', '9812346324', 'ernest_walter@gmail.com', 'www.ernest.com', '5413195', '12458495','4579074','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Octavio', 'Roth', '1971-10-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742165', '1-5479105', '9812346325', 'octavio_roth@gmail.com', 'www.octavio.com', '5413196', '12458496','4579075','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brennen', 'Maynard', '1971-10-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742166', '1-5479106', '9812346326', 'brennen_maynard@gmail.com', 'www.brennen.com', '5413197', '12458497','4579076','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lewis', 'Farrell', '1971-10-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742167', '1-5479107', '9812346327', 'lewis_farrell@gmail.com', 'www.lewis.com', '5413198', '12458498','4579077','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Layne', 'Lowery', '1971-10-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742168', '1-5479108', '9812346328', 'layne_lowery@gmail.com', 'www.layne.com', '5413199', '12458499','4579078','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sincere', 'Hurst', '1971-10-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742169', '1-5479109', '9812346329', 'sincere_hurst@gmail.com', 'www.sincere.com', '5413200', '12458500','4579079','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dale', 'Nixon', '1971-10-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742170', '1-5479110', '9812346330', 'dale_nixon@gmail.com', 'www.dale.com', '5413201', '12458501','4579080','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kenyon', 'Weiss', '1971-10-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742171', '1-5479111', '9812346331', 'kenyon_weiss@gmail.com', 'www.kenyon.com', '5413202', '12458502','4579081','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Omari', 'Trujillo', '1971-10-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742172', '1-5479112', '9812346332', 'omari_trujillo@gmail.com', 'www.omari.com', '5413203', '12458503','4579082','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alessandro', 'Ellison', '1971-10-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742173', '1-5479113', '9812346333', 'alessandro_ellison@gmail.com', 'www.alessandro.com', '5413204', '12458504','4579083','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyree', 'Sloan', '1971-10-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742174', '1-5479114', '9812346334', 'tyree_sloan@gmail.com', 'www.tyree.com', '5413205', '12458505','4579084','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jair', 'Juarez', '1971-10-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742175', '1-5479115', '9812346335', 'jair_juarez@gmail.com', 'www.jair.com', '5413206', '12458506','4579085','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Demarion', 'Winters', '1971-10-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742176', '1-5479116', '9812346336', 'demarion_winters@gmail.com', 'www.demarion.com', '5413207', '12458507','4579086','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Oswaldo', 'Mclean', '1971-10-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742177', '1-5479117', '9812346337', 'oswaldo_mclean@gmail.com', 'www.oswaldo.com', '5413208', '12458508','4579087','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Raphael', 'Randolph', '1971-10-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742178', '1-5479118', '9812346338', 'raphael_randolph@gmail.com', 'www.raphael.com', '5413209', '12458509','4579088','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bradyn', 'Leon', '1971-11-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742179', '1-5479119', '9812346339', 'bradyn_leon@gmail.com', 'www.bradyn.com', '5413210', '12458510','4579089','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ignacio', 'Boyer', '1971-11-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742180', '1-5479120', '9812346340', 'ignacio_boyer@gmail.com', 'www.ignacio.com', '5413211', '12458511','4579090','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Wayne', 'Villarreal', '1971-11-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742181', '1-5479121', '9812346341', 'wayne_villarreal@gmail.com', 'www.wayne.com', '5413212', '12458512','4579091','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Agustin', 'Mccall', '1971-11-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742182', '1-5479122', '9812346342', 'agustin_mccall@gmail.com', 'www.agustin.com', '5413213', '12458513','4579092','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cash', 'Gentry', '1971-11-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742183', '1-5479123', '9812346343', 'cash_gentry@gmail.com', 'www.cash.com', '5413214', '12458514','4579093','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jordon', 'Carrillo', '1971-11-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742184', '1-5479124', '9812346344', 'jordon_carrillo@gmail.com', 'www.jordon.com', '5413215', '12458515','4579094','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Davian', 'Kent', '1971-11-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742185', '1-5479125', '9812346345', 'davian_kent@gmail.com', 'www.davian.com', '5413216', '12458516','4579095','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Xzavier', 'Ayers', '1971-11-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742186', '1-5479126', '9812346346', 'xzavier_ayers@gmail.com', 'www.xzavier.com', '5413217', '12458517','4579096','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ross', 'Lara', '1971-11-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742187', '1-5479127', '9812346347', 'ross_lara@gmail.com', 'www.ross.com', '5413218', '12458518','4579097','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aryan', 'Shannon', '1971-11-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742188', '1-5479128', '9812346348', 'aryan_shannon@gmail.com', 'www.aryan.com', '5413219', '12458519','4579098','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Boston', 'Sexton', '1971-11-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742189', '1-5479129', '9812346349', 'boston_sexton@gmail.com', 'www.boston.com', '5413220', '12458520','4579099','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Garret', 'Pace', '1971-11-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742190', '1-5479130', '9812346350', 'garret_pace@gmail.com', 'www.garret.com', '5413221', '12458521','4579100','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lamar', 'Hull', '1971-11-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742191', '1-5479131', '9812346351', 'lamar_hull@gmail.com', 'www.lamar.com', '5413222', '12458522','4579101','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Matteo', 'Leblanc', '1971-11-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742192', '1-5479132', '9812346352', 'matteo_leblanc@gmail.com', 'www.matteo.com', '5413223', '12458523','4579102','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reagan', 'Browning', '1971-11-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742193', '1-5479133', '9812346353', 'reagan_browning@gmail.com', 'www.reagan.com', '5413224', '12458524','4579103','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dominique', 'Velasquez', '1971-11-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742194', '1-5479134', '9812346354', 'dominique_velasquez@gmail.com', 'www.dominique.com', '5413225', '12458525','4579104','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mike', 'Leach', '1971-11-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742195', '1-5479135', '9812346355', 'mike_leach@gmail.com', 'www.mike.com', '5413226', '12458526','4579105','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rhett', 'Chang', '1971-11-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742196', '1-5479136', '9812346356', 'rhett_chang@gmail.com', 'www.rhett.com', '5413227', '12458527','4579106','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'London', 'House', '1971-11-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742197', '1-5479137', '9812346357', 'london_house@gmail.com', 'www.london.com', '5413228', '12458528','4579107','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Muhammad', 'Sellers', '1971-11-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742198', '1-5479138', '9812346358', 'muhammad_sellers@gmail.com', 'www.muhammad.com', '5413229', '12458529','4579108','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gideon', 'Herring', '1971-11-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742199', '1-5479139', '9812346359', 'gideon_herring@gmail.com', 'www.gideon.com', '5413230', '12458530','4579109','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Humberto', 'Noble', '1971-11-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742200', '1-5479140', '9812346360', 'humberto_noble@gmail.com', 'www.humberto.com', '5413231', '12458531','4579110','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lawson', 'Foley', '1971-11-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742201', '1-5479141', '9812346361', 'lawson_foley@gmail.com', 'www.lawson.com', '5413232', '12458532','4579111','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adrien', 'Bartlett', '1971-11-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742202', '1-5479142', '9812346362', 'adrien_bartlett@gmail.com', 'www.adrien.com', '5413233', '12458533','4579112','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Clarence', 'Mercado', '1971-11-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742203', '1-5479143', '9812346363', 'clarence_mercado@gmail.com', 'www.clarence.com', '5413234', '12458534','4579113','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jadyn', 'Landry', '1971-11-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742204', '1-5479144', '9812346364', 'jadyn_landry@gmail.com', 'www.jadyn.com', '5413235', '12458535','4579114','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Javion', 'Durham', '1971-11-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742205', '1-5479145', '9812346365', 'javion_durham@gmail.com', 'www.javion.com', '5413236', '12458536','4579115','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Derick', 'Walls', '1971-11-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742206', '1-5479146', '9812346366', 'derick_walls@gmail.com', 'www.derick.com', '5413237', '12458537','4579116','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kyan', 'Barr', '1971-11-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742207', '1-5479147', '9812346367', 'kyan_barr@gmail.com', 'www.kyan.com', '5413238', '12458538','4579117','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Salvatore', 'Mckee', '1971-11-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742208', '1-5479148', '9812346368', 'salvatore_mckee@gmail.com', 'www.salvatore.com', '5413239', '12458539','4579118','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kareem', 'Bauer', '1971-12-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742209', '1-5479149', '9812346369', 'kareem_bauer@gmail.com', 'www.kareem.com', '5413240', '12458540','4579119','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Arjun', 'Rivers', '1971-12-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742210', '1-5479150', '9812346370', 'arjun_rivers@gmail.com', 'www.arjun.com', '5413241', '12458541','4579120','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyrese', 'Everett', '1971-12-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742211', '1-5479151', '9812346371', 'tyrese_everett@gmail.com', 'www.tyrese.com', '5413242', '12458542','4579121','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Markus', 'Bradshaw', '1971-12-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742212', '1-5479152', '9812346372', 'markus_bradshaw@gmail.com', 'www.markus.com', '5413243', '12458543','4579122','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Semaj', 'Pugh', '1971-12-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742213', '1-5479153', '9812346373', 'semaj_pugh@gmail.com', 'www.semaj.com', '5413244', '12458544','4579123','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Barrett', 'Velez', '1971-12-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742214', '1-5479154', '9812346374', 'barrett_velez@gmail.com', 'www.barrett.com', '5413245', '12458545','4579124','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gavyn', 'Rush', '1971-12-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742215', '1-5479155', '9812346375', 'gavyn_rush@gmail.com', 'www.gavyn.com', '5413246', '12458546','4579125','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kian', 'Estes', '1971-12-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742216', '1-5479156', '9812346376', 'kian_estes@gmail.com', 'www.kian.com', '5413247', '12458547','4579126','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ryland', 'Dodson', '1971-12-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742217', '1-5479157', '9812346377', 'ryland_dodson@gmail.com', 'www.ryland.com', '5413248', '12458548','4579127','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamar', 'Morse', '1971-12-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742218', '1-5479158', '9812346378', 'jamar_morse@gmail.com', 'www.jamar.com', '5413249', '12458549','4579128','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nathanial', 'Sheppard', '1971-12-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742219', '1-5479159', '9812346379', 'nathanial_sheppard@gmail.com', 'www.nathanial.com', '5413250', '12458550','4579129','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Moshe', 'Weeks', '1971-12-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742220', '1-5479160', '9812346380', 'moshe_weeks@gmail.com', 'www.moshe.com', '5413251', '12458551','4579130','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Landyn', 'Camacho', '1971-12-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742221', '1-5479161', '9812346381', 'landyn_camacho@gmail.com', 'www.landyn.com', '5413252', '12458552','4579131','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ryker', 'Bean', '1971-12-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742222', '1-5479162', '9812346382', 'ryker_bean@gmail.com', 'www.ryker.com', '5413253', '12458553','4579132','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alfred', 'Barron', '1971-12-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742223', '1-5479163', '9812346383', 'alfred_barron@gmail.com', 'www.alfred.com', '5413254', '12458554','4579133','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Giancarlo', 'Livingston', '1971-12-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742224', '1-5479164', '9812346384', 'giancarlo_livingston@gmail.com', 'www.giancarlo.com', '5413255', '12458555','4579134','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kane', 'Middleton', '1971-12-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742225', '1-5479165', '9812346385', 'kane_middleton@gmail.com', 'www.kane.com', '5413256', '12458556','4579135','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Malakai', 'Spears', '1971-12-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742226', '1-5479166', '9812346386', 'malakai_spears@gmail.com', 'www.malakai.com', '5413257', '12458557','4579136','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rory', 'Branch', '1971-12-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742227', '1-5479167', '9812346387', 'rory_branch@gmail.com', 'www.rory.com', '5413258', '12458558','4579137','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darnell', 'Blevins', '1971-12-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742228', '1-5479168', '9812346388', 'darnell_blevins@gmail.com', 'www.darnell.com', '5413259', '12458559','4579138','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hamza', 'Chen', '1971-12-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742229', '1-5479169', '9812346389', 'hamza_chen@gmail.com', 'www.hamza.com', '5413260', '12458560','4579139','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaron', 'Kerr', '1971-12-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742230', '1-5479170', '9812346390', 'jaron_kerr@gmail.com', 'www.jaron.com', '5413261', '12458561','4579140','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ari', 'Mcconnell', '1971-12-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742231', '1-5479171', '9812346391', 'ari_mcconnell@gmail.com', 'www.ari.com', '5413262', '12458562','4579141','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Frankie', 'Hatfield', '1971-12-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742232', '1-5479172', '9812346392', 'frankie_hatfield@gmail.com', 'www.frankie.com', '5413263', '12458563','4579142','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aditya', 'Harding', '1971-12-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742233', '1-5479173', '9812346393', 'aditya_harding@gmail.com', 'www.aditya.com', '5413264', '12458564','4579143','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Clinton', 'Ashley', '1971-12-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742234', '1-5479174', '9812346394', 'clinton_ashley@gmail.com', 'www.clinton.com', '5413265', '12458565','4579144','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cullen', 'Solis', '1971-12-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742235', '1-5479175', '9812346395', 'cullen_solis@gmail.com', 'www.cullen.com', '5413266', '12458566','4579145','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keshawn', 'Herman', '1971-12-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742236', '1-5479176', '9812346396', 'keshawn_herman@gmail.com', 'www.keshawn.com', '5413267', '12458567','4579146','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Milo', 'Frost', '1971-12-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742237', '1-5479177', '9812346397', 'milo_frost@gmail.com', 'www.milo.com', '5413268', '12458568','4579147','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Devyn', 'Giles', '1971-12-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742238', '1-5479178', '9812346398', 'devyn_giles@gmail.com', 'www.devyn.com', '5413269', '12458569','4579148','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Armani', 'Blackburn', '1971-12-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742239', '1-5479179', '9812346399', 'armani_blackburn@gmail.com', 'www.armani.com', '5413270', '12458570','4579149','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isai', 'William', '1972-01-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742240', '1-5479180', '9812346400', 'isai_william@gmail.com', 'www.isai.com', '5413271', '12458571','4579150','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaylan', 'Pennington', '1972-01-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742241', '1-5479181', '9812346401', 'jaylan_pennington@gmail.com', 'www.jaylan.com', '5413272', '12458572','4579151','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kamari', 'Woodward', '1972-01-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742242', '1-5479182', '9812346402', 'kamari_woodward@gmail.com', 'www.kamari.com', '5413273', '12458573','4579152','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nigel', 'Finley', '1972-01-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742243', '1-5479183', '9812346403', 'nigel_finley@gmail.com', 'www.nigel.com', '5413274', '12458574','4579153','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jovani', 'Mcintosh', '1972-01-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742244', '1-5479184', '9812346404', 'jovani_mcintosh@gmail.com', 'www.jovani.com', '5413275', '12458575','4579154','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sterling', 'Koch', '1972-01-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742245', '1-5479185', '9812346405', 'sterling_koch@gmail.com', 'www.sterling.com', '5413276', '12458576','4579155','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Justus', 'Best', '1972-01-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742246', '1-5479186', '9812346406', 'justus_best@gmail.com', 'www.justus.com', '5413277', '12458577','4579156','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dillan', 'Solomon', '1972-01-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742247', '1-5479187', '9812346407', 'dillan_solomon@gmail.com', 'www.dillan.com', '5413278', '12458578','4579157','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keon', 'Mccullough', '1972-01-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742248', '1-5479188', '9812346408', 'keon_mccullough@gmail.com', 'www.keon.com', '5413279', '12458579','4579158','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marques', 'Dudley', '1972-01-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742249', '1-5479189', '9812346409', 'marques_dudley@gmail.com', 'www.marques.com', '5413280', '12458580','4579159','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nico', 'Nolan', '1972-01-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742250', '1-5479190', '9812346410', 'nico_nolan@gmail.com', 'www.nico.com', '5413281', '12458581','4579160','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Roland', 'Blanchard', '1972-01-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742251', '1-5479191', '9812346411', 'roland_blanchard@gmail.com', 'www.roland.com', '5413282', '12458582','4579161','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Donavan', 'Rivas', '1972-01-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742252', '1-5479192', '9812346412', 'donavan_rivas@gmail.com', 'www.donavan.com', '5413283', '12458583','4579162','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Giovanny', 'Brennan', '1972-01-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742253', '1-5479193', '9812346413', 'giovanny_brennan@gmail.com', 'www.giovanny.com', '5413284', '12458584','4579163','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jorden', 'Mejia', '1972-01-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742254', '1-5479194', '9812346414', 'jorden_mejia@gmail.com', 'www.jorden.com', '5413285', '12458585','4579164','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rigoberto', 'Kane', '1972-01-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742255', '1-5479195', '9812346415', 'rigoberto_kane@gmail.com', 'www.rigoberto.com', '5413286', '12458586','4579165','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Anton', 'Benton', '1972-01-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742256', '1-5479196', '9812346416', 'anton_benton@gmail.com', 'www.anton.com', '5413287', '12458587','4579166','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Johnpaul', 'Joyce', '1972-01-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742257', '1-5479197', '9812346417', 'johnpaul_joyce@gmail.com', 'www.johnpaul.com', '5413288', '12458588','4579167','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Konner', 'Buckley', '1972-01-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742258', '1-5479198', '9812346418', 'konner_buckley@gmail.com', 'www.konner.com', '5413289', '12458589','4579168','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaeden', 'Haley', '1972-01-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742259', '1-5479199', '9812346419', 'jaeden_haley@gmail.com', 'www.jaeden.com', '5413290', '12458590','4579169','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Enzo', 'Valentine', '1972-01-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742260', '1-5479200', '9812346420', 'enzo_valentine@gmail.com', 'www.enzo.com', '5413291', '12458591','4579170','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Josh', 'Maddox', '1972-01-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742261', '1-5479201', '9812346421', 'josh_maddox@gmail.com', 'www.josh.com', '5413292', '12458592','4579171','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Demarcus', 'Russo', '1972-01-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742262', '1-5479202', '9812346422', 'demarcus_russo@gmail.com', 'www.demarcus.com', '5413293', '12458593','4579172','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Estevan', 'Mcknight', '1972-01-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742263', '1-5479203', '9812346423', 'estevan_mcknight@gmail.com', 'www.estevan.com', '5413294', '12458594','4579173','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rylee', 'Buck', '1972-01-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742264', '1-5479204', '9812346424', 'rylee_buck@gmail.com', 'www.rylee.com', '5413295', '12458595','4579174','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Yair', 'Moon', '1972-01-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742265', '1-5479205', '9812346425', 'yair_moon@gmail.com', 'www.yair.com', '5413296', '12458596','4579175','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cale', 'Mcmillan', '1972-01-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742266', '1-5479206', '9812346426', 'cale_mcmillan@gmail.com', 'www.cale.com', '5413297', '12458597','4579176','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kale', 'Crosby', '1972-01-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742267', '1-5479207', '9812346427', 'kale_crosby@gmail.com', 'www.kale.com', '5413298', '12458598','4579177','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braedon', 'Dotson', '1972-01-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742269', '1-5479209', '9812346429', 'braedon_dotson@gmail.com', 'www.braedon.com', '5413300', '12458600','4579179','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Santos', 'Mays', '1972-01-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742270', '1-5479210', '9812346430', 'santos_mays@gmail.com', 'www.santos.com', '5413301', '12458601','4579180','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ralph', 'Roach', '1972-02-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742271', '1-5479211', '9812346431', 'ralph_roach@gmail.com', 'www.ralph.com', '5413302', '12458602','4579181','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Vance', 'Church', '1972-02-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742272', '1-5479212', '9812346432', 'vance_church@gmail.com', 'www.vance.com', '5413303', '12458603','4579182','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alden', 'Chan', '1972-02-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742273', '1-5479213', '9812346433', 'alden_chan@gmail.com', 'www.alden.com', '5413304', '12458604','4579183','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bo', 'Richmond', '1972-02-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742274', '1-5479214', '9812346434', 'bo_richmond@gmail.com', 'www.bo.com', '5413305', '12458605','4579184','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Augustus', 'Meadows', '1972-02-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742275', '1-5479215', '9812346435', 'augustus_meadows@gmail.com', 'www.augustus.com', '5413306', '12458606','4579185','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cannon', 'Faulkner', '1972-02-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742276', '1-5479216', '9812346436', 'cannon_faulkner@gmail.com', 'www.cannon.com', '5413307', '12458607','4579186','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darryl', 'Oneill', '1972-02-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742277', '1-5479217', '9812346437', 'darryl_oneill@gmail.com', 'www.darryl.com', '5413308', '12458608','4579187','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gaven', 'Knapp', '1972-02-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742278', '1-5479218', '9812346438', 'gaven_knapp@gmail.com', 'www.gaven.com', '5413309', '12458609','4579188','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sheldon', 'Kline', '1972-02-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742279', '1-5479219', '9812346439', 'sheldon_kline@gmail.com', 'www.sheldon.com', '5413310', '12458610','4579189','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darien', 'Barry', '1972-02-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742280', '1-5479220', '9812346440', 'darien_barry@gmail.com', 'www.darien.com', '5413311', '12458611','4579190','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Coby', 'Ochoa', '1972-02-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742281', '1-5479221', '9812346441', 'coby_ochoa@gmail.com', 'www.coby.com', '5413312', '12458612','4579191','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Glenn', 'Jacobson', '1972-02-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742282', '1-5479222', '9812346442', 'glenn_jacobson@gmail.com', 'www.glenn.com', '5413313', '12458613','4579192','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Vaughn', 'Gay', '1972-02-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742283', '1-5479223', '9812346443', 'vaughn_gay@gmail.com', 'www.vaughn.com', '5413314', '12458614','4579193','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'German', 'Avery', '1972-02-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742284', '1-5479224', '9812346444', 'german_avery@gmail.com', 'www.german.com', '5413315', '12458615','4579194','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hassan', 'Hendricks', '1972-02-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742285', '1-5479225', '9812346445', 'hassan_hendricks@gmail.com', 'www.hassan.com', '5413316', '12458616','4579195','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Seamus', 'Horne', '1972-02-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742286', '1-5479226', '9812346446', 'seamus_horne@gmail.com', 'www.seamus.com', '5413317', '12458617','4579196','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braulio', 'Shepard', '1972-02-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742287', '1-5479227', '9812346447', 'braulio_shepard@gmail.com', 'www.braulio.com', '5413318', '12458618','4579197','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Layton', 'Hebert', '1972-02-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742288', '1-5479228', '9812346448', 'layton_hebert@gmail.com', 'www.layton.com', '5413319', '12458619','4579198','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nathen', 'Cherry', '1972-02-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742289', '1-5479229', '9812346449', 'nathen_cherry@gmail.com', 'www.nathen.com', '5413320', '12458620','4579199','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Luciano', 'Cardenas', '1972-02-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742290', '1-5479230', '9812346450', 'luciano_cardenas@gmail.com', 'www.luciano.com', '5413321', '12458621','4579200','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Roderick', 'Mcintyre', '1972-02-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742291', '1-5479231', '9812346451', 'roderick_mcintyre@gmail.com', 'www.roderick.com', '5413322', '12458622','4579201','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Antony', 'Whitney', '1972-02-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742292', '1-5479232', '9812346452', 'antony_whitney@gmail.com', 'www.antony.com', '5413323', '12458623','4579202','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elvis', 'Waller', '1972-02-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742293', '1-5479233', '9812346453', 'elvis_waller@gmail.com', 'www.elvis.com', '5413324', '12458624','4579203','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jovanni', 'Holman', '1972-02-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742294', '1-5479234', '9812346454', 'jovanni_holman@gmail.com', 'www.jovanni.com', '5413325', '12458625','4579204','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Samir', 'Donaldson', '1972-02-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742295', '1-5479235', '9812346455', 'samir_donaldson@gmail.com', 'www.samir.com', '5413326', '12458626','4579205','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ellis', 'Cantu', '1972-02-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742296', '1-5479236', '9812346456', 'ellis_cantu@gmail.com', 'www.ellis.com', '5413327', '12458627','4579206','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Malaki', 'Terrell', '1972-02-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742297', '1-5479237', '9812346457', 'malaki_terrell@gmail.com', 'www.malaki.com', '5413328', '12458628','4579207','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deangelo', 'Morin', '1972-02-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742298', '1-5479238', '9812346458', 'deangelo_morin@gmail.com', 'www.deangelo.com', '5413329', '12458629','4579208','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jean', 'Gillespie', '1972-02-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742299', '1-5479239', '9812346459', 'jean_gillespie@gmail.com', 'www.jean.com', '5413330', '12458630','4579209','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Winston', 'Fuentes', '1972-03-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742300', '1-5479240', '9812346460', 'winston_fuentes@gmail.com', 'www.winston.com', '5413331', '12458631','4579210','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Stefan', 'Tillman', '1972-03-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742301', '1-5479241', '9812346461', 'stefan_tillman@gmail.com', 'www.stefan.com', '5413332', '12458632','4579211','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adriel', 'Sanford', '1972-03-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742302', '1-5479242', '9812346462', 'adriel_sanford@gmail.com', 'www.adriel.com', '5413333', '12458633','4579212','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Atticus', 'Bentley', '1972-03-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742303', '1-5479243', '9812346463', 'atticus_bentley@gmail.com', 'www.atticus.com', '5413334', '12458634','4579213','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Clark', 'Peck', '1972-03-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742304', '1-5479244', '9812346464', 'clark_peck@gmail.com', 'www.clark.com', '5413335', '12458635','4579214','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Heath', 'Key', '1972-03-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742305', '1-5479245', '9812346465', 'heath_key@gmail.com', 'www.heath.com', '5413336', '12458636','4579215','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamir', 'Salas', '1972-03-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742306', '1-5479246', '9812346466', 'jamir_salas@gmail.com', 'www.jamir.com', '5413337', '12458637','4579216','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Korbin', 'Rollins', '1972-03-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742307', '1-5479247', '9812346467', 'korbin_rollins@gmail.com', 'www.korbin.com', '5413338', '12458638','4579217','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bruno', 'Gamble', '1972-03-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742308', '1-5479248', '9812346468', 'bruno_gamble@gmail.com', 'www.bruno.com', '5413339', '12458639','4579218','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexandro', 'Dickson', '1972-03-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742309', '1-5479249', '9812346469', 'alexandro_dickson@gmail.com', 'www.alexandro.com', '5413340', '12458640','4579219','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marquise', 'Battle', '1972-03-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742310', '1-5479250', '9812346470', 'marquise_battle@gmail.com', 'www.marquise.com', '5413341', '12458641','4579220','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sonny', 'Santana', '1972-03-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742311', '1-5479251', '9812346471', 'sonny_santana@gmail.com', 'www.sonny.com', '5413342', '12458642','4579221','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deacon', 'Cabrera', '1972-03-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742312', '1-5479252', '9812346472', 'deacon_cabrera@gmail.com', 'www.deacon.com', '5413343', '12458643','4579222','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marcel', 'Cervantes', '1972-03-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742313', '1-5479253', '9812346473', 'marcel_cervantes@gmail.com', 'www.marcel.com', '5413344', '12458644','4579223','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rex', 'Howe', '1972-03-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742314', '1-5479254', '9812346474', 'rex_howe@gmail.com', 'www.rex.com', '5413345', '12458645','4579224','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Santino', 'Hinton', '1972-03-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742315', '1-5479255', '9812346475', 'santino_hinton@gmail.com', 'www.santino.com', '5413346', '12458646','4579225','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mathias', 'Hurley', '1972-03-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742316', '1-5479256', '9812346476', 'mathias_hurley@gmail.com', 'www.mathias.com', '5413347', '12458647','4579226','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kylan', 'Spence', '1972-03-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742317', '1-5479257', '9812346477', 'kylan_spence@gmail.com', 'www.kylan.com', '5413348', '12458648','4579227','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shamar', 'Zamora', '1972-03-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742318', '1-5479258', '9812346478', 'shamar_zamora@gmail.com', 'www.shamar.com', '5413349', '12458649','4579228','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cason', 'Yang', '1972-03-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742319', '1-5479259', '9812346479', 'cason_yang@gmail.com', 'www.cason.com', '5413350', '12458650','4579229','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jovanny', 'Mcneil', '1972-03-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742320', '1-5479260', '9812346480', 'jovanny_mcneil@gmail.com', 'www.jovanny.com', '5413351', '12458651','4579230','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Garrison', 'Suarez', '1972-03-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742321', '1-5479261', '9812346481', 'garrison_suarez@gmail.com', 'www.garrison.com', '5413352', '12458652','4579231','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nick', 'Case', '1972-03-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742322', '1-5479262', '9812346482', 'nick_case@gmail.com', 'www.nick.com', '5413353', '12458653','4579232','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reynaldo', 'Petty', '1972-03-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742323', '1-5479263', '9812346483', 'reynaldo_petty@gmail.com', 'www.reynaldo.com', '5413354', '12458654','4579233','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Milton', 'Gould', '1972-03-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742324', '1-5479264', '9812346484', 'milton_gould@gmail.com', 'www.milton.com', '5413355', '12458655','4579234','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brad', 'Mcfarland', '1972-03-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742325', '1-5479265', '9812346485', 'brad_mcfarland@gmail.com', 'www.brad.com', '5413356', '12458656','4579235','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adonis', 'Sampson', '1972-03-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742326', '1-5479266', '9812346486', 'adonis_sampson@gmail.com', 'www.adonis.com', '5413357', '12458657','4579236','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Makai', 'Carver', '1972-03-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742327', '1-5479267', '9812346487', 'makai_carver@gmail.com', 'www.makai.com', '5413358', '12458658','4579237','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Prince', 'Bray', '1972-03-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742328', '1-5479268', '9812346488', 'prince_bray@gmail.com', 'www.prince.com', '5413359', '12458659','4579238','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Howard', 'Rosario', '1972-03-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742329', '1-5479269', '9812346489', 'howard_rosario@gmail.com', 'www.howard.com', '5413360', '12458660','4579239','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jovany', 'Macdonald', '1972-03-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742330', '1-5479270', '9812346490', 'jovany_macdonald@gmail.com', 'www.jovany.com', '5413361', '12458661','4579240','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Konnor', 'Stout', '1972-04-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742331', '1-5479271', '9812346491', 'konnor_stout@gmail.com', 'www.konnor.com', '5413362', '12458662','4579241','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Quinten', 'Hester', '1972-04-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742332', '1-5479272', '9812346492', 'quinten_hester@gmail.com', 'www.quinten.com', '5413363', '12458663','4579242','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Remington', 'Melendez', '1972-04-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742333', '1-5479273', '9812346493', 'remington_melendez@gmail.com', 'www.remington.com', '5413364', '12458664','4579243','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nestor', 'Dillon', '1972-04-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742334', '1-5479274', '9812346494', 'nestor_dillon@gmail.com', 'www.nestor.com', '5413365', '12458665','4579244','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cortez', 'Farley', '1972-04-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742335', '1-5479275', '9812346495', 'cortez_farley@gmail.com', 'www.cortez.com', '5413366', '12458666','4579245','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kaeden', 'Hopper', '1972-04-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742336', '1-5479276', '9812346496', 'kaeden_hopper@gmail.com', 'www.kaeden.com', '5413367', '12458667','4579246','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Braylen', 'Galloway', '1972-04-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742337', '1-5479277', '9812346497', 'braylen_galloway@gmail.com', 'www.braylen.com', '5413368', '12458668','4579247','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carlo', 'Potts', '1972-04-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742338', '1-5479278', '9812346498', 'carlo_potts@gmail.com', 'www.carlo.com', '5413369', '12458669','4579248','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bernard', 'Bernard', '1972-04-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742339', '1-5479279', '9812346499', 'bernard_bernard@gmail.com', 'www.bernard.com', '5413370', '12458670','4579249','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deon', 'Joyner', '1972-04-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742340', '1-5479280', '9812346500', 'deon_joyner@gmail.com', 'www.deon.com', '5413371', '12458671','4579250','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sidney', 'Stein', '1972-04-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742341', '1-5479281', '9812346501', 'sidney_stein@gmail.com', 'www.sidney.com', '5413372', '12458672','4579251','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jordy', 'Aguirre', '1972-04-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742342', '1-5479282', '9812346502', 'jordy_aguirre@gmail.com', 'www.jordy.com', '5413373', '12458673','4579252','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Conrad', 'Osborn', '1972-04-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742343', '1-5479283', '9812346503', 'conrad_osborn@gmail.com', 'www.conrad.com', '5413374', '12458674','4579253','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Pranav', 'Mercer', '1972-04-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742344', '1-5479284', '9812346504', 'pranav_mercer@gmail.com', 'www.pranav.com', '5413375', '12458675','4579254','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dangelo', 'Bender', '1972-04-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742345', '1-5479285', '9812346505', 'dangelo_bender@gmail.com', 'www.dangelo.com', '5413376', '12458676','4579255','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ean', 'Franco', '1972-04-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742346', '1-5479286', '9812346506', 'ean_franco@gmail.com', 'www.ean.com', '5413377', '12458677','4579256','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Houston', 'Rowland', '1972-04-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742347', '1-5479287', '9812346507', 'houston_rowland@gmail.com', 'www.houston.com', '5413378', '12458678','4579257','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Savion', 'Sykes', '1972-04-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742348', '1-5479288', '9812346508', 'savion_sykes@gmail.com', 'www.savion.com', '5413379', '12458679','4579258','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cael', 'Benjamin', '1972-04-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742349', '1-5479289', '9812346509', 'cael_benjamin@gmail.com', 'www.cael.com', '5413380', '12458680','4579259','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elmer', 'Travis', '1972-04-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742350', '1-5479290', '9812346510', 'elmer_travis@gmail.com', 'www.elmer.com', '5413381', '12458681','4579260','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Josef', 'Pickett', '1972-04-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742351', '1-5479291', '9812346511', 'josef_pickett@gmail.com', 'www.josef.com', '5413382', '12458682','4579261','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Antwan', 'Crane', '1972-04-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742352', '1-5479292', '9812346512', 'antwan_crane@gmail.com', 'www.antwan.com', '5413383', '12458683','4579262','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aydin', 'Sears', '1972-04-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742353', '1-5479293', '9812346513', 'aydin_sears@gmail.com', 'www.aydin.com', '5413384', '12458684','4579263','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dario', 'Mayo', '1972-04-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742354', '1-5479294', '9812346514', 'dario_mayo@gmail.com', 'www.dario.com', '5413385', '12458685','4579264','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Devonte', 'Dunlap', '1972-04-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742355', '1-5479295', '9812346515', 'devonte_dunlap@gmail.com', 'www.devonte.com', '5413386', '12458686','4579265','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marcelo', 'Hayden', '1972-04-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742356', '1-5479296', '9812346516', 'marcelo_hayden@gmail.com', 'www.marcelo.com', '5413387', '12458687','4579266','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kamden', 'Wilder', '1972-04-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742357', '1-5479297', '9812346517', 'kamden_wilder@gmail.com', 'www.kamden.com', '5413388', '12458688','4579267','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keyon', 'Mckay', '1972-04-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742358', '1-5479298', '9812346518', 'keyon_mckay@gmail.com', 'www.keyon.com', '5413389', '12458689','4579268','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lucian', 'Coffey', '1972-04-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742359', '1-5479299', '9812346519', 'lucian_coffey@gmail.com', 'www.lucian.com', '5413390', '12458690','4579269','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gonzalo', 'Mccarty', '1972-04-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742360', '1-5479300', '9812346520', 'gonzalo_mccarty@gmail.com', 'www.gonzalo.com', '5413391', '12458691','4579270','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jan', 'Ewing', '1972-05-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742361', '1-5479301', '9812346521', 'jan_ewing@gmail.com', 'www.jan.com', '5413392', '12458692','4579271','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kadyn', 'Cooley', '1972-05-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742362', '1-5479302', '9812346522', 'kadyn_cooley@gmail.com', 'www.kadyn.com', '5413393', '12458693','4579272','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rashad', 'Vaughan', '1972-05-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742363', '1-5479303', '9812346523', 'rashad_vaughan@gmail.com', 'www.rashad.com', '5413394', '12458694','4579273','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zain', 'Bonner', '1972-05-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742364', '1-5479304', '9812346524', 'zain_bonner@gmail.com', 'www.zain.com', '5413395', '12458695','4579274','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tristian', 'Cotton', '1972-05-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742365', '1-5479305', '9812346525', 'tristian_cotton@gmail.com', 'www.tristian.com', '5413396', '12458696','4579275','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Blaze', 'Holder', '1972-05-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742366', '1-5479306', '9812346526', 'blaze_holder@gmail.com', 'www.blaze.com', '5413397', '12458697','4579276','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darwin', 'Stark', '1972-05-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742367', '1-5479307', '9812346527', 'darwin_stark@gmail.com', 'www.darwin.com', '5413398', '12458698','4579277','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elian', 'Ferrell', '1972-05-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742368', '1-5479308', '9812346528', 'elian_ferrell@gmail.com', 'www.elian.com', '5413399', '12458699','4579278','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Giovani', 'Cantrell', '1972-05-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742369', '1-5479309', '9812346529', 'giovani_cantrell@gmail.com', 'www.giovani.com', '5413400', '12458700','4579279','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Haden', 'Fulton', '1972-05-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742370', '1-5479310', '9812346530', 'haden_fulton@gmail.com', 'www.haden.com', '5413401', '12458701','4579280','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sammy', 'Lynn', '1972-05-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742371', '1-5479311', '9812346531', 'sammy_lynn@gmail.com', 'www.sammy.com', '5413402', '12458702','4579281','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Yosef', 'Lott', '1972-05-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742372', '1-5479312', '9812346532', 'yosef_lott@gmail.com', 'www.yosef.com', '5413403', '12458703','4579282','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Domenic', 'Calderon', '1972-05-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742373', '1-5479313', '9812346533', 'domenic_calderon@gmail.com', 'www.domenic.com', '5413404', '12458704','4579283','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jovan', 'Rosa', '1972-05-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742374', '1-5479314', '9812346534', 'jovan_rosa@gmail.com', 'www.jovan.com', '5413405', '12458705','4579284','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Matias', 'Pollard', '1972-05-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742375', '1-5479315', '9812346535', 'matias_pollard@gmail.com', 'www.matias.com', '5413406', '12458706','4579285','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Simeon', 'Hooper', '1972-05-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742376', '1-5479316', '9812346536', 'simeon_hooper@gmail.com', 'www.simeon.com', '5413407', '12458707','4579286','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nikhil', 'Burch', '1972-05-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742377', '1-5479317', '9812346537', 'nikhil_burch@gmail.com', 'www.nikhil.com', '5413408', '12458708','4579287','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Teagan', 'Mullen', '1972-05-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742378', '1-5479318', '9812346538', 'teagan_mullen@gmail.com', 'www.teagan.com', '5413409', '12458709','4579288','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Luka', 'Fry', '1972-05-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742379', '1-5479319', '9812346539', 'luka_fry@gmail.com', 'www.luka.com', '5413410', '12458710','4579289','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zaire', 'Riddle', '1972-05-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742380', '1-5479320', '9812346540', 'zaire_riddle@gmail.com', 'www.zaire.com', '5413411', '12458711','4579290','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bronson', 'Levy', '1972-05-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742381', '1-5479321', '9812346541', 'bronson_levy@gmail.com', 'www.bronson.com', '5413412', '12458712','4579291','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carmine', 'David', '1972-05-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742382', '1-5479322', '9812346542', 'carmine_david@gmail.com', 'www.carmine.com', '5413413', '12458713','4579292','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chaim', 'Duke', '1972-05-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742383', '1-5479323', '9812346543', 'chaim_duke@gmail.com', 'www.chaim.com', '5413414', '12458714','4579293','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dexter', 'Odonnell', '1972-05-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742384', '1-5479324', '9812346544', 'dexter_odonnell@gmail.com', 'www.dexter.com', '5413415', '12458715','4579294','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jamel', 'Guy', '1972-05-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742385', '1-5479325', '9812346545', 'jamel_guy@gmail.com', 'www.jamel.com', '5413416', '12458716','4579295','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Paxton', 'Michael', '1972-05-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742386', '1-5479326', '9812346546', 'paxton_michael@gmail.com', 'www.paxton.com', '5413417', '12458717','4579296','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Franco', 'Britt', '1972-05-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742387', '1-5479327', '9812346547', 'franco_britt@gmail.com', 'www.franco.com', '5413418', '12458718','4579297','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gordon', 'Frederick', '1972-05-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742388', '1-5479328', '9812346548', 'gordon_frederick@gmail.com', 'www.gordon.com', '5413419', '12458719','4579298','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leandro', 'Daugherty', '1972-05-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742389', '1-5479329', '9812346549', 'leandro_daugherty@gmail.com', 'www.leandro.com', '5413420', '12458720','4579299','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maximillian', 'Berger', '1972-05-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742390', '1-5479330', '9812346550', 'maximillian_berger@gmail.com', 'www.maximillian.com', '5413421', '12458721','4579300','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Korey', 'Dillard', '1972-05-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742391', '1-5479331', '9812346551', 'korey_dillard@gmail.com', 'www.korey.com', '5413422', '12458722','4579301','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Abdullah', 'Alston', '1972-06-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742392', '1-5479332', '9812346552', 'abdullah_alston@gmail.com', 'www.abdullah.com', '5413423', '12458723','4579302','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Aedan', 'Jarvis', '1972-06-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742393', '1-5479333', '9812346553', 'aedan_jarvis@gmail.com', 'www.aedan.com', '5413424', '12458724','4579303','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kason', 'Frye', '1972-06-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742394', '1-5479334', '9812346554', 'kason_frye@gmail.com', 'www.kason.com', '5413425', '12458725','4579304','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Andreas', 'Riggs', '1972-06-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742395', '1-5479335', '9812346555', 'andreas_riggs@gmail.com', 'www.andreas.com', '5413426', '12458726','4579305','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Finnegan', 'Chaney', '1972-06-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742396', '1-5479336', '9812346556', 'finnegan_chaney@gmail.com', 'www.finnegan.com', '5413427', '12458727','4579306','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zakary', 'Odom', '1972-06-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742397', '1-5479337', '9812346557', 'zakary_odom@gmail.com', 'www.zakary.com', '5413428', '12458728','4579307','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kanye', 'Duffy', '1972-06-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742398', '1-5479338', '9812346558', 'kanye_duffy@gmail.com', 'www.kanye.com', '5413429', '12458729','4579308','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Matthias', 'Fitzpatrick', '1972-06-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742399', '1-5479339', '9812346559', 'matthias_fitzpatrick@gmail.com', 'www.matthias.com', '5413430', '12458730','4579309','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Denzel', 'Valenzuela', '1972-06-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742400', '1-5479340', '9812346560', 'denzel_valenzuela@gmail.com', 'www.denzel.com', '5413431', '12458731','4579310','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reuben', 'Merrill', '1972-06-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742401', '1-5479341', '9812346561', 'reuben_merrill@gmail.com', 'www.reuben.com', '5413432', '12458732','4579311','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Soren', 'Mayer', '1972-06-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742402', '1-5479342', '9812346562', 'soren_mayer@gmail.com', 'www.soren.com', '5413433', '12458733','4579312','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Travon', 'Alford', '1972-06-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742403', '1-5479343', '9812346563', 'travon_alford@gmail.com', 'www.travon.com', '5413434', '12458734','4579313','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Vincenzo', 'Mcpherson', '1972-06-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742404', '1-5479344', '9812346564', 'vincenzo_mcpherson@gmail.com', 'www.vincenzo.com', '5413435', '12458735','4579314','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carmelo', 'Acevedo', '1972-06-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742405', '1-5479345', '9812346565', 'carmelo_acevedo@gmail.com', 'www.carmelo.com', '5413436', '12458736','4579315','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jacoby', 'Donovan', '1972-06-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742406', '1-5479346', '9812346566', 'jacoby_donovan@gmail.com', 'www.jacoby.com', '5413437', '12458737','4579316','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Karter', 'Barrera', '1972-06-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742407', '1-5479347', '9812346567', 'karter_barrera@gmail.com', 'www.karter.com', '5413438', '12458738','4579317','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ronaldo', 'Albert', '1972-06-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742408', '1-5479348', '9812346568', 'ronaldo_albert@gmail.com', 'www.ronaldo.com', '5413439', '12458739','4579318','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chaz', 'Cote', '1972-06-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742409', '1-5479349', '9812346569', 'chaz_cote@gmail.com', 'www.chaz.com', '5413440', '12458740','4579319','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Damari', 'Reilly', '1972-06-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742410', '1-5479350', '9812346570', 'damari_reilly@gmail.com', 'www.damari.com', '5413441', '12458741','4579320','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rey', 'Compton', '1972-06-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742411', '1-5479351', '9812346571', 'rey_compton@gmail.com', 'www.rey.com', '5413442', '12458742','4579321','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Messiah', 'Raymond', '1972-06-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742412', '1-5479352', '9812346572', 'messiah_raymond@gmail.com', 'www.messiah.com', '5413443', '12458743','4579322','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zavier', 'Mooney', '1972-06-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742413', '1-5479353', '9812346573', 'zavier_mooney@gmail.com', 'www.zavier.com', '5413444', '12458744','4579323','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Broderick', 'Mcgowan', '1972-06-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742414', '1-5479354', '9812346574', 'broderick_mcgowan@gmail.com', 'www.broderick.com', '5413445', '12458745','4579324','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darrius', 'Craft', '1972-06-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742415', '1-5479355', '9812346575', 'darrius_craft@gmail.com', 'www.darrius.com', '5413446', '12458746','4579325','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gaige', 'Cleveland', '1972-06-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742416', '1-5479356', '9812346576', 'gaige_cleveland@gmail.com', 'www.gaige.com', '5413447', '12458747','4579326','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maximo', 'Clemons', '1972-06-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742417', '1-5479357', '9812346577', 'maximo_clemons@gmail.com', 'www.maximo.com', '5413448', '12458748','4579327','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Royce', 'Wynn', '1972-06-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742418', '1-5479358', '9812346578', 'royce_wynn@gmail.com', 'www.royce.com', '5413449', '12458749','4579328','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shannon', 'Nielsen', '1972-06-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742419', '1-5479359', '9812346579', 'shannon_nielsen@gmail.com', 'www.shannon.com', '5413450', '12458750','4579329','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Stephan', 'Baird', '1972-06-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742420', '1-5479360', '9812346580', 'stephan_baird@gmail.com', 'www.stephan.com', '5413451', '12458751','4579330','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Makhi', 'Stanton', '1972-06-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742421', '1-5479361', '9812346581', 'makhi_stanton@gmail.com', 'www.makhi.com', '5413452', '12458752','4579331','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gannon', 'Snider', '1972-07-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742422', '1-5479362', '9812346582', 'gannon_snider@gmail.com', 'www.gannon.com', '5413453', '12458753','4579332','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Immanuel', 'Rosales', '1972-07-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742423', '1-5479363', '9812346583', 'immanuel_rosales@gmail.com', 'www.immanuel.com', '5413454', '12458754','4579333','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tyshawn', 'Bright', '1972-07-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742424', '1-5479364', '9812346584', 'tyshawn_bright@gmail.com', 'www.tyshawn.com', '5413455', '12458755','4579334','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cristofer', 'Witt', '1972-07-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742425', '1-5479365', '9812346585', 'cristofer_witt@gmail.com', 'www.cristofer.com', '5413456', '12458756','4579335','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ethen', 'Stuart', '1972-07-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742426', '1-5479366', '9812346586', 'ethen_stuart@gmail.com', 'www.ethen.com', '5413457', '12458757','4579336','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jeramiah', 'Hays', '1972-07-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742427', '1-5479367', '9812346587', 'jeramiah_hays@gmail.com', 'www.jeramiah.com', '5413458', '12458758','4579337','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Fredrick', 'Holden', '1972-07-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742428', '1-5479368', '9812346588', 'fredrick_holden@gmail.com', 'www.fredrick.com', '5413459', '12458759','4579338','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sullivan', 'Rutledge', '1972-07-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742429', '1-5479369', '9812346589', 'sullivan_rutledge@gmail.com', 'www.sullivan.com', '5413460', '12458760','4579339','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Norman', 'Kinney', '1972-07-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742430', '1-5479370', '9812346590', 'norman_kinney@gmail.com', 'www.norman.com', '5413461', '12458761','4579340','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brenton', 'Clements', '1972-07-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742431', '1-5479371', '9812346591', 'brenton_clements@gmail.com', 'www.brenton.com', '5413462', '12458762','4579341','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Campbell', 'Castaneda', '1972-07-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742432', '1-5479372', '9812346592', 'campbell_castaneda@gmail.com', 'www.campbell.com', '5413463', '12458763','4579342','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Fredy', 'Slater', '1972-07-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742433', '1-5479373', '9812346593', 'fredy_slater@gmail.com', 'www.fredy.com', '5413464', '12458764','4579343','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keven', 'Hahn', '1972-07-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742434', '1-5479374', '9812346594', 'keven_hahn@gmail.com', 'www.keven.com', '5413465', '12458765','4579344','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Stone', 'Emerson', '1972-07-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742435', '1-5479375', '9812346595', 'stone_emerson@gmail.com', 'www.stone.com', '5413466', '12458766','4579345','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Clifford', 'Conrad', '1972-07-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742436', '1-5479376', '9812346596', 'clifford_conrad@gmail.com', 'www.clifford.com', '5413467', '12458767','4579346','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Deshaun', 'Burks', '1972-07-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742437', '1-5479377', '9812346597', 'deshaun_burks@gmail.com', 'www.deshaun.com', '5413468', '12458768','4579347','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jordyn', 'Delaney', '1972-07-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742438', '1-5479378', '9812346598', 'jordyn_delaney@gmail.com', 'www.jordyn.com', '5413469', '12458769','4579348','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kamren', 'Pate', '1972-07-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742439', '1-5479379', '9812346599', 'kamren_pate@gmail.com', 'www.kamren.com', '5413470', '12458770','4579349','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nash', 'Lancaster', '1972-07-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742440', '1-5479380', '9812346600', 'nash_lancaster@gmail.com', 'www.nash.com', '5413471', '12458771','4579350','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rishi', 'Sweet', '1972-07-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742441', '1-5479381', '9812346601', 'rishi_sweet@gmail.com', 'www.rishi.com', '5413472', '12458772','4579351','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zack', 'Justice', '1972-07-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742442', '1-5479382', '9812346602', 'zack_justice@gmail.com', 'www.zack.com', '5413473', '12458773','4579352','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darin', 'Tyson', '1972-07-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742443', '1-5479383', '9812346603', 'darin_tyson@gmail.com', 'www.darin.com', '5413474', '12458774','4579353','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Destin', 'Sharpe', '1972-07-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742444', '1-5479384', '9812346604', 'destin_sharpe@gmail.com', 'www.destin.com', '5413475', '12458775','4579354','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Guadalupe', 'Whitfield', '1972-07-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742445', '1-5479385', '9812346605', 'guadalupe_whitfield@gmail.com', 'www.guadalupe.com', '5413476', '12458776','4579355','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jarvis', 'Talley', '1972-07-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742446', '1-5479386', '9812346606', 'jarvis_talley@gmail.com', 'www.jarvis.com', '5413477', '12458777','4579356','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Perry', 'Macias', '1972-07-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742447', '1-5479387', '9812346607', 'perry_macias@gmail.com', 'www.perry.com', '5413478', '12458778','4579357','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dandre', 'Irwin', '1972-07-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742448', '1-5479388', '9812346608', 'dandre_irwin@gmail.com', 'www.dandre.com', '5413479', '12458779','4579358','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Daryl', 'Burris', '1972-07-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742449', '1-5479389', '9812346609', 'daryl_burris@gmail.com', 'www.daryl.com', '5413480', '12458780','4579359','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaidyn', 'Ratliff', '1972-07-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742450', '1-5479390', '9812346610', 'jaidyn_ratliff@gmail.com', 'www.jaidyn.com', '5413481', '12458781','4579360','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Thaddeus', 'Mccray', '1972-07-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742451', '1-5479391', '9812346611', 'thaddeus_mccray@gmail.com', 'www.thaddeus.com', '5413482', '12458782','4579361','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elvin', 'Madden', '1972-07-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742452', '1-5479392', '9812346612', 'elvin_madden@gmail.com', 'www.elvin.com', '5413483', '12458783','4579362','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tayshaun', 'Kaufman', '1972-08-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742453', '1-5479393', '9812346613', 'tayshaun_kaufman@gmail.com', 'www.tayshaun.com', '5413484', '12458784','4579363','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Valentin', 'Beach', '1972-08-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742454', '1-5479394', '9812346614', 'valentin_beach@gmail.com', 'www.valentin.com', '5413485', '12458785','4579364','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Yusuf', 'Goff', '1972-08-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742455', '1-5479395', '9812346615', 'yusuf_goff@gmail.com', 'www.yusuf.com', '5413486', '12458786','4579365','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Austen', 'Cash', '1972-08-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742456', '1-5479396', '9812346616', 'austen_cash@gmail.com', 'www.austen.com', '5413487', '12458787','4579366','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Camryn', 'Bolton', '1972-08-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742457', '1-5479397', '9812346617', 'camryn_bolton@gmail.com', 'www.camryn.com', '5413488', '12458788','4579367','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Colt', 'Mcfadden', '1972-08-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742458', '1-5479398', '9812346618', 'colt_mcfadden@gmail.com', 'www.colt.com', '5413489', '12458789','4579368','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Darion', 'Levine', '1972-08-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742459', '1-5479399', '9812346619', 'darion_levine@gmail.com', 'www.darion.com', '5413490', '12458790','4579369','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dimitri', 'Good', '1972-08-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742460', '1-5479400', '9812346620', 'dimitri_good@gmail.com', 'www.dimitri.com', '5413491', '12458791','4579370','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Earl', 'Byers', '1972-08-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742461', '1-5479401', '9812346621', 'earl_byers@gmail.com', 'www.earl.com', '5413492', '12458792','4579371','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Arnav', 'Kirkland', '1972-08-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742462', '1-5479402', '9812346622', 'arnav_kirkland@gmail.com', 'www.arnav.com', '5413493', '12458793','4579372','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Baby', 'Kidd', '1972-08-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742463', '1-5479403', '9812346623', 'baby_kidd@gmail.com', 'www.baby.com', '5413494', '12458794','4579373','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kelton', 'Workman', '1972-08-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742464', '1-5479404', '9812346624', 'kelton_workman@gmail.com', 'www.kelton.com', '5413495', '12458795','4579374','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kurt', 'Carney', '1972-08-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742465', '1-5479405', '9812346625', 'kurt_carney@gmail.com', 'www.kurt.com', '5413496', '12458796','4579375','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leroy', 'Dale', '1972-08-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742466', '1-5479406', '9812346626', 'leroy_dale@gmail.com', 'www.leroy.com', '5413497', '12458797','4579376','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mariano', 'Mcleod', '1972-08-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742467', '1-5479407', '9812346627', 'mariano_mcleod@gmail.com', 'www.mariano.com', '5413498', '12458798','4579377','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dallin', 'Holcomb', '1972-08-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742468', '1-5479408', '9812346628', 'dallin_holcomb@gmail.com', 'www.dallin.com', '5413499', '12458799','4579378','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dion', 'England', '1972-08-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742469', '1-5479409', '9812346629', 'dion_england@gmail.com', 'www.dion.com', '5413500', '12458800','4579379','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Reilly', 'Finch', '1972-08-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742470', '1-5479410', '9812346630', 'reilly_finch@gmail.com', 'www.reilly.com', '5413501', '12458801','4579380','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Efren', 'Head', '1972-08-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742471', '1-5479411', '9812346631', 'efren_head@gmail.com', 'www.efren.com', '5413502', '12458802','4579381','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Fidel', 'Burt', '1972-08-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742472', '1-5479412', '9812346632', 'fidel_burt@gmail.com', 'www.fidel.com', '5413503', '12458803','4579382','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaydin', 'Hendrix', '1972-08-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742473', '1-5479413', '9812346633', 'jaydin_hendrix@gmail.com', 'www.jaydin.com', '5413504', '12458804','4579383','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rocky', 'Sosa', '1972-08-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742474', '1-5479414', '9812346634', 'rocky_sosa@gmail.com', 'www.rocky.com', '5413505', '12458805','4579384','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shayne', 'Haney', '1972-08-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742475', '1-5479415', '9812346635', 'shayne_haney@gmail.com', 'www.shayne.com', '5413506', '12458806','4579385','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bernardo', 'Franks', '1972-08-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742476', '1-5479416', '9812346636', 'bernardo_franks@gmail.com', 'www.bernardo.com', '5413507', '12458807','4579386','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Branson', 'Sargent', '1972-08-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742477', '1-5479417', '9812346637', 'branson_sargent@gmail.com', 'www.branson.com', '5413508', '12458808','4579387','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hugh', 'Nieves', '1972-08-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742478', '1-5479418', '9812346638', 'hugh_nieves@gmail.com', 'www.hugh.com', '5413509', '12458809','4579388','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maxim', 'Downs', '1972-08-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742479', '1-5479419', '9812346639', 'maxim_downs@gmail.com', 'www.maxim.com', '5413510', '12458810','4579389','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Shea', 'Rasmussen', '1972-08-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742480', '1-5479420', '9812346640', 'shea_rasmussen@gmail.com', 'www.shea.com', '5413511', '12458811','4579390','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Carlton', 'Bird', '1972-08-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742481', '1-5479421', '9812346641', 'carlton_bird@gmail.com', 'www.carlton.com', '5413512', '12458812','4579391','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Dylon', 'Hewitt', '1972-08-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742482', '1-5479422', '9812346642', 'dylon_hewitt@gmail.com', 'www.dylon.com', '5413513', '12458813','4579392','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gauge', 'Lindsay', '1972-08-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742483', '1-5479423', '9812346643', 'gauge_lindsay@gmail.com', 'www.gauge.com', '5413514', '12458814','4579393','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cristobal', 'Le', '1972-09-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742484', '1-5479424', '9812346644', 'cristobal_le@gmail.com', 'www.cristobal.com', '5413515', '12458815','4579394','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Irving', 'Foreman', '1972-09-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742485', '1-5479425', '9812346645', 'irving_foreman@gmail.com', 'www.irving.com', '5413516', '12458816','4579395','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Marquez', 'Valencia', '1972-09-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742486', '1-5479426', '9812346646', 'marquez_valencia@gmail.com', 'www.marquez.com', '5413517', '12458817','4579396','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ulysses', 'Oneil', '1972-09-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742487', '1-5479427', '9812346647', 'ulysses_oneil@gmail.com', 'www.ulysses.com', '5413518', '12458818','4579397','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jax', 'Delacruz', '1972-09-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742488', '1-5479428', '9812346648', 'jax_delacruz@gmail.com', 'www.jax.com', '5413519', '12458819','4579398','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mikel', 'Vinson', '1972-09-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742489', '1-5479429', '9812346649', 'mikel_vinson@gmail.com', 'www.mikel.com', '5413520', '12458820','4579399','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rhys', 'Dejesus', '1972-09-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742490', '1-5479430', '9812346650', 'rhys_dejesus@gmail.com', 'www.rhys.com', '5413521', '12458821','4579400','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Van', 'Hyde', '1972-09-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742491', '1-5479431', '9812346651', 'van_hyde@gmail.com', 'www.van.com', '5413522', '12458822','4579401','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Cornelius', 'Forbes', '1972-09-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742492', '1-5479432', '9812346652', 'cornelius_forbes@gmail.com', 'www.cornelius.com', '5413523', '12458823','4579402','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Geoffrey', 'Gilliam', '1972-09-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742493', '1-5479433', '9812346653', 'geoffrey_gilliam@gmail.com', 'www.geoffrey.com', '5413524', '12458824','4579403','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Eliseo', 'Guthrie', '1972-09-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742494', '1-5479434', '9812346654', 'eliseo_guthrie@gmail.com', 'www.eliseo.com', '5413525', '12458825','4579404','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaren', 'Wooten', '1972-09-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742495', '1-5479435', '9812346655', 'jaren_wooten@gmail.com', 'www.jaren.com', '5413526', '12458826','4579405','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kennedy', 'Huber', '1972-09-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742496', '1-5479436', '9812346656', 'kennedy_huber@gmail.com', 'www.kennedy.com', '5413527', '12458827','4579406','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Markell', 'Barlow', '1972-09-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742497', '1-5479437', '9812346657', 'markell_barlow@gmail.com', 'www.markell.com', '5413528', '12458828','4579407','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Adin', 'Boyle', '1972-09-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742498', '1-5479438', '9812346658', 'adin_boyle@gmail.com', 'www.adin.com', '5413529', '12458829','4579408','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isaak', 'Mcmahon', '1972-09-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742499', '1-5479439', '9812346659', 'isaak_mcmahon@gmail.com', 'www.isaak.com', '5413530', '12458830','4579409','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keanu', 'Buckner', '1972-09-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742500', '1-5479440', '9812346660', 'keanu_buckner@gmail.com', 'www.keanu.com', '5413531', '12458831','4579410','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Keyshawn', 'Rocha', '1972-09-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742501', '1-5479441', '9812346661', 'keyshawn_rocha@gmail.com', 'www.keyshawn.com', '5413532', '12458832','4579411','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tariq', 'Puckett', '1972-09-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742502', '1-5479442', '9812346662', 'tariq_puckett@gmail.com', 'www.tariq.com', '5413533', '12458833','4579412','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Daquan', 'Langley', '1972-09-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742503', '1-5479443', '9812346663', 'daquan_langley@gmail.com', 'www.daquan.com', '5413534', '12458834','4579413','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Edison', 'Knowles', '1972-09-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742504', '1-5479444', '9812346664', 'edison_knowles@gmail.com', 'www.edison.com', '5413535', '12458835','4579414','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Bridger', 'Cooke', '1972-09-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742505', '1-5479445', '9812346665', 'bridger_cooke@gmail.com', 'www.bridger.com', '5413536', '12458836','4579415','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Fisher', 'Velazquez', '1972-09-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742506', '1-5479446', '9812346666', 'fisher_velazquez@gmail.com', 'www.fisher.com', '5413537', '12458837','4579416','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jaheim', 'Whitley', '1972-09-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742507', '1-5479447', '9812346667', 'jaheim_whitley@gmail.com', 'www.jaheim.com', '5413538', '12458838','4579417','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Tye', 'Noel', '1972-09-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742508', '1-5479448', '9812346668', 'tye_noel@gmail.com', 'www.tye.com', '5413539', '12458839','4579418','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Blaise', 'Vang', '1972-09-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742509', '1-5479449', '9812346669', 'blaise_vang@gmail.com', 'www.blaise.com', '5413540', '12458840','4579419','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emily', 'Shea', '1972-09-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742510', '1-5479450', '9812346670', 'emily_shea@gmail.com', 'www.emily.com', '5413541', '12458841','4579420','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Emma', 'Rouse', '1972-09-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742511', '1-5479451', '9812346671', 'emma_rouse@gmail.com', 'www.emma.com', '5413542', '12458842','4579421','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Madison', 'Hartley', '1972-09-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742512', '1-5479452', '9812346672', 'madison_hartley@gmail.com', 'www.madison.com', '5413543', '12458843','4579422','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Abigail', 'Mayfield', '1972-09-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742513', '1-5479453', '9812346673', 'abigail_mayfield@gmail.com', 'www.abigail.com', '5413544', '12458844','4579423','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Olivia', 'Elder', '1972-10-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742514', '1-5479454', '9812346674', 'olivia_elder@gmail.com', 'www.olivia.com', '5413545', '12458845','4579424','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Isabella', 'Rankin', '1972-10-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742515', '1-5479455', '9812346675', 'isabella_rankin@gmail.com', 'www.isabella.com', '5413546', '12458846','4579425','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hannah', 'Hanna', '1972-10-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742516', '1-5479456', '9812346676', 'hannah_hanna@gmail.com', 'www.hannah.com', '5413547', '12458847','4579426','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Samantha', 'Cowan', '1972-10-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742517', '1-5479457', '9812346677', 'samantha_cowan@gmail.com', 'www.samantha.com', '5413548', '12458848','4579427','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ava', 'Lucero', '1972-10-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742518', '1-5479458', '9812346678', 'ava_lucero@gmail.com', 'www.ava.com', '5413549', '12458849','4579428','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ashley', 'Arroyo', '1972-10-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742519', '1-5479459', '9812346679', 'ashley_arroyo@gmail.com', 'www.ashley.com', '5413550', '12458850','4579429','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sophia', 'Slaughter', '1972-10-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742520', '1-5479460', '9812346680', 'sophia_slaughter@gmail.com', 'www.sophia.com', '5413551', '12458851','4579430','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Elizabeth', 'Haas', '1972-10-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742521', '1-5479461', '9812346681', 'elizabeth_haas@gmail.com', 'www.elizabeth.com', '5413552', '12458852','4579431','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexis', 'Oconnell', '1972-10-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742522', '1-5479462', '9812346682', 'alexis_oconnell@gmail.com', 'www.alexis.com', '5413553', '12458853','4579432','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Grace', 'Minor', '1972-10-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742523', '1-5479463', '9812346683', 'grace_minor@gmail.com', 'www.grace.com', '5413554', '12458854','4579433','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sarah', 'Kendrick', '1972-10-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742524', '1-5479464', '9812346684', 'sarah_kendrick@gmail.com', 'www.sarah.com', '5413555', '12458855','4579434','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alyssa', 'Shirley', '1972-10-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742525', '1-5479465', '9812346685', 'alyssa_shirley@gmail.com', 'www.alyssa.com', '5413556', '12458856','4579435','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mia', 'Kendall', '1972-10-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742526', '1-5479466', '9812346686', 'mia_kendall@gmail.com', 'www.mia.com', '5413557', '12458857','4579436','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Natalie', 'Boucher', '1972-10-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742527', '1-5479467', '9812346687', 'natalie_boucher@gmail.com', 'www.natalie.com', '5413558', '12458858','4579437','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Chloe', 'Archer', '1972-10-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742528', '1-5479468', '9812346688', 'chloe_archer@gmail.com', 'www.chloe.com', '5413559', '12458859','4579438','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brianna', 'Boggs', '1972-10-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742529', '1-5479469', '9812346689', 'brianna_boggs@gmail.com', 'www.brianna.com', '5413560', '12458860','4579439','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lauren', 'Odell', '1972-10-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742530', '1-5479470', '9812346690', 'lauren_odell@gmail.com', 'www.lauren.com', '5413561', '12458861','4579440','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Ella', 'Dougherty', '1972-10-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742531', '1-5479471', '9812346691', 'ella_dougherty@gmail.com', 'www.ella.com', '5413562', '12458862','4579441','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Anna', 'Andersen', '1972-10-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742532', '1-5479472', '9812346692', 'anna_andersen@gmail.com', 'www.anna.com', '5413563', '12458863','4579442','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Taylor', 'Newell', '1972-10-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742533', '1-5479473', '9812346693', 'taylor_newell@gmail.com', 'www.taylor.com', '5413564', '12458864','4579443','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kayla', 'Crowe', '1972-10-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742534', '1-5479474', '9812346694', 'kayla_crowe@gmail.com', 'www.kayla.com', '5413565', '12458865','4579444','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Hailey', 'Wang', '1972-10-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742535', '1-5479475', '9812346695', 'hailey_wang@gmail.com', 'www.hailey.com', '5413566', '12458866','4579445','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jessica', 'Friedman', '1972-10-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742536', '1-5479476', '9812346696', 'jessica_friedman@gmail.com', 'www.jessica.com', '5413567', '12458867','4579446','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Victoria', 'Bland', '1972-10-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742537', '1-5479477', '9812346697', 'victoria_bland@gmail.com', 'www.victoria.com', '5413568', '12458868','4579447','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jasmine', 'Swain', '1972-10-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742538', '1-5479478', '9812346698', 'jasmine_swain@gmail.com', 'www.jasmine.com', '5413569', '12458869','4579448','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sydney', 'Holley', '1972-10-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742539', '1-5479479', '9812346699', 'sydney_holley@gmail.com', 'www.sydney.com', '5413570', '12458870','4579449','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Julia', 'Felix', '1972-10-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742540', '1-5479480', '9812346700', 'julia_felix@gmail.com', 'www.julia.com', '5413571', '12458871','4579450','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Destiny', 'Pearce', '1972-10-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742541', '1-5479481', '9812346701', 'destiny_pearce@gmail.com', 'www.destiny.com', '5413572', '12458872','4579451','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Morgan', 'Childs', '1972-10-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742542', '1-5479482', '9812346702', 'morgan_childs@gmail.com', 'www.morgan.com', '5413573', '12458873','4579452','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kaitlyn', 'Yarbrough', '1972-10-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742543', '1-5479483', '9812346703', 'kaitlyn_yarbrough@gmail.com', 'www.kaitlyn.com', '5413574', '12458874','4579453','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Savannah', 'Galvan', '1972-10-31'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742544', '1-5479484', '9812346704', 'savannah_galvan@gmail.com', 'www.savannah.com', '5413575', '12458875','4579454','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Katherine', 'Proctor', '1972-11-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742545', '1-5479485', '9812346705', 'katherine_proctor@gmail.com', 'www.katherine.com', '5413576', '12458876','4579455','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexandra', 'Meeks', '1972-11-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742546', '1-5479486', '9812346706', 'alexandra_meeks@gmail.com', 'www.alexandra.com', '5413577', '12458877','4579456','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Rachel', 'Lozano', '1972-11-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742547', '1-5479487', '9812346707', 'rachel_lozano@gmail.com', 'www.rachel.com', '5413578', '12458878','4579457','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lily', 'Mora', '1972-11-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742548', '1-5479488', '9812346708', 'lily_mora@gmail.com', 'www.lily.com', '5413579', '12458879','4579458','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Megan', 'Rangel', '1972-11-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742549', '1-5479489', '9812346709', 'megan_rangel@gmail.com', 'www.megan.com', '5413580', '12458880','4579459','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kaylee', 'Bacon', '1972-11-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742550', '1-5479490', '9812346710', 'kaylee_bacon@gmail.com', 'www.kaylee.com', '5413581', '12458881','4579460','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jennifer', 'Villanueva', '1972-11-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742551', '1-5479491', '9812346711', 'jennifer_villanueva@gmail.com', 'www.jennifer.com', '5413582', '12458882','4579461','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Angelina', 'Schaefer', '1972-11-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742552', '1-5479492', '9812346712', 'angelina_schaefer@gmail.com', 'www.angelina.com', '5413583', '12458883','4579462','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Makayla', 'Rosado', '1972-11-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742553', '1-5479493', '9812346713', 'makayla_rosado@gmail.com', 'www.makayla.com', '5413584', '12458884','4579463','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Allison', 'Helms', '1972-11-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742554', '1-5479494', '9812346714', 'allison_helms@gmail.com', 'www.allison.com', '5413585', '12458885','4579464','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Brooke', 'Boyce', '1972-11-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742555', '1-5479495', '9812346715', 'brooke_boyce@gmail.com', 'www.brooke.com', '5413586', '12458886','4579465','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maria', 'Goss', '1972-11-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742556', '1-5479496', '9812346716', 'maria_goss@gmail.com', 'www.maria.com', '5413587', '12458887','4579466','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Trinity', 'Stinson', '1972-11-13'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742557', '1-5479497', '9812346717', 'trinity_stinson@gmail.com', 'www.trinity.com', '5413588', '12458888','4579467','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Lillian', 'Smart', '1972-11-14'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742558', '1-5479498', '9812346718', 'lillian_smart@gmail.com', 'www.lillian.com', '5413589', '12458889','4579468','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mackenzie', 'Lake', '1972-11-15'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742559', '1-5479499', '9812346719', 'mackenzie_lake@gmail.com', 'www.mackenzie.com', '5413590', '12458890','4579469','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Faith', 'Ibarra', '1972-11-16'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742560', '1-5479500', '9812346720', 'faith_ibarra@gmail.com', 'www.faith.com', '5413591', '12458891','4579470','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sofia', 'Hutchins', '1972-11-17'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742561', '1-5479501', '9812346721', 'sofia_hutchins@gmail.com', 'www.sofia.com', '5413592', '12458892','4579471','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Riley', 'Covington', '1972-11-18'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742562', '1-5479502', '9812346722', 'riley_covington@gmail.com', 'www.riley.com', '5413593', '12458893','4579472','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Haley', 'Reyna', '1972-11-19'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742563', '1-5479503', '9812346723', 'haley_reyna@gmail.com', 'www.haley.com', '5413594', '12458894','4579473','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gabrielle', 'Gregg', '1972-11-20'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742564', '1-5479504', '9812346724', 'gabrielle_gregg@gmail.com', 'www.gabrielle.com', '5413595', '12458895','4579474','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nicole', 'Werner', '1972-11-21'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742565', '1-5479505', '9812346725', 'nicole_werner@gmail.com', 'www.nicole.com', '5413596', '12458896','4579475','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kylie', 'Crowley', '1972-11-22'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742566', '1-5479506', '9812346726', 'kylie_crowley@gmail.com', 'www.kylie.com', '5413597', '12458897','4579476','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Katelyn', 'Hatcher', '1972-11-23'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742567', '1-5479507', '9812346727', 'katelyn_hatcher@gmail.com', 'www.katelyn.com', '5413598', '12458898','4579477','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Zoe', 'Mackey', '1972-11-24'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742568', '1-5479508', '9812346728', 'zoe_mackey@gmail.com', 'www.zoe.com', '5413599', '12458899','4579478','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Paige', 'Bunch', '1972-11-25'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742569', '1-5479509', '9812346729', 'paige_bunch@gmail.com', 'www.paige.com', '5413600', '12458900','4579479','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Gabriella', 'Womack', '1972-11-26'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742570', '1-5479510', '9812346730', 'gabriella_womack@gmail.com', 'www.gabriella.com', '5413601', '12458901','4579480','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jenna', 'Polk', '1972-11-27'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742571', '1-5479511', '9812346731', 'jenna_polk@gmail.com', 'www.jenna.com', '5413602', '12458902','4579481','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Kimberly', 'Jamison', '1972-11-28'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742572', '1-5479512', '9812346732', 'kimberly_jamison@gmail.com', 'www.kimberly.com', '5413603', '12458903','4579482','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Stephanie', 'Dodd', '1972-11-29'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742573', '1-5479513', '9812346733', 'stephanie_dodd@gmail.com', 'www.stephanie.com', '5413604', '12458904','4579483','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Alexa', 'Childress', '1972-11-30'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742574', '1-5479514', '9812346734', 'alexa_childress@gmail.com', 'www.alexa.com', '5413605', '12458905','4579484','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Avery', 'Childers', '1972-12-01'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742575', '1-5479515', '9812346735', 'avery_childers@gmail.com', 'www.avery.com', '5413606', '12458906','4579485','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Andrea', 'Camp', '1972-12-02'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742576', '1-5479516', '9812346736', 'andrea_camp@gmail.com', 'www.andrea.com', '5413607', '12458907','4579486','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Leah', 'Villa', '1972-12-03'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742577', '1-5479517', '9812346737', 'leah_villa@gmail.com', 'www.leah.com', '5413608', '12458908','4579487','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Madeline', 'Dye', '1972-12-04'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742578', '1-5479518', '9812346738', 'madeline_dye@gmail.com', 'www.madeline.com', '5413609', '12458909','4579488','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Nevaeh', 'Springer', '1972-12-05'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742579', '1-5479519', '9812346739', 'nevaeh_springer@gmail.com', 'www.nevaeh.com', '5413610', '12458910','4579489','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Evelyn', 'Mahoney', '1972-12-06'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742580', '1-5479520', '9812346740', 'evelyn_mahoney@gmail.com', 'www.evelyn.com', '5413611', '12458911','4579490','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Maya', 'Dailey', '1972-12-07'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742581', '1-5479521', '9812346741', 'maya_dailey@gmail.com', 'www.maya.com', '5413612', '12458912','4579491','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Mary', 'Belcher', '1972-12-08'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742582', '1-5479522', '9812346742', 'mary_belcher@gmail.com', 'www.mary.com', '5413613', '12458913','4579492','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Michelle', 'Lockhart', '1972-12-09'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742583', '1-5479523', '9812346743', 'michelle_lockhart@gmail.com', 'www.michelle.com', '5413614', '12458914','4579493','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Jada', 'Griggs', '1972-12-10'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742584', '1-5479524', '9812346744', 'jada_griggs@gmail.com', 'www.jada.com', '5413615', '12458915','4579494','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Sara', 'Costa', '1972-12-11'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742585', '1-5479525', '9812346745', 'sara_costa@gmail.com', 'www.sara.com', '5413616', '12458916','4579495','f'::boolean,0,0,'t'::boolean,5,3,15 UNION ALL
SELECT  1, 'Audrey', 'Connor', '1972-12-12'::date, 'Yuma', ' Colorado', ' USA', 'Yuma  Colorado  USA', '1-5742586', '1-5479526', '9812346746', 'audrey_connor@gmail.com', 'www.audrey.com', '5413617', '12458917','4579496','f'::boolean,0,0,'t'::boolean,5,3,15;


UPDATE core.parties
SET party_name = REPLACE(TRIM(COALESCE(last_name, '') || ', ' || first_name || ' ' || COALESCE(middle_name, '')), ' ', '');

ALTER TABLE core.parties
DROP column shipping_address;


/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'ITP', 'IBM Thinkpadd II Laptop', 1, 1, 1, 1, 'No', 1, 10, 'Yes', 80000, 125000;

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'AIT', 'Acer Iconia Tab', 1, 1, 1, 1, 'Yes', 1, 10, 'Yes', 40000, 65000;

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'IXM', 'Intex Mouse', 1, 1, 1, 1, 'No', 1, 10, 'Yes', 200, 350;

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'MSO', 'Microsoft Office Premium Edition', 1, 1, 1, 1, 'Yes', 1, 10, 'Yes', 30000, 35000;

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'LBS', 'Lotus Banking Solution', 1, 1, 1, 1, 'Yes', 1, 10, 'No', 150000, 150000;

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'CAS', 'CAS Banking Solution', 1, 1, 1, 1, 'Yes', 1, 10, 'No', 40000, 40000;

INSERT INTO core.items(item_code, item_name, item_group_id, brand_id, preferred_supplier_id, unit_id, hot_item, tax_id, reorder_level, maintain_stock, cost_price, selling_price)
SELECT 'SGT', 'Samsung Galaxy Tab 10.1', 1, 1, 1, 1, 'No', 1, 10, 'Yes', 30000, 45000;

INSERT INTO office.stores(office_id, store_code, store_name, address, store_type_id, allow_sales)
SELECT 1, 'STORE-1', 'Store 1', 'Office', 2, true UNION ALL
SELECT 1, 'GODOW-1', 'Godown 1', 'Office', 2, false;

INSERT INTO office.cash_repositories(office_id, cash_repository_code, cash_repository_name, description)
SELECT 2, 'DRW1', 'Drawer 1', 'Drawer' UNION ALL
SELECT 2, 'VLT', 'Vault', 'Vault';

INSERT INTO core.shippers(company_name, account_id)
SELECT 'Default', core.get_account_id_by_account_code('20110');
