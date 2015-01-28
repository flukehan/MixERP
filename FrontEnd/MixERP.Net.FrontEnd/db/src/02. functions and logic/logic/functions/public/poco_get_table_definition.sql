DROP FUNCTION IF EXISTS public.poco_get_table_function_definition
(
    _schema         text,
    _name           text
);

CREATE FUNCTION public.poco_get_table_function_definition
(
    _schema         text,
    _name           text
)
RETURNS TABLE
(
    column_name     text,
    is_nullable     text,
    udt_name        text,
    column_default  text
)
STABLE
AS
$$
    DECLARE _oid oid;
BEGIN
    SELECT pg_proc.oid INTO _oid
    FROM pg_proc
    INNER JOIN pg_namespace
    ON pg_proc.pronamespace = pg_namespace.oid
    WHERE pg_proc.proname=_name
    AND pg_namespace.nspname=_schema
    LIMIT 1;

    IF(_oid IS NULL) THEN
        RETURN QUERY
        SELECT 
            information_schema.columns.column_name::text, 
            information_schema.columns.is_nullable::text, 
            information_schema.columns.udt_name::text, 
            information_schema.columns.column_default::text
        FROM information_schema.columns 
        WHERE table_schema=_schema 
        AND table_name=_name;
    END IF;

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
    WHERE column_mode='t';

END
$$
LANGUAGE plpgsql;


--SELECT * from public.poco_get_table_function_definition('core', 'get_associated_units');