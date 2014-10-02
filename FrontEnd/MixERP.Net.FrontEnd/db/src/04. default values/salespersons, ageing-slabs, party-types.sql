INSERT INTO core.sales_teams(sales_team_code, sales_team_name)
SELECT 'DEF', 'Default' UNION ALL
SELECT 'CST', 'Corporate Sales Team' UNION ALL
SELECT 'RST', 'Retail Sales Team';

INSERT INTO core.salespersons(sales_team_id, salesperson_code, salesperson_name, address, contact_number, commission_rate, account_id)
SELECT 1, 'OFF', 'Office', 'Office', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100') UNION ALL
SELECT 2, 'ROS', 'Robert Schintowski', 'Russia', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100') UNION ALL
SELECT 2, 'PHJ', 'Phillipe Jones', 'France', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100') UNION ALL
SELECT 3, 'AWB', 'Alexander Walter Bishop', 'Texas', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100') UNION ALL
SELECT 3, 'LMA', 'Lisa Mary Ann', 'Austin', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100');




INSERT INTO core.ageing_slabs(ageing_slab_name,from_days,to_days)
SELECT 'SLAB 1',0, 30 UNION ALL
SELECT 'SLAB 2',31, 60 UNION ALL
SELECT 'SLAB 3',61, 90 UNION ALL
SELECT 'SLAB 4',91, 365 UNION ALL
SELECT 'SLAB 5',366, 999999;



INSERT INTO core.party_types(party_type_code, party_type_name) SELECT 'A', 'Agent';
INSERT INTO core.party_types(party_type_code, party_type_name) SELECT 'C', 'Customer';
INSERT INTO core.party_types(party_type_code, party_type_name) SELECT 'D', 'Dealer';
INSERT INTO core.party_types(party_type_code, party_type_name, is_supplier) SELECT 'S', 'Supplier', true;


