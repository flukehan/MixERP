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