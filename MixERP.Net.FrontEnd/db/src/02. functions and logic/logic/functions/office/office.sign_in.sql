DROP FUNCTION IF EXISTS office.sign_in(office_id integer_strict, user_name text, password text, browser text, ip_address text, remote_user text, culture text);
CREATE FUNCTION office.sign_in(office_id integer_strict, user_name text, password text, browser text, ip_address text, remote_user text, culture text)
RETURNS integer
AS
$$
	DECLARE _user_id integer;
	DECLARE _lock_out_till TIMESTAMP;
BEGIN
	_user_id:=office.get_user_id_by_user_name($2);

	IF _user_id IS NULL THEN
		INSERT INTO audit.failed_logins(user_name,browser,ip_address,remote_user,details)
		SELECT $2, $4, $5, $6, 'Invalid user name.';
	ELSE
		_lock_out_till:=policy.is_locked_out_till(_user_id);
		IF NOT ((_lock_out_till IS NOT NULL) AND (_lock_out_till>NOW())) THEN
			IF office.validate_login($2,$3) THEN
				IF office.can_login(_user_id,$1) THEN
					INSERT INTO audit.logins(office_id,user_id,browser,ip_address,remote_user, culture)
					SELECT $1, _user_id, $4, $5, $6, $7;

					RETURN CAST(currval('audit.logins_login_id_seq') AS integer);
				ELSE
					INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
					SELECT $1, _user_id, $2, $4, $5, $6, FORMAT('A user from %1$s cannot login to %2$s.', office.get_office_name_by_id(office.get_office_id_by_user_id(_user_id)), office.get_office_name_by_id($1));
				END IF;
			ELSE
				INSERT INTO audit.failed_logins(office_id,user_id,user_name,browser,ip_address,remote_user,details)
				SELECT $1, _user_id, $2, $4, $5, $6, 'Invalid login attempt.';
			END IF;
		END IF;
	END IF;

	RETURN 0;
END
$$
LANGUAGE plpgsql;

