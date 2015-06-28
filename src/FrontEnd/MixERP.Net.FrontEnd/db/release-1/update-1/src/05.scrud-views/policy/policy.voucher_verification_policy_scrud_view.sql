
DROP VIEW IF EXISTS policy.voucher_verification_policy_scrud_view CASCADE;

CREATE VIEW policy.voucher_verification_policy_scrud_view
AS
SELECT
	policy.voucher_verification_policy.policy_id,
    policy.voucher_verification_policy.user_id,
    office.users.user_name,
    policy.voucher_verification_policy.can_verify_sales_transactions,
    policy.voucher_verification_policy.sales_verification_limit,
    policy.voucher_verification_policy.can_verify_purchase_transactions,
    policy.voucher_verification_policy.purchase_verification_limit,
    policy.voucher_verification_policy.can_verify_gl_transactions,
    policy.voucher_verification_policy.gl_verification_limit,
    policy.voucher_verification_policy.can_self_verify,
    policy.voucher_verification_policy.self_verification_limit,
    policy.voucher_verification_policy.effective_from,
    policy.voucher_verification_policy.ends_on,
    policy.voucher_verification_policy.is_active
FROM policy.voucher_verification_policy
INNER JOIN office.users
ON policy.voucher_verification_policy.user_id=office.users.user_id;