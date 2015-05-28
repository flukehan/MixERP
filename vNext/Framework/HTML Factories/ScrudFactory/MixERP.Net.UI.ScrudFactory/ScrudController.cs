using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory.Models;

namespace MixERP.Net.UI.ScrudFactory
{
    public abstract class ScrudController : Controller
    {
        public abstract Config GetConfig();

        [HttpPost]
        public JsonResult Add(Field[] fields)
        {
            const int userId = 2;
            FormHandler.Insert(fields, userId, this.GetConfig());
            return Json("OK", JsonRequestBehavior.DenyGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public JsonResult Update(object id, Field[] fields)
        {
            const int userId = 2;
            FormHandler.Update(id, fields, userId, this.GetConfig());
            return Json("OK", JsonRequestBehavior.DenyGet);
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Content("OK");
        }
    }
}