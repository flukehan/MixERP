
using MixERP.Net.Entities;
using PetaPoco;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

// ReSharper Disable All 
namespace MixERP.Net.Core.Api.CRM
{
    /// <summary>
    /// A CRUD API for lead-sources. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class LeadSourceController: MixERPApiController
    {
        [HttpGet]
        [Route("api/crm/lead-sources")]
        [Route("api/crm/lead-sources/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.CRM.LeadSource> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadSource), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.CRM.LeadSource>(page, 10, "SELECT * FROM crm.lead_sources ORDER BY lead_source_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/crm/lead-sources/{id}")]
        public MixERP.Net.Entities.CRM.LeadSource GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadSource), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.CRM.LeadSource>("SELECT * FROM crm.lead_sources WHERE lead_source_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/crm/lead-sources/post/{item}")]
        public bool Post(MixERP.Net.Entities.CRM.LeadSource item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadSource), "POST");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Insert(item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return true;
        }

        [HttpPut]
        [Route("api/crm/lead-sources/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.CRM.LeadSource item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadSource), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("crm.lead_sources", "lead_source_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/crm/lead-sources/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadSource), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("crm.lead_sources", "lead_source_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of LeadSourceController Class        
    }

    
    /// <summary>
    /// A CRUD API for lead-statuses. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class LeadStatusController: MixERPApiController
    {
        [HttpGet]
        [Route("api/crm/lead-statuses")]
        [Route("api/crm/lead-statuses/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.CRM.LeadStatus> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadStatus), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.CRM.LeadStatus>(page, 10, "SELECT * FROM crm.lead_statuses ORDER BY lead_status_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/crm/lead-statuses/{id}")]
        public MixERP.Net.Entities.CRM.LeadStatus GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadStatus), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.CRM.LeadStatus>("SELECT * FROM crm.lead_statuses WHERE lead_status_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/crm/lead-statuses/post/{item}")]
        public bool Post(MixERP.Net.Entities.CRM.LeadStatus item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadStatus), "POST");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Insert(item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return true;
        }

        [HttpPut]
        [Route("api/crm/lead-statuses/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.CRM.LeadStatus item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadStatus), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("crm.lead_statuses", "lead_status_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/crm/lead-statuses/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.LeadStatus), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("crm.lead_statuses", "lead_status_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of LeadStatusController Class        
    }

    
    /// <summary>
    /// A CRUD API for opportunity-stages. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class OpportunityStageController: MixERPApiController
    {
        [HttpGet]
        [Route("api/crm/opportunity-stages")]
        [Route("api/crm/opportunity-stages/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.CRM.OpportunityStage> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.OpportunityStage), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.CRM.OpportunityStage>(page, 10, "SELECT * FROM crm.opportunity_stages ORDER BY opportunity_stage_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/crm/opportunity-stages/{id}")]
        public MixERP.Net.Entities.CRM.OpportunityStage GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.OpportunityStage), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.CRM.OpportunityStage>("SELECT * FROM crm.opportunity_stages WHERE opportunity_stage_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/crm/opportunity-stages/post/{item}")]
        public bool Post(MixERP.Net.Entities.CRM.OpportunityStage item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.OpportunityStage), "POST");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Insert(item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return true;
        }

        [HttpPut]
        [Route("api/crm/opportunity-stages/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.CRM.OpportunityStage item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.OpportunityStage), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("crm.opportunity_stages", "opportunity_stage_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/crm/opportunity-stages/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.CRM.OpportunityStage), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("crm.opportunity_stages", "opportunity_stage_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of OpportunityStageController Class        
    }

    
}

