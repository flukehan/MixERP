INSERT INTO core.agents(agent_code, agent_name, address, contact_number, commission_rate, account_id)
SELECT 'OFF', 'Office', 'Office', '', 0, (SELECT account_id FROM core.accounts WHERE account_code='20100');



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


