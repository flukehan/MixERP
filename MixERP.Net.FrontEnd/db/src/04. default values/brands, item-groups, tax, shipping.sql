INSERT INTO core.brands(brand_code, brand_name)
SELECT 'DEF', 'Default';



INSERT INTO core.tax_types(tax_type_code, tax_type_name)
SELECT 'DEF', 'Default';


INSERT INTO core.taxes(tax_type_id, tax_code, tax_name, rate, account_id)
SELECT 1, 'VAT', 'Value Added Tax', 13, (SELECT account_id FROM core.accounts WHERE account_name='Sales Tax Payable') UNION ALL
SELECT 1, 'SAT', 'Sales Tax', 5, (SELECT account_id FROM core.accounts WHERE account_name='Sales Tax Payable');

INSERT INTO core.item_groups(item_group_code, item_group_name, tax_id)
SELECT 'DEF', 'Default', 1;


INSERT INTO core.shipping_mail_types(shipping_mail_type_code, shipping_mail_type_name)
SELECT 'FCM', 	'First Class Mail'			UNION ALL
SELECT 'PM', 	'Priority Mail'			UNION ALL
SELECT 'PP', 	'Parcel Post'			UNION ALL
SELECT 'EM', 	'Express Mail'			UNION ALL
SELECT 'MM', 	'Media Mail';

INSERT INTO core.shipping_package_shapes(shipping_package_shape_code, is_rectangular, shipping_package_shape_name)
SELECT 'REC',	true,	'Rectangular Box Packaging'			UNION ALL
SELECT 'IRR',	false,	'Irregular Packaging';



