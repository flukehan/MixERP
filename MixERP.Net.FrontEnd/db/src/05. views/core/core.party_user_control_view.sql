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
	core.parties.charge_interest,
	core.parties.interest_rate,
	core.accounts.account_code,
	core.accounts.account_name,
	core.parties.po_box,
	core.parties.address_line_1,
	core.parties.address_line_2,
	core.parties.street,
	core.parties.city,
	core.parties.state,
	core.parties.country
FROM core.parties
INNER JOIN core.party_types
ON core.parties.party_type_id = core.party_types.party_type_id
INNER JOIN core.accounts
ON core.parties.account_id = core.accounts.account_id;
