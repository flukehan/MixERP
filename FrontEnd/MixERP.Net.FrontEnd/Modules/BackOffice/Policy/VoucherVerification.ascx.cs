using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

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
                scrud.Table = "auto_verification_policy";
                scrud.ViewSchema = "policy";
                scrud.View = "auto_verification_policy_view";

                scrud.PageSize = 100;
                scrud.Width = 2000;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.AutoVerificationPolicy;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.users.user_id", ConfigurationHelper.GetDbParameter("UserDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.users.user_id", "office.user_view");
            return string.Join(",", displayViews);
        }
    }
}