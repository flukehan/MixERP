CREATE VIEW core.late_fee_scrud_view
AS
SELECT 
  late_fee.late_fee_id, 
  late_fee.late_fee_code, 
  late_fee.late_fee_name, 
  late_fee.is_flat_amount, 
  late_fee.rate
FROM 
  core.late_fee;
