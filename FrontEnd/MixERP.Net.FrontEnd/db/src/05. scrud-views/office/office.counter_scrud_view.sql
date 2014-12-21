CREATE VIEW office.counter_scrud_view 
AS 
SELECT counters.counter_id,
	stores.store_code,
	stores.store_name,
	cash_repositories.cash_repository_code,
	cash_repositories.cash_repository_name,
	counters.counter_code,
	counters.counter_name
   FROM office.counters
   INNER JOIN office.stores ON counters.store_id = stores.store_id
   INNER JOIN office.cash_repositories ON counters.cash_repository_id = cash_repositories.cash_repository_id;
