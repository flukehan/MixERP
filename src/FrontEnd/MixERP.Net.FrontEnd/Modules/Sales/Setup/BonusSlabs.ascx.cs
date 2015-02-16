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

using System;
using System.Collections.Generic;
using System.Reflection;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;

namespace MixERP.Net.Core.Modules.Sales.Setup
{
    public partial class BonusSlabs : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "bonus_slab_id";

                scrud.TableSchema = "core";
                scrud.Table = "bonus_slabs";

                scrud.ViewSchema = "core";
                scrud.View = "bonus_slab_scrud_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.Text = Titles.AgentBonusSlabs;
                scrud.ResourceAssembly = Assembly.GetAssembly(typeof (BonusSlabs));

                this.AddScrudCustomValidatorErrorMessages();

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private void AddScrudCustomValidatorErrorMessages()
        {
            string javascript = JSUtility.GetVar("dateErrorMessageLocalized", Warnings.DateErrorMessage);

            Common.PageUtility.RegisterJavascript("SalesPerson_ScrudCustomValidatorMessages", javascript, this.Page, true);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id",
                ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequency_selector_view");
            return string.Join(",", displayViews);
        }
    }
}