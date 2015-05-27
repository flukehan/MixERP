DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO core.sales_teams(sales_team_code, sales_team_name)
        SELECT 'DEF', 'Default'                 UNION ALL
        SELECT 'CST', 'Corporate Sales Team'    UNION ALL
        SELECT 'RST', 'Retail Sales Team';

        INSERT INTO core.party_types(party_type_code, party_type_name, account_id) SELECT 'A', 'Agent', core.get_account_id_by_account_number('20100');
        INSERT INTO core.party_types(party_type_code, party_type_name, account_id) SELECT 'C', 'Customer', core.get_account_id_by_account_number('10400');
        INSERT INTO core.party_types(party_type_code, party_type_name, account_id) SELECT 'D', 'Dealer', core.get_account_id_by_account_number('10400');
        INSERT INTO core.party_types(party_type_code, party_type_name, is_supplier, account_id) SELECT 'S', 'Supplier', true, core.get_account_id_by_account_number('20100');
    END IF;
END
$$
LANGUAGE plpgsql;