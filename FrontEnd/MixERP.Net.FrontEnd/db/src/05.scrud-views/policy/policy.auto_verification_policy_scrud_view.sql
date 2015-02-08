CREATE VIEW policy.auto_verification_policy_scrud_view
AS
SELECT
    policy.auto_verification_policy.user_id,
    office.users.user_name,
    policy.auto_verification_policy.verify_sales_transactions,
    policy.auto_verification_policy.sales_verification_limit,
    policy.auto_verification_policy.verify_purchase_transactions,
    policy.auto_verification_policy.purchase_verification_limit,
    policy.auto_verification_policy.verify_gl_transactions,
    policy.auto_verification_policy.gl_verification_limit,
    policy.auto_verification_policy.effective_from,
    policy.auto_verification_policy.ends_on,
    policy.auto_verification_policy.is_active
FROM policy.auto_verification_policy
INNER JOIN office.users
ON policy.auto_verification_policy.user_id=office.users.user_id;

