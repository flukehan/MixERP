CREATE VIEW core.industry_scrud_view
AS          
SELECT 
    core.industries.industry_id, 
    core.industries.industry_name,
    parent_industry.industry_name AS parent_industry_name
FROM core.industries
LEFT JOIN core.industries AS parent_industry
ON core.industries.parent_industry_id = parent_industry.industry_id;