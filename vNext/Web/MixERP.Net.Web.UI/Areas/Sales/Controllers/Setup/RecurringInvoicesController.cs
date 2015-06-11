/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).
This file is part of MixERP.
MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with MixERP. If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Sales.Resources;
using MixERP.Net.Web.UI.Providers;
using MixERP.Net.Common.Helpers;
using System.Collections.Generic;
using System.Reflection;
namespace MixERP.Net.Web.UI.Sales.Controllers.Setup
{
    [RouteArea("Sales")]
    [RoutePrefix("setup/recurring-invoices")]
    [Route("{action=index}")]
    public class RecurringInvoicesController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Sales/Views/Setup/RecurringInvoices.cshtml";

            return View(view, this.GetConfig());
        }


        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.KeyColumn = "recurring_invoice_id";
            config.TableSchema = "core";
            config.Table = "recurring_invoices";
            config.ViewSchema = "core";
            config.View = "recurring_invoice_scrud_view";
            config.Text = Titles.RecurringInvoices;
            config.DisplayFields = GetDisplayFields();
            config.DisplayViews = GetDisplayViews();
            config.ResourceAssembly = Assembly.GetAssembly(typeof(RecurringInvoicesController));

            this.AddScrudCustomValidatorErrorMessages();

            return config;
        }


        private void AddScrudCustomValidatorErrorMessages()
        {
            ViewData["ItemErrorMessage"] = Warnings.ItemErrorMessage;
            ViewData["RecurringAmountErrorMessage"] = Warnings.RecurringAmountErrorMessage;
        }
        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.recurrence_types.recurrence_type_id", ConfigurationHelper.GetDbParameter("RecurrenceTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id", ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.items.item_id", ConfigurationHelper.GetDbParameter("ItemDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.payment_terms.payment_term_id", ConfigurationHelper.GetDbParameter("PaymentTermDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }
        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.recurrence_types.recurrence_type_id", "core.recurrence_types");//Todo:Change to scrud view
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequencies");
            ScrudHelper.AddDisplayView(displayViews, "core.items.item_id", "core.item_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.payment_terms.payment_term_id", "core.payment_term_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_scrud_view");
            return string.Join(",", displayViews);
        }
    }
}