DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO core.entities(entity_name)
        SELECT 'Federal Government'                         UNION
        SELECT 'Sole Proprietorship'                        UNION
        SELECT 'General Partnership'                        UNION
        SELECT 'Limited Partnership'                        UNION
        SELECT 'Limited Liability Partnership'              UNION
        SELECT 'Limited Liability Limited Partnership'      UNION
        SELECT 'Limited Liability Company'                  UNION
        SELECT 'Professional Limited Liability Company'     UNION
        SELECT 'Benefit Corporation'                        UNION
        SELECT 'C Corporation'                              UNION
        SELECT 'Series Limited Liability Company'           UNION
        SELECT 'S Corporation'                              UNION
        SELECT 'Delaware Corporation'                       UNION
        SELECT 'Delaware Statutory Trust'                   UNION
        SELECT 'Massachusetts Business Trust'               UNION
        SELECT 'Nevada Corporation';

        INSERT INTO core.industries(industry_name)
        SELECT 'Accounting'                                 UNION
        SELECT 'Advertising'                                UNION
        SELECT 'Aerospace'                                  UNION
        SELECT 'Aircraft'                                   UNION
        SELECT 'Airline'                                    UNION
        SELECT 'Apparel & Accessories'                      UNION
        SELECT 'Automotive'                                 UNION
        SELECT 'Banking'                                    UNION
        SELECT 'Broadcasting'                               UNION
        SELECT 'Brokerage'                                  UNION
        SELECT 'Biotechnology'                              UNION
        SELECT 'Call Centers'                               UNION
        SELECT 'Cargo Handling'                             UNION
        SELECT 'Chemical'                                   UNION
        SELECT 'Computer'                                   UNION
        SELECT 'Consulting'                                 UNION
        SELECT 'Consumer Products'                          UNION
        SELECT 'Cosmetics'                                  UNION
        SELECT 'Defence'                                    UNION
        SELECT 'Department Stores'                          UNION
        SELECT 'Education'                                  UNION
        SELECT 'Electronics'                                UNION
        SELECT 'Energy'                                     UNION
        SELECT 'Entertainment & Leisure'                    UNION
        SELECT 'Executive Search'                           UNION
        SELECT 'Financial Services'                         UNION
        SELECT 'Food, Beverage & Tobacco'                   UNION
        SELECT 'Grocery'                                    UNION
        SELECT 'Health Care'                                UNION
        SELECT 'Internet Publishing'                        UNION
        SELECT 'Investment Banking'                         UNION
        SELECT 'Legal'                                      UNION
        SELECT 'Manufacturing'                              UNION
        SELECT 'Motion Picture & Video'                     UNION
        SELECT 'Music'                                      UNION
        SELECT 'Newspaper Publishers'                       UNION
        SELECT 'On-line Auctions'                           UNION
        SELECT 'Pension Funds'                              UNION
        SELECT 'Pharmaceuticals'                            UNION
        SELECT 'Private Equity'                             UNION
        SELECT 'Publishing'                                 UNION
        SELECT 'Real Estate'                                UNION
        SELECT 'Retail & Wholesale'                         UNION
        SELECT 'Securities & Commodity Exchanges'           UNION
        SELECT 'Service'                                    UNION
        SELECT 'Soap & Detergent'                           UNION
        SELECT 'Software'                                   UNION
        SELECT 'Sports'                                     UNION
        SELECT 'Technology'                                 UNION
        SELECT 'Telecommunications'                         UNION
        SELECT 'Television'                                 UNION
        SELECT 'Transportation'                             UNION
        SELECT 'Trucking'                                   UNION
        SELECT 'Venture Capital';


        INSERT INTO core.sales_tax_types(sales_tax_type_code, sales_tax_type_name, is_vat)
        SELECT 'SAT',   'Sales Tax',            false   UNION ALL
        SELECT 'VAT',   'Value Added Tax',      true;

        INSERT INTO core.tax_exempt_types(tax_exempt_type_code, tax_exempt_type_name)
        SELECT 'EXI', 'Exempt (Item)' UNION ALL
        SELECT 'EXP', 'Exempt (Party)' UNION ALL
        SELECT 'EXS', 'Exempt (Industry)' UNION ALL
        SELECT 'EXE', 'Exempt (Entity)';

        INSERT INTO core.tax_master(tax_master_code, tax_master_name)
        SELECT 'UST', 'United States Taxation' UNION ALL
        SELECT 'NPT', 'Nepal Taxation';

        INSERT INTO core.tax_authorities(tax_master_id, tax_authority_code, tax_authority_name, country_id, state_id, zip_code, address_line_1, address_line_2, street, city, phone, fax, email, url)
        SELECT 1, 'IRS', 'Internal Revenue Service', core.get_country_id_by_country_code('US'), core.get_state_id_by_state_code('NY'), '11201', '2 Metro Tech', '1st floor', '', 'Brooklyn', '(718) 834-6559', '', '', 'http://www.irs.gov' UNION ALL
        SELECT 1, 'IRD', 'Inland Revenue Department', core.get_country_id_by_country_code('NP'), NULL, '', 'INLAND REVENUE DEPARTMENT', 'Large Taxpayers Office', 'Hariharbhawan', 'Kathmandu', '5010049, 5010050, 5010051, 5010052, 5010053', '4411788', 'iro52@ird.gov.np', 'www.ird.gov.np/';


        INSERT INTO core.state_sales_taxes(state_sales_tax_code, state_sales_tax_name, state_id, rate) VALUES
        ('AL-STT', 'Alabama State Tax',             core.get_state_id_by_state_name('Alabama'),                 4), 
        ('AZ-STT', 'Arizona State Tax',             core.get_state_id_by_state_name('Arizona'),                 5.6), 
        ('AR-STT', 'Arkansas State Tax',            core.get_state_id_by_state_name('Arkansas'),                6.5), 
        ('CA-STT', 'California State Tax',          core.get_state_id_by_state_name('California'),              7.5), 
        ('CO-STT', 'Colorado State Tax',            core.get_state_id_by_state_name('Colorado'),                2.9), 
        ('CT-STT', 'Connecticut State Tax',         core.get_state_id_by_state_name('Connecticut'),             6.35), 
        ('DE-STT', 'Delaware State Tax',            core.get_state_id_by_state_name('Delaware'),                0), 
        ('DC-TAX', 'District of Columbia Tax',      core.get_state_id_by_state_name('District of Columbia'),    5.75), 
        ('FL-STT', 'Florida State Tax',             core.get_state_id_by_state_name('Florida'),                 6), 
        ('GA-STT', 'Georgia State Tax',             core.get_state_id_by_state_name('Georgia'),                 4), 
        ('HI-STT', 'Hawaii State Tax',              core.get_state_id_by_state_name('Hawaii'),                  4), 
        ('ID-STT', 'Idaho State Tax',               core.get_state_id_by_state_name('Idaho'),                   6), 
        ('IL-STT', 'Illinois State Tax',            core.get_state_id_by_state_name('Illinois'),                6.25), 
        ('IN-STT', 'Indiana State Tax',             core.get_state_id_by_state_name('Indiana'),                 7), 
        ('IA-STT', 'Iowa State Tax',                core.get_state_id_by_state_name('Iowa'),                    6), 
        ('KS-STT', 'Kansas State Tax',              core.get_state_id_by_state_name('Kansas'),                  6.15), 
        ('KY-STT', 'Kentucky State Tax',            core.get_state_id_by_state_name('Kentucky'),                6), 
        ('LA-STT', 'Louisiana State Tax',           core.get_state_id_by_state_name('Louisiana'),               4), 
        ('ME-STT', 'Maine State Tax',               core.get_state_id_by_state_name('Maine'),                   5.5), 
        ('MD-STT', 'Maryland State Tax',            core.get_state_id_by_state_name('Maryland'),                6), 
        ('MA-STT', 'Massachusetts State Tax',       core.get_state_id_by_state_name('Massachusetts'),           6.25), 
        ('MI-STT', 'Michigan State Tax',            core.get_state_id_by_state_name('Michigan'),                6), 
        ('MN-STT', 'Minnesota State Tax',           core.get_state_id_by_state_name('Minnesota'),               6.875), 
        ('MS-STT', 'Mississippi State Tax',         core.get_state_id_by_state_name('Mississippi'),             7), 
        ('MO-STT', 'Missouri State Tax',            core.get_state_id_by_state_name('Missouri'),                4.225), 
        ('NE-STT', 'Nebraska State Tax',            core.get_state_id_by_state_name('Nebraska'),                5.5), 
        ('NV-STT', 'Nevada State Tax',              core.get_state_id_by_state_name('Nevada'),                  6.85), 
        ('NJ-STT', 'New Jersey State Tax',          core.get_state_id_by_state_name('New Jersey'),              7), 
        ('NM-STT', 'New Mexico State Tax',          core.get_state_id_by_state_name('New Mexico'),              5.125), 
        ('NY-STT', 'New York State Tax',            core.get_state_id_by_state_name('New York'),                4), 
        ('NC-STT', 'North Carolina State Tax',      core.get_state_id_by_state_name('North Carolina'),          4.75), 
        ('ND-STT', 'North Dakota State Tax',        core.get_state_id_by_state_name('North Dakota'),            5), 
        ('OH-STT', 'Ohio State Tax',                core.get_state_id_by_state_name('Ohio'),                    5.75), 
        ('OK-STT', 'Oklahoma State Tax',            core.get_state_id_by_state_name('Oklahoma'),                4.5), 
        ('PA-STT', 'Pennsylvania State Tax',        core.get_state_id_by_state_name('Pennsylvania'),            6), 
        ('RI-STT', 'Rhode Island State Tax',        core.get_state_id_by_state_name('Rhode Island'),            7), 
        ('SC-STT', 'South Carolina State Tax',      core.get_state_id_by_state_name('South Carolina'),          6), 
        ('SD-STT', 'South Dakota State Tax',        core.get_state_id_by_state_name('South Dakota'),            4), 
        ('TN-STT', 'Tennessee State Tax',           core.get_state_id_by_state_name('Tennessee'),               7), 
        ('TX-STT', 'Texas State Tax',               core.get_state_id_by_state_name('Texas'),                   6.25), 
        ('UT-STT', 'Utah State Tax',                core.get_state_id_by_state_name('Utah'),                    4.7), 
        ('VT-STT', 'Vermont State Tax',             core.get_state_id_by_state_name('Vermont'),                 6), 
        ('VA-STT', 'Virginia State Tax',            core.get_state_id_by_state_name('Virginia'),                4.3), 
        ('WA-STT', 'Washington State Tax',          core.get_state_id_by_state_name('Washington'),              6.5), 
        ('WV-STT', 'West Virginia State Tax',       core.get_state_id_by_state_name('West Virginia'),           6), 
        ('WI-STT', 'Wisconsin State Tax',           core.get_state_id_by_state_name('Wisconsin'),               5), 
        ('WY-STT', 'Wyoming State Tax',             core.get_state_id_by_state_name('Wyoming'),                 4);

        INSERT INTO core.county_sales_taxes(county_id, county_sales_tax_code, county_sales_tax_name, rate)
        SELECT core.get_county_id_by_county_code('36047'), '36047-STX', 'Kings County Sales Tax', 4.875 UNION ALL
        SELECT core.get_county_id_by_county_code('6095'), '6095-STX', 'Solano County Sales Tax', 0.125;

        INSERT INTO core.brands(brand_code, brand_name)
        SELECT 'DEF', 'Default';

        INSERT INTO core.item_types(item_type_code, item_type_name)
        SELECT 'GEN', 'General'         UNION ALL
        SELECT 'COM', 'Component'       UNION ALL
        SELECT 'MAF', 'Manufacturing';

        INSERT INTO core.shipping_mail_types(shipping_mail_type_code, shipping_mail_type_name)
        SELECT 'FCM',   'First Class Mail'      UNION ALL
        SELECT 'PM',    'Priority Mail'         UNION ALL
        SELECT 'PP',    'Parcel Post'           UNION ALL
        SELECT 'EM',    'Express Mail'          UNION ALL
        SELECT 'MM',    'Media Mail';

        INSERT INTO core.shipping_package_shapes(shipping_package_shape_code, is_rectangular, shipping_package_shape_name)
        SELECT 'REC',   true,   'Rectangular Box Packaging'         UNION ALL
        SELECT 'IRR',   false,  'Irregular Packaging';
    END IF;
END
$$
LANGUAGE plpgsql;