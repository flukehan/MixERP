
using MixERP.Net.Entities;
using PetaPoco;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

// ReSharper Disable All 
namespace MixERP.Net.Core.Api.Core
{
    /// <summary>
    /// A CRUD API for flag-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class FlagTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/flag-types")]
        [Route("api/core/flag-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.FlagType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FlagType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.FlagType>(page, 10, "SELECT * FROM core.flag_types ORDER BY flag_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/flag-types/{id}")]
        public MixERP.Net.Entities.Core.FlagType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FlagType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.FlagType>("SELECT * FROM core.flag_types WHERE flag_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/flag-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.FlagType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FlagType), "POST");
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
        [Route("api/core/flag-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.FlagType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FlagType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.flag_types", "flag_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/flag-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FlagType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.flag_types", "flag_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of FlagTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for flags. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class FlagController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/flags")]
        [Route("api/core/flags/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Flag> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Flag), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Flag>(page, 10, "SELECT * FROM core.flags ORDER BY flag_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/flags/{id}")]
        public MixERP.Net.Entities.Core.Flag GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Flag), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Flag>("SELECT * FROM core.flags WHERE flag_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/flags/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Flag item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Flag), "POST");
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
        [Route("api/core/flags/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.Flag item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Flag), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.flags", "flag_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/flags/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Flag), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.flags", "flag_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of FlagController Class        
    }

    
    /// <summary>
    /// A CRUD API for zip-code-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ZipCodeTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/zip-code-types")]
        [Route("api/core/zip-code-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ZipCodeType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCodeType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ZipCodeType>(page, 10, "SELECT * FROM core.zip_code_types ORDER BY zip_code_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/zip-code-types/{id}")]
        public MixERP.Net.Entities.Core.ZipCodeType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCodeType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ZipCodeType>("SELECT * FROM core.zip_code_types WHERE zip_code_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/zip-code-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ZipCodeType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCodeType), "POST");
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
        [Route("api/core/zip-code-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.ZipCodeType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCodeType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.zip_code_types", "zip_code_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/zip-code-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCodeType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.zip_code_types", "zip_code_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ZipCodeTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for zip-codes. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ZipCodeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/zip-codes")]
        [Route("api/core/zip-codes/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ZipCode> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCode), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ZipCode>(page, 10, "SELECT * FROM core.zip_codes ORDER BY zip_code_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/zip-codes/{id}")]
        public MixERP.Net.Entities.Core.ZipCode GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCode), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ZipCode>("SELECT * FROM core.zip_codes WHERE zip_code_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/zip-codes/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ZipCode item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCode), "POST");
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
        [Route("api/core/zip-codes/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.ZipCode item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCode), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.zip_codes", "zip_code_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/zip-codes/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ZipCode), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.zip_codes", "zip_code_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ZipCodeController Class        
    }

    
    /// <summary>
    /// A CRUD API for attachment-lookup. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AttachmentLookupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/attachment-lookup")]
        [Route("api/core/attachment-lookup/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.AttachmentLookup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AttachmentLookup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.AttachmentLookup>(page, 10, "SELECT * FROM core.attachment_lookup ORDER BY attachment_lookup_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/attachment-lookup/{id}")]
        public MixERP.Net.Entities.Core.AttachmentLookup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AttachmentLookup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.AttachmentLookup>("SELECT * FROM core.attachment_lookup WHERE attachment_lookup_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/attachment-lookup/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.AttachmentLookup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AttachmentLookup), "POST");
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
        [Route("api/core/attachment-lookup/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.AttachmentLookup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AttachmentLookup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.attachment_lookup", "attachment_lookup_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/attachment-lookup/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AttachmentLookup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.attachment_lookup", "attachment_lookup_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of AttachmentLookupController Class        
    }

    
    /// <summary>
    /// A CRUD API for attachments. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AttachmentController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/attachments")]
        [Route("api/core/attachments/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Attachment> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Attachment), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Attachment>(page, 10, "SELECT * FROM core.attachments ORDER BY attachment_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/attachments/{id}")]
        public MixERP.Net.Entities.Core.Attachment GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Attachment), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Attachment>("SELECT * FROM core.attachments WHERE attachment_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/attachments/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Attachment item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Attachment), "POST");
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
        [Route("api/core/attachments/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.Attachment item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Attachment), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.attachments", "attachment_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/attachments/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Attachment), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.attachments", "attachment_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of AttachmentController Class        
    }

    
    /// <summary>
    /// A CRUD API for exchange-rates. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ExchangeRateController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/exchange-rates")]
        [Route("api/core/exchange-rates/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ExchangeRate> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRate), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ExchangeRate>(page, 10, "SELECT * FROM core.exchange_rates ORDER BY exchange_rate_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/exchange-rates/{id}")]
        public MixERP.Net.Entities.Core.ExchangeRate GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRate), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ExchangeRate>("SELECT * FROM core.exchange_rates WHERE exchange_rate_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/exchange-rates/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ExchangeRate item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRate), "POST");
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
        [Route("api/core/exchange-rates/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.ExchangeRate item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRate), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.exchange_rates", "exchange_rate_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/exchange-rates/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRate), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.exchange_rates", "exchange_rate_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ExchangeRateController Class        
    }

    
    /// <summary>
    /// A CRUD API for exchange-rate-details. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ExchangeRateDetailController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/exchange-rate-details")]
        [Route("api/core/exchange-rate-details/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ExchangeRateDetail> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRateDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ExchangeRateDetail>(page, 10, "SELECT * FROM core.exchange_rate_details ORDER BY exchange_rate_detail_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/exchange-rate-details/{id}")]
        public MixERP.Net.Entities.Core.ExchangeRateDetail GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRateDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ExchangeRateDetail>("SELECT * FROM core.exchange_rate_details WHERE exchange_rate_detail_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/exchange-rate-details/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ExchangeRateDetail item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRateDetail), "POST");
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
        [Route("api/core/exchange-rate-details/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.ExchangeRateDetail item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRateDetail), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.exchange_rate_details", "exchange_rate_detail_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/exchange-rate-details/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ExchangeRateDetail), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.exchange_rate_details", "exchange_rate_detail_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ExchangeRateDetailController Class        
    }

    
    /// <summary>
    /// A CRUD API for menu-locale. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class MenuLocaleController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/menu-locale")]
        [Route("api/core/menu-locale/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.MenuLocale> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.MenuLocale), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.MenuLocale>(page, 10, "SELECT * FROM core.menu_locale ORDER BY menu_locale_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/menu-locale/{id}")]
        public MixERP.Net.Entities.Core.MenuLocale GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.MenuLocale), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.MenuLocale>("SELECT * FROM core.menu_locale WHERE menu_locale_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/menu-locale/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.MenuLocale item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.MenuLocale), "POST");
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
        [Route("api/core/menu-locale/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.MenuLocale item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.MenuLocale), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.menu_locale", "menu_locale_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/menu-locale/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.MenuLocale), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.menu_locale", "menu_locale_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of MenuLocaleController Class        
    }

    
    /// <summary>
    /// A CRUD API for menus. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class MenuController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/menus")]
        [Route("api/core/menus/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Menu> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Menu), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Menu>(page, 10, "SELECT * FROM core.menus ORDER BY menu_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/menus/{id}")]
        public MixERP.Net.Entities.Core.Menu GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Menu), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Menu>("SELECT * FROM core.menus WHERE menu_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/menus/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Menu item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Menu), "POST");
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
        [Route("api/core/menus/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Menu item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Menu), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.menus", "menu_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/menus/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Menu), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.menus", "menu_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of MenuController Class        
    }

    
    /// <summary>
    /// A CRUD API for fiscal-year. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class FiscalYearController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/fiscal-year")]
        [Route("api/core/fiscal-year/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FiscalYear), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.FiscalYear>(page, 10, "SELECT * FROM core.fiscal_year ORDER BY fiscal_year_code").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/fiscal-year/{id}")]
        public MixERP.Net.Entities.Core.FiscalYear GetSingle(string id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FiscalYear), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.FiscalYear>("SELECT * FROM core.fiscal_year WHERE fiscal_year_code=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/fiscal-year/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.FiscalYear item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FiscalYear), "POST");
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
        [Route("api/core/fiscal-year/put/{id}")]
        public void Put(string id, MixERP.Net.Entities.Core.FiscalYear item)
        {
                        if (item == null || string.IsNullOrWhiteSpace(id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FiscalYear), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.fiscal_year", "fiscal_year_code", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/fiscal-year/delete/{id}")]
        public void Delete(string id)
        {
                        if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FiscalYear), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.fiscal_year", "fiscal_year_code", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of FiscalYearController Class        
    }

    
    /// <summary>
    /// A CRUD API for frequency-setups. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class FrequencySetupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/frequency-setups")]
        [Route("api/core/frequency-setups/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.FrequencySetup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FrequencySetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.FrequencySetup>(page, 10, "SELECT * FROM core.frequency_setups ORDER BY frequency_setup_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/frequency-setups/{id}")]
        public MixERP.Net.Entities.Core.FrequencySetup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FrequencySetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.FrequencySetup>("SELECT * FROM core.frequency_setups WHERE frequency_setup_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/frequency-setups/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.FrequencySetup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FrequencySetup), "POST");
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
        [Route("api/core/frequency-setups/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.FrequencySetup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FrequencySetup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.frequency_setups", "frequency_setup_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/frequency-setups/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.FrequencySetup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.frequency_setups", "frequency_setup_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of FrequencySetupController Class        
    }

    
    /// <summary>
    /// A CRUD API for compound-units. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CompoundUnitController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/compound-units")]
        [Route("api/core/compound-units/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.CompoundUnit> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundUnit), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.CompoundUnit>(page, 10, "SELECT * FROM core.compound_units ORDER BY compound_unit_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/compound-units/{id}")]
        public MixERP.Net.Entities.Core.CompoundUnit GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundUnit), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.CompoundUnit>("SELECT * FROM core.compound_units WHERE compound_unit_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/compound-units/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.CompoundUnit item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundUnit), "POST");
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
        [Route("api/core/compound-units/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.CompoundUnit item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundUnit), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.compound_units", "compound_unit_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/compound-units/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundUnit), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.compound_units", "compound_unit_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CompoundUnitController Class        
    }

    
    /// <summary>
    /// A CRUD API for transaction-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TransactionTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/transaction-types")]
        [Route("api/core/transaction-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.TransactionType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TransactionType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.TransactionType>(page, 10, "SELECT * FROM core.transaction_types ORDER BY transaction_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/transaction-types/{id}")]
        public MixERP.Net.Entities.Core.TransactionType GetSingle(short id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TransactionType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.TransactionType>("SELECT * FROM core.transaction_types WHERE transaction_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/transaction-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.TransactionType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TransactionType), "POST");
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
        [Route("api/core/transaction-types/put/{id}")]
        public void Put(short id, MixERP.Net.Entities.Core.TransactionType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TransactionType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.transaction_types", "transaction_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/transaction-types/delete/{id}")]
        public void Delete(short id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TransactionType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.transaction_types", "transaction_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of TransactionTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for cash-flow-headings. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CashFlowHeadingController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/cash-flow-headings")]
        [Route("api/core/cash-flow-headings/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.CashFlowHeading> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowHeading), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.CashFlowHeading>(page, 10, "SELECT * FROM core.cash_flow_headings ORDER BY cash_flow_heading_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/cash-flow-headings/{id}")]
        public MixERP.Net.Entities.Core.CashFlowHeading GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowHeading), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.CashFlowHeading>("SELECT * FROM core.cash_flow_headings WHERE cash_flow_heading_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/cash-flow-headings/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.CashFlowHeading item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowHeading), "POST");
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
        [Route("api/core/cash-flow-headings/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.CashFlowHeading item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowHeading), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.cash_flow_headings", "cash_flow_heading_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/cash-flow-headings/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowHeading), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.cash_flow_headings", "cash_flow_heading_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CashFlowHeadingController Class        
    }

    
    /// <summary>
    /// A CRUD API for account-masters. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AccountMasterController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/account-masters")]
        [Route("api/core/account-masters/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.AccountMaster> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AccountMaster), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.AccountMaster>(page, 10, "SELECT * FROM core.account_masters ORDER BY account_master_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/account-masters/{id}")]
        public MixERP.Net.Entities.Core.AccountMaster GetSingle(short id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AccountMaster), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.AccountMaster>("SELECT * FROM core.account_masters WHERE account_master_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/account-masters/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.AccountMaster item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AccountMaster), "POST");
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
        [Route("api/core/account-masters/put/{id}")]
        public void Put(short id, MixERP.Net.Entities.Core.AccountMaster item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AccountMaster), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.account_masters", "account_master_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/account-masters/delete/{id}")]
        public void Delete(short id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AccountMaster), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.account_masters", "account_master_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of AccountMasterController Class        
    }

    
    /// <summary>
    /// A CRUD API for cash-flow-setup. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CashFlowSetupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/cash-flow-setup")]
        [Route("api/core/cash-flow-setup/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.CashFlowSetup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.CashFlowSetup>(page, 10, "SELECT * FROM core.cash_flow_setup ORDER BY cash_flow_setup_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/cash-flow-setup/{id}")]
        public MixERP.Net.Entities.Core.CashFlowSetup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.CashFlowSetup>("SELECT * FROM core.cash_flow_setup WHERE cash_flow_setup_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/cash-flow-setup/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.CashFlowSetup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowSetup), "POST");
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
        [Route("api/core/cash-flow-setup/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.CashFlowSetup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowSetup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.cash_flow_setup", "cash_flow_setup_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/cash-flow-setup/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CashFlowSetup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.cash_flow_setup", "cash_flow_setup_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CashFlowSetupController Class        
    }

    
    /// <summary>
    /// A CRUD API for sales-teams. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalesTeamController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/sales-teams")]
        [Route("api/core/sales-teams/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalesTeam> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTeam), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalesTeam>(page, 10, "SELECT * FROM core.sales_teams ORDER BY sales_team_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/sales-teams/{id}")]
        public MixERP.Net.Entities.Core.SalesTeam GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTeam), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalesTeam>("SELECT * FROM core.sales_teams WHERE sales_team_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/sales-teams/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalesTeam item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTeam), "POST");
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
        [Route("api/core/sales-teams/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalesTeam item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTeam), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.sales_teams", "sales_team_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/sales-teams/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTeam), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.sales_teams", "sales_team_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalesTeamController Class        
    }

    
    /// <summary>
    /// A CRUD API for bonus-slab-details. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class BonusSlabDetailController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/bonus-slab-details")]
        [Route("api/core/bonus-slab-details/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.BonusSlabDetail> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlabDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.BonusSlabDetail>(page, 10, "SELECT * FROM core.bonus_slab_details ORDER BY bonus_slab_detail_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/bonus-slab-details/{id}")]
        public MixERP.Net.Entities.Core.BonusSlabDetail GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlabDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.BonusSlabDetail>("SELECT * FROM core.bonus_slab_details WHERE bonus_slab_detail_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/bonus-slab-details/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.BonusSlabDetail item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlabDetail), "POST");
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
        [Route("api/core/bonus-slab-details/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.BonusSlabDetail item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlabDetail), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.bonus_slab_details", "bonus_slab_detail_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/bonus-slab-details/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlabDetail), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.bonus_slab_details", "bonus_slab_detail_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of BonusSlabDetailController Class        
    }

    
    /// <summary>
    /// A CRUD API for salesperson-bonus-setups. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalespersonBonusSetupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/salesperson-bonus-setups")]
        [Route("api/core/salesperson-bonus-setups/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalespersonBonusSetup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalespersonBonusSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalespersonBonusSetup>(page, 10, "SELECT * FROM core.salesperson_bonus_setups ORDER BY salesperson_bonus_setup_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/salesperson-bonus-setups/{id}")]
        public MixERP.Net.Entities.Core.SalespersonBonusSetup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalespersonBonusSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalespersonBonusSetup>("SELECT * FROM core.salesperson_bonus_setups WHERE salesperson_bonus_setup_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/salesperson-bonus-setups/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalespersonBonusSetup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalespersonBonusSetup), "POST");
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
        [Route("api/core/salesperson-bonus-setups/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalespersonBonusSetup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalespersonBonusSetup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.salesperson_bonus_setups", "salesperson_bonus_setup_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/salesperson-bonus-setups/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalespersonBonusSetup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.salesperson_bonus_setups", "salesperson_bonus_setup_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalespersonBonusSetupController Class        
    }

    
    /// <summary>
    /// A CRUD API for ageing-slabs. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AgeingSlabController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/ageing-slabs")]
        [Route("api/core/ageing-slabs/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.AgeingSlab> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AgeingSlab), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.AgeingSlab>(page, 10, "SELECT * FROM core.ageing_slabs ORDER BY ageing_slab_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/ageing-slabs/{id}")]
        public MixERP.Net.Entities.Core.AgeingSlab GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AgeingSlab), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.AgeingSlab>("SELECT * FROM core.ageing_slabs WHERE ageing_slab_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/ageing-slabs/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.AgeingSlab item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AgeingSlab), "POST");
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
        [Route("api/core/ageing-slabs/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.AgeingSlab item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AgeingSlab), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.ageing_slabs", "ageing_slab_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/ageing-slabs/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.AgeingSlab), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.ageing_slabs", "ageing_slab_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of AgeingSlabController Class        
    }

    
    /// <summary>
    /// A CRUD API for countries. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CountryController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/countries")]
        [Route("api/core/countries/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Country> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Country), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Country>(page, 10, "SELECT * FROM core.countries ORDER BY country_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/countries/{id}")]
        public MixERP.Net.Entities.Core.Country GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Country), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Country>("SELECT * FROM core.countries WHERE country_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/countries/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Country item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Country), "POST");
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
        [Route("api/core/countries/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Country item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Country), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.countries", "country_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/countries/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Country), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.countries", "country_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CountryController Class        
    }

    
    /// <summary>
    /// A CRUD API for income-tax-setup. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class IncomeTaxSetupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/income-tax-setup")]
        [Route("api/core/income-tax-setup/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.IncomeTaxSetup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.IncomeTaxSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.IncomeTaxSetup>(page, 10, "SELECT * FROM core.income_tax_setup ORDER BY income_tax_setup_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/income-tax-setup/{id}")]
        public MixERP.Net.Entities.Core.IncomeTaxSetup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.IncomeTaxSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.IncomeTaxSetup>("SELECT * FROM core.income_tax_setup WHERE income_tax_setup_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/income-tax-setup/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.IncomeTaxSetup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.IncomeTaxSetup), "POST");
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
        [Route("api/core/income-tax-setup/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.IncomeTaxSetup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.IncomeTaxSetup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.income_tax_setup", "income_tax_setup_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/income-tax-setup/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.IncomeTaxSetup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.income_tax_setup", "income_tax_setup_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of IncomeTaxSetupController Class        
    }

    
    /// <summary>
    /// A CRUD API for states. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class StateController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/states")]
        [Route("api/core/states/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.State> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.State), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.State>(page, 10, "SELECT * FROM core.states ORDER BY state_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/states/{id}")]
        public MixERP.Net.Entities.Core.State GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.State), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.State>("SELECT * FROM core.states WHERE state_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/states/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.State item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.State), "POST");
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
        [Route("api/core/states/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.State item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.State), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.states", "state_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/states/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.State), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.states", "state_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of StateController Class        
    }

    
    /// <summary>
    /// A CRUD API for counties. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CountyController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/counties")]
        [Route("api/core/counties/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.County> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.County), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.County>(page, 10, "SELECT * FROM core.counties ORDER BY county_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/counties/{id}")]
        public MixERP.Net.Entities.Core.County GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.County), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.County>("SELECT * FROM core.counties WHERE county_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/counties/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.County item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.County), "POST");
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
        [Route("api/core/counties/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.County item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.County), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.counties", "county_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/counties/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.County), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.counties", "county_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CountyController Class        
    }

    
    /// <summary>
    /// A CRUD API for tax-base-amount-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TaxBaseAmountTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/tax-base-amount-types")]
        [Route("api/core/tax-base-amount-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxBaseAmountType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxBaseAmountType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.TaxBaseAmountType>(page, 10, "SELECT * FROM core.tax_base_amount_types ORDER BY tax_base_amount_type_code").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/tax-base-amount-types/{id}")]
        public MixERP.Net.Entities.Core.TaxBaseAmountType GetSingle(string id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxBaseAmountType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.TaxBaseAmountType>("SELECT * FROM core.tax_base_amount_types WHERE tax_base_amount_type_code=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/tax-base-amount-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.TaxBaseAmountType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxBaseAmountType), "POST");
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
        [Route("api/core/tax-base-amount-types/put/{id}")]
        public void Put(string id, MixERP.Net.Entities.Core.TaxBaseAmountType item)
        {
                        if (item == null || string.IsNullOrWhiteSpace(id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxBaseAmountType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.tax_base_amount_types", "tax_base_amount_type_code", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/tax-base-amount-types/delete/{id}")]
        public void Delete(string id)
        {
                        if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxBaseAmountType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.tax_base_amount_types", "tax_base_amount_type_code", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of TaxBaseAmountTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for sales-tax-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalesTaxTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/sales-tax-types")]
        [Route("api/core/sales-tax-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalesTaxType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalesTaxType>(page, 10, "SELECT * FROM core.sales_tax_types ORDER BY sales_tax_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/sales-tax-types/{id}")]
        public MixERP.Net.Entities.Core.SalesTaxType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalesTaxType>("SELECT * FROM core.sales_tax_types WHERE sales_tax_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/sales-tax-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalesTaxType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxType), "POST");
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
        [Route("api/core/sales-tax-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalesTaxType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.sales_tax_types", "sales_tax_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/sales-tax-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.sales_tax_types", "sales_tax_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalesTaxTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for tax-rate-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TaxRateTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/tax-rate-types")]
        [Route("api/core/tax-rate-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxRateType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxRateType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.TaxRateType>(page, 10, "SELECT * FROM core.tax_rate_types ORDER BY tax_rate_type_code").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/tax-rate-types/{id}")]
        public MixERP.Net.Entities.Core.TaxRateType GetSingle(string id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxRateType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.TaxRateType>("SELECT * FROM core.tax_rate_types WHERE tax_rate_type_code=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/tax-rate-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.TaxRateType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxRateType), "POST");
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
        [Route("api/core/tax-rate-types/put/{id}")]
        public void Put(string id, MixERP.Net.Entities.Core.TaxRateType item)
        {
                        if (item == null || string.IsNullOrWhiteSpace(id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxRateType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.tax_rate_types", "tax_rate_type_code", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/tax-rate-types/delete/{id}")]
        public void Delete(string id)
        {
                        if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxRateType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.tax_rate_types", "tax_rate_type_code", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of TaxRateTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for tax-authorities. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TaxAuthorityController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/tax-authorities")]
        [Route("api/core/tax-authorities/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxAuthority> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxAuthority), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.TaxAuthority>(page, 10, "SELECT * FROM core.tax_authorities ORDER BY tax_authority_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/tax-authorities/{id}")]
        public MixERP.Net.Entities.Core.TaxAuthority GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxAuthority), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.TaxAuthority>("SELECT * FROM core.tax_authorities WHERE tax_authority_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/tax-authorities/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.TaxAuthority item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxAuthority), "POST");
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
        [Route("api/core/tax-authorities/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.TaxAuthority item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxAuthority), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.tax_authorities", "tax_authority_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/tax-authorities/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxAuthority), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.tax_authorities", "tax_authority_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of TaxAuthorityController Class        
    }

    
    /// <summary>
    /// A CRUD API for rounding-methods. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class RoundingMethodController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/rounding-methods")]
        [Route("api/core/rounding-methods/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.RoundingMethod> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RoundingMethod), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.RoundingMethod>(page, 10, "SELECT * FROM core.rounding_methods ORDER BY rounding_method_code").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/rounding-methods/{id}")]
        public MixERP.Net.Entities.Core.RoundingMethod GetSingle(string id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RoundingMethod), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.RoundingMethod>("SELECT * FROM core.rounding_methods WHERE rounding_method_code=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/rounding-methods/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.RoundingMethod item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RoundingMethod), "POST");
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
        [Route("api/core/rounding-methods/put/{id}")]
        public void Put(string id, MixERP.Net.Entities.Core.RoundingMethod item)
        {
                        if (item == null || string.IsNullOrWhiteSpace(id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RoundingMethod), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.rounding_methods", "rounding_method_code", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/rounding-methods/delete/{id}")]
        public void Delete(string id)
        {
                        if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RoundingMethod), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.rounding_methods", "rounding_method_code", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of RoundingMethodController Class        
    }

    
    /// <summary>
    /// A CRUD API for tax-master. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TaxMasterController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/tax-master")]
        [Route("api/core/tax-master/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxMaster), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.TaxMaster>(page, 10, "SELECT * FROM core.tax_master ORDER BY tax_master_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/tax-master/{id}")]
        public MixERP.Net.Entities.Core.TaxMaster GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxMaster), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.TaxMaster>("SELECT * FROM core.tax_master WHERE tax_master_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/tax-master/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.TaxMaster item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxMaster), "POST");
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
        [Route("api/core/tax-master/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.TaxMaster item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxMaster), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.tax_master", "tax_master_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/tax-master/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxMaster), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.tax_master", "tax_master_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of TaxMasterController Class        
    }

    
    /// <summary>
    /// A CRUD API for tax-exempt-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class TaxExemptTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/tax-exempt-types")]
        [Route("api/core/tax-exempt-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxExemptType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxExemptType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.TaxExemptType>(page, 10, "SELECT * FROM core.tax_exempt_types ORDER BY tax_exempt_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/tax-exempt-types/{id}")]
        public MixERP.Net.Entities.Core.TaxExemptType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxExemptType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.TaxExemptType>("SELECT * FROM core.tax_exempt_types WHERE tax_exempt_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/tax-exempt-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.TaxExemptType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxExemptType), "POST");
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
        [Route("api/core/tax-exempt-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.TaxExemptType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxExemptType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.tax_exempt_types", "tax_exempt_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/tax-exempt-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.TaxExemptType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.tax_exempt_types", "tax_exempt_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of TaxExemptTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for entities. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class EntityController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/entities")]
        [Route("api/core/entities/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Entity> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Entity), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Entity>(page, 10, "SELECT * FROM core.entities ORDER BY entity_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/entities/{id}")]
        public MixERP.Net.Entities.Core.Entity GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Entity), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Entity>("SELECT * FROM core.entities WHERE entity_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/entities/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Entity item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Entity), "POST");
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
        [Route("api/core/entities/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Entity item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Entity), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.entities", "entity_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/entities/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Entity), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.entities", "entity_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of EntityController Class        
    }

    
    /// <summary>
    /// A CRUD API for industries. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class IndustryController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/industries")]
        [Route("api/core/industries/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Industry> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Industry), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Industry>(page, 10, "SELECT * FROM core.industries ORDER BY industry_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/industries/{id}")]
        public MixERP.Net.Entities.Core.Industry GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Industry), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Industry>("SELECT * FROM core.industries WHERE industry_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/industries/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Industry item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Industry), "POST");
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
        [Route("api/core/industries/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Industry item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Industry), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.industries", "industry_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/industries/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Industry), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.industries", "industry_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of IndustryController Class        
    }

    
    /// <summary>
    /// A CRUD API for item-groups. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ItemGroupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/item-groups")]
        [Route("api/core/item-groups/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ItemGroup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemGroup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ItemGroup>(page, 10, "SELECT * FROM core.item_groups ORDER BY item_group_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/item-groups/{id}")]
        public MixERP.Net.Entities.Core.ItemGroup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemGroup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ItemGroup>("SELECT * FROM core.item_groups WHERE item_group_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/item-groups/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ItemGroup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemGroup), "POST");
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
        [Route("api/core/item-groups/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.ItemGroup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemGroup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.item_groups", "item_group_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/item-groups/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemGroup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.item_groups", "item_group_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ItemGroupController Class        
    }

    
    /// <summary>
    /// A CRUD API for item-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ItemTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/item-types")]
        [Route("api/core/item-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ItemType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ItemType>(page, 10, "SELECT * FROM core.item_types ORDER BY item_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/item-types/{id}")]
        public MixERP.Net.Entities.Core.ItemType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ItemType>("SELECT * FROM core.item_types WHERE item_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/item-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ItemType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemType), "POST");
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
        [Route("api/core/item-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.ItemType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.item_types", "item_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/item-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.item_types", "item_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ItemTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for brands. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class BrandController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/brands")]
        [Route("api/core/brands/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Brand> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Brand), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Brand>(page, 10, "SELECT * FROM core.brands ORDER BY brand_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/brands/{id}")]
        public MixERP.Net.Entities.Core.Brand GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Brand), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Brand>("SELECT * FROM core.brands WHERE brand_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/brands/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Brand item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Brand), "POST");
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
        [Route("api/core/brands/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Brand item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Brand), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.brands", "brand_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/brands/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Brand), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.brands", "brand_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of BrandController Class        
    }

    
    /// <summary>
    /// A CRUD API for shipping-mail-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ShippingMailTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/shipping-mail-types")]
        [Route("api/core/shipping-mail-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ShippingMailType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingMailType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ShippingMailType>(page, 10, "SELECT * FROM core.shipping_mail_types ORDER BY shipping_mail_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/shipping-mail-types/{id}")]
        public MixERP.Net.Entities.Core.ShippingMailType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingMailType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ShippingMailType>("SELECT * FROM core.shipping_mail_types WHERE shipping_mail_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/shipping-mail-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ShippingMailType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingMailType), "POST");
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
        [Route("api/core/shipping-mail-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.ShippingMailType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingMailType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.shipping_mail_types", "shipping_mail_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/shipping-mail-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingMailType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.shipping_mail_types", "shipping_mail_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ShippingMailTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for shipping-package-shapes. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ShippingPackageShapeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/shipping-package-shapes")]
        [Route("api/core/shipping-package-shapes/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ShippingPackageShape> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingPackageShape), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ShippingPackageShape>(page, 10, "SELECT * FROM core.shipping_package_shapes ORDER BY shipping_package_shape_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/shipping-package-shapes/{id}")]
        public MixERP.Net.Entities.Core.ShippingPackageShape GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingPackageShape), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ShippingPackageShape>("SELECT * FROM core.shipping_package_shapes WHERE shipping_package_shape_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/shipping-package-shapes/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ShippingPackageShape item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingPackageShape), "POST");
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
        [Route("api/core/shipping-package-shapes/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.ShippingPackageShape item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingPackageShape), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.shipping_package_shapes", "shipping_package_shape_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/shipping-package-shapes/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingPackageShape), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.shipping_package_shapes", "shipping_package_shape_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ShippingPackageShapeController Class        
    }

    
    /// <summary>
    /// A CRUD API for sales-tax-exempt-details. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalesTaxExemptDetailController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/sales-tax-exempt-details")]
        [Route("api/core/sales-tax-exempt-details/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalesTaxExemptDetail> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExemptDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalesTaxExemptDetail>(page, 10, "SELECT * FROM core.sales_tax_exempt_details ORDER BY sales_tax_exempt_detail_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/sales-tax-exempt-details/{id}")]
        public MixERP.Net.Entities.Core.SalesTaxExemptDetail GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExemptDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalesTaxExemptDetail>("SELECT * FROM core.sales_tax_exempt_details WHERE sales_tax_exempt_detail_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/sales-tax-exempt-details/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalesTaxExemptDetail item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExemptDetail), "POST");
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
        [Route("api/core/sales-tax-exempt-details/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalesTaxExemptDetail item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExemptDetail), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.sales_tax_exempt_details", "sales_tax_exempt_detail_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/sales-tax-exempt-details/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExemptDetail), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.sales_tax_exempt_details", "sales_tax_exempt_detail_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalesTaxExemptDetailController Class        
    }

    
    /// <summary>
    /// A CRUD API for compound-items. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CompoundItemController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/compound-items")]
        [Route("api/core/compound-items/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.CompoundItem> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItem), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.CompoundItem>(page, 10, "SELECT * FROM core.compound_items ORDER BY compound_item_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/compound-items/{id}")]
        public MixERP.Net.Entities.Core.CompoundItem GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItem), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.CompoundItem>("SELECT * FROM core.compound_items WHERE compound_item_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/compound-items/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.CompoundItem item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItem), "POST");
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
        [Route("api/core/compound-items/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.CompoundItem item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItem), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.compound_items", "compound_item_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/compound-items/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItem), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.compound_items", "compound_item_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CompoundItemController Class        
    }

    
    /// <summary>
    /// A CRUD API for party-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PartyTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/party-types")]
        [Route("api/core/party-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.PartyType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PartyType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.PartyType>(page, 10, "SELECT * FROM core.party_types ORDER BY party_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/party-types/{id}")]
        public MixERP.Net.Entities.Core.PartyType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PartyType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.PartyType>("SELECT * FROM core.party_types WHERE party_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/party-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.PartyType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PartyType), "POST");
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
        [Route("api/core/party-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.PartyType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PartyType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.party_types", "party_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/party-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PartyType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.party_types", "party_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of PartyTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for sales-tax-exempts. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalesTaxExemptController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/sales-tax-exempts")]
        [Route("api/core/sales-tax-exempts/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalesTaxExempt> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExempt), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalesTaxExempt>(page, 10, "SELECT * FROM core.sales_tax_exempts ORDER BY sales_tax_exempt_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/sales-tax-exempts/{id}")]
        public MixERP.Net.Entities.Core.SalesTaxExempt GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExempt), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalesTaxExempt>("SELECT * FROM core.sales_tax_exempts WHERE sales_tax_exempt_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/sales-tax-exempts/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalesTaxExempt item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExempt), "POST");
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
        [Route("api/core/sales-tax-exempts/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalesTaxExempt item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExempt), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.sales_tax_exempts", "sales_tax_exempt_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/sales-tax-exempts/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxExempt), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.sales_tax_exempts", "sales_tax_exempt_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalesTaxExemptController Class        
    }

    
    /// <summary>
    /// A CRUD API for verification-statuses. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class VerificationStatusController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/verification-statuses")]
        [Route("api/core/verification-statuses/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.VerificationStatus> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.VerificationStatus), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.VerificationStatus>(page, 10, "SELECT * FROM core.verification_statuses ORDER BY verification_status_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/verification-statuses/{id}")]
        public MixERP.Net.Entities.Core.VerificationStatus GetSingle(short id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.VerificationStatus), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.VerificationStatus>("SELECT * FROM core.verification_statuses WHERE verification_status_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/verification-statuses/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.VerificationStatus item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.VerificationStatus), "POST");
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
        [Route("api/core/verification-statuses/put/{id}")]
        public void Put(short id, MixERP.Net.Entities.Core.VerificationStatus item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.VerificationStatus), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.verification_statuses", "verification_status_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/verification-statuses/delete/{id}")]
        public void Delete(short id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.VerificationStatus), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.verification_statuses", "verification_status_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of VerificationStatusController Class        
    }

    
    /// <summary>
    /// A CRUD API for currencies. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CurrencyController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/currencies")]
        [Route("api/core/currencies/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Currency> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Currency), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Currency>(page, 10, "SELECT * FROM core.currencies ORDER BY currency_code").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/currencies/{id}")]
        public MixERP.Net.Entities.Core.Currency GetSingle(string id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Currency), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Currency>("SELECT * FROM core.currencies WHERE currency_code=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/currencies/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Currency item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Currency), "POST");
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
        [Route("api/core/currencies/put/{id}")]
        public void Put(string id, MixERP.Net.Entities.Core.Currency item)
        {
                        if (item == null || string.IsNullOrWhiteSpace(id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Currency), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.currencies", "currency_code", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/currencies/delete/{id}")]
        public void Delete(string id)
        {
                        if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Currency), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.currencies", "currency_code", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CurrencyController Class        
    }

    
    /// <summary>
    /// A CRUD API for price-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PriceTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/price-types")]
        [Route("api/core/price-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.PriceType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PriceType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.PriceType>(page, 10, "SELECT * FROM core.price_types ORDER BY price_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/price-types/{id}")]
        public MixERP.Net.Entities.Core.PriceType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PriceType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.PriceType>("SELECT * FROM core.price_types WHERE price_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/price-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.PriceType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PriceType), "POST");
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
        [Route("api/core/price-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.PriceType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PriceType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.price_types", "price_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/price-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PriceType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.price_types", "price_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of PriceTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for salespersons. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalespersonController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/salespersons")]
        [Route("api/core/salespersons/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Salesperson> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Salesperson), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Salesperson>(page, 10, "SELECT * FROM core.salespersons ORDER BY salesperson_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/salespersons/{id}")]
        public MixERP.Net.Entities.Core.Salesperson GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Salesperson), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Salesperson>("SELECT * FROM core.salespersons WHERE salesperson_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/salespersons/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Salesperson item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Salesperson), "POST");
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
        [Route("api/core/salespersons/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Salesperson item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Salesperson), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.salespersons", "salesperson_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/salespersons/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Salesperson), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.salespersons", "salesperson_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalespersonController Class        
    }

    
    /// <summary>
    /// A CRUD API for units. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class UnitController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/units")]
        [Route("api/core/units/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Unit> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Unit), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Unit>(page, 10, "SELECT * FROM core.units ORDER BY unit_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/units/{id}")]
        public MixERP.Net.Entities.Core.Unit GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Unit), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Unit>("SELECT * FROM core.units WHERE unit_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/units/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Unit item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Unit), "POST");
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
        [Route("api/core/units/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Unit item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Unit), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.units", "unit_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/units/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Unit), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.units", "unit_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of UnitController Class        
    }

    
    /// <summary>
    /// A CRUD API for sales-taxes. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalesTaxController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/sales-taxes")]
        [Route("api/core/sales-taxes/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalesTax> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTax), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalesTax>(page, 10, "SELECT * FROM core.sales_taxes ORDER BY sales_tax_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/sales-taxes/{id}")]
        public MixERP.Net.Entities.Core.SalesTax GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTax), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalesTax>("SELECT * FROM core.sales_taxes WHERE sales_tax_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/sales-taxes/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalesTax item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTax), "POST");
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
        [Route("api/core/sales-taxes/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalesTax item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTax), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.sales_taxes", "sales_tax_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/sales-taxes/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTax), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.sales_taxes", "sales_tax_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalesTaxController Class        
    }

    
    /// <summary>
    /// A CRUD API for sales-tax-details. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SalesTaxDetailController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/sales-tax-details")]
        [Route("api/core/sales-tax-details/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.SalesTaxDetail> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.SalesTaxDetail>(page, 10, "SELECT * FROM core.sales_tax_details ORDER BY sales_tax_detail_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/sales-tax-details/{id}")]
        public MixERP.Net.Entities.Core.SalesTaxDetail GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.SalesTaxDetail>("SELECT * FROM core.sales_tax_details WHERE sales_tax_detail_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/sales-tax-details/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.SalesTaxDetail item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxDetail), "POST");
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
        [Route("api/core/sales-tax-details/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.SalesTaxDetail item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxDetail), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.sales_tax_details", "sales_tax_detail_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/sales-tax-details/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.SalesTaxDetail), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.sales_tax_details", "sales_tax_detail_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of SalesTaxDetailController Class        
    }

    
    /// <summary>
    /// A CRUD API for state-sales-taxes. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class StateSalesTaxController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/state-sales-taxes")]
        [Route("api/core/state-sales-taxes/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.StateSalesTax> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.StateSalesTax), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.StateSalesTax>(page, 10, "SELECT * FROM core.state_sales_taxes ORDER BY state_sales_tax_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/state-sales-taxes/{id}")]
        public MixERP.Net.Entities.Core.StateSalesTax GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.StateSalesTax), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.StateSalesTax>("SELECT * FROM core.state_sales_taxes WHERE state_sales_tax_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/state-sales-taxes/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.StateSalesTax item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.StateSalesTax), "POST");
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
        [Route("api/core/state-sales-taxes/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.StateSalesTax item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.StateSalesTax), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.state_sales_taxes", "state_sales_tax_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/state-sales-taxes/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.StateSalesTax), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.state_sales_taxes", "state_sales_tax_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of StateSalesTaxController Class        
    }

    
    /// <summary>
    /// A CRUD API for county-sales-taxes. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CountySalesTaxController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/county-sales-taxes")]
        [Route("api/core/county-sales-taxes/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.CountySalesTax> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CountySalesTax), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.CountySalesTax>(page, 10, "SELECT * FROM core.county_sales_taxes ORDER BY county_sales_tax_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/county-sales-taxes/{id}")]
        public MixERP.Net.Entities.Core.CountySalesTax GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CountySalesTax), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.CountySalesTax>("SELECT * FROM core.county_sales_taxes WHERE county_sales_tax_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/county-sales-taxes/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.CountySalesTax item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CountySalesTax), "POST");
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
        [Route("api/core/county-sales-taxes/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.CountySalesTax item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CountySalesTax), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.county_sales_taxes", "county_sales_tax_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/county-sales-taxes/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CountySalesTax), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.county_sales_taxes", "county_sales_tax_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CountySalesTaxController Class        
    }

    
    /// <summary>
    /// A CRUD API for config. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ConfigController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/config")]
        [Route("api/core/config/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Config> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Config), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Config>(page, 10, "SELECT * FROM core.config ORDER BY config_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/config/{id}")]
        public MixERP.Net.Entities.Core.Config GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Config), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Config>("SELECT * FROM core.config WHERE config_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/config/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Config item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Config), "POST");
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
        [Route("api/core/config/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Config item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Config), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.config", "config_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/config/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Config), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.config", "config_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ConfigController Class        
    }

    
    /// <summary>
    /// A CRUD API for widgets. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class WidgetController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/widgets")]
        [Route("api/core/widgets/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Widget> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Widget), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Widget>(page, 10, "SELECT * FROM core.widgets ORDER BY widget_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/widgets/{id}")]
        public MixERP.Net.Entities.Core.Widget GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Widget), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Widget>("SELECT * FROM core.widgets WHERE widget_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/widgets/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Widget item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Widget), "POST");
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
        [Route("api/core/widgets/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Widget item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Widget), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.widgets", "widget_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/widgets/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Widget), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.widgets", "widget_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of WidgetController Class        
    }

    
    /// <summary>
    /// A CRUD API for bank-accounts. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class BankAccountController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/bank-accounts")]
        [Route("api/core/bank-accounts/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.BankAccount> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BankAccount), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.BankAccount>(page, 10, "SELECT * FROM core.bank_accounts ORDER BY account_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/bank-accounts/{id}")]
        public MixERP.Net.Entities.Core.BankAccount GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BankAccount), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.BankAccount>("SELECT * FROM core.bank_accounts WHERE account_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/bank-accounts/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.BankAccount item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BankAccount), "POST");
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
        [Route("api/core/bank-accounts/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.BankAccount item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BankAccount), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.bank_accounts", "account_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/bank-accounts/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BankAccount), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.bank_accounts", "account_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of BankAccountController Class        
    }

    
    /// <summary>
    /// A CRUD API for shippers. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ShipperController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/shippers")]
        [Route("api/core/shippers/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Shipper> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Shipper), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Shipper>(page, 10, "SELECT * FROM core.shippers ORDER BY shipper_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/shippers/{id}")]
        public MixERP.Net.Entities.Core.Shipper GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Shipper), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Shipper>("SELECT * FROM core.shippers WHERE shipper_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/shippers/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Shipper item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Shipper), "POST");
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
        [Route("api/core/shippers/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Shipper item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Shipper), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.shippers", "shipper_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/shippers/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Shipper), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.shippers", "shipper_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ShipperController Class        
    }

    
    /// <summary>
    /// A CRUD API for shipping-addresses. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ShippingAddressController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/shipping-addresses")]
        [Route("api/core/shipping-addresses/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ShippingAddress> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingAddress), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ShippingAddress>(page, 10, "SELECT * FROM core.shipping_addresses ORDER BY shipping_address_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/shipping-addresses/{id}")]
        public MixERP.Net.Entities.Core.ShippingAddress GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingAddress), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ShippingAddress>("SELECT * FROM core.shipping_addresses WHERE shipping_address_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/shipping-addresses/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ShippingAddress item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingAddress), "POST");
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
        [Route("api/core/shipping-addresses/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.ShippingAddress item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingAddress), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.shipping_addresses", "shipping_address_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/shipping-addresses/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ShippingAddress), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.shipping_addresses", "shipping_address_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ShippingAddressController Class        
    }

    
    /// <summary>
    /// A CRUD API for compound-item-details. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CompoundItemDetailController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/compound-item-details")]
        [Route("api/core/compound-item-details/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.CompoundItemDetail> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItemDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.CompoundItemDetail>(page, 10, "SELECT * FROM core.compound_item_details ORDER BY compound_item_detail_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/compound-item-details/{id}")]
        public MixERP.Net.Entities.Core.CompoundItemDetail GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItemDetail), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.CompoundItemDetail>("SELECT * FROM core.compound_item_details WHERE compound_item_detail_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/compound-item-details/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.CompoundItemDetail item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItemDetail), "POST");
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
        [Route("api/core/compound-item-details/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.CompoundItemDetail item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItemDetail), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.compound_item_details", "compound_item_detail_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/compound-item-details/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.CompoundItemDetail), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.compound_item_details", "compound_item_detail_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CompoundItemDetailController Class        
    }

    
    /// <summary>
    /// A CRUD API for item-cost-prices. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ItemCostPriceController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/item-cost-prices")]
        [Route("api/core/item-cost-prices/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ItemCostPrice> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemCostPrice), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ItemCostPrice>(page, 10, "SELECT * FROM core.item_cost_prices ORDER BY item_cost_price_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/item-cost-prices/{id}")]
        public MixERP.Net.Entities.Core.ItemCostPrice GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemCostPrice), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ItemCostPrice>("SELECT * FROM core.item_cost_prices WHERE item_cost_price_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/item-cost-prices/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ItemCostPrice item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemCostPrice), "POST");
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
        [Route("api/core/item-cost-prices/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.ItemCostPrice item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemCostPrice), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.item_cost_prices", "item_cost_price_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/item-cost-prices/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemCostPrice), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.item_cost_prices", "item_cost_price_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ItemCostPriceController Class        
    }

    
    /// <summary>
    /// A CRUD API for item-selling-prices. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ItemSellingPriceController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/item-selling-prices")]
        [Route("api/core/item-selling-prices/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.ItemSellingPrice> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemSellingPrice), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.ItemSellingPrice>(page, 10, "SELECT * FROM core.item_selling_prices ORDER BY item_selling_price_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/item-selling-prices/{id}")]
        public MixERP.Net.Entities.Core.ItemSellingPrice GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemSellingPrice), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.ItemSellingPrice>("SELECT * FROM core.item_selling_prices WHERE item_selling_price_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/item-selling-prices/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.ItemSellingPrice item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemSellingPrice), "POST");
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
        [Route("api/core/item-selling-prices/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.ItemSellingPrice item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemSellingPrice), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.item_selling_prices", "item_selling_price_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/item-selling-prices/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.ItemSellingPrice), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.item_selling_prices", "item_selling_price_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ItemSellingPriceController Class        
    }

    
    /// <summary>
    /// A CRUD API for items. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ItemController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/items")]
        [Route("api/core/items/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Item> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Item), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Item>(page, 10, "SELECT * FROM core.items ORDER BY item_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/items/{id}")]
        public MixERP.Net.Entities.Core.Item GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Item), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Item>("SELECT * FROM core.items WHERE item_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/items/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Item item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Item), "POST");
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
        [Route("api/core/items/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Item item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Item), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.items", "item_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/items/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Item), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.items", "item_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ItemController Class        
    }

    
    /// <summary>
    /// A CRUD API for recurring-invoices. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class RecurringInvoiceController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/recurring-invoices")]
        [Route("api/core/recurring-invoices/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.RecurringInvoice> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoice), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.RecurringInvoice>(page, 10, "SELECT * FROM core.recurring_invoices ORDER BY recurring_invoice_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/recurring-invoices/{id}")]
        public MixERP.Net.Entities.Core.RecurringInvoice GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoice), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.RecurringInvoice>("SELECT * FROM core.recurring_invoices WHERE recurring_invoice_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/recurring-invoices/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.RecurringInvoice item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoice), "POST");
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
        [Route("api/core/recurring-invoices/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.RecurringInvoice item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoice), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.recurring_invoices", "recurring_invoice_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/recurring-invoices/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoice), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.recurring_invoices", "recurring_invoice_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of RecurringInvoiceController Class        
    }

    
    /// <summary>
    /// A CRUD API for recurrence-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class RecurrenceTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/recurrence-types")]
        [Route("api/core/recurrence-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.RecurrenceType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurrenceType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.RecurrenceType>(page, 10, "SELECT * FROM core.recurrence_types ORDER BY recurrence_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/recurrence-types/{id}")]
        public MixERP.Net.Entities.Core.RecurrenceType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurrenceType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.RecurrenceType>("SELECT * FROM core.recurrence_types WHERE recurrence_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/recurrence-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.RecurrenceType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurrenceType), "POST");
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
        [Route("api/core/recurrence-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.RecurrenceType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurrenceType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.recurrence_types", "recurrence_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/recurrence-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurrenceType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.recurrence_types", "recurrence_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of RecurrenceTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for frequencies. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class FrequencyController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/frequencies")]
        [Route("api/core/frequencies/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Frequency> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Frequency), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Frequency>(page, 10, "SELECT * FROM core.frequencies ORDER BY frequency_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/frequencies/{id}")]
        public MixERP.Net.Entities.Core.Frequency GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Frequency), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Frequency>("SELECT * FROM core.frequencies WHERE frequency_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/frequencies/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Frequency item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Frequency), "POST");
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
        [Route("api/core/frequencies/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.Frequency item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Frequency), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.frequencies", "frequency_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/frequencies/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Frequency), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.frequencies", "frequency_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of FrequencyController Class        
    }

    
    /// <summary>
    /// A CRUD API for recurring-invoice-setup. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class RecurringInvoiceSetupController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/recurring-invoice-setup")]
        [Route("api/core/recurring-invoice-setup/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.RecurringInvoiceSetup> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoiceSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.RecurringInvoiceSetup>(page, 10, "SELECT * FROM core.recurring_invoice_setup ORDER BY recurring_invoice_setup_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/recurring-invoice-setup/{id}")]
        public MixERP.Net.Entities.Core.RecurringInvoiceSetup GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoiceSetup), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.RecurringInvoiceSetup>("SELECT * FROM core.recurring_invoice_setup WHERE recurring_invoice_setup_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/recurring-invoice-setup/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.RecurringInvoiceSetup item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoiceSetup), "POST");
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
        [Route("api/core/recurring-invoice-setup/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.RecurringInvoiceSetup item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoiceSetup), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.recurring_invoice_setup", "recurring_invoice_setup_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/recurring-invoice-setup/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.RecurringInvoiceSetup), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.recurring_invoice_setup", "recurring_invoice_setup_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of RecurringInvoiceSetupController Class        
    }

    
    /// <summary>
    /// A CRUD API for bonus-slabs. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class BonusSlabController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/bonus-slabs")]
        [Route("api/core/bonus-slabs/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.BonusSlab> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlab), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.BonusSlab>(page, 10, "SELECT * FROM core.bonus_slabs ORDER BY bonus_slab_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/bonus-slabs/{id}")]
        public MixERP.Net.Entities.Core.BonusSlab GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlab), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.BonusSlab>("SELECT * FROM core.bonus_slabs WHERE bonus_slab_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/bonus-slabs/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.BonusSlab item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlab), "POST");
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
        [Route("api/core/bonus-slabs/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.BonusSlab item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlab), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.bonus_slabs", "bonus_slab_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/bonus-slabs/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.BonusSlab), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.bonus_slabs", "bonus_slab_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of BonusSlabController Class        
    }

    
    /// <summary>
    /// A CRUD API for payment-terms. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PaymentTermController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/payment-terms")]
        [Route("api/core/payment-terms/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.PaymentTerm> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PaymentTerm), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.PaymentTerm>(page, 10, "SELECT * FROM core.payment_terms ORDER BY payment_term_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/payment-terms/{id}")]
        public MixERP.Net.Entities.Core.PaymentTerm GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PaymentTerm), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.PaymentTerm>("SELECT * FROM core.payment_terms WHERE payment_term_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/payment-terms/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.PaymentTerm item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PaymentTerm), "POST");
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
        [Route("api/core/payment-terms/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Core.PaymentTerm item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PaymentTerm), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.payment_terms", "payment_term_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/payment-terms/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.PaymentTerm), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.payment_terms", "payment_term_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of PaymentTermController Class        
    }

    
    /// <summary>
    /// A CRUD API for accounts. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class AccountController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/accounts")]
        [Route("api/core/accounts/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Account> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Account), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Account>(page, 10, "SELECT * FROM core.accounts ORDER BY account_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/accounts/{id}")]
        public MixERP.Net.Entities.Core.Account GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Account), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Account>("SELECT * FROM core.accounts WHERE account_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/accounts/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Account item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Account), "POST");
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
        [Route("api/core/accounts/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.Account item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Account), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.accounts", "account_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/accounts/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Account), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.accounts", "account_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of AccountController Class        
    }

    
    /// <summary>
    /// A CRUD API for parties. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class PartyController: MixERPApiController
    {
        [HttpGet]
        [Route("api/core/parties")]
        [Route("api/core/parties/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Core.Party> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Party), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Core.Party>(page, 10, "SELECT * FROM core.parties ORDER BY party_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/core/parties/{id}")]
        public MixERP.Net.Entities.Core.Party GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Party), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Core.Party>("SELECT * FROM core.parties WHERE party_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/core/parties/post/{item}")]
        public bool Post(MixERP.Net.Entities.Core.Party item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Party), "POST");
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
        [Route("api/core/parties/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Core.Party item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Party), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("core.parties", "party_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/core/parties/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Core.Party), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("core.parties", "party_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of PartyController Class        
    }

    
}

