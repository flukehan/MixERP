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

using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Sales.Resources;
using MixERP.Net.Web.UI.Providers;
using System.Collections.Generic;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Web.UI.Sales.Controllers.Setup
{
    [RouteArea("Sales")]
    [RoutePrefix("setup/payment-terms")]
    [Route("{action=index}")]
    public class PaymentTermsController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Sales/Views/Setup/PaymentTerms.cshtml";

            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();
            {
                config.KeyColumn = "payment_term_id";
                config.TableSchema = "core";
                config.Table = "payment_terms";
                config.ViewSchema = "core";
                config.View = "payment_term_scrud_view";
                config.Text = Titles.PaymentTerms;

                config.DisplayFields = GetDisplayFields();
                config.DisplayViews = GetDisplayViews();

                config.ResourceAssembly = typeof(PaymentTermsController).Assembly;

                this.AddScrudCustomValidatorErrorMessages();

                return config;
            }
        }

        private void AddScrudCustomValidatorErrorMessages()
        {
            ViewData["DueFrequencyErrorMessage"] = Warnings.DueFrequencyErrorMessage;
            ViewData["LateFeeErrorMessage"] = Warnings.LateFeeErrorMessage;
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id", ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.late_fee.late_fee_id", ConfigurationHelper.GetDbParameter("LateFeeDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequencies");
            ScrudHelper.AddDisplayView(displayViews, "core.late_fee.late_fee_id", "core.late_fee_scrud_view");
            return string.Join(",", displayViews);
        }
    }
}