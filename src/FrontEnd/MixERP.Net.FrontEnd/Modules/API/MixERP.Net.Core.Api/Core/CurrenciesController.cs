using System.Collections.Generic;
using System.Web.Http;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;
using PetaPoco;

namespace MixERP.Net.Core.Api.Core
{
    public sealed class CurrenciesController : MixERPApiController
    {
        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(Currency), new HttpGetAttribute());
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                return null;
            }

            using (Database db = new Database(Factory.GetConnectionString()))
            {
                return db.Page<Currency>(this.Page(), 10, "SELECT * FROM core.currencies ORDER BY currency_code").Items;
            }
        }

        [HttpGet]
        public Currency Get(string id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(Currency), new HttpGetAttribute());
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                return null;
            }

            using (Database db = new Database(Factory.GetConnectionString()))
            {
                return db.SingleOrDefault<Currency>("SELECT * FROM core.currencies WHERE currency_code=@0", id);
            }
        }

        [HttpPost]
        public bool Post(Currency currency)
        {
            if (currency == null)
            {
                return false;
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(Currency), new HttpGetAttribute());
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                return false;
            }

            using (Database db = new Database(Factory.GetConnectionString()))
            {
                db.Insert(currency);
            }

            return true;
        }

        [HttpPut]
        public void Put(string id, Currency currency)
        {
            if (currency == null || string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(Currency), new HttpGetAttribute());
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                return;
            }

            using (Database db = new Database(Factory.GetConnectionString()))
            {
                db.Update("core.currencies", "currency_code", currency);
            }
        }

        [HttpDelete]
        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(Currency), new HttpGetAttribute());
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                return;
            }

            using (Database db = new Database(Factory.GetConnectionString()))
            {
                db.Delete("core.currencies", "currency_code", null, id);
            }
        }
    }
}