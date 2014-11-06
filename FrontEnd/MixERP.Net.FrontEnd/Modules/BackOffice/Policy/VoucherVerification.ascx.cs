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
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MixERP.Net.Core.Modules.BackOffice.Policy
{
    public partial class VoucherVerification : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.DenyAdd = !SessionHelper.IsAdmin();
                scrud.DenyEdit = !SessionHelper.IsAdmin();
                scrud.DenyDelete = !SessionHelper.IsAdmin();

                scrud.KeyColumn = "user_id";

                scrud.TableSchema = "policy";
                scrud.Table = "voucher_verification_policy";
                scrud.ViewSchema = "policy";
                scrud.View = "voucher_verification_policy_scrud_view";

                scrud.PageSize = 100;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.VoucherVerificationPolicy;
                scrud.ResourceAssembly = Assembly.GetAssembly(typeof(VoucherVerification));

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.users.user_id",
                ConfigurationHelper.GetDbParameter("UserDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.users.user_id", "office.user_selector_view");
            return string.Join(",", displayViews);
        }
    }
}