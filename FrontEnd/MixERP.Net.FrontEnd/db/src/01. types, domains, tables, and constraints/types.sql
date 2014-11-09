DROP TYPE IF EXISTS transactions.stock_detail_type CASCADE;
CREATE TYPE transactions.stock_detail_type AS
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

DROP TYPE IF EXISTS core.attachment_type CASCADE;
CREATE TYPE core.attachment_type AS
(
    comment                 national character varying(96),
    file_path               text,
    original_file_name          text
);

DROP TYPE IF EXISTS transactions.purchase_reorder_type CASCADE;
CREATE TYPE transactions.purchase_reorder_type
AS
(
        item_id                 integer,
        supplier_code           national character varying(12),
        unit_id                 integer,
        price                   decimal_strict,
        tax_rate                decimal_strict2,
        order_quantity          integer_strict
);


DROP TYPE IF EXISTS transactions.stock_adjustment_type CASCADE;
CREATE TYPE transactions.stock_adjustment_type AS
(
        tran_type       transaction_type,
        store_name      national character varying(50),
        item_code       national character varying(12),
        unit_name       national character varying(50),
        quantity        integer_strict
);
