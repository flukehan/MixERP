ALTER TABLE policy.auto_verification_policy
DROP CONSTRAINT IF EXISTS auto_verification_policy_pkey;

ALTER TABLE policy.auto_verification_policy
ADD PRIMARY KEY(user_id, office_id);