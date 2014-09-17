DROP FUNCTION IF EXISTS explode_array(in_array anyarray);

CREATE FUNCTION explode_array(in_array anyarray) 
RETURNS SETOF anyelement as
$$
    SELECT ($1)[s] FROM generate_series(1,array_upper($1, 1)) AS s;
$$
LANGUAGE sql 
IMMUTABLE;

--select * from explode_array(ARRAY[ROW(1, 1)::FOO_TYPE,ROW(1, 1)::FOO_TYPE])