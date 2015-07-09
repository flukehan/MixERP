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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class CashFlowSetup : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "cash_flow_setup_id";
                scrud.TableSchema = "core";
                scrud.Table = "cash_flow_setup";
                scrud.ViewSchema = "core";
                scrud.View = "cash_flow_setup_scrud_view";
                scrud.Text = Titles.CashFlowSetup;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.cash_flow_headings.cash_flow_heading_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "CashFlowHeadingDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.account_masters.account_master_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "AccountMasterDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.cash_flow_headings.cash_flow_heading_id",
                "core.cash_flow_heading_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.account_masters.account_master_id",
                "core.account_master_selector_view");
            return string.Join(",", displayViews);
        }
    }
}