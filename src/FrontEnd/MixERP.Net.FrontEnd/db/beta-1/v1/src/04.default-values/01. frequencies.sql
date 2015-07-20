DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO core.frequencies
        SELECT 2, 'EOM', 'End of Month'                 UNION ALL
        SELECT 3, 'EOQ', 'End of Quarter'               UNION ALL
        SELECT 4, 'EOH', 'End of Half'                  UNION ALL
        SELECT 5, 'EOY', 'End of Year';
     END IF;
END
$$
LANGUAGE plpgsql;