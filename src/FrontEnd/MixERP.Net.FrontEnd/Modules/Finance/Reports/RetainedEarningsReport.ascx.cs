using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class RetainedEarningsReport : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {

            DateTime date = Conversion.TryCastDate(this.Page.Request["Date"]);
            decimal factor = Conversion.TryCastDecimal(this.Page.Request["Factor"]);

            int officeId = AppUsers.GetCurrentLogin().View.OfficeId.ToInt();


            Collection<KeyValuePair<string, object>> parameter1 = new Collection<KeyValuePair<string, object>>();
            parameter1.Add(new KeyValuePair<string, object>("@Date", date));
            parameter1.Add(new KeyValuePair<string, object>("@Factor", factor.ToString(CultureInfo.InvariantCulture)));
            parameter1.Add(new KeyValuePair<string, object>("@OfficeId", officeId.ToString(CultureInfo.InvariantCulture)));

            using (WebReport report = new WebReport())
            {
                report.AddParameterToCollection(parameter1);
                report.RunningTotalText = Titles.RunningTotal;
                report.Path = "~/Modules/Finance/Reports/Source/Transactions.RetainedEarnings.xml";
                report.AutoInitialize = true;

                this.Placeholder1.Controls.Add(report);
            }
        }
    }
}