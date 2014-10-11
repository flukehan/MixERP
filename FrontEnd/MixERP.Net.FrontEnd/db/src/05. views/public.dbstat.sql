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
