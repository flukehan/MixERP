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

using MixERP.Net.Common.Helpers;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Inventory.Resources;
using MixERP.Net.Web.UI.Providers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MixERP.Net.Web.UI.Inventory.Controllers.Setup
{
    [RouteArea("Inventory")]
    [RoutePrefix("setup/compound-items")]
    [Route("{action=index}")]
    public class CompoundItemsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Inventory/Views/Setup/CompoundItems.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.KeyColumn = "compound_item_id";

            config.TableSchema = "core";
            config.Table = "compound_items";
            config.ViewSchema = "core";
            config.View = "compound_item_scrud_view";

            config.Text = Titles.CompoundItems;
            config.ResourceAssembly = typeof(CompoundItemsController).Assembly;

            return config;
        }
    }
}