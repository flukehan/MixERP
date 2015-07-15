DROP VIEW IF EXISTS core.party_scrud_view;

CREATE VIEW core.party_scrud_view
AS 
SELECT 
    parties.party_id,
    party_types.party_type_id,
    party_types.is_supplier,
    ((party_types.party_type_code::text || ' ('::text) || party_types.party_type_name::text) || ')'::text AS party_type,
    parties.party_code,
    parties.first_name,
    parties.middle_name,
    parties.last_name,
    CASE WHEN parties.company_name IS NULL THEN
    parties.party_name 
    ELSE
    parties.company_name  END AS party_name,
    parties.zip_code,
    parties.address_line_1,
    parties.address_line_2,
    parties.street,
    parties.city,
    core.get_state_name_by_state_id(parties.state_id) AS state,
    core.get_country_name_by_country_id(parties.country_id) AS country,
    parties.allow_credit,
    parties.maximum_credit_period,
    parties.maximum_credit_amount,
    parties.pan_number,
    parties.sst_number,
    parties.cst_number,
    parties.phone,
    parties.fax,
    parties.cell,
    parties.email,
    parties.url,
    accounts.account_id,
    accounts.account_number,
    ((accounts.account_number::text || ' ('::text) || accounts.account_name::text) || ')'::text AS gl_head
FROM core.parties
INNER JOIN core.party_types 
ON parties.party_type_id = party_types.party_type_id
INNER JOIN core.accounts 
ON parties.account_id = accounts.account_id;