CREATE OR REPLACE VIEW scrud.mixerp_table_view
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
AND pg_class.relnamespace = (SELECT oid FROM pg_namespace WHERE nspname=pg_tables.schemaname)
INNER JOIN pg_attribute ON pg_class.oid = pg_attribute.attrelid 
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
AND pg_attribute.attnum > 0 
ORDER BY pg_attribute.attnum;