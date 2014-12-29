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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ReportEngine;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class AccountStatementReport : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            string accountNumber = this.Page.Request["AccountNumber"];
            DateTime from = Conversion.TryCastDate(this.Page.Request["From"]);
            DateTime to = Conversion.TryCastDate(this.Page.Request["To"]);

            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();


            Collection<KeyValuePair<string, string>> parameter1 = new Collection<KeyValuePair<string, string>>();
            parameter1.Add(new KeyValuePair<string, string>("@OfficeId", officeId.ToString(CultureInfo.InvariantCulture)));
            parameter1.Add(new KeyValuePair<string, string>("@AccountNumber", accountNumber));

            Collection<KeyValuePair<string, string>> parameter2 = new Collection<KeyValuePair<string, string>>();
            parameter2.Add(new KeyValuePair<string, string>("@From", from.ToString(SessionHelper.GetCulture())));
            parameter2.Add(new KeyValuePair<string, string>("@To", to.ToString(SessionHelper.GetCulture())));
            parameter2.Add(new KeyValuePair<string, string>("@UserId", userId.ToString(CultureInfo.InvariantCulture)));
            parameter2.Add(new KeyValuePair<string, string>("@AccountNumber", accountNumber));
            parameter2.Add(new KeyValuePair<string, string>("@OfficeId", officeId.ToString(CultureInfo.InvariantCulture)));

            using (Report report = new Report())
            {
                report.AddParameterToCollection(parameter1);
                report.AddParameterToCollection(parameter2);
                report.RunningTotalText = Titles.RunningTotal;
                report.ResourceAssembly = Assembly.GetAssembly(typeof(GLAdviceReport));
                report.Path = "~/Modules/Finance/Reports/Source/Transactions.AccountStatement.xml";
                report.AutoInitialize = true;

                this.Placeholder1.Controls.Add(report);
            }

            base.OnControlLoad(sender, e);
        }
    }
}