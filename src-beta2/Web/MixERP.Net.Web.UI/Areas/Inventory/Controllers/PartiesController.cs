using System.Collections.Generic;
using System.Web.Mvc;
using MixERP.Net.Common.Helpers;
using MixERP.Net.UI.ScrudFactory;

namespace MixERP.Net.Web.UI.Inventory.Controllers
{
    public class PartiesController : ScrudController
    {
        //
        // GET: /Parties/

        public ActionResult Index()
        {
            return View(this.GetConfig());
        }

        public override Config GetConfig()
        {
            return new Config
            {
                Text = "Parties",
                TableSchema = "core",
                Table = "parties",
                ViewSchema = "core",
                View = "party_scrud_view",
                OfficeId = 2,
                UserId = 2,
                UserName = "binod",
                OfficeCode = "MOF",
                KeyColumn = "party_id",
                Description = "Parties collectively refer to suppliers, customers, agents, and dealers.",
                ResourceAssembly = typeof(PartiesController).Assembly,
                DisplayFields = GetDisplayFields(),
                DisplayViews = GetDisplayViews(),
                //Party code and account_id will be automatically generated on the database.
                Exclude = "party_code, account_id"
            };
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.party_types.party_type_id",
                ConfigurationHelper.GetDbParameter("PartyTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id",
                ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.currencies.currency_code",
                ConfigurationHelper.GetDbParameter("CurrencyDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.countries.country_id",
                ConfigurationHelper.GetDbParameter("CountryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.states.state_id",
                ConfigurationHelper.GetDbParameter("StateDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.entities.entity_id",
                ConfigurationHelper.GetDbParameter("EntityDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.industries.industry_id",
                ConfigurationHelper.GetDbParameter("IndustryDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.party_types.party_type_id", "core.party_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequency_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.currencies.currency_code", "core.currency_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.countries.country_id", "core.country_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.states.state_id", "core.state_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.entities.entity_id", "core.entity_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.industries.industry_id", "core.industry_scrud_view");

            return string.Join(",", displayViews);
        }
    }
}