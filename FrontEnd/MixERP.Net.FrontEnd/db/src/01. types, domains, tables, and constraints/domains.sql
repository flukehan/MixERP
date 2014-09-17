DROP DOMAIN IF EXISTS transaction_type CASCADE;
CREATE DOMAIN transaction_type
AS char(2)
CHECK
(
	VALUE IN
	(
		'Dr', --Debit
		'Cr' --Credit
	)
);

/*******************************************************************
	MIXERP STRICT Data Types: NEGATIVES ARE NOT ALLOWED
*******************************************************************/

DROP DOMAIN IF EXISTS money_strict CASCADE;
CREATE DOMAIN money_strict
AS DECIMAL(24, 4)
CHECK
(
	VALUE > 0
);


DROP DOMAIN IF EXISTS money_strict2 CASCADE;
CREATE DOMAIN money_strict2
AS DECIMAL(24, 4)
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS integer_strict CASCADE;
CREATE DOMAIN integer_strict
AS integer
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS integer_strict2 CASCADE;
CREATE DOMAIN integer_strict2
AS integer
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS smallint_strict CASCADE;
CREATE DOMAIN smallint_strict
AS smallint
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS smallint_strict2 CASCADE;
CREATE DOMAIN smallint_strict2
AS smallint
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS decimal_strict CASCADE;
CREATE DOMAIN decimal_strict
AS decimal
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS decimal_strict2 CASCADE;
CREATE DOMAIN decimal_strict2
AS decimal
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS image_path CASCADE;
CREATE DOMAIN image_path
AS text;

DROP DOMAIN IF EXISTS color CASCADE;
CREATE DOMAIN color
AS text;
