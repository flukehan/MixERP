using System.Collections.ObjectModel;
using System.Text;
using System.Web.Mvc;
using MixERP.Net.UI.ScrudFactory.Layout;
using MixERP.Net.Common.Helpers;
using System;
using System.Globalization;

namespace MixERP.Net.UI.ScrudFactory
{
    public static class View
    {
        public static MvcHtmlString ScrudFor(this HtmlHelper helper, Config config)
        {
            helper.ViewBag.Title = config.Text;
            return GetScrud(config);
        }

        public static MvcHtmlString GetScrud(Config config)
        {
            StringBuilder scrud = new StringBuilder();

            foreach (ILayout layout in GetLayouts(config))
            {
                scrud.Append(layout.Get());
            }

            scrud.Append(GetJavascript(config));

            return new MvcHtmlString(scrud.ToString());
        }

        private static string GetJavascript(Config config)
        {
            string script = "<script type='text/javascript'>";
            script += JSUtility.GetVar("scrudAreYouSureLocalized", Resources.Titles.AreYouSure);
            script += JSUtility.GetVar("scrudNothingSelectedLocalized", Resources.Titles.NothingSelected);
            script += JSUtility.GetVar("requiredLocalized", Resources.Titles.RequiredField);
            script += JSUtility.GetVar("invalidNumberLocalized", Resources.Titles.OnlyNumbersAllowed);
            script += JSUtility.GetVar("reportTemplatePath", ConfigurationHelper.GetScrudParameter("TemplatePath"));
            script += JSUtility.GetVar("reportHeaderPath", ConfigurationHelper.GetScrudParameter("HeaderPath"));
            script += JSUtility.GetVar("date", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            script += JSUtility.GetVar("customFormUrl", config.CustomFormUrl);
            script += JSUtility.GetVar("keyColumn", config.KeyColumn);
            script += "</script>";

            return script;
        }

        private static Collection<ILayout> GetLayouts(Config config)
        {
            Collection<ILayout> layouts = new Collection<ILayout>
            {
                new Title(config),
                new Divider(config),
                new Description(config),
                new CommandPanel(config),
                new GridPanel(config),
                new ScrudForm(config),
                new CommandPanel(config),
                new HiddenFields(config)
            };

            return layouts;
        }
    }
}