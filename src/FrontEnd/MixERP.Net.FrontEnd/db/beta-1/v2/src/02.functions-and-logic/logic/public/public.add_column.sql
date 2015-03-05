DROP FUNCTION IF EXISTS public.add_column(regclass, text, regtype, text, text);

CREATE FUNCTION public.add_column
(
    _table_name     regclass, 
    _column_name    text, 
    _data_type      regtype,
    _default        text = '',
    _comment        text = ''
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS (
       SELECT 1 FROM pg_attribute
       WHERE  attrelid = _table_name
       AND    attname = _column_name
       AND    NOT attisdropped) THEN
        EXECUTE '
        ALTER TABLE ' || _table_name || ' ADD COLUMN ' || quote_ident(_column_name) || ' ' || _data_type;
    END IF;

    IF(COALESCE(_default, '') != '') THEN
        EXECUTE '
        ALTER TABLE ' || _table_name || ' ALTER COLUMN ' || _column_name || ' SET DEFAULT ' || _default;
    END IF;

    IF(COALESCE(_comment, '') != '') THEN
        EXECUTE '
        COMMENT ON COLUMN ' || _table_name || '.' || _column_name || ' IS ''' || _comment || ''''; 
    END IF;    
END
$$
LANGUAGE plpgsql;