--This table should not be localized.
WITH http_actions
AS
(
SELECT 'GET' as code UNION ALL
SELECT 'PUT' UNION ALL
SELECT 'POST' UNION ALL
SELECT 'DELETE'
)

INSERT INTO policy.http_actions
SELECT * FROM http_actions
WHERE code NOT IN
(
    SELECT http_action_code FROM policy.http_actions
    WHERE http_action_code IN('GET','PUT','POST','DELETE')
);
