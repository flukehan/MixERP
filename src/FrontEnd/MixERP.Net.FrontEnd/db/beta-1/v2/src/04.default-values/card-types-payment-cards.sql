SELECT * FROM core.create_card_type(1, 'CRC', 'Credit Card'          );
SELECT * FROM core.create_card_type(2, 'DRC', 'Debit Card'           );
SELECT * FROM core.create_card_type(3, 'CHC', 'Charge Card'          );
SELECT * FROM core.create_card_type(4, 'ATM', 'ATM Card'             );
SELECT * FROM core.create_card_type(5, 'SVC', 'Store-value Card'     );
SELECT * FROM core.create_card_type(6, 'FLC', 'Fleet Card'           );
SELECT * FROM core.create_card_type(7, 'GFC', 'Gift Card'            );
SELECT * FROM core.create_card_type(8, 'SCR', 'Scrip'                );
SELECT * FROM core.create_card_type(9, 'ELP', 'Electronic Purse'     );


SELECT * FROM core.create_payment_card('CR-VSA', 'Visa',                1);
SELECT * FROM core.create_payment_card('CR-AME', 'American Express',    1);
SELECT * FROM core.create_payment_card('CR-MAS', 'MasterCard',          1);
SELECT * FROM core.create_payment_card('DR-MAE', 'Maestro',             2);
SELECT * FROM core.create_payment_card('DR-MAS', 'MasterCard Debit',    2);
SELECT * FROM core.create_payment_card('DR-VSE', 'Visa Electron',       2);
SELECT * FROM core.create_payment_card('DR-VSD', 'Visa Debit',          2);
SELECT * FROM core.create_payment_card('DR-DEL', 'Delta',               2);
