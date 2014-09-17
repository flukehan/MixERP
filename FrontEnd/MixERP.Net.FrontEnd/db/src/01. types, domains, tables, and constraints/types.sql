DROP TYPE IF EXISTS stock_detail_type CASCADE;
CREATE TYPE stock_detail_type AS
(
        store_id        integer,
        item_code       national character varying(12),
        quantity        integer_strict,
        unit_name       national character varying(50),
        price           money_strict,
        discount        money_strict2,
        tax_rate        decimal_strict2,
        tax             money_strict2
);

DROP TYPE IF EXISTS attachment_type CASCADE;
CREATE TYPE attachment_type AS
(
	comment					national character varying(96),
	file_path				text,
	original_file_name			text
);