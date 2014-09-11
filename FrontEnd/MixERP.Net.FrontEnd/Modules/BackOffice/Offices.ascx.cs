

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class Offices : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "office_id";

                scrud.TableSchema = "office";
                scrud.Table = "offices";
                scrud.ViewSchema = "office";
                scrud.View = "offices";

                scrud.Width = 4000;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.OfficeSetup;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id", ConfigurationHelper.GetDbParameter("OfficeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.currencies.currency_code", ConfigurationHelper.GetDbParameter("CurrencyDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_view");
            ScrudHelper.AddDisplayView(displayViews, "core.currencies.currency_code", "core.currencies");
            return string.Join(",", displayViews);
        }
    }
}