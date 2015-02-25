--This table should not be localized.
INSERT INTO policy.http_actions
SELECT 'GET' UNION ALL
SELECT 'PUT' UNION ALL
SELECT 'POST' UNION ALL
SELECT 'DELETE';
