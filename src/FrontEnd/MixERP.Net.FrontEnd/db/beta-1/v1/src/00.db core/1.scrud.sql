DROP SCHEMA IF EXISTS scrud CASCADE;
CREATE SCHEMA scrud;

COMMENT ON SCHEMA scrud IS 'Contains objects related to MixERP''s ScrudFactory project.';


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

COMMENT ON VIEW scrud.constraint_column_usage IS 'Lists all columns having constraints.';

    

CREATE VIEW scrud.relationship_view
AS
SELECT 
    o.conname                                                       AS constraint_name,
    (SELECT nspname FROM pg_namespace WHERE oid=m.relnamespace)     AS table_schema,
    m.relname                                                       AS table_name,
    (SELECT a.attname FROM pg_attribute a 
    WHERE a.attrelid = m.oid
    AND a.attnum = o.conkey[1]
    AND a.attisdropped = FALSE)
                                                                    AS column_name,
    (SELECT nspname FROM pg_namespace
     WHERE oid=f.relnamespace) AS references_schema,
       f.relname
                                                                    AS references_table,
    (SELECT a.attname FROM pg_attribute a
     WHERE a.attrelid = f.oid
     AND a.attnum = o.confkey[1]
     AND a.attisdropped = FALSE)
                                                                    AS references_field
FROM pg_constraint o
LEFT JOIN pg_class c ON c.oid = o.conrelid
LEFT JOIN pg_class f ON f.oid = o.confrelid
LEFT JOIN pg_class m ON m.oid = o.conrelid
WHERE o.contype = 'f'
AND o.conrelid IN
(SELECT oid
 FROM pg_class c
 WHERE c.relkind = 'r');
 
COMMENT ON VIEW scrud.relationship_view IS 'Lists all foreign key columns and their relation with the parent tables.';

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

COMMENT ON FUNCTION scrud.parse_default(text) IS 'Parses default constraint column values.';


CREATE VIEW scrud.mixerp_table_view
AS
SELECT 
    pg_tables.schemaname                                    AS table_schema, 
    pg_tables.tablename                                     AS table_name, 
    pg_attribute.attname                                    AS column_name,
    constraint_name,
    references_schema, 
    references_table, 
    references_field, 
    pg_attribute.attnum                                     AS ordinal_position,
    CASE pg_attribute.attnotnull 
    WHEN false THEN 'YES' 
    ELSE 'NO' END                                           AS is_nullable, 
    (SELECT 
        scrud.parse_default(pg_attrdef.adsrc) 
        FROM pg_attrdef 
        WHERE pg_attrdef.adrelid = pg_class.oid 
        AND pg_attrdef.adnum = pg_attribute.attnum)         AS column_default,    
    format_type(pg_attribute.atttypid, NULL)                AS data_type, 
    format_type(pg_attribute.atttypid, NULL)                AS domain_name, 
    CASE pg_attribute.atttypmod
    WHEN -1 THEN NULL 
    ELSE pg_attribute.atttypmod - 4
    END                                         AS character_maximum_length,    
    pg_constraint.conname AS "key", 
    pc2.conname AS ckey
FROM pg_tables
INNER JOIN pg_class 
ON pg_class.relname = pg_tables.tablename 
INNER JOIN pg_attribute ON pg_class.oid = pg_attribute.attrelid 
    AND pg_attribute.attnum > 0 
LEFT JOIN pg_constraint ON pg_constraint.contype = 'p'::"char" 
    AND pg_constraint.conrelid = pg_class.oid AND
    (pg_attribute.attnum = ANY (pg_constraint.conkey)) 
LEFT JOIN pg_constraint AS pc2 ON pc2.contype = 'f'::"char" 
    AND pc2.conrelid = pg_class.oid 
    AND (pg_attribute.attnum = ANY (pc2.conkey))    
LEFT JOIN scrud.relationship_view 
ON pg_tables.schemaname = scrud.relationship_view.table_schema 
 AND pg_tables.tablename = scrud.relationship_view.table_name 
 AND pg_attribute.attname = scrud.relationship_view.column_name 
WHERE pg_attribute.attname NOT IN
    (
        'audit_user_id', 'audit_ts'
    )
AND NOT pg_attribute.attisdropped
ORDER BY pg_attribute.attnum;


COMMENT ON VIEW scrud.mixerp_table_view IS 'Lists all schema, table, and columns with associated types, domains, references, and constraints.';