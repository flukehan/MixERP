INSERT INTO core.switch_categories(switch_category_name)
SELECT 'General';


INSERT INTO policy.auto_verification_policy
SELECT 2, true, 0, true, 0, true, 0, '1-1-2010', '1-1-2020', true;


