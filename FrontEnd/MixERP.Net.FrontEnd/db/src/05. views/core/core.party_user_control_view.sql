CREATE VIEW core.party_user_control_view
AS
SELECT
    core.party_types.party_type_code,
    core.party_types.party_type_name,
    core.parties.email,
    core.parties.url,
    core.parties.pan_number,
    core.parties.sst_number,
    core.parties.cst_number,
    core.parties.allow_credit,
    core.parties.maximum_credit_period,
    core.parties.maximum_credit_amount,
    core.accounts.account_number,
    core.accounts.account_name,
    core.parties.zip_code,
    core.parties.address_line_1,
    core.parties.address_line_2,
    core.parties.street,
    core.get_state_name_by_state_id(core.parties.state_id) AS state,
    core.get_country_name_by_country_id(core.parties.country_id) AS country
FROM core.parties
INNER JOIN core.party_types
ON core.parties.party_type_id = core.party_types.party_type_id
INNER JOIN core.accounts
ON core.parties.account_id = core.accounts.account_id;

