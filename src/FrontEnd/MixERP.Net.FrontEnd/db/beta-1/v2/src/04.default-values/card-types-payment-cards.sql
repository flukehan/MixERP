INSERT INTO core.card_types(card_type_id, card_type_code, card_type_name)
SELECT 1, 'CRC', 'Credit Card'          UNION ALL
SELECT 2, 'DRC', 'Debit Card'           UNION ALL
SELECT 3, 'CHC', 'Charge Card'          UNION ALL
SELECT 4, 'ATM', 'ATM Card'             UNION ALL
SELECT 5, 'SVC', 'Store-value Card'     UNION ALL
SELECT 6, 'FLC', 'Fleet Card'           UNION ALL
SELECT 7, 'GFC', 'Gift Card'            UNION ALL
SELECT 8, 'SCR', 'Scrip'                UNION ALL
SELECT 9, 'ELP', 'Electronic Purse';


INSERT INTO core.payment_cards(payment_card_code, payment_card_name, card_type_id)
SELECT 'CR-VSA', 'Visa',                1 UNION ALL
SELECT 'CR-AME', 'American Express',    1 UNION ALL
SELECT 'CR-MAS', 'MasterCard',          1 UNION ALL
SELECT 'DR-MAE', 'Maestro',             2 UNION ALL
SELECT 'DR-MAS', 'MasterCard Debit',    2 UNION ALL
SELECT 'DR-VSE', 'Visa Electron',       2 UNION ALL
SELECT 'DR-VSD', 'Visa Debit',          2 UNION ALL
SELECT 'DR-DEL', 'Delta',               2;

