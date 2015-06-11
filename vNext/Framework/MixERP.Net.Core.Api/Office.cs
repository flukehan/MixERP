
using MixERP.Net.Entities;
using PetaPoco;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

// ReSharper Disable All 
namespace MixERP.Net.Core.Api.Office
{
    /// <summary>
    /// A CRUD API for departments. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class DepartmentController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/departments")]
        [Route("api/office/departments/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Department> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Department), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Department>(page, 10, "SELECT * FROM office.departments ORDER BY department_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/departments/{id}")]
        public MixERP.Net.Entities.Office.Department GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Department), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Department>("SELECT * FROM office.departments WHERE department_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/departments/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Department item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Department), "POST");
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
        [Route("api/office/departments/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.Department item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Department), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.departments", "department_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/departments/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Department), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.departments", "department_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of DepartmentController Class        
    }

    
    /// <summary>
    /// A CRUD API for roles. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class RoleController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/roles")]
        [Route("api/office/roles/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Role> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Role), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Role>(page, 10, "SELECT * FROM office.roles ORDER BY role_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/roles/{id}")]
        public MixERP.Net.Entities.Office.Role GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Role), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Role>("SELECT * FROM office.roles WHERE role_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/roles/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Role item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Role), "POST");
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
        [Route("api/office/roles/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.Role item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Role), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.roles", "role_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/roles/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Role), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.roles", "role_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of RoleController Class        
    }

    
    /// <summary>
    /// A CRUD API for store-types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class StoreTypeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/store-types")]
        [Route("api/office/store-types/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.StoreType> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.StoreType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.StoreType>(page, 10, "SELECT * FROM office.store_types ORDER BY store_type_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/store-types/{id}")]
        public MixERP.Net.Entities.Office.StoreType GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.StoreType), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.StoreType>("SELECT * FROM office.store_types WHERE store_type_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/store-types/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.StoreType item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.StoreType), "POST");
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
        [Route("api/office/store-types/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.StoreType item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.StoreType), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.store_types", "store_type_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/store-types/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.StoreType), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.store_types", "store_type_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of StoreTypeController Class        
    }

    
    /// <summary>
    /// A CRUD API for counters. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CounterController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/counters")]
        [Route("api/office/counters/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Counter> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Counter), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Counter>(page, 10, "SELECT * FROM office.counters ORDER BY counter_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/counters/{id}")]
        public MixERP.Net.Entities.Office.Counter GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Counter), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Counter>("SELECT * FROM office.counters WHERE counter_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/counters/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Counter item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Counter), "POST");
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
        [Route("api/office/counters/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.Counter item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Counter), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.counters", "counter_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/counters/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Counter), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.counters", "counter_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CounterController Class        
    }

    
    /// <summary>
    /// A CRUD API for cashiers. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CashierController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/cashiers")]
        [Route("api/office/cashiers/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Cashier> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Cashier), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Cashier>(page, 10, "SELECT * FROM office.cashiers ORDER BY cashier_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/cashiers/{id}")]
        public MixERP.Net.Entities.Office.Cashier GetSingle(long id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Cashier), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Cashier>("SELECT * FROM office.cashiers WHERE cashier_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/cashiers/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Cashier item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Cashier), "POST");
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
        [Route("api/office/cashiers/put/{id}")]
        public void Put(long id, MixERP.Net.Entities.Office.Cashier item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Cashier), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.cashiers", "cashier_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/cashiers/delete/{id}")]
        public void Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Cashier), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.cashiers", "cashier_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CashierController Class        
    }

    
    /// <summary>
    /// A CRUD API for cost-centers. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CostCenterController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/cost-centers")]
        [Route("api/office/cost-centers/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.CostCenter> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CostCenter), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.CostCenter>(page, 10, "SELECT * FROM office.cost_centers ORDER BY cost_center_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/cost-centers/{id}")]
        public MixERP.Net.Entities.Office.CostCenter GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CostCenter), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.CostCenter>("SELECT * FROM office.cost_centers WHERE cost_center_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/cost-centers/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.CostCenter item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CostCenter), "POST");
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
        [Route("api/office/cost-centers/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.CostCenter item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CostCenter), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.cost_centers", "cost_center_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/cost-centers/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CostCenter), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.cost_centers", "cost_center_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CostCenterController Class        
    }

    
    /// <summary>
    /// A CRUD API for cash-repositories. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class CashRepositoryController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/cash-repositories")]
        [Route("api/office/cash-repositories/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.CashRepository> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CashRepository), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.CashRepository>(page, 10, "SELECT * FROM office.cash_repositories ORDER BY cash_repository_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/cash-repositories/{id}")]
        public MixERP.Net.Entities.Office.CashRepository GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CashRepository), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.CashRepository>("SELECT * FROM office.cash_repositories WHERE cash_repository_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/cash-repositories/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.CashRepository item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CashRepository), "POST");
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
        [Route("api/office/cash-repositories/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.CashRepository item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CashRepository), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.cash_repositories", "cash_repository_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/cash-repositories/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.CashRepository), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.cash_repositories", "cash_repository_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of CashRepositoryController Class        
    }

    
    /// <summary>
    /// A CRUD API for work-centers. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class WorkCenterController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/work-centers")]
        [Route("api/office/work-centers/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.WorkCenter> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.WorkCenter), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.WorkCenter>(page, 10, "SELECT * FROM office.work_centers ORDER BY work_center_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/work-centers/{id}")]
        public MixERP.Net.Entities.Office.WorkCenter GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.WorkCenter), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.WorkCenter>("SELECT * FROM office.work_centers WHERE work_center_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/work-centers/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.WorkCenter item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.WorkCenter), "POST");
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
        [Route("api/office/work-centers/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.WorkCenter item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.WorkCenter), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.work_centers", "work_center_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/work-centers/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.WorkCenter), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.work_centers", "work_center_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of WorkCenterController Class        
    }

    
    /// <summary>
    /// A CRUD API for configuration. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ConfigurationController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/configuration")]
        [Route("api/office/configuration/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Configuration> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Configuration), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Configuration>(page, 10, "SELECT * FROM office.configuration ORDER BY configuration_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/configuration/{id}")]
        public MixERP.Net.Entities.Office.Configuration GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Configuration), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Configuration>("SELECT * FROM office.configuration WHERE configuration_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/configuration/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Configuration item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Configuration), "POST");
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
        [Route("api/office/configuration/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.Configuration item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Configuration), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.configuration", "configuration_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/configuration/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Configuration), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.configuration", "configuration_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of ConfigurationController Class        
    }

    
    /// <summary>
    /// A CRUD API for stores. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class StoreController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/stores")]
        [Route("api/office/stores/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Store> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Store), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Store>(page, 10, "SELECT * FROM office.stores ORDER BY store_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/stores/{id}")]
        public MixERP.Net.Entities.Office.Store GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Store), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Store>("SELECT * FROM office.stores WHERE store_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/stores/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Store item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Store), "POST");
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
        [Route("api/office/stores/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.Store item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Store), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.stores", "store_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/stores/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Store), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.stores", "store_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of StoreController Class        
    }

    
    /// <summary>
    /// A CRUD API for offices. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class OfficeController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/offices")]
        [Route("api/office/offices/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.Office> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Office), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.Office>(page, 10, "SELECT * FROM office.offices ORDER BY office_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/offices/{id}")]
        public MixERP.Net.Entities.Office.Office GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Office), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.Office>("SELECT * FROM office.offices WHERE office_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/offices/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.Office item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Office), "POST");
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
        [Route("api/office/offices/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.Office item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Office), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.offices", "office_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/offices/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.Office), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.offices", "office_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of OfficeController Class        
    }

    
    /// <summary>
    /// A CRUD API for users. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class UserController: MixERPApiController
    {
        [HttpGet]
        [Route("api/office/users")]
        [Route("api/office/users/page/{page:long}")]
        public IEnumerable<MixERP.Net.Entities.Office.User> GetPagedResult(long page=1)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.User), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.Page<MixERP.Net.Entities.Office.User>(page, 10, "SELECT * FROM office.users ORDER BY user_id").Items;
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/office/users/{id}")]
        public MixERP.Net.Entities.Office.User GetSingle(int id)
        {
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.User), "GET");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    return db.FirstOrDefault<MixERP.Net.Entities.Office.User>("SELECT * FROM office.users WHERE user_id=@0", id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("api/office/users/post/{item}")]
        public bool Post(MixERP.Net.Entities.Office.User item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.User), "POST");
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
        [Route("api/office/users/put/{id}")]
        public void Put(int id, MixERP.Net.Entities.Office.User item)
        {
            if (item == null || id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
                        
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.User), "PUT");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Update("office.users", "user_id", item);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("api/office/users/delete/{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            
            ApiAccessPolicy policy = new ApiAccessPolicy(typeof(MixERP.Net.Entities.Office.User), "DELETE");
            policy.Authorize();

            if (!policy.IsAuthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            try
            {
                using (Database db = new Database(Factory.GetConnectionString(), "Npgsql"))
                {
                    db.Delete("office.users", "user_id", null, id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        //End of UserController Class        
    }

    
}

