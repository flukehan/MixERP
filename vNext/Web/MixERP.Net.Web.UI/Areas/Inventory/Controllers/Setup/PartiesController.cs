using System.Collections.Generic;
using System.Web.Mvc;
using MixERP.Net.Common.Helpers;
using MixERP.Net.UI.ScrudFactory;
using MixERP.Net.Web.UI.Providers;

namespace MixERP.Net.Web.UI.Inventory.Controllers.Setup
{
    [RouteArea("Inventory")]
    [RoutePrefix("setup/parties")]
    [Route("{action=index}")]
    public class PartiesController : ScrudController
    {
        public ActionResult Index()
        {
            const string view = "~/Areas/Inventory/Views/Setup/Parties.cshtml";
            return View(view, this.GetConfig());
        }

        public override Config GetConfig()
        {
            Config config = ScrudProvider.GetScrudConfig();

            config.Text = Resources.Titles.Parties;
            config.Description = Resources.Labels.PartyDescription;

            config.TableSchema = "core";
            config.Table = "parties";
            config.ViewSchema = "core";
            config.View = "party_scrud_view";

            config.KeyColumn = "party_id";

            //Party code and account_id will be automatically generated on the database.
            config.Exclude = "party_code, account_id";

            config.ResourceAssembly = typeof(PartiesController).Assembly;
            config.DisplayFields = GetDisplayFields();
            config.DisplayViews = GetDisplayViews();

            return config;
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