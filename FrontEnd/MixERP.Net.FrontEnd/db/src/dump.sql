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


--
-- Name: logins_login_id_seq; Type: SEQUENCE SET; Schema: audit; Owner: postgres
--

SELECT pg_catalog.setval('logins_login_id_seq', 2, true);


SET search_path = transactions, pg_catalog;

--
-- Data for Name: attachments; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: attachments_attachment_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('attachments_attachment_id_seq', 1, false);


--
-- Data for Name: transaction_master; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (1, 1, '1-2014-09-05-2-2-1-15-23-24', 'Journal', '2014-09-05', '2014-09-05 15:23:24.577+00', 1, 2, NULL, 2, 1, '', NULL, '2014-09-05 15:23:24.601+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_master (transaction_master_id, transaction_counter, transaction_code, book, value_date, transaction_ts, login_id, user_id, sys_user_id, office_id, cost_center_id, reference_number, statement_reference, last_verified_on, verified_by_user_id, verification_status_id, verification_reason, audit_user_id, audit_ts) VALUES (2, 2, '2-2014-09-05-2-2-2-15-37-22', 'Journal', '2014-09-05', '2014-09-05 15:37:22.802+00', 2, 2, NULL, 2, 1, '', NULL, '2014-09-05 15:37:22.819+00', 1, 2, 'Automatically verified by workflow.', NULL, '2014-09-05 15:37:22.802+00');


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



--
-- Data for Name: non_gl_stock_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: non_gl_stock_details_non_gl_stock_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_details_non_gl_stock_detail_id_seq', 1, false);


--
-- Name: non_gl_stock_master_non_gl_stock_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('non_gl_stock_master_non_gl_stock_master_id_seq', 1, false);


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



--
-- Data for Name: stock_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--



--
-- Name: stock_details_stock_master_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('stock_details_stock_master_detail_id_seq', 1, false);


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

SELECT pg_catalog.setval('stock_master_stock_master_id_seq', 1, false);


--
-- Data for Name: transaction_details; Type: TABLE DATA; Schema: transactions; Owner: postgres
--

INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (1, 1, 'Cr', 113, 'Cash Invested by nirvan.', NULL, 'NPR', 5000000.0000, 'NPR', 1, 5000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (2, 1, 'Dr', 8, 'Cash Invested by nirvan.', 1, 'NPR', 5000000.0000, 'NPR', 1, 5000000.0000, NULL, '2014-09-05 15:23:24.577+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (3, 2, 'Dr', 8, 'Cash transfer', 2, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');
INSERT INTO transaction_details (transaction_detail_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id, audit_ts) VALUES (4, 2, 'Cr', 8, 'Cash transfer', 1, 'NPR', 100.0000, 'NPR', 1, 100.0000, NULL, '2014-09-05 15:37:22.802+00');


--
-- Name: transaction_details_transaction_detail_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_details_transaction_detail_id_seq', 4, true);


--
-- Name: transaction_master_transaction_master_id_seq; Type: SEQUENCE SET; Schema: transactions; Owner: postgres
--

SELECT pg_catalog.setval('transaction_master_transaction_master_id_seq', 2, true);


--
-- PostgreSQL database dump complete
--

