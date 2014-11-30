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





--
-- Data for Name: transaction_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (3, 2, '2014-02-05', 'Dr', 8, 'Cash transfer', 2, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (4, 2, '2014-02-05', 'Cr', 8, 'Cash transfer', 1, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (1, 1, '2014-01-05', 'Cr', 114, 'Cash Invested by nirvan.', NULL, 'NPR', 500000000.0000, 'NPR', 1, 500000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, value_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (2, 1, '2014-01-05', 'Dr', 8, 'Cash Invested by nirvan.', 1, 'NPR', 500000000.0000, 'NPR', 1, 500000000.0000, NULL, '2014-09-05 15:23:24.577+00');

--
-- Name: transaction_details_transaction_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_details_transaction_detail_id_seq', 5, true);


--
-- Name: transaction_master_transaction_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_master_transaction_master_id_seq', 3, true);


--
-- PostgreSQL database dump complete
--

