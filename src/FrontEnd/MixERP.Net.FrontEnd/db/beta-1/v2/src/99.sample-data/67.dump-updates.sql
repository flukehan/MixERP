ALTER TABLE transactions.transaction_master DISABLE TRIGGER verification_update_trigger;

UPDATE transactions.transaction_master
SET book_date = value_date;

ALTER TABLE transactions.transaction_master ENABLE TRIGGER verification_update_trigger;
