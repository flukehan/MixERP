
ALTER TABLE transactions.stock_details
DROP CONSTRAINT IF EXISTS stock_details_unit_chk;

ALTER TABLE transactions.stock_details
ADD CONSTRAINT stock_details_unit_chk
CHECK(core.is_valid_unit(item_id, unit_id));


ALTER TABLE transactions.non_gl_stock_details
DROP CONSTRAINT IF EXISTS non_gl_stock_details_unit_chk;

ALTER TABLE transactions.non_gl_stock_details
ADD CONSTRAINT non_gl_stock_details_unit_chk
CHECK(core.is_valid_unit(item_id, unit_id));

ALTER TABLE transactions.transaction_master
DROP CONSTRAINT IF EXISTS transaction_master_sys_user_id_chk ;

ALTER TABLE transactions.transaction_master
ADD CONSTRAINT transaction_master_sys_user_id_chk 
CHECK(sys_user_id IS NULL OR office.is_sys_user(sys_user_id)=true);
