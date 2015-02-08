CREATE VIEW core.party_scrud_view
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
    core.parties.zip_code,
    core.parties.address_line_1,
    core.parties.address_line_2,
    core.parties.street,
    core.parties.city,
    core.get_state_name_by_state_id(core.parties.state_id) AS state,
    core.get_country_name_by_country_id(core.parties.country_id) AS country,
    core.parties.allow_credit,
    core.parties.maximum_credit_period,
    core.parties.maximum_credit_amount,
    core.parties.pan_number,
    core.parties.sst_number,
    core.parties.cst_number,
    core.parties.phone,
    core.parties.fax,
    core.parties.cell,
    core.parties.email,
    core.parties.url,
    core.accounts.account_id,
    core.accounts.account_number,
    core.accounts.account_number || ' (' || core.accounts.account_name || ')' AS gl_head
FROM
core.parties
INNER JOIN
core.party_types
ON core.parties.party_type_id = core.party_types.party_type_id
INNER JOIN core.accounts
ON core.parties.account_id=core.accounts.account_id;
