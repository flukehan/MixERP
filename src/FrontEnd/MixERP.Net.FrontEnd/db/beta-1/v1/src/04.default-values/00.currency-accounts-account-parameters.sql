DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        ALTER TABLE core.accounts
        ALTER column currency_code DROP NOT NULL;

        INSERT INTO core.currencies
        SELECT 'NPR', 'रू.',       'Nepali Rupees',        'paisa'     UNION ALL
        SELECT 'USD', '$',      'United States Dollar', 'cents'     UNION ALL
        SELECT 'GBP', '£',      'Pound Sterling',       'penny'     UNION ALL
        SELECT 'EUR', '€',      'Euro',                 'cents'     UNION ALL
        SELECT 'JPY', '¥',      'Japanese Yen',         'sen'       UNION ALL
        SELECT 'CHF', 'CHF',    'Swiss Franc',          'centime'   UNION ALL
        SELECT 'CAD', '¢',      'Canadian Dollar',      'cent'      UNION ALL
        SELECT 'AUD', 'AU$',    'Australian Dollar',    'cent'      UNION ALL
        SELECT 'HKD', 'HK$',    'Hong Kong Dollar',     'cent'      UNION ALL
        SELECT 'INR', '₹',      'Indian Rupees',        'paise'     UNION ALL
        SELECT 'SEK', 'kr',     'Swedish Krona',        'öre'       UNION ALL
        SELECT 'NZD', 'NZ$',    'New Zealand Dollar',   'cent';

        INSERT INTO core.attachment_lookup(book, resource, resource_key)
        SELECT 'transaction',           'transactions.transaction_master',  'transaction_master_id' UNION ALL
        SELECT 'non-gl-transaction',    'transactions.non_gl_stock_master', 'non_gl_stock_master_id';

        INSERT INTO core.account_masters(account_master_id, account_master_code, account_master_name)
        SELECT 1, 'BSA', 'Balance Sheet A/C' UNION ALL
        SELECT 2, 'PLA', 'Profit & Loss A/C' UNION ALL
        SELECT 3, 'OBS', 'Off Balance Sheet A/C';

        INSERT INTO core.account_masters(account_master_id, account_master_code, account_master_name, parent_account_master_id, normally_debit)
        SELECT 10100, 'CRA', 'Current Assets',                      1,      true    UNION ALL
        SELECT 10101, 'CAS', 'Cash A/C',                            10100,  true    UNION ALL
        SELECT 10102, 'CAB', 'Bank A/C',                            10100,  true    UNION ALL
        SELECT 10110, 'ACR', 'Accounts Receivable',                 10100,  true    UNION ALL
        SELECT 10200, 'FIA', 'Fixed Assets',                        1,      true    UNION ALL
        SELECT 10201, 'PPE', 'Property, Plants, and Equipments',    1,      true    UNION ALL
        SELECT 10300, 'OTA', 'Other Assets',                        1,      true    UNION ALL
        SELECT 15000, 'CRL', 'Current Liabilities',                 1,      false   UNION ALL
        SELECT 15010, 'ACP', 'Accounts Payable',                    15000,  false   UNION ALL
        SELECT 15011, 'SAP', 'Salary Payable',                      15000,  false   UNION ALL
        SELECT 15100, 'LTL', 'Long-Term Liabilities',               1,      false   UNION ALL
        SELECT 15200, 'SHE', 'Shareholders'' Equity',               1,      false   UNION ALL
        SELECT 15300, 'RET', 'Retained Earnings',                   15200,  false   UNION ALL
        SELECT 15400, 'DIP', 'Dividends Paid',                      15300,  false;


        INSERT INTO core.account_masters(account_master_id, account_master_code, account_master_name, parent_account_master_id, normally_debit)
        SELECT 20100, 'REV', 'Revenue',                           2,        false   UNION ALL
        SELECT 20200, 'NOI', 'Non Operating Income',              2,        false   UNION ALL
        SELECT 20300, 'FII', 'Financial Incomes',                 2,        false   UNION ALL
        SELECT 20301, 'DIR', 'Dividends Received',                20300,    false   UNION ALL
        SELECT 20400, 'COS', 'Cost of Sales',                     2,        true    UNION ALL
        SELECT 20500, 'DRC', 'Direct Costs',                      2,        true    UNION ALL
        SELECT 20600, 'ORX', 'Operating Expenses',                2,        true    UNION ALL
        SELECT 20700, 'FIX', 'Financial Expenses',                2,        true    UNION ALL
        SELECT 20701, 'INT', 'Interest Expenses',                 20700,    true    UNION ALL
        SELECT 20800, 'ITX', 'Income Tax Expenses',               2,        true;

        INSERT INTO core.cash_flow_headings(cash_flow_heading_id, cash_flow_heading_code, cash_flow_heading_name, cash_flow_heading_type, is_debit)
        SELECT 20001, 'CRC',    'Cash Receipts from Customers',                 'O',   true    UNION ALL
        SELECT 20002, 'CPS',    'Cash Paid to Suppliers',                       'O',   false   UNION ALL
        SELECT 20003, 'CPE',    'Cash Paid to Employees',                       'O',   false   UNION ALL
        SELECT 20004, 'IP',     'Interest Paid',                                'O',   false   UNION ALL
        SELECT 20005, 'ITP',    'Income Taxes Paid',                            'O',   false   UNION ALL
        SELECT 20006, 'SUS',    'Against Suspense Accounts',                    'O',   true   UNION ALL
        SELECT 30001, 'PSE',    'Proceeds from the Sale of Equipment',          'I',   true    UNION ALL
        SELECT 30002, 'DR',     'Dividends Received',                           'I',   true    UNION ALL
        SELECT 40001, 'DP',     'Dividends Paid',                               'F',   false;

        UPDATE core.cash_flow_headings SET is_sales=true WHERE cash_flow_heading_code='CRC';
        UPDATE core.cash_flow_headings SET is_purchase=true WHERE cash_flow_heading_code='CPS';

        INSERT INTO core.cash_flow_setup(cash_flow_heading_id, account_master_id)
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('CRC'), core.get_account_master_id_by_account_master_code('ACR') UNION ALL --Cash Receipts from Customers/Accounts Receivable
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('CPS'), core.get_account_master_id_by_account_master_code('ACP') UNION ALL --Cash Paid to Suppliers/Accounts Payable
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('CPE'), core.get_account_master_id_by_account_master_code('SAP') UNION ALL --Cash Paid to Employees/Salary Payable
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('IP'),  core.get_account_master_id_by_account_master_code('INT') UNION ALL --Interest Paid/Interest Expenses
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('ITP'), core.get_account_master_id_by_account_master_code('ITX') UNION ALL --Income Taxes Paid/Income Tax Expenses
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('PSE'), core.get_account_master_id_by_account_master_code('PPE') UNION ALL --Proceeds from the Sale of Equipment/Property, Plants, and Equipments
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('DR'),  core.get_account_master_id_by_account_master_code('DIR') UNION ALL --Dividends Received/Dividends Received
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('DP'),  core.get_account_master_id_by_account_master_code('DIP') UNION ALL --Dividends Paid/Dividends Paid

        --We cannot guarantee that every transactions posted is 100% correct and falls under the above-mentioned categories.
        --The following is the list of suspense accounts, cash entries posted directly against all other account masters.
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('BSA') UNION ALL --Against Suspense Accounts/Balance Sheet A/C
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('PLA') UNION ALL --Against Suspense Accounts/Profit & Loss A/C
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('CRA') UNION ALL --Against Suspense Accounts/Current Assets
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('FIA') UNION ALL --Against Suspense Accounts/Fixed Assets
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('OTA') UNION ALL --Against Suspense Accounts/Other Assets
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('CRL') UNION ALL --Against Suspense Accounts/Current Liabilities
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('LTL') UNION ALL --Against Suspense Accounts/Long-Term Liabilities
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('SHE') UNION ALL --Against Suspense Accounts/Shareholders' Equity
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('RET') UNION ALL --Against Suspense Accounts/Retained Earning
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('REV') UNION ALL --Against Suspense Accounts/Revenue
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('NOI') UNION ALL --Against Suspense Accounts/Non Operating Income
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('FII') UNION ALL --Against Suspense Accounts/Financial Incomes
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('COS') UNION ALL --Against Suspense Accounts/Cost of Sales
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('DRC') UNION ALL --Against Suspense Accounts/Direct Costs
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('ORX') UNION ALL --Against Suspense Accounts/Operating Expenses
        SELECT core.get_cash_flow_heading_id_by_cash_flow_heading_code('SUS'), core.get_account_master_id_by_account_master_code('FIX');          --Against Suspense Accounts/Financial Expenses



        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10000', 'Assets', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Balance Sheet A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10001', 'Current Assets', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10100', 'Cash at Bank A/C', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10110', 'Regular Checking Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10120', 'Payroll Checking Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10130', 'Savings Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10140', 'Special Account', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cash at Bank A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10200', 'Cash in Hand A/C', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10300', 'Investments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10310', 'Short Term Investment', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Investments');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10320', 'Other Investments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Investments');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10321', 'Investments-Money Market', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Investments');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10322', 'Bank Deposit Contract (Fixed Deposit)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Investments');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10323', 'Investments-Certificates of Deposit', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Investments');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10400', 'Accounts Receivable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10500', 'Other Receivables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10501', 'Purchase Return (Receivables)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Receivables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10600', 'Allowance for Doubtful Accounts', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10700', 'Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10720', 'Raw Materials Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10730', 'Supplies Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10740', 'Work in Progress Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10750', 'Finished Goods Inventory', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Inventory');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10800', 'Prepaid Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '10900', 'Employee Advances', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '11000', 'Notes Receivable-Current', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '11100', 'Prepaid Interest', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '11200', 'Accrued Incomes (Assets)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '11300', 'Other Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '11400', 'Other Current Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12001', 'Noncurrent Assets', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12100', 'Furniture and Fixtures', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12200', 'Plants & Equipments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12300', 'Rental Property', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12400', 'Vehicles', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12500', 'Intangibles', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12600', 'Other Depreciable Properties', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12700', 'Leasehold Improvements', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12800', 'Buildings', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '12900', 'Building Improvements', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13000', 'Interior Decorations', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13100', 'Land', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13200', 'Long Term Investments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13300', 'Trade Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13400', 'Rental Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13500', 'Staff Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13600', 'Other Noncurrent Debtors', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13700', 'Other Financial Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13710', 'Deposits Held', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Financial Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13800', 'Accumulated Depreciations', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13810', 'Accumulated Depreciation-Furniture and Fixtures', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13820', 'Accumulated Depreciation-Equipment', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13830', 'Accumulated Depreciation-Vehicles', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13840', 'Accumulated Depreciation-Other Depreciable Properties', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13850', 'Accumulated Depreciation-Leasehold', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13860', 'Accumulated Depreciation-Buildings', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13870', 'Accumulated Depreciation-Building Improvements', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '13880', 'Accumulated Depreciation-Interior Decorations', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Accumulated Depreciations');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14001', 'Other Assets', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14100', 'Other Assets-Deposits', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14200', 'Other Assets-Organization Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14300', 'Other Assets-Accumulated Amortization-Organization Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14400', 'Notes Receivable-Non-current', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14500', 'Other Non-current Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '14600', 'Non-financial Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Assets');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20000', 'Liabilities', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Balance Sheet A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20001', 'Current Liabilities', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20100', 'Accounts Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20110', 'Shipping Charge Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20200', 'Accrued Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20300', 'Wages Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20400', 'Deductions Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20500', 'Health Insurance Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20600', 'Superannuation Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20700', 'Tax Payables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20701', 'Sales Return (Payables)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20710', 'Sales Tax Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20720', 'Federal Payroll Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20730', 'FUTA Tax Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20740', 'State Payroll Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20750', 'SUTA Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20760', 'Local Payroll Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20770', 'Income Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20780', 'Other Taxes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Tax Payables');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20800', 'Employee Benefits Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20810', 'Provision for Annual Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20820', 'Provision for Long Service Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20830', 'Provision for Personal Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20840', 'Provision for Health Leave', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefits Payable');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '20900', 'Current Portion of Long-term Debt', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21000', 'Advance Incomes', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21010', 'Advance Sales Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Advance Incomes');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21020', 'Grant Received in Advance', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Advance Incomes');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21100', 'Deposits from Customers', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21200', 'Other Current Liabilities', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21210', 'Short Term Loan Payables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21220', 'Short Term Hire-purchase Payables', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21230', 'Short Term Lease Liability', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '21240', 'Grants Repayable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Current Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24001', 'Noncurrent Liabilities', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24100', 'Notes Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24200', 'Land Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24300', 'Equipment Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24400', 'Vehicles Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24500', 'Lease Liability', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24600', 'Loan Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24700', 'Hire-purchase Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24800', 'Bank Loans Payable', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '24900', 'Deferred Revenue', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '25000', 'Other Long-term Liabilities', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Noncurrent Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '25010', 'Long Term Employee Benefit Provision', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Other Long-term Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28001', 'Equity', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Liabilities');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28100', 'Stated Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28110', 'Founder Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Stated Capital');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28120', 'Promoter Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Stated Capital');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28130', 'Member Capital', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Stated Capital');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28200', 'Capital Surplus', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28210', 'Share Premium', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28220', 'Capital Redemption Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28230', 'Statutory Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28240', 'Asset Revaluation Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28250', 'Exchange Rate Fluctuation Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28260', 'Capital Reserves Arising From Merger', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28270', 'Capital Reserves Arising From Acuisition', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Capital Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28300', 'Retained Surplus', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28310', 'Accumulated Profits', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Retained Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28320', 'Accumulated Losses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Retained Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28330', 'Dividends Declared (Common Stock)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Retained Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28340', 'Dividends Declared (Preferred Stock)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Retained Surplus');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28400', 'Treasury Stock', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28500', 'Current Year Surplus', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28600', 'General Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28700', 'Other Reserves', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28800', 'Dividends Payable (Common Stock)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 1, '28900', 'Dividends Payable (Preferred Stock)', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Equity');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30000', 'Revenues', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Profit and Loss A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30100', 'Sales A/C', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30200', 'Interest Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30300', 'Other Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30400', 'Finance Charge Income', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30500', 'Shipping Charges Reimbursed', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30600', 'Sales Returns and Allowances', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '30700', 'Purchase Discounts', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Revenues');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40000', 'Expenses', TRUE, (SELECT account_id FROM core.accounts WHERE account_name='Profit and Loss A/C');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40100', 'Purchase A/C', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40200', 'Cost of Goods Sold', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40205', 'Product Cost', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40210', 'Raw Material Purchases', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40215', 'Direct Labor Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40220', 'Indirect Labor Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40225', 'Heat and Power', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40230', 'Commissions', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40235', 'Miscellaneous Factory Costs', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40240', 'Cost of Goods Sold-Salaries and Wages', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40245', 'Cost of Goods Sold-Contract Labor', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40250', 'Cost of Goods Sold-Freight', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40255', 'Cost of Goods Sold-Other', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40260', 'Inventory Adjustments', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40265', 'Purchase Returns and Allowances', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40270', 'Sales Discounts', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Cost of Goods Sold');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40300', 'General Purchase Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40400', 'Advertising Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40500', 'Amortization Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40600', 'Auto Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40700', 'Bad Debt Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40800', 'Bank Fees', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '40900', 'Cash Over and Short', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41000', 'Charitable Contributions Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41100', 'Commissions and Fees Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41200', 'Depreciation Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41300', 'Dues and Subscriptions Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41400', 'Employee Benefit Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41410', 'Employee Benefit Expenses-Health Insurance', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41420', 'Employee Benefit Expenses-Pension Plans', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41430', 'Employee Benefit Expenses-Profit Sharing Plan', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41440', 'Employee Benefit Expenses-Other', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Employee Benefit Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41500', 'Freight Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41600', 'Gifts Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41700', 'Income Tax Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41710', 'Income Tax Expenses-Federal', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Income Tax Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41720', 'Income Tax Expenses-State', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Income Tax Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41730', 'Income Tax Expenses-Local', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Income Tax Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41800', 'Insurance Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41810', 'Insurance Expenses-Product Liability', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Insurance Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41820', 'Insurance Expenses-Vehicle', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Insurance Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '41900', 'Interest Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42000', 'Laundry and Dry Cleaning Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42100', 'Legal and Professional Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42200', 'Licenses Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42300', 'Loss on NSF Checks', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42400', 'Maintenance Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42500', 'Meals and Entertainment Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42600', 'Office Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42700', 'Payroll Tax Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42800', 'Penalties and Fines Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '42900', 'Other Taxe Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43000', 'Postage Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43100', 'Rent or Lease Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43200', 'Repair and Maintenance Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43210', 'Repair and Maintenance Expenses-Office', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Repair and Maintenance Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43220', 'Repair and Maintenance Expenses-Vehicle', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Repair and Maintenance Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43300', 'Supplies Expenses-Office', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43400', 'Telephone Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43500', 'Training Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43600', 'Travel Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43700', 'Salary Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43800', 'Wages Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '43900', 'Utilities Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '44000', 'Other Expenses', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');
        INSERT INTO core.accounts(account_master_id,account_number,account_name, sys_type, parent_account_id) SELECT 2,  '44100', 'Gain/Loss on Sale of Assets', FALSE, (SELECT account_id FROM core.accounts WHERE account_name='Expenses');


        UPDATE core.accounts SET account_master_id=1 WHERE account_number='10000';
        UPDATE core.accounts SET account_master_id=1 WHERE account_number='20000';
        UPDATE core.accounts SET account_master_id=20400 WHERE account_number='40200';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10001';
        UPDATE core.accounts SET account_master_id=10102 WHERE account_number='10100';
        UPDATE core.accounts SET account_master_id=10102 WHERE account_number='10110';
        UPDATE core.accounts SET account_master_id=10102 WHERE account_number='10120';
        UPDATE core.accounts SET account_master_id=10102 WHERE account_number='10130';
        UPDATE core.accounts SET account_master_id=10102 WHERE account_number='10140';
        UPDATE core.accounts SET account_master_id=10101 WHERE account_number='10200';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10300';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10310';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10320';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10321';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10322';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10323';
        UPDATE core.accounts SET account_master_id=10110 WHERE account_number='10400';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10500';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10501';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10600';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10700';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10720';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10730';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10740';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10750';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10800';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='10900';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='11000';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='11100';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='11200';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='11300';
        UPDATE core.accounts SET account_master_id=10100 WHERE account_number='11400';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20001';
        UPDATE core.accounts SET account_master_id=15010 WHERE account_number='20100';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20110';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20200';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20300';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20400';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20500';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20600';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20700';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20701';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20710';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20720';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20730';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20740';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20750';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20760';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20770';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20780';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20800';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20810';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20820';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20830';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20840';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='20900';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21000';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21010';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21020';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21100';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21200';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21210';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21220';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21230';
        UPDATE core.accounts SET account_master_id=15000 WHERE account_number='21240';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40205';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40210';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40215';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40220';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40225';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40230';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40235';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40240';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40245';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40250';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40255';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40260';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40265';
        UPDATE core.accounts SET account_master_id=20500 WHERE account_number='40270';
        UPDATE core.accounts SET account_master_id=20700 WHERE account_number='40800';
        UPDATE core.accounts SET account_master_id=20700 WHERE account_number='41100';
        UPDATE core.accounts SET account_master_id=20701 WHERE account_number='41900';
        UPDATE core.accounts SET account_master_id=20700 WHERE account_number='42800';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12001';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12100';
        UPDATE core.accounts SET account_master_id=10201 WHERE account_number='12200';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12300';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12400';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12500';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12600';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12700';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12800';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='12900';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13000';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13100';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13200';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13300';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13400';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13500';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13600';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13700';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13710';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13800';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13810';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13820';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13830';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13840';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13850';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13860';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13870';
        UPDATE core.accounts SET account_master_id=10200 WHERE account_number='13880';
        UPDATE core.accounts SET account_master_id=20800 WHERE account_number='41700';
        UPDATE core.accounts SET account_master_id=20800 WHERE account_number='41710';
        UPDATE core.accounts SET account_master_id=20800 WHERE account_number='41720';
        UPDATE core.accounts SET account_master_id=20800 WHERE account_number='41730';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24001';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24100';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24200';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24300';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24400';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24500';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24600';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24700';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24800';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='24900';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='25000';
        UPDATE core.accounts SET account_master_id=15100 WHERE account_number='25010';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='40300';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='40400';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='40500';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='40600';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='40700';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='40900';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41000';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41200';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41300';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41400';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41410';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41420';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41430';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41440';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41500';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41600';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41800';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41810';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='41820';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42000';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42100';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42200';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42300';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42400';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42500';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42600';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42700';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='42900';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43000';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43100';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43200';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43210';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43220';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43300';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43400';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43500';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43600';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43700';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43800';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='43900';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='44000';
        UPDATE core.accounts SET account_master_id=20600 WHERE account_number='44100';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14001';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14100';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14200';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14300';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14400';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14500';
        UPDATE core.accounts SET account_master_id=10300 WHERE account_number='14600';
        UPDATE core.accounts SET account_master_id=2 WHERE account_number='30000';
        UPDATE core.accounts SET account_master_id=2 WHERE account_number='40000';
        UPDATE core.accounts SET account_master_id=2 WHERE account_number='40100';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30100';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30200';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30300';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30400';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30500';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30600';
        UPDATE core.accounts SET account_master_id=20100 WHERE account_number='30700';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28001';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28100';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28110';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28120';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28130';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28200';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28210';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28220';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28230';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28240';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28250';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28260';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28270';
        UPDATE core.accounts SET account_master_id=15300 WHERE account_number='28300';
        UPDATE core.accounts SET account_master_id=15300 WHERE account_number='28310';
        UPDATE core.accounts SET account_master_id=15300 WHERE account_number='28320';
        UPDATE core.accounts SET account_master_id=15400 WHERE account_number='28330';
        UPDATE core.accounts SET account_master_id=15400 WHERE account_number='28340';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28400';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28500';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28600';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28700';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28800';
        UPDATE core.accounts SET account_master_id=15200 WHERE account_number='28900';

        UPDATE core.accounts
        SET currency_code='NPR';


        ALTER TABLE core.accounts
        ALTER column currency_code SET NOT NULL;
    END IF;
END
$$
LANGUAGE plpgsql;
