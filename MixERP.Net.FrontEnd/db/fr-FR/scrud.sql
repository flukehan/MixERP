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
	
