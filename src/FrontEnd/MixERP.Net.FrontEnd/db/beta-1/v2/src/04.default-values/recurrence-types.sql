WITH recurrence_types
AS
(
SELECT 'FRE' AS code,   'Frequency' AS name,    true AS is_freq UNION ALL
SELECT 'DUR',           'Duration',             false
)

INSERT INTO core.recurrence_types(recurrence_type_code, recurrence_type_name, is_frequency)
SELECT * FROM recurrence_types
WHERE code NOT IN
(
    SELECT recurrence_type_code FROM core.recurrence_types
    WHERE recurrence_type_code IN('FRE','DUR')
);

UPDATE core.recurring_invoices 
SET recurrence_type_id = core.get_recurrence_type_id_by_recurrence_type_code('FRE')
WHERE recurrence_type_id IS NULL;

ALTER TABLE core.recurring_invoices
ALTER COLUMN recurrence_type_id SET NOT NULL;

ALTER TABLE core.recurring_invoices
ALTER COLUMN recurring_frequency_id DROP NOT NULL;