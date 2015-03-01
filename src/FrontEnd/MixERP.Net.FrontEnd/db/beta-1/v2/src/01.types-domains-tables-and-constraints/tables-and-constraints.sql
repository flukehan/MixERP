CREATE TABLE policy.http_actions
(
    http_action_code                text NOT NULL PRIMARY KEY
);

CREATE UNIQUE INDEX policy_http_action_code_uix
ON policy.http_actions(UPPER(http_action_code));

CREATE TABLE policy.api_access_policy
(
    api_access_policy_id            BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                         integer NOT NULL REFERENCES office.users(user_id),
    office_id                       integer NOT NULL REFERENCES office.offices(office_id),
    poco_type_name                  text NOT NULL,
    http_action_code                text NOT NULL REFERENCES policy.http_actions(http_action_code),
    valid_till                      date NOT NULL,
    audit_user_id                   integer NULL REFERENCES office.users(user_id),
    audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                    DEFAULT(NOW())    
);

CREATE UNIQUE INDEX api_access_policy_uix
ON policy.api_access_policy(user_id, poco_type_name, http_action_code, valid_till);