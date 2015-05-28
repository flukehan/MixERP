-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/release-1/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
ALTER TABLE policy.auto_verification_policy
DROP CONSTRAINT IF EXISTS auto_verification_policy_pkey;

ALTER TABLE policy.auto_verification_policy
ADD PRIMARY KEY(user_id, office_id);
