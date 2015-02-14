DROP FUNCTION IF EXISTS public.poco_get_table_function_definition
(
    _schema         text,
    _name           text
);

CREATE FUNCTION public.poco_get_table_function_definition
(
    _schema                 text,
    _name                   text
)
RETURNS TABLE
(
    column_name             text,
    is_nullable             text,
    udt_name                text,
    column_default          text
)
STABLE
AS
$$
    DECLARE _oid            oid;
    DECLARE _typoid         oid;
BEGIN
    SELECT 
        pg_proc.oid,
        pg_proc.prorettype
    INTO 
        _oid,
        _typoid
    FROM pg_proc
    INNER JOIN pg_namespace
    ON pg_proc.pronamespace = pg_namespace.oid
    WHERE pg_proc.proname=_name
    AND pg_namespace.nspname=_schema
    LIMIT 1;

    IF EXISTS
    (
        SELECT 1
        FROM information_schema.columns 
        WHERE table_schema=_schema 
        AND table_name=_name
    ) THEN
        RETURN QUERY
        SELECT 
            information_schema.columns.column_name::text, 
            information_schema.columns.is_nullable::text, 
            information_schema.columns.udt_name::text, 
            information_schema.columns.column_default::text
        FROM information_schema.columns 
        WHERE table_schema=_schema 
        AND table_name=_name;
        RETURN;
    END IF;

    IF EXISTS(SELECT * FROM pg_type WHERE oid = _typoid AND typtype='c') THEN
        --Composite Type
        RETURN QUERY
        SELECT 
            attname::text               AS column_name,
            'NO'::text                  AS is_nullable, 
            format_type(t.oid,NULL)     AS udt_name,
            ''::text                    AS column_default
        FROM pg_attribute att
        JOIN pg_type t ON t.oid=atttypid
        JOIN pg_namespace nsp ON t.typnamespace=nsp.oid
        LEFT OUTER JOIN pg_type b ON t.typelem=b.oid
        LEFT OUTER JOIN pg_collation c ON att.attcollation=c.oid
        LEFT OUTER JOIN pg_namespace nspc ON c.collnamespace=nspc.oid
        WHERE att.attrelid=(SELECT typrelid FROM pg_type WHERE pg_type.oid = _typoid)
        ORDER by attnum;
        RETURN;
    END IF;

    IF(_oid IS NOT NULL) THEN
        RETURN QUERY
        WITH procs
        AS
        (
            SELECT 
            explode_array(proargnames) as column_name,
            explode_array(proargmodes) as column_mode,
            explode_array(proallargtypes) as argument_type
            FROM pg_proc
            WHERE oid = _oid
        )
        SELECT 
            procs.column_name::text,
            'NO'::text AS is_nullable, 
            format_type(procs.argument_type, null) as udt_name,
            ''::text AS column_default
        FROM procs
        WHERE column_mode=ANY(ARRAY['t', 'o']);

        RETURN;
    END IF;

    RETURN QUERY
    SELECT 
        attname::text               AS column_name,
        'NO'::text                  AS is_nullable, 
        format_type(t.oid,NULL)     AS udt_name,
        ''::text                    AS column_default
    FROM pg_attribute att
    JOIN pg_type t ON t.oid=atttypid
    JOIN pg_namespace nsp ON t.typnamespace=nsp.oid
    LEFT OUTER JOIN pg_type b ON t.typelem=b.oid
    LEFT OUTER JOIN pg_collation c ON att.attcollation=c.oid
    LEFT OUTER JOIN pg_namespace nspc ON c.collnamespace=nspc.oid
    WHERE att.attrelid=
    (
        SELECT typrelid 
        FROM pg_type
        INNER JOIN pg_namespace
        ON pg_type.typnamespace = pg_namespace.oid
        WHERE typname=_name
        AND pg_namespace.nspname=_schema
    )
    ORDER by attnum;
END;
$$
LANGUAGE plpgsql;


--SELECT * from public.poco_get_table_function_definition('office', 'get_offices');

--SELECT * FROM public.poco_get_table_function_definition('transactions', 'opening_stock_type');

--SELECT * FROM public.poco_get_table_function_definition('core', 'item_types');