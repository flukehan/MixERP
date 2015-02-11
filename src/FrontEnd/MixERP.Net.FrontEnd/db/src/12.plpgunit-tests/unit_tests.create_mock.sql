DROP FUNCTION IF EXISTS unit_tests.create_mock();

CREATE FUNCTION unit_tests.create_mock()
RETURNS void
AS
$$
BEGIN
    PERFORM unit_tests.create_dummy_offices();
    PERFORM unit_tests.create_dummy_users();
    PERFORM unit_tests.create_dummy_accounts();
    PERFORM unit_tests.create_dummy_sales_taxes();
    PERFORM unit_tests.create_dummy_countries();
    PERFORM unit_tests.create_dummy_states();
    PERFORM unit_tests.create_dummy_party_types();
    PERFORM unit_tests.create_dummy_item_groups();
    PERFORM unit_tests.create_dummy_item_types();
    PERFORM unit_tests.create_dummy_units();
    PERFORM unit_tests.create_dummy_brands();
    PERFORM unit_tests.create_dummy_parties();
    PERFORM unit_tests.create_dummy_items();
    PERFORM unit_tests.create_dummy_cost_centers();
    PERFORM unit_tests.create_dummy_late_fees();
    PERFORM unit_tests.create_dummy_sales_teams();
    PERFORM unit_tests.create_dummy_salespersons();
    PERFORM unit_tests.create_dummy_shippers();
    PERFORM unit_tests.create_dummy_cash_repositories();
    PERFORM unit_tests.create_dummy_store_types();
    PERFORM unit_tests.create_dummy_stores();
END
$$
LANGUAGE plpgsql;