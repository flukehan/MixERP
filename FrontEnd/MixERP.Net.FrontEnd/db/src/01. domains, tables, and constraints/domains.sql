DROP DOMAIN IF EXISTS transaction_type;
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

DROP DOMAIN IF EXISTS money_strict;
CREATE DOMAIN money_strict
AS DECIMAL(24, 4)
CHECK
(
	VALUE > 0
);


DROP DOMAIN IF EXISTS money_strict2;
CREATE DOMAIN money_strict2
AS DECIMAL(24, 4)
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS integer_strict;
CREATE DOMAIN integer_strict
AS integer
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS integer_strict2;
CREATE DOMAIN integer_strict2
AS integer
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS smallint_strict;
CREATE DOMAIN smallint_strict
AS smallint
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS smallint_strict2;
CREATE DOMAIN smallint_strict2
AS smallint
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS decimal_strict;
CREATE DOMAIN decimal_strict
AS decimal
CHECK
(
	VALUE > 0
);

DROP DOMAIN IF EXISTS decimal_strict2;
CREATE DOMAIN decimal_strict2
AS decimal
CHECK
(
	VALUE >= 0
);

DROP DOMAIN IF EXISTS image_path;
CREATE DOMAIN image_path
AS text;

DROP DOMAIN IF EXISTS color;
CREATE DOMAIN color
AS text;
