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
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MixERP.Net.Core.Modules.Sales.Setup
{
    public partial class RecurringInvoiceSetup : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "recurring_invoice_setup_id";
                scrud.TableSchema = "core";
                scrud.Table = "recurring_invoice_setup";
                scrud.ViewSchema = "core";
                scrud.View = "recurring_invoice_setup_scrud_view";
                scrud.Text = Titles.RecurringInvoiceSetup;

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.ResourceAssembly = Assembly.GetAssembly(typeof(RecurringInvoiceSetup));
                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()

        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.recurring_invoices.recurring_invoice_id", ConfigurationHelper.GetDbParameter("RecurringInvoiceDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.parties.party_id", ConfigurationHelper.GetDbParameter("PartyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.payment_terms.payment_term_id", ConfigurationHelper.GetDbParameter("PaymentTermDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.recurring_invoices.recurring_invoice_id", "core.recurring_invoices");
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.parties");
            ScrudHelper.AddDisplayView(displayViews, "core.payment_terms.payment_term_id", "core.payment_terms");
            return string.Join(",", displayViews);
        }
    }
}