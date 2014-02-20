using System;
using System.Collections.Generic;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Setup
{
    public partial class Frequency : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "frequency_setup_id";

                scrud.TableSchema = "core";
                scrud.Table = "frequency_setups";
                scrud.ViewSchema = "core";
                scrud.View = "frequency_setups";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.Frequencies;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id", ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.fiscal_year.fiscal_year_code", ConfigurationHelper.GetDbParameter("FiscalYearDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequencies");
            ScrudHelper.AddDisplayView(displayViews, "core.fiscal_year.fiscal_year_code", "core.fiscal_year");
            return string.Join(",", displayViews);
        }
    }
}