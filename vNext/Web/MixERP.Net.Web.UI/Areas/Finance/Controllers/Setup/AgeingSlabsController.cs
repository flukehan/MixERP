//Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

//This file is part of MixERP.

//MixERP is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//MixERP is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with MixERP.  If not, see <http://www.gnu.org/licenses/>.

using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Finance.Resources;
using MixERP.Net.Web.UI.Providers;

namespace MixERP.Net.Web.UI.Finance.Controllers.Setup
{
    [RouteArea("Finance")]
    [RoutePrefix("setup/ageing-slabs")]
    [Route("{action=index}")]
    public class AgeingSlabsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Finance/Views/Setup/AgeingSlabs.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.Text = Titles.AgeingSlabs;

            config.TableSchema = "core";
            config.Table = "ageing_slabs";
            config.ViewSchema = "core";
            config.View = "ageing_slab_scrud_view";

            config.KeyColumn = "ageing_slab_id";

            config.ResourceAssembly = typeof(AgeingSlabsController).Assembly;
            this.AddScrudCustomValidatorErrorMessages();

            return config;
        }

        private void AddScrudCustomValidatorErrorMessages()
        {
            ViewData["CompareDaysErrorMessage"] = Warnings.CompareDaysErrorMessage;
        }


    }
}