/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
SET search_path = public;

DO 
$$
BEGIN   
    IF NOT EXISTS
    (
        SELECT * FROM pg_database 
        WHERE datcollate::text IN('C', 'POSIX')
        AND datctype::text IN('C', 'POSIX')
        AND datname=current_database()
    ) THEN
        RAISE EXCEPTION '%', 'The current server collation is not supported. Please change your database collation to "C" or "POSIX".';
    END IF;
    
    IF NOT EXISTS
    (
        SELECT 0 FROM pg_database 
        WHERE pg_encoding_to_char(encoding)::text = 'UTF8' 
        AND datname=current_database()
    ) THEN
        RAISE EXCEPTION '%', 'The current database encoding is not supported. Please change your encoding to "UTF8".';
    END IF;
    
   EXECUTE 'ALTER DATABASE ' || current_database() || ' SET timezone TO ''UTC''';    
END;
$$
LANGUAGE plpgsql;


CREATE EXTENSION IF NOT EXISTS tablefunc;
CREATE EXTENSION IF NOT EXISTS pgcrypto;
CREATE EXTENSION IF NOT EXISTS hstore;