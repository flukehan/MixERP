CREATE VIEW core.tax_authority_scrud_view
AS
SELECT
	core.tax_authorities.tax_authority_id,
	core.tax_master.tax_master_code || '(' || core.tax_master.tax_master_name ||')' AS tax_master,
	core.tax_authorities.tax_authority_code,
	core.tax_authorities.tax_authority_name,
	core.countries.country_code || '(' || core.countries.country_name ||')' AS country,
	core.states.state_code || '(' || core.states.state_name ||')' AS county,
	core.tax_authorities.zip_code,
	core.tax_authorities.address_line_1,
	core.tax_authorities.address_line_2,
	core.tax_authorities.street,
	core.tax_authorities.city,
	core.tax_authorities.phone,
	core.tax_authorities.fax,
	core.tax_authorities.cell,
	core.tax_authorities.email,
	core.tax_authorities.url	
FROM core.tax_authorities
INNER JOIN core.tax_master
ON core.tax_authorities.tax_master_id = core.tax_master.tax_master_id
INNER JOIN core.countries
ON core.tax_authorities.country_id = core.countries.country_id
LEFT JOIN core.states
ON core.tax_authorities.state_id = core.states.state_id;
