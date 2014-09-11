

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class TaxSetup : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "tax_id";

                scrud.TableSchema = "core";
                scrud.Table = "taxes";
                scrud.ViewSchema = "core";
                scrud.View = "tax_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.SelectedValues = GetSelectedValues();

                scrud.Text = Titles.TaxSetup;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.tax_types.tax_type_id", ConfigurationHelper.GetDbParameter("TaxTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.tax_types.tax_type_id", "core.tax_types");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            return string.Join(",", displayViews);
        }

        private static string GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();

            //Todo:
            //The default selected value of tax account
            //should be implemented via GL Mapping.
            ScrudHelper.AddSelectedValue(selectedValues, "core.accounts.account_id", "'20700 (Tax Payables)'");
            return string.Join(",", selectedValues);
        }
    }
}