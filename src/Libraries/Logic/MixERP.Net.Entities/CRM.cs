
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;

namespace MixERP.Net.Entities.CRM
{

    [TableName("crm.lead_sources")]
    [PrimaryKey("lead_source_id")]
    [ExplicitColumns]
    public class LeadSource : PetaPocoDB.Record<LeadSource> , IPoco
    {
        [Column("lead_source_id")] 
        public int LeadSourceId { get; set; }

        [Column("lead_source_code")] 
        public string LeadSourceCode { get; set; }

        [Column("lead_source_name")] 
        public string LeadSourceName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("crm.lead_statuses")]
    [PrimaryKey("lead_status_id")]
    [ExplicitColumns]
    public class LeadStatus : PetaPocoDB.Record<LeadStatus> , IPoco
    {
        [Column("lead_status_id")] 
        public int LeadStatusId { get; set; }

        [Column("lead_status_code")] 
        public string LeadStatusCode { get; set; }

        [Column("lead_status_name")] 
        public string LeadStatusName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }

    [TableName("crm.opportunity_stages")]
    [PrimaryKey("opportunity_stage_id")]
    [ExplicitColumns]
    public class OpportunityStage : PetaPocoDB.Record<OpportunityStage> , IPoco
    {
        [Column("opportunity_stage_id")] 
        public int OpportunityStageId { get; set; }

        [Column("opportunity_stage_code")] 
        public string OpportunityStageCode { get; set; }

        [Column("opportunity_stage_name")] 
        public string OpportunityStageName { get; set; }

        [Column("audit_user_id")] 
        public int? AuditUserId { get; set; }

        [Column("audit_ts")] 
        public DateTime? AuditTs { get; set; }

    }
}


