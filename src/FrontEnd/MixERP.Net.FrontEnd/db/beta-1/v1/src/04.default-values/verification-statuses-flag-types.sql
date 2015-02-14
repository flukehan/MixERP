DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        --These are hardcoded values and therefore the meanings should always remain intact
        --regardless of the language.
        INSERT INTO core.verification_statuses
        SELECT -3,  'Rejected'                              UNION ALL
        SELECT -2,  'Closed'                                UNION ALL
        SELECT -1,  'Withdrawn'                             UNION ALL
        SELECT 0,   'Unverified'                            UNION ALL
        SELECT 1,   'Automatically Approved by Workflow'    UNION ALL
        SELECT 2,   'Approved';

        INSERT INTO core.flag_types(flag_type_name, background_color, foreground_color)
        SELECT 'Critical',      '#FA5882', '#FFFFFF'    UNION ALL
        SELECT 'Important',     '#F6CEF5', '#000000'    UNION ALL
        SELECT 'Review',        '#CEECF5', '#000000'    UNION ALL
        SELECT 'Todo',          '#F7F8E0', '#000000'    UNION ALL
        SELECT 'OK',            '#D0F5A9', '#000000';
    END IF;
END
$$
LANGUAGE plpgsql;