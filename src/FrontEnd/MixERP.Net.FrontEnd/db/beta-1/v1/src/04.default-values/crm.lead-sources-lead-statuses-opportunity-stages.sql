DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO crm.lead_sources(lead_source_code, lead_source_name)
        SELECT 'AG', 'Agent'                UNION ALL
        SELECT 'CC', 'Cold Call'            UNION ALL
        SELECT 'CR', 'Customer Reference'   UNION ALL
        SELECT 'DI', 'Direct Inquiry'       UNION ALL
        SELECT 'EV', 'Events'               UNION ALL
        SELECT 'PR', 'Partner';


        INSERT INTO crm.lead_statuses(lead_status_code, lead_status_name)
        SELECT 'CL', 'Cool'                 UNION ALL
        SELECT 'CF', 'Contact in Future'    UNION ALL
        SELECT 'LO', 'Lost'                 UNION ALL
        SELECT 'IP', 'In Prgress'           UNION ALL
        SELECT 'QF', 'Qualified';


        INSERT INTO crm.opportunity_stages(opportunity_stage_code, opportunity_stage_name)
        SELECT 'PRO', 'Prospecting'         UNION ALL
        SELECT 'QUA', 'Qualification'       UNION ALL
        SELECT 'NEG', 'Negotiating'         UNION ALL
        SELECT 'VER', 'Verbal'              UNION ALL
        SELECT 'CLW', 'Closed Won'          UNION ALL
        SELECT 'CLL', 'Closed Lost';
    END IF;
END
$$
LANGUAGE plpgsql;