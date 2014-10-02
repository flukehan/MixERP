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


--
-- Name: logins_login_id_seq; Type: SEQUENCE SET; Schema: audit; Owner: postgres
--

SELECT pg_catalog.setval('logins_login_id_seq', 4, true);


SET search_path = core, pg_catalog;

--
-- Data for Name: fiscal_year; Type: TABLE DATA; Schema: core; Owner: postgres
--

INSERT INTO fiscal_year (fiscal_year_code, fiscal_year_name, starts_from, ends_on, audit_user_id, audit_ts) VALUES ('FY1415', 'FY 2014/2015', '2014-10-01', '2015-09-30', 2, '2014-10-01 09:18:17.722+00');


--
-- Data for Name: flags; Type: TABLE DATA; Schema: core; Owner: postgres
--

INSERT INTO flags (flag_id, user_id, flag_type_id, resource, resource_key, resource_id, flagged_on) VALUES (1, 2, 3, 'transactions.stock_master', 'stock_master_id', 7, '2014-09-28 17:50:51.967+00');
INSERT INTO flags (flag_id, user_id, flag_type_id, resource, resource_key, resource_id, flagged_on) VALUES (2, 2, 4, 'transactions.stock_master', 'stock_master_id', 5, '2014-09-28 17:51:32.284+00');
INSERT INTO flags (flag_id, user_id, flag_type_id, resource, resource_key, resource_id, flagged_on) VALUES (3, 2, 5, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', 1, '2014-09-28 17:51:40.242+00');


--
-- Name: flags_flag_id_seq; Type: SEQUENCE SET; Schema: core; Owner: postgres
--

SELECT pg_catalog.setval('flags_flag_id_seq', 3, true);


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

INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (1, 1, '1-2014-09-05-2-2-1-15-23-24', 'Journal', '2014-01-05', '2014-09-05 15:23:24.577+00', 1, 2, NULL, 2, 1, '', NULL, '2014-09-05 15:23:24.601+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (2, 2, '2-2014-09-05-2-2-2-15-37-22', 'Journal', '2014-02-05', '2014-09-05 15:37:22.802+00', 2, 2, NULL, 2, 1, '', NULL, '2014-09-05 15:37:22.819+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (3, 1, '1-2014-09-20-2-2-3-12-45-25', 'Purchase.Direct', '2014-03-20', '2014-09-20 12:45:25.485+00', 3, 2, NULL, 2, 7, '', 'Being various items purchased from Mr. Martin for Store 1.', '2014-09-20 12:45:25.532+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (4, 1, '1-2014-09-28-2-2-4-15-44-28', 'Sales.Direct', '2014-04-28', '2014-09-28 15:44:28.992+00', 4, 2, NULL, 2, 1, '', 'Apple products sold to Mr. James.', '2014-09-28 15:44:29.097+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (5, 2, '2-2014-09-28-2-2-4-15-45-44', 'Sales.Direct', '2014-05-28', '2014-09-28 15:45:44.307+00', 4, 2, NULL, 2, 1, '', 'Macbook Pro Late 2013 model, sold to Mr. Jones.', '2014-09-28 15:45:44.361+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-28 15:45:44.307+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (6, 3, '3-2014-09-28-2-2-4-15-57-47', 'Sales.Direct', '2014-06-28', '2014-09-28 15:57:47.832+00', 4, 2, NULL, 2, 4, '', 'Being MixNP Classifieds purchased by Alexander Thomas.', '2014-09-28 15:57:47.874+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-28 15:57:47.832+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (7, 4, '4-2014-09-28-2-2-4-15-58-44', 'Sales.Direct', '2014-07-28', '2014-09-28 15:58:44.155+00', 4, 2, NULL, 2, 7, '', 'Being IPhone 6 Plus purchased by Mr. Jacob.', '2014-09-28 15:58:44.201+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-28 15:58:44.155+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (8, 5, '5-2014-09-28-2-2-4-15-59-43', 'Sales.Direct', '2014-09-28', '2014-09-28 15:59:43.838+00', 4, 2, NULL, 2, 3, '', 'Being ITP sold to Mr. Walker.', '2014-09-28 15:59:43.889+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-28 15:59:43.838+00');


--
-- Data for Name: customer_receipts; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: customer_receipts_receipt_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('customer_receipts_receipt_id_seq', 1, false);


--
-- Data for Name: non_gl_stock_master; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, audit_user_id, audit_ts) VALUES (1, '2014-09-28', 'Sales.Quotation', 3, 1, '2014-09-28 16:00:26.805+00', 4, 2, 2, '', 'Quotation sent to Mr. Williams. #follow-up', NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, audit_user_id, audit_ts) VALUES (2, '2014-09-28', 'Sales.Quotation', 46, 1, '2014-09-28 16:14:42.477+00', 4, 2, 2, '', 'Quotation for 100 piece Iphone 6 Plus sent to Jordan. #follow-up', NULL, '2014-09-28 16:14:42.477+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, audit_user_id, audit_ts) VALUES (3, '2014-09-28', 'Sales.Quotation', 3, 1, '2014-09-28 16:15:42.16+00', 4, 2, 2, '', 'Quotation of AIT sent to Mr. Williams.', NULL, '2014-09-28 16:15:42.16+00');
INSERT INTO non_gl_stock_master (non_gl_stock_master_id, value_date, book, party_id, price_type_id, transaction_ts, login_id, user_id, office_id, reference_number, statement_reference, audit_user_id, audit_ts) VALUES (4, '2014-09-28', 'Sales.Order', 38, 1, '2014-09-28 16:17:14.178+00', 4, 2, 2, '', 'PO for 13MBA sent by Gonzalez Evan. #new-client, #important, #contact', NULL, '2014-09-28 16:17:14.178+00');


--
-- Data for Name: non_gl_stock_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (1, 1, 1, 2, 1, 2.00, 1, 225000.0000, 0.0000, 13, 58500.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (2, 1, 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 13, 20150.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (3, 1, 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 13, 17550.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (4, 1, 4, 1, 1, 1.00, 1, 70000.0000, 0.0000, 13, 9100.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (5, 1, 5, 1, 1, 1.00, 1, 80000.0000, 0.0000, 13, 10400.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (6, 1, 6, 1, 1, 1.00, 1, 50000.0000, 0.0000, 13, 6500.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (7, 1, 7, 1, 1, 1.00, 1, 70000.0000, 0.0000, 13, 9100.0000, NULL, '2014-09-28 16:00:26.805+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (8, 2, 9, 100, 1, 100.00, 1, 110000.0000, 0.0000, 13, 1430000.0000, NULL, '2014-09-28 16:14:42.477+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (9, 3, 11, 10, 1, 10.00, 1, 65000.0000, 0.0000, 13, 84500.0000, NULL, '2014-09-28 16:15:42.16+00');
INSERT INTO non_gl_stock_details (non_gl_stock_detail_id, non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (10, 4, 2, 10, 1, 10.00, 1, 155000.0000, 0.0000, 13, 201500.0000, NULL, '2014-09-28 16:17:14.178+00');


--
-- Name: non_gl_stock_details_non_gl_stock_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_details_non_gl_stock_detail_id_seq', 10, true);


--
-- Name: non_gl_stock_master_non_gl_stock_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_master_non_gl_stock_master_id_seq', 4, true);


--
-- Data for Name: non_gl_stock_master_relations; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: non_gl_stock_master_relations_non_gl_stock_master_relation__seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_master_relations_non_gl_stock_master_relation__seq', 1, false);



--
-- Data for Name: stock_master; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO stock_master (stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, audit_user_id, audit_ts) VALUES (1, 3, 16, NULL, NULL, false, NULL, NULL, 0.0000, 1, 1, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, audit_user_id, audit_ts) VALUES (2, 4, 17, 1, 1, false, 1, NULL, 0.0000, 1, 1, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, audit_user_id, audit_ts) VALUES (3, 5, 4, 2, 2, false, 1, NULL, 0.0000, 1, 1, NULL, '2014-09-28 15:45:44.307+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, audit_user_id, audit_ts) VALUES (4, 6, 12, 3, 1, false, 1, NULL, 0.0000, 1, 1, NULL, '2014-09-28 15:57:47.832+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, audit_user_id, audit_ts) VALUES (5, 7, 1, 4, 1, false, 1, NULL, 0.0000, 1, 1, NULL, '2014-09-28 15:58:44.155+00');
INSERT INTO stock_master (stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, audit_user_id, audit_ts) VALUES (6, 8, 25, 5, 1, false, 1, NULL, 0.0000, 1, 2, NULL, '2014-09-28 15:59:43.838+00');


--
-- Data for Name: stock_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (1, 1, 'Dr', 1, 1, 50, 1, 50.00, 1, 180000.0000, 0.0000, 13, 1170000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (2, 1, 'Dr', 1, 2, 50, 1, 50.00, 1, 130000.0000, 0.0000, 13, 845000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (3, 1, 'Dr', 1, 3, 30, 1, 30.00, 1, 110000.0000, 0.0000, 13, 429000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (4, 1, 'Dr', 1, 4, 50, 1, 50.00, 1, 53000.0000, 0.0000, 13, 344500.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (5, 1, 'Dr', 1, 5, 50, 1, 50.00, 1, 63000.0000, 0.0000, 13, 409500.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (6, 1, 'Dr', 1, 6, 30, 1, 30.00, 1, 33000.0000, 0.0000, 13, 128700.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (7, 1, 'Dr', 1, 7, 30, 1, 30.00, 1, 53000.0000, 0.0000, 13, 206700.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (8, 1, 'Dr', 1, 8, 100, 1, 100.00, 1, 93000.0000, 0.0000, 13, 1209000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (9, 1, 'Dr', 1, 9, 100, 1, 100.00, 1, 103000.0000, 0.0000, 13, 1339000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (10, 1, 'Dr', 1, 10, 25, 1, 25.00, 1, 80000.0000, 0.0000, 13, 260000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (11, 1, 'Dr', 1, 11, 10, 1, 10.00, 1, 40000.0000, 0.0000, 13, 52000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (12, 1, 'Dr', 1, 12, 100, 8, 120000, 1, 240000.0000, 0.0000, 13, 3120000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (13, 1, 'Dr', 1, 13, 100, 1, 100.00, 1, 30000.0000, 0.0000, 13, 390000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (14, 1, 'Dr', 1, 17, 5, 1, 5.00, 1, 30000.0000, 0.0000, 13, 19500.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (15, 2, 'Cr', 1, 1, 1, 1, 1.00, 1, 225000.0000, 0.0000, 13, 29250.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (16, 2, 'Cr', 1, 2, 1, 1, 1.00, 1, 155000.0000, 0.0000, 13, 20150.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (17, 2, 'Cr', 1, 3, 1, 1, 1.00, 1, 135000.0000, 0.0000, 13, 17550.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (18, 2, 'Cr', 1, 4, 1, 1, 1.00, 1, 70000.0000, 0.0000, 13, 9100.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (19, 2, 'Cr', 1, 5, 1, 1, 1.00, 1, 80000.0000, 0.0000, 13, 10400.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (20, 2, 'Cr', 1, 6, 1, 1, 1.00, 1, 50000.0000, 0.0000, 13, 6500.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (21, 2, 'Cr', 1, 7, 1, 1, 1.00, 1, 70000.0000, 0.0000, 13, 9100.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (22, 2, 'Cr', 1, 8, 1, 1, 1.00, 1, 105000.0000, 0.0000, 13, 13650.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (23, 2, 'Cr', 1, 9, 1, 1, 1.00, 1, 115000.0000, 0.0000, 13, 14950.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (24, 3, 'Cr', 1, 1, 1, 1, 1.00, 1, 225000.0000, 0.0000, 13, 29250.0000, NULL, '2014-09-28 15:45:44.307+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (25, 4, 'Cr', 1, 14, 1, 1, 1.00, 1, 150000.0000, 0.0000, 13, 19500.0000, NULL, '2014-09-28 15:57:47.832+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (26, 5, 'Cr', 1, 9, 10, 1, 10.00, 1, 115000.0000, 0.0000, 13, 149500.0000, NULL, '2014-09-28 15:58:44.155+00');
INSERT INTO stock_details (stock_master_detail_id, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id, audit_ts) VALUES (27, 6, 'Cr', 1, 10, 10, 1, 10.00, 1, 125000.0000, 0.0000, 13, 162500.0000, NULL, '2014-09-28 15:59:43.838+00');


--
-- Name: stock_details_stock_master_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('stock_details_stock_master_detail_id_seq', 27, true);


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

SELECT pg_catalog.setval('stock_master_stock_master_id_seq', 6, true);


--
-- Data for Name: transaction_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (3, 2, 'Dr', 8, 'Cash transfer', 2, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (4, 2, 'Cr', 8, 'Cash transfer', 1, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (5, 3, 'Dr', 140, 'Being various items purchased from Mr. Martin for Store 1.', NULL, 'NPR', 76330000.0000, 'NPR', 1, 76330000.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (6, 3, 'Dr', 74, 'Being various items purchased from Mr. Martin for Store 1.', NULL, 'NPR', 9922900.0000, 'NPR', 1, 9922900.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (7, 3, 'Cr', 8, 'Being various items purchased from Mr. Martin for Store 1.', 1, 'NPR', 86252900.0000, 'NPR', 1, 86252900.0000, NULL, '2014-09-20 12:45:25.485+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (1, 1, 'Cr', 113, 'Cash Invested by nirvan.', NULL, 'NPR', 500000000.0000, 'NPR', 1, 500000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (2, 1, 'Dr', 8, 'Cash Invested by nirvan.', 1, 'NPR', 500000000.0000, 'NPR', 1, 500000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (8, 4, 'Cr', 132, 'Apple products sold to Mr. James.', NULL, 'NPR', 1005000.0000, 'NPR', 1, 1005000.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (9, 4, 'Cr', 74, 'Apple products sold to Mr. James.', NULL, 'NPR', 130650.0000, 'NPR', 1, 130650.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (10, 4, 'Dr', 8, 'Apple products sold to Mr. James.', 1, 'NPR', 1135650.0000, 'NPR', 1, 1135650.0000, NULL, '2014-09-28 15:44:28.992+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (11, 5, 'Cr', 132, 'Macbook Pro Late 2013 model, sold to Mr. Jones.', NULL, 'NPR', 225000.0000, 'NPR', 1, 225000.0000, NULL, '2014-09-28 15:45:44.307+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (12, 5, 'Cr', 74, 'Macbook Pro Late 2013 model, sold to Mr. Jones.', NULL, 'NPR', 29250.0000, 'NPR', 1, 29250.0000, NULL, '2014-09-28 15:45:44.307+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (13, 5, 'Dr', 8, 'Macbook Pro Late 2013 model, sold to Mr. Jones.', 1, 'NPR', 254250.0000, 'NPR', 1, 254250.0000, NULL, '2014-09-28 15:45:44.307+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (14, 6, 'Cr', 132, 'Being MixNP Classifieds purchased by Alexander Thomas.', NULL, 'NPR', 150000.0000, 'NPR', 1, 150000.0000, NULL, '2014-09-28 15:57:47.832+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (15, 6, 'Cr', 74, 'Being MixNP Classifieds purchased by Alexander Thomas.', NULL, 'NPR', 19500.0000, 'NPR', 1, 19500.0000, NULL, '2014-09-28 15:57:47.832+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (16, 6, 'Dr', 8, 'Being MixNP Classifieds purchased by Alexander Thomas.', 1, 'NPR', 169500.0000, 'NPR', 1, 169500.0000, NULL, '2014-09-28 15:57:47.832+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (17, 7, 'Cr', 132, 'Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 1150000.0000, 'NPR', 1, 1150000.0000, NULL, '2014-09-28 15:58:44.155+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (18, 7, 'Cr', 74, 'Being IPhone 6 Plus purchased by Mr. Jacob.', NULL, 'NPR', 149500.0000, 'NPR', 1, 149500.0000, NULL, '2014-09-28 15:58:44.155+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (19, 7, 'Dr', 8, 'Being IPhone 6 Plus purchased by Mr. Jacob.', 1, 'NPR', 1299500.0000, 'NPR', 1, 1299500.0000, NULL, '2014-09-28 15:58:44.155+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (20, 8, 'Cr', 132, 'Being ITP sold to Mr. Walker.', NULL, 'NPR', 1250000.0000, 'NPR', 1, 1250000.0000, NULL, '2014-09-28 15:59:43.838+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (21, 8, 'Cr', 74, 'Being ITP sold to Mr. Walker.', NULL, 'NPR', 162500.0000, 'NPR', 1, 162500.0000, NULL, '2014-09-28 15:59:43.838+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (22, 8, 'Dr', 8, 'Being ITP sold to Mr. Walker.', 2, 'NPR', 1412500.0000, 'NPR', 1, 1412500.0000, NULL, '2014-09-28 15:59:43.838+00');


--
-- Name: transaction_details_transaction_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_details_transaction_detail_id_seq', 22, true);


--
-- Name: transaction_master_transaction_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_master_transaction_master_id_seq', 8, true);


--
-- PostgreSQL database dump complete
--

