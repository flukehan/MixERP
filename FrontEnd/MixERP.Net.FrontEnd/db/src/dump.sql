--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

SET search_path = audit, pg_catalog;

--
-- Data for Name: logins; Type: TABLE DATA; Schema: audit; Owner: postgres
--

INSERT INTO logins (login_id, user_id, office_id, browser, ip_address, login_date_time, remote_user, culture) VALUES (1, 2, 2, 'Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0', '::1', '2014-09-05 15:22:04.51+00', '', 'en-US');
INSERT INTO logins (login_id, user_id, office_id, browser, ip_address, login_date_time, remote_user, culture) VALUES (2, 2, 2, 'Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0', '::1', '2014-09-05 15:37:19.661+00', '', 'en-US');
INSERT INTO logins (login_id, user_id, office_id, browser, ip_address, login_date_time, remote_user, culture) VALUES (3, 2, 2, 'Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0', '::1', '2014-09-20 12:43:26.808+00', '', 'en-US');
INSERT INTO logins (login_id, user_id, office_id, browser, ip_address, login_date_time, remote_user, culture) VALUES (4, 2, 2, 'Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0', '::1', '2014-09-28 15:43:36.907+00', '', 'en-US');
INSERT INTO logins (login_id, user_id, office_id, browser, ip_address, login_date_time, remote_user, culture) VALUES (5, 2, 2, 'Mozilla/5.0 (Windows NT 6.3; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0', '::1', '2014-12-04 11:24:11.771+00', '', 'en-US');


--
-- Name: logins_login_id_seq; Type: SEQUENCE SET; Schema: audit; Owner: postgres
--

SELECT pg_catalog.setval('logins_login_id_seq', 5, true);


SET search_path = core, pg_catalog;

--
-- Data for Name: fiscal_year; Type: TABLE DATA; Schema: core; Owner: postgres
--

INSERT INTO fiscal_year (fiscal_year_code, fiscal_year_name, starts_from, ends_on, audit_user_id, audit_ts) VALUES ('FY1415', 'FY 2014/2015', '2014-10-01', '2015-09-30', 2, '2014-10-01 09:18:17.722+00');


--
-- Data for Name: flags; Type: TABLE DATA; Schema: core; Owner: postgres
--

