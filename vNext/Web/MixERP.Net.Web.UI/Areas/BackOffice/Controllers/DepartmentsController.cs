/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Providers;
using System.Reflection;
using System.Web.Mvc;
using MixERP.Net.Web.UI.BackOffice.Resources;

namespace MixERP.Net.Web.UI.BackOffice.Controllers
{
    [RouteArea("BackOffice", AreaPrefix = "back-office")]
    [RoutePrefix("departments")]
    [Route("{action=index}")]

    public class DepartmentsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/BackOffice/Views/Departments.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.KeyColumn = "department_id";

                config.TableSchema = "office";
                config.Table = "departments";
                config.ViewSchema = "office";
                config.View = "department_scrud_view";

                config.Text = Titles.Departments;
                config.ResourceAssembly = Assembly.GetAssembly(typeof(DepartmentsController));
                
                return config;
            }
        }
    }
}