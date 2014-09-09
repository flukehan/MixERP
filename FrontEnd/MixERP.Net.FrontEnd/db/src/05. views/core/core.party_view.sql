CREATE VIEW core.party_view
AS
SELECT
	core.parties.party_id,
	core.party_types.party_type_id,
	core.party_types.is_supplier,
	core.party_types.party_type_code || ' (' || core.party_types.party_type_name || ')' AS party_type,
	core.parties.party_code,
	core.parties.first_name,
	core.parties.middle_name,
	core.parties.last_name,
	core.parties.party_name,
	core.parties.po_box,
	core.parties.address_line_1,
	core.parties.address_line_2,
	core.parties.street,
	core.parties.city,
	core.parties.state,
	core.parties.country,
	core.parties.allow_credit,
	core.parties.maximum_credit_period,
	core.parties.maximum_credit_amount,
	core.parties.charge_interest,
	core.parties.interest_rate,
	core.get_frequency_code_by_frequency_id(interest_compounding_frequency_id) AS compounding_frequency,
	core.parties.pan_number,
	core.parties.sst_number,
	core.parties.cst_number,
	core.parties.phone,
	core.parties.fax,
	core.parties.cell,
	core.parties.email,
	core.parties.url,
	core.accounts.account_id,
	core.accounts.account_code,
	core.accounts.account_code || ' (' || core.accounts.account_name || ')' AS gl_head
FROM
core.parties
INNER JOIN
core.party_types
ON core.parties.party_type_id = core.party_types.party_type_id
INNER JOIN core.accounts
ON core.parties.account_id=core.accounts.account_id;