INSERT INTO flags (flag_id, user_id, flag_type_id, resource, resource_key, resource_id, flagged_on) VALUES (1, 2, 2, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', 1, '2014-12-04 11:35:51.294+00');
INSERT INTO flags (flag_id, user_id, flag_type_id, resource, resource_key, resource_id, flagged_on) VALUES (2, 2, 3, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', 2, '2014-12-04 11:37:07.665+00');


--
-- Name: flags_flag_id_seq; Type: SEQUENCE SET; Schema: core; Owner: postgres
--

SELECT pg_catalog.setval('flags_flag_id_seq', 2, true);


--
-- Data for Name: frequency_setups; Type: TABLE DATA; Schema: core; Owner: postgres
--

INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (1, 'FY1415', '2014-10-31', 2, 2, '2014-10-01 09:19:22.178+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (2, 'FY1415', '2014-11-30', 2, 2, '2014-10-01 09:19:34.598+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (3, 'FY1415', '2014-12-31', 3, 2, '2014-10-01 09:19:45.302+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (4, 'FY1415', '2015-01-31', 2, 2, '2014-10-01 09:19:59.272+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (5, 'FY1415', '2015-02-28', 2, 2, '2014-10-01 09:20:07.57+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (6, 'FY1415', '2015-03-31', 4, 2, '2014-10-01 09:20:25.697+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (7, 'FY1415', '2015-04-30', 2, 2, '2014-10-01 09:20:43.197+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (8, 'FY1415', '2015-05-31', 2, 2, '2014-10-01 09:20:54.711+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (9, 'FY1415', '2015-06-30', 3, 2, '2014-10-01 09:21:07.837+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (10, 'FY1415', '2015-07-31', 2, 2, '2014-10-01 09:21:25.103+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (11, 'FY1415', '2015-08-31', 2, 2, '2014-10-01 09:21:37.995+00');
INSERT INTO frequency_setups (frequency_setup_id, fiscal_year_code, value_date, frequency_id, audit_user_id, audit_ts) VALUES (12, 'FY1415', '2015-09-30', 5, 2, '2014-10-01 09:21:52.293+00');


--
-- Name: frequency_setups_frequency_setup_id_seq; Type: SEQUENCE SET; Schema: core; Owner: postgres
--

SELECT pg_catalog.setval('frequency_setups_frequency_setup_id_seq', 12, true);


SET search_path = transactions, pg_catalog;

--
-- Data for Name: transaction_master; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (12, 9, '9-2014-12-04-2-2-5-11-45-47', 'Sales.Receipt', '2014-12-04', '2014-12-04 11:45:47.669+00', 5, 2, NULL, 2, 7, '', 'Cash received from Mr. Green.', '2014-12-04 11:45:47.716+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:45:47.669+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (1, 1, '1-2014-09-05-2-2-1-15-23-24', 'Journal', '2014-01-05', '2014-09-05 15:23:24.577+00', 1, 2, NULL, 2, 1, '', NULL, '2014-12-04 11:35:41.346+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (2, 2, '2-2014-09-05-2-2-2-15-37-22', 'Journal', '2014-02-05', '2014-09-05 15:37:22.802+00', 2, 2, NULL, 2, 1, '', NULL, '2014-12-04 11:36:56.608+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (4, 1, '1-2014-12-04-2-2-5-11-26-09', 'Purchase.Direct', '2014-12-04', '2014-12-04 11:26:09.931+00', 5, 2, NULL, 2, 1, '', 'Being various items purchased from Mr. Moore for Store 1.', '2014-12-04 11:39:24.629+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (5, 2, '2-2014-12-04-2-2-5-11-28-55', 'Sales.Direct', '2014-12-04', '2014-12-04 11:28:55.685+00', 5, 2, NULL, 2, 1, '', 'Being apple products sold to Smith.', '2014-12-04 11:40:32.503+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (6, 3, '3-2014-12-04-2-2-5-11-30-05', 'Sales.Direct', '2014-12-04', '2014-12-04 11:30:05.163+00', 5, 2, NULL, 2, 1, '', 'Being IPhone 6 Plus purchased by Mr. Jacob.', '2014-12-04 11:41:49.558+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (7, 4, '4-2014-12-04-2-2-5-11-31-11', 'Sales.Direct', '2014-12-04', '2014-12-04 11:31:11.132+00', 5, 2, NULL, 2, 1, '', 'Macbook Pro Late 2013 model, sold to Mr. James.', '2014-12-04 11:42:21.342+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (8, 5, '5-2014-12-04-2-2-5-11-32-11', 'Sales.Direct', '2014-12-04', '2014-12-04 11:32:11.287+00', 5, 2, NULL, 2, 1, '', 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', '2014-12-04 11:43:04.222+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (9, 6, '6-2014-12-04-2-2-5-11-33-27', 'Sales.Direct', '2014-12-04', '2014-12-04 11:33:27.3+00', 5, 2, NULL, 2, 1, '', 'MixNP Classifieds sold to Mr. Martinez. #software', '2014-12-04 11:43:26.764+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:33:27.3+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (10, 7, '7-2014-12-04-2-2-5-11-43-54', 'Sales.Delivery', '2014-12-04', '2014-12-04 11:43:54.291+00', 5, 2, NULL, 2, 6, '', 'Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', '2014-12-04 11:43:54.541+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (11, 8, '8-2014-12-04-2-2-5-11-44-27', 'Sales.Delivery', '2014-12-04', '2014-12-04 11:44:27.437+00', 5, 2, NULL, 2, 7, '', 'Delivery. PO received from Mr. Green. Quotation of various Apple products sent to Ms. Green. #review.
(SQ# 2)
(SO# 8)', '2014-12-04 11:44:27.812+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-12-04 11:44:27.437+00');


--
-- Data for Name: customer_receipts; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO customer_receipts (receipt_id, transaction_master_id, party_id, currency_code, amount, er_debit, er_credit, cash_repository_id, posted_date, bank_account_id, bank_instrument_code, bank_tran_code) VALUES (1, 12, 35, 'NPR', 1000000.0000, 1, 1, 1, NULL, NULL, '', '');


--
-- Name: customer_receipts_receipt_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('customer_receipts_receipt_id_seq', 1, true);


--
-- Data for Name: day_operation; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: day_operation_day_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('day_operation_day_id_seq', 1, false);


--
-- Data for Name: day_operation_routines; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: day_operation_routines_day_operation_routine_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('day_operation_routines_day_operation_routine_id_seq', 1, false);


--
-- Data for Name: non_gl_stock_master; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (1, '2014-12-04', 'Sales.Quotation', 5, 2, '2014-12-04 11:35:41.19+00', 5, 2, 2, '', 'Being quotation sent to Mr. Brown. #followup #important', false, 1, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:35:41.19+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (2, '2014-12-04', 'Sales.Quotation', 35, 1, '2014-12-04 11:36:56.467+00', 5, 2, 2, '', 'Quotation of various Apple products sent to Ms. Green. #review.', true, 5, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (3, '2014-12-04', 'Sales.Quotation', 18, 1, '2014-12-04 11:38:16.597+00', 5, 2, 2, '', 'Quotation sent to Ms. Garcia. #follow-up', false, 1, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (4, '2014-12-04', 'Sales.Quotation', 16, 1, '2014-12-04 11:39:24.535+00', 5, 2, 2, '', 'Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.', false, 4, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:39:24.535+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (5, '2014-12-04', 'Sales.Quotation', 47, 1, '2014-12-04 11:40:32.362+00', 5, 2, 2, '', 'Quotation sent to Mr. Parker. #schintowski', false, 2, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (6, '2014-12-04', 'Sales.Quotation', 23, 1, '2014-12-04 11:41:49.449+00', 5, 2, 2, '', 'Quotation to Ms. Lewis via Phillipe Jones.', false, 3, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:41:49.449+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (7, '2014-12-04', 'Sales.Order', 5, 2, '2014-12-04 11:42:21.248+00', 5, 2, 2, '', 'Being quotation sent to Mr. Brown. #followup #important
(SQ# 1)', false, 1, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:42:21.248+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (8, '2014-12-04', 'Sales.Order', 35, 1, '2014-12-04 11:43:04.003+00', 5, 2, 2, '', 'PO received from Mr. Green. Quotation of various Apple products sent to Ms. Green. #review.
(SQ# 2)', true, 5, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id, audit_user_id, audit_ts) VALUES (9, '2014-12-04', 'Sales.Order', 16, 1, '2014-12-04 11:43:26.717+00', 5, 2, 2, '', 'PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)', false, 4, 1, NULL, 0.0000, 1, NULL, '2014-12-04 11:43:26.717+00');


--
-- Data for Name: non_gl_stock_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (1, 1, '2014-12-04', 1, 50, 1, 50.00, 1, 225000.0000, 0.0000, 0.0000, 1, 998437.5000, NULL, '2014-12-04 11:35:41.19+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (2, 1, '2014-12-04', 2, 100, 1, 100.00, 1, 145000.0000, 0.0000, 0.0000, 1, 1286875.0000, NULL, '2014-12-04 11:35:41.19+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (3, 1, '2014-12-04', 3, 25, 1, 25.00, 1, 135000.0000, 0.0000, 0.0000, 1, 299531.2500, NULL, '2014-12-04 11:35:41.19+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (4, 1, '2014-12-04', 4, 100, 1, 100.00, 1, 67000.0000, 0.0000, 0.0000, 1, 594625.0000, NULL, '2014-12-04 11:35:41.19+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (5, 2, '2014-12-04', 1, 1, 1, 1.00, 1, 225000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (6, 2, '2014-12-04', 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (7, 2, '2014-12-04', 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (8, 2, '2014-12-04', 4, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (9, 2, '2014-12-04', 5, 1, 1, 1.00, 1, 80000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (10, 2, '2014-12-04', 6, 1, 1, 1.00, 1, 50000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (11, 2, '2014-12-04', 7, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (12, 2, '2014-12-04', 8, 1, 1, 1.00, 1, 105000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (13, 2, '2014-12-04', 9, 1, 1, 1.00, 1, 115000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:36:56.467+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (14, 3, '2014-12-04', 1, 1, 1, 1.00, 1, 225000.0000, 0.0000, 0.0000, 1, 19968.7500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (15, 3, '2014-12-04', 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 0.0000, 1, 13756.2500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (16, 3, '2014-12-04', 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 0.0000, 1, 11981.2500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (17, 3, '2014-12-04', 4, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, 1, 6212.5000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (18, 3, '2014-12-04', 5, 1, 1, 1.00, 1, 80000.0000, 0.0000, 0.0000, 1, 7100.0000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (19, 3, '2014-12-04', 6, 1, 1, 1.00, 1, 50000.0000, 0.0000, 0.0000, 1, 4437.5000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (20, 3, '2014-12-04', 7, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, 1, 6212.5000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (21, 3, '2014-12-04', 8, 1, 1, 1.00, 1, 105000.0000, 0.0000, 0.0000, 1, 9318.7500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (22, 3, '2014-12-04', 9, 1, 1, 1.00, 1, 115000.0000, 0.0000, 0.0000, 1, 10206.2500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (23, 3, '2014-12-04', 10, 1, 1, 1.00, 1, 125000.0000, 0.0000, 0.0000, 1, 11093.7500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (24, 3, '2014-12-04', 11, 1, 1, 1.00, 1, 65000.0000, 0.0000, 0.0000, 1, 5768.7500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (25, 3, '2014-12-04', 12, 1, 1, 1.00, 1, 350.0000, 0.0000, 0.0000, 1, 31.0600, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (26, 3, '2014-12-04', 13, 1, 1, 1.00, 1, 35000.0000, 0.0000, 0.0000, 1, 3106.2500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (27, 3, '2014-12-04', 14, 1, 1, 1.00, 1, 150000.0000, 0.0000, 0.0000, 1, 13312.5000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (28, 3, '2014-12-04', 15, 1, 1, 1.00, 1, 40000.0000, 0.0000, 0.0000, 1, 3550.0000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (29, 3, '2014-12-04', 16, 1, 1, 1.00, 1, 40000.0000, 0.0000, 0.0000, 1, 3550.0000, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (30, 3, '2014-12-04', 17, 3, 1, 3.00, 1, 45000.0000, 0.0000, 0.0000, 1, 11981.2500, NULL, '2014-12-04 11:38:16.597+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (31, 4, '2014-12-04', 10, 1, 1, 1.00, 1, 125000.0000, 0.0000, 0.0000, 1, 11093.7500, NULL, '2014-12-04 11:39:24.535+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (32, 5, '2014-12-04', 1, 1, 1, 1.00, 1, 225000.0000, 0.0000, 0.0000, 1, 19968.7500, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (33, 5, '2014-12-04', 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 0.0000, 1, 13756.2500, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (34, 5, '2014-12-04', 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 0.0000, 1, 11981.2500, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (35, 5, '2014-12-04', 4, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, 1, 6212.5000, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (36, 5, '2014-12-04', 5, 1, 1, 1.00, 1, 80000.0000, 0.0000, 0.0000, 1, 7100.0000, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (37, 5, '2014-12-04', 6, 1, 1, 1.00, 1, 50000.0000, 0.0000, 0.0000, 1, 4437.5000, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (38, 5, '2014-12-04', 7, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, 1, 6212.5000, NULL, '2014-12-04 11:40:32.362+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (39, 6, '2014-12-04', 1, 2, 1, 2.00, 1, 225000.0000, 0.0000, 0.0000, 1, 39937.5000, NULL, '2014-12-04 11:41:49.449+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (40, 6, '2014-12-04', 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 0.0000, 1, 13756.2500, NULL, '2014-12-04 11:41:49.449+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (41, 6, '2014-12-04', 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 0.0000, 1, 11981.2500, NULL, '2014-12-04 11:41:49.449+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (42, 7, '2014-12-04', 1, 50, 1, 50.00, 1, 225000.0000, 0.0000, 0.0000, 1, 998437.5000, NULL, '2014-12-04 11:42:21.248+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (43, 7, '2014-12-04', 2, 100, 1, 100.00, 1, 145000.0000, 0.0000, 0.0000, 1, 1286875.0000, NULL, '2014-12-04 11:42:21.248+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (44, 7, '2014-12-04', 3, 25, 1, 25.00, 1, 135000.0000, 0.0000, 0.0000, 1, 299531.2500, NULL, '2014-12-04 11:42:21.248+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (45, 7, '2014-12-04', 4, 100, 1, 100.00, 1, 67000.0000, 0.0000, 0.0000, 1, 594625.0000, NULL, '2014-12-04 11:42:21.248+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (46, 8, '2014-12-04', 1, 1, 1, 1.00, 1, 225000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (47, 8, '2014-12-04', 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (48, 8, '2014-12-04', 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (49, 8, '2014-12-04', 4, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (50, 8, '2014-12-04', 5, 1, 1, 1.00, 1, 80000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (51, 8, '2014-12-04', 6, 1, 1, 1.00, 1, 50000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (52, 8, '2014-12-04', 7, 1, 1, 1.00, 1, 70000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (53, 8, '2014-12-04', 8, 1, 1, 1.00, 1, 105000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (54, 8, '2014-12-04', 9, 1, 1, 1.00, 1, 115000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:43:04.003+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (55, 9, '2014-12-04', 10, 1, 1, 1.00, 1, 125000.0000, 0.0000, 0.0000, 1, 11093.7500, NULL, '2014-12-04 11:43:26.717+00');


--
-- Name: non_gl_stock_details_non_gl_stock_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_details_non_gl_stock_detail_id_seq', 55, true);


--
-- Name: non_gl_stock_master_non_gl_stock_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_master_non_gl_stock_master_id_seq', 9, true);


--
-- Data for Name: non_gl_stock_master_relations; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: non_gl_stock_master_relations_non_gl_stock_master_relation__seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_master_relations_non_gl_stock_master_relation__seq', 1, false);


--
-- Data for Name: non_gl_stock_tax_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (1, 1, 30, NULL, 11250000.0000, 4, 450000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (1, 2, NULL, 1, 11250000.0000, 4.875, 548437.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (2, 1, 30, NULL, 14500000.0000, 4, 580000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (2, 2, NULL, 1, 14500000.0000, 4.875, 706875.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (3, 1, 30, NULL, 3375000.0000, 4, 135000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (3, 2, NULL, 1, 3375000.0000, 4.875, 164531.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (4, 1, 30, NULL, 6700000.0000, 4, 268000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (4, 2, NULL, 1, 6700000.0000, 4.875, 326625.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (14, 1, 30, NULL, 225000.0000, 4, 9000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (14, 2, NULL, 1, 225000.0000, 4.875, 10968.7500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (15, 1, 30, NULL, 155000.0000, 4, 6200.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (15, 2, NULL, 1, 155000.0000, 4.875, 7556.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (16, 1, 30, NULL, 135000.0000, 4, 5400.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (16, 2, NULL, 1, 135000.0000, 4.875, 6581.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (17, 1, 30, NULL, 70000.0000, 4, 2800.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (17, 2, NULL, 1, 70000.0000, 4.875, 3412.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (18, 1, 30, NULL, 80000.0000, 4, 3200.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (18, 2, NULL, 1, 80000.0000, 4.875, 3900.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (19, 1, 30, NULL, 50000.0000, 4, 2000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (19, 2, NULL, 1, 50000.0000, 4.875, 2437.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (20, 1, 30, NULL, 70000.0000, 4, 2800.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (20, 2, NULL, 1, 70000.0000, 4.875, 3412.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (21, 1, 30, NULL, 105000.0000, 4, 4200.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (21, 2, NULL, 1, 105000.0000, 4.875, 5118.7500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (22, 1, 30, NULL, 115000.0000, 4, 4600.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (22, 2, NULL, 1, 115000.0000, 4.875, 5606.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (23, 1, 30, NULL, 125000.0000, 4, 5000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (23, 2, NULL, 1, 125000.0000, 4.875, 6093.7500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (24, 1, 30, NULL, 65000.0000, 4, 2600.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (24, 2, NULL, 1, 65000.0000, 4.875, 3168.7500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (25, 1, 30, NULL, 350.0000, 4, 14.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (25, 2, NULL, 1, 350.0000, 4.875, 17.0600);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (26, 1, 30, NULL, 35000.0000, 4, 1400.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (26, 2, NULL, 1, 35000.0000, 4.875, 1706.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (27, 1, 30, NULL, 150000.0000, 4, 6000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (27, 2, NULL, 1, 150000.0000, 4.875, 7312.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (28, 1, 30, NULL, 40000.0000, 4, 1600.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (28, 2, NULL, 1, 40000.0000, 4.875, 1950.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (29, 1, 30, NULL, 40000.0000, 4, 1600.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (29, 2, NULL, 1, 40000.0000, 4.875, 1950.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (30, 1, 30, NULL, 135000.0000, 4, 5400.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (30, 2, NULL, 1, 135000.0000, 4.875, 6581.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (31, 1, 30, NULL, 125000.0000, 4, 5000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (31, 2, NULL, 1, 125000.0000, 4.875, 6093.7500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (32, 1, 30, NULL, 225000.0000, 4, 9000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (32, 2, NULL, 1, 225000.0000, 4.875, 10968.7500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (33, 1, 30, NULL, 155000.0000, 4, 6200.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (33, 2, NULL, 1, 155000.0000, 4.875, 7556.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (34, 1, 30, NULL, 135000.0000, 4, 5400.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (34, 2, NULL, 1, 135000.0000, 4.875, 6581.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (35, 1, 30, NULL, 70000.0000, 4, 2800.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (35, 2, NULL, 1, 70000.0000, 4.875, 3412.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (36, 1, 30, NULL, 80000.0000, 4, 3200.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (36, 2, NULL, 1, 80000.0000, 4.875, 3900.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (37, 1, 30, NULL, 50000.0000, 4, 2000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (37, 2, NULL, 1, 50000.0000, 4.875, 2437.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (38, 1, 30, NULL, 70000.0000, 4, 2800.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (38, 2, NULL, 1, 70000.0000, 4.875, 3412.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (39, 1, 30, NULL, 450000.0000, 4, 18000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (39, 2, NULL, 1, 450000.0000, 4.875, 21937.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (40, 1, 30, NULL, 155000.0000, 4, 6200.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (40, 2, NULL, 1, 155000.0000, 4.875, 7556.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (41, 1, 30, NULL, 135000.0000, 4, 5400.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (41, 2, NULL, 1, 135000.0000, 4.875, 6581.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (42, 1, 30, NULL, 11250000.0000, 4, 450000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (42, 2, NULL, 1, 11250000.0000, 4.875, 548437.5000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (43, 1, 30, NULL, 14500000.0000, 4, 580000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (43, 2, NULL, 1, 14500000.0000, 4.875, 706875.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (44, 1, 30, NULL, 3375000.0000, 4, 135000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (44, 2, NULL, 1, 3375000.0000, 4.875, 164531.2500);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (45, 1, 30, NULL, 6700000.0000, 4, 268000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (45, 2, NULL, 1, 6700000.0000, 4.875, 326625.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (55, 1, 30, NULL, 125000.0000, 4, 5000.0000);
INSERT INTO non_gl_stock_tax_details (non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (55, 2, NULL, 1, 125000.0000, 4.875, 6093.7500);


--
-- Name: routines_routine_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('routines_routine_id_seq', 1, true);


--
-- Data for Name: stock_master; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (1, 4, '2014-12-04', 9, NULL, NULL, false, NULL, NULL, 0.0000, 1, false, 1, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (2, 5, '2014-12-04', 1, 1, 1, false, 1, NULL, 0.0000, 1, false, 1, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (3, 6, '2014-12-04', 1, 1, 1, false, 1, NULL, 200.0000, 1, false, 1, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (4, 7, '2014-12-04', 17, 1, 1, false, 1, NULL, 500.0000, 1, false, 1, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (5, 8, '2014-12-04', 8, 3, 1, false, 1, NULL, 200.0000, 1, true, 1, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (6, 9, '2014-12-04', 19, 1, 1, false, 1, NULL, 0.0000, 1, true, 1, NULL, '2014-12-04 11:33:27.3+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (7, 10, '2014-12-04', 16, 4, 1, true, 1, NULL, 0.0000, 1, false, NULL, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, value_date, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, non_taxable, cash_repository_id, audit_user_id, audit_ts) VALUES (8, 11, '2014-12-04', 35, 5, 1, true, 1, NULL, 0.0000, 1, true, NULL, NULL, '2014-12-04 11:44:27.437+00');


--
-- Data for Name: stock_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (1, '2014-12-04', 1, 'Dr', 1, 1, 100, 1, 100.00, 1, 180000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (2, '2014-12-04', 1, 'Dr', 1, 2, 100, 1, 100.00, 1, 130000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (3, '2014-12-04', 1, 'Dr', 1, 3, 100, 1, 100.00, 1, 110000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (4, '2014-12-04', 1, 'Dr', 1, 4, 200, 1, 200.00, 1, 53000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (5, '2014-12-04', 1, 'Dr', 1, 5, 200, 1, 200.00, 1, 63000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (6, '2014-12-04', 1, 'Dr', 1, 6, 100, 1, 100.00, 1, 33000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (7, '2014-12-04', 1, 'Dr', 1, 7, 50, 1, 50.00, 1, 53000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (8, '2014-12-04', 1, 'Dr', 1, 8, 500, 1, 500.00, 1, 93000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (9, '2014-12-04', 1, 'Dr', 1, 9, 500, 1, 500.00, 1, 103000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (10, '2014-12-04', 1, 'Dr', 1, 10, 20, 1, 20.00, 1, 80000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (11, '2014-12-04', 1, 'Dr', 1, 11, 20, 1, 20.00, 1, 40000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (12, '2014-12-04', 1, 'Dr', 1, 12, 1000, 8, 1200000, 1, 240000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (13, '2014-12-04', 1, 'Dr', 1, 13, 100, 1, 100.00, 1, 30000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (14, '2014-12-04', 1, 'Dr', 1, 17, 20, 1, 20.00, 1, 30000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (15, '2014-12-04', 2, 'Cr', 1, 1, 1, 1, 1.00, 1, 225000.0000, 180000.0000, 0.0000, 0.0000, 1, 19968.7500, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (16, '2014-12-04', 2, 'Cr', 1, 2, 1, 1, 1.00, 1, 155000.0000, 130000.0000, 0.0000, 0.0000, 1, 13756.2500, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (17, '2014-12-04', 2, 'Cr', 1, 3, 1, 1, 1.00, 1, 135000.0000, 110000.0000, 0.0000, 0.0000, 1, 11981.2500, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (18, '2014-12-04', 2, 'Cr', 1, 4, 1, 1, 1.00, 1, 70000.0000, 53000.0000, 0.0000, 0.0000, 1, 6212.5000, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (19, '2014-12-04', 3, 'Cr', 1, 9, 1, 1, 1.00, 1, 115000.0000, 103000.0000, 0.0000, 200.0000, 1, 10224.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (20, '2014-12-04', 4, 'Cr', 1, 1, 1, 1, 1.00, 1, 225000.0000, 180000.0000, 100.0000, 500.0000, 1, 20004.2500, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (21, '2014-12-04', 5, 'Cr', 1, 1, 1, 1, 1.00, 1, 225000.0000, 180000.0000, 100.0000, 200.0000, NULL, 0.0000, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (22, '2014-12-04', 6, 'Cr', 1, 14, 1, 1, 1.00, 1, 150000.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:33:27.3+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (23, '2014-12-04', 7, 'Cr', 1, 10, 1, 1, 1.00, 1, 125000.0000, 80000.0000, 0.0000, 0.0000, 1, 11093.7500, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (24, '2014-12-04', 8, 'Cr', 1, 1, 1, 1, 1.00, 1, 225000.0000, 180000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (25, '2014-12-04', 8, 'Cr', 1, 2, 1, 1, 1.00, 1, 155000.0000, 130000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (26, '2014-12-04', 8, 'Cr', 1, 3, 1, 1, 1.00, 1, 135000.0000, 110000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (27, '2014-12-04', 8, 'Cr', 1, 4, 1, 1, 1.00, 1, 70000.0000, 53000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (28, '2014-12-04', 8, 'Cr', 1, 5, 1, 1, 1.00, 1, 80000.0000, 63000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (29, '2014-12-04', 8, 'Cr', 1, 6, 1, 1, 1.00, 1, 50000.0000, 33000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (30, '2014-12-04', 8, 'Cr', 1, 7, 1, 1, 1.00, 1, 70000.0000, 53000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (31, '2014-12-04', 8, 'Cr', 1, 8, 1, 1, 1.00, 1, 105000.0000, 93000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO stock_details (stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax, audit_user_id, audit_ts) VALUES (32, '2014-12-04', 8, 'Cr', 1, 9, 1, 1, 1.00, 1, 115000.0000, 103000.0000, 0.0000, 0.0000, NULL, 0.0000, NULL, '2014-12-04 11:44:27.437+00');


--
-- Name: stock_details_stock_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('stock_details_stock_detail_id_seq', 32, true);


--
-- Data for Name: stock_master_non_gl_relations; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: stock_master_non_gl_relations_stock_master_non_gl_relation__seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('stock_master_non_gl_relations_stock_master_non_gl_relation__seq', 1, false);


--
-- Name: stock_master_stock_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('stock_master_stock_master_id_seq', 8, true);


--
-- Data for Name: stock_return; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: stock_return_sales_return_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('stock_return_sales_return_id_seq', 1, false);


--
-- Data for Name: stock_tax_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (15, 1, 30, NULL, 225000.0000, 4, 9000.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (15, 2, NULL, 1, 225000.0000, 4.875, 10968.7500);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (16, 1, 30, NULL, 155000.0000, 4, 6200.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (16, 2, NULL, 1, 155000.0000, 4.875, 7556.2500);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (17, 1, 30, NULL, 135000.0000, 4, 5400.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (17, 2, NULL, 1, 135000.0000, 4.875, 6581.2500);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (18, 1, 30, NULL, 70000.0000, 4, 2800.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (18, 2, NULL, 1, 70000.0000, 4.875, 3412.5000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (19, 1, 30, NULL, 115200.0000, 4, 4608.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (19, 2, NULL, 1, 115200.0000, 4.875, 5616.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (20, 1, 30, NULL, 225400.0000, 4, 9016.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (20, 2, NULL, 1, 225400.0000, 4.875, 10988.2500);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (23, 1, 30, NULL, 125000.0000, 4, 5000.0000);
INSERT INTO stock_tax_details (stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax) VALUES (23, 2, NULL, 1, 125000.0000, 4.875, 6093.7500);


--
-- Data for Name: transaction_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (3, 2, '2014-02-05', 'Dr', 8, 'Cash transfer', 2, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (4, 2, '2014-02-05', 'Cr', 8, 'Cash transfer', 1, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (1, 1, '2014-01-05', 'Cr', 114, 'Cash Invested by nirvan.', NULL, 'NPR', 500000000.0000, 'NPR', 1, 500000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (2, 1, '2014-01-05', 'Dr', 8, 'Cash Invested by nirvan.', 1, 'NPR', 500000000.0000, 'NPR', 1, 500000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (6, 4, '2014-12-04', 'Dr', 19, 'Being various items purchased from Mr. Moore for Store 1.', NULL, 'NPR', 415150000.0000, 'NPR', 1, 415150000.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (7, 4, '2014-12-04', 'Cr', 8, 'Being various items purchased from Mr. Moore for Store 1.', 1, 'NPR', 415150000.0000, 'NPR', 1, 415150000.0000, NULL, '2014-12-04 11:26:09.931+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (8, 5, '2014-12-04', 'Dr', 142, 'Being apple products sold to Smith.', NULL, 'NPR', 473000.0000, 'NPR', 1, 473000.0000, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (9, 5, '2014-12-04', 'Dr', 8, 'Being apple products sold to Smith.', 1, 'NPR', 636918.7500, 'NPR', 1, 636918.7500, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (10, 5, '2014-12-04', 'Cr', 133, 'Being apple products sold to Smith.', NULL, 'NPR', 585000.0000, 'NPR', 1, 585000.0000, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (11, 5, '2014-12-04', 'Cr', 19, 'Being apple products sold to Smith.', NULL, 'NPR', 473000.0000, 'NPR', 1, 473000.0000, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (12, 5, '2014-12-04', 'Cr', 77, 'P: 585000.0000 x R: 4 % = 23400.0000 (BK-NYC-STX)/Being apple products sold to Smith.', NULL, 'NPR', 23400.0000, 'NPR', 1, 23400.0000, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (13, 5, '2014-12-04', 'Cr', 77, 'P: 585000.0000 x R: 4.875 % = 28518.7500 (BK-36047-STX)/Being apple products sold to Smith.', NULL, 'NPR', 28518.7500, 'NPR', 1, 28518.7500, NULL, '2014-12-04 11:28:55.685+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (14, 6, '2014-12-04', 'Dr', 142, 'Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 103000.0000, 'NPR', 1, 103000.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (15, 6, '2014-12-04', 'Dr', 8, 'Being IPhone 6 Plus purchased by Mr. Jacob.', 1, 'NPR', 125424.0000, 'NPR', 1, 125424.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (16, 6, '2014-12-04', 'Cr', 133, 'Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 115000.0000, 'NPR', 1, 115000.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (17, 6, '2014-12-04', 'Cr', 77, 'P: 115200.0000 x R: 4.875 % = 5616.0000 (BK-36047-STX)/Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 5616.0000, 'NPR', 1, 5616.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (18, 6, '2014-12-04', 'Cr', 69, 'Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 200.0000, 'NPR', 1, 200.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (19, 6, '2014-12-04', 'Cr', 77, 'P: 115200.0000 x R: 4 % = 4608.0000 (BK-NYC-STX)/Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 4608.0000, 'NPR', 1, 4608.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (20, 6, '2014-12-04', 'Cr', 19, 'Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 103000.0000, 'NPR', 1, 103000.0000, NULL, '2014-12-04 11:30:05.163+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (21, 7, '2014-12-04', 'Dr', 142, 'Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 180000.0000, 'NPR', 1, 180000.0000, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (23, 7, '2014-12-04', 'Dr', 8, 'Macbook Pro Late 2013 model, sold to Mr. James.', 1, 'NPR', 245404.2500, 'NPR', 1, 245404.2500, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (24, 7, '2014-12-04', 'Cr', 133, 'Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 225000.0000, 'NPR', 1, 225000.0000, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (25, 7, '2014-12-04', 'Cr', 69, 'Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 500.0000, 'NPR', 1, 500.0000, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (26, 7, '2014-12-04', 'Cr', 77, 'P: 225400.0000 x R: 4.875 % = 10988.2500 (BK-36047-STX)/Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 10988.2500, 'NPR', 1, 10988.2500, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (27, 7, '2014-12-04', 'Cr', 19, 'Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 180000.0000, 'NPR', 1, 180000.0000, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (28, 7, '2014-12-04', 'Cr', 77, 'P: 225400.0000 x R: 4 % = 9016.0000 (BK-NYC-STX)/Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 9016.0000, 'NPR', 1, 9016.0000, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (29, 8, '2014-12-04', 'Dr', 142, 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', NULL, 'NPR', 180000.0000, 'NPR', 1, 180000.0000, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (31, 8, '2014-12-04', 'Dr', 8, 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', 1, 'NPR', 225100.0000, 'NPR', 1, 225100.0000, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (32, 8, '2014-12-04', 'Cr', 133, 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', NULL, 'NPR', 225000.0000, 'NPR', 1, 225000.0000, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (33, 8, '2014-12-04', 'Cr', 19, 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', NULL, 'NPR', 180000.0000, 'NPR', 1, 180000.0000, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (34, 8, '2014-12-04', 'Cr', 69, 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', NULL, 'NPR', 200.0000, 'NPR', 1, 200.0000, NULL, '2014-12-04 11:32:11.287+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (35, 9, '2014-12-04', 'Dr', 8, 'MixNP Classifieds sold to Mr. Martinez. #software', 1, 'NPR', 150000.0000, 'NPR', 1, 150000.0000, NULL, '2014-12-04 11:33:27.3+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (36, 9, '2014-12-04', 'Cr', 133, 'MixNP Classifieds sold to Mr. Martinez. #software', NULL, 'NPR', 150000.0000, 'NPR', 1, 150000.0000, NULL, '2014-12-04 11:33:27.3+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (37, 10, '2014-12-04', 'Dr', 142, 'Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', NULL, 'NPR', 80000.0000, 'NPR', 1, 80000.0000, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (38, 10, '2014-12-04', 'Dr', 222, 'Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', NULL, 'NPR', 136093.7500, 'NPR', 1, 136093.7500, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (39, 10, '2014-12-04', 'Cr', 133, 'Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', NULL, 'NPR', 125000.0000, 'NPR', 1, 125000.0000, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (40, 10, '2014-12-04', 'Cr', 19, 'Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', NULL, 'NPR', 80000.0000, 'NPR', 1, 80000.0000, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (41, 10, '2014-12-04', 'Cr', 77, 'P: 125000.0000 x R: 4 % = 5000.0000 (BK-NYC-STX)/Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', NULL, 'NPR', 5000.0000, 'NPR', 1, 5000.0000, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (42, 10, '2014-12-04', 'Cr', 77, 'P: 125000.0000 x R: 4.875 % = 6093.7500 (BK-36047-STX)/Delivery. PO. Quotation of IBM Thinkpad II Laptop sent to Mr. Martin.
(SQ# 4)
(SO# 9)', NULL, 'NPR', 6093.7500, 'NPR', 1, 6093.7500, NULL, '2014-12-04 11:43:54.291+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (43, 11, '2014-12-04', 'Dr', 142, 'Delivery. PO received from Mr. Green. Quotation of various Apple products sent to Ms. Green. #review.
(SQ# 2)
(SO# 8)', NULL, 'NPR', 818000.0000, 'NPR', 1, 818000.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (44, 11, '2014-12-04', 'Dr', 241, 'Delivery. PO received from Mr. Green. Quotation of various Apple products sent to Ms. Green. #review.
(SQ# 2)
(SO# 8)', NULL, 'NPR', 1005000.0000, 'NPR', 1, 1005000.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (45, 11, '2014-12-04', 'Cr', 133, 'Delivery. PO received from Mr. Green. Quotation of various Apple products sent to Ms. Green. #review.
(SQ# 2)
(SO# 8)', NULL, 'NPR', 1005000.0000, 'NPR', 1, 1005000.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (46, 11, '2014-12-04', 'Cr', 19, 'Delivery. PO received from Mr. Green. Quotation of various Apple products sent to Ms. Green. #review.
(SQ# 2)
(SO# 8)', NULL, 'NPR', 818000.0000, 'NPR', 1, 818000.0000, NULL, '2014-12-04 11:44:27.437+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (47, 12, '2014-12-04', 'Dr', 8, 'Cash received from Mr. Green.', 1, 'NPR', 1000000.0000, 'NPR', 1, 1000000.0000, 2, '2014-12-04 11:45:47.669+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (48, 12, '2014-12-04', 'Cr', 241, 'Cash received from Mr. Green.', NULL, 'NPR', 1000000.0000, 'NPR', 1, 1000000.0000, 2, '2014-12-04 11:45:47.669+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (22, 7, '2014-12-04', 'Dr', 156, 'Macbook Pro Late 2013 model, sold to Mr. James.', NULL, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-12-04 11:31:11.132+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (30, 8, '2014-12-04', 'Dr', 156, 'Macbook Pro Late 2013 model, sold to Mr. Wilson.', NULL, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-12-04 11:32:11.287+00');


--
-- Name: transaction_details_transaction_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_details_transaction_detail_id_seq', 48, true);


--
-- Name: transaction_master_transaction_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_master_transaction_master_id_seq', 12, true);


--
-- PostgreSQL database dump complete
--

