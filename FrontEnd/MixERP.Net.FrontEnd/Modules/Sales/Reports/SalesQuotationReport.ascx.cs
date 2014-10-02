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

using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ReportEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MixERP.Net.Core.Modules.Sales.Reports
{
    public partial class SalesQuotationReport : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            Collection<KeyValuePair<string, string>> list = new Collection<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@non_gl_stock_master_id", this.Page.Request["TranId"]));

            using (Report report = new Report())
            {
                report.AddParameterToCollection(list);
                report.AddParameterToCollection(list);
                report.AutoInitialize = true;
                report.ResourceAssembly = Assembly.GetAssembly(typeof(SalesQuotationReport));
                report.Path = "~/Modules/Sales/Reports/Source/Sales.Quotation.xml";

                this.Controls.Add(report);
            }

            base.OnControlLoad(sender, e);
        }
    }
}