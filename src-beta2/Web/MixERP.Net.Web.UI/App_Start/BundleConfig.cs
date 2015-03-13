using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MixERP.Net.Web.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Maybe that was just me, but I could not create a bundle at all due to minification failed errors.


            //Sticking with Web Essentials since it seems to be a far better choice.


            //(+1) What I like about Web Essentials is that it 
            //displays an error message if a file was moved or deleted
            //instead of silently creating a bundle file.



            //Collection<string> libraries = new Collection<string>
            //{
            //    //Libraries
            //    "~/Scripts/jquery-1.9.1.js",
            //    "~/Scripts/jquery-ui.js", 
            //    "~/Scripts/jquery.address.js", 
            //    "~/Scripts/shortcut.js", 
            //    "~/Scripts/colorbox/jquery.colorbox.js", 
            //    "~/Scripts/jqueryNumber/jquery.number.js", 
            //    "~/Scripts/date.js", 
            //    "~/Scripts/sprintf/sprintf.min.js",
            //    "~/Scripts/chartjs/Chart.min.js", 
            //    "~/Scripts/chartjs/legend.js", 
            //    "~/Scripts/notify-combined.min.js", 
            //    "~/Scripts/semantic-ui/semantic.js", 
            //    "~/Scripts/jquery.signalR.js", 
            //    "~/Scripts/vakata-jstree/dist/jstree.min.js", 
            //};

            //Collection<string> mixerp = new Collection<string>
            //{
            //    "~/Scripts/mixerp/core/dom/loader.js",
            //    "~/Scripts/mixerp/core/conversion.js",
            //    "~/Scripts/mixerp/core/date-expressions.js",
            //    "~/Scripts/mixerp/core/flag.js",
            //    "~/Scripts/mixerp/core/json.js",
            //    "~/Scripts/mixerp/core/localization.js",
            //    "~/Scripts/mixerp/core/location.js",
            //    "~/Scripts/mixerp/core/mixerp-ajax.js",
            //    "~/Scripts/mixerp/core/transaction.js",
            //    "~/Scripts/mixerp/core/validation.js",
            //    "~/Scripts/mixerp/core/window.js",
            //    "~/Scripts/mixerp/core/dom/cascading-pair.js",
            //    "~/Scripts/mixerp/core/dom/checkable.js",
            //    "~/Scripts/mixerp/core/dom/document.js",
            //    "~/Scripts/mixerp/core/dom/events.js",
            //    "~/Scripts/mixerp/core/dom/popunder.js",
            //    "~/Scripts/mixerp/core/dom/select.js",
            //    "~/Scripts/mixerp/core/dom/visibility.js",
            //    "~/Scripts/mixerp/core/grid/cell.js",
            //    "~/Scripts/mixerp/core/grid/grid.js",
            //    "~/Scripts/mixerp/core/grid/print-grid.js",
            //    "~/Scripts/mixerp/core/libraries/chartjs.js",
            //    "~/Scripts/mixerp/core/libraries/colorbox.js",
            //    "~/Scripts/mixerp/core/libraries/jquery-notify.js",
            //    "~/Scripts/mixerp/core/libraries/semantic-ui.js",
            //    "~/Scripts/mixerp/core/prototype/string.js",
            //};

            //bundles.Add(new ScriptBundle("~/bundles/libraries").Include(libraries.ToArray()));
            //bundles.Add(new ScriptBundle("~/bundles/mixerp").Include(mixerp.ToArray()));



            //libraries.Clear();

            //libraries.Add("~/Scripts/colorbox/colorbox.css");
            //libraries.Add("~/Scripts/jquery-ui/css/custom-theme/jquery-ui.css");
            //libraries.Add("~/Scripts/vakata-jstree/dist/themes/default/style.css");
            //libraries.Add("~/Scripts/semantic-ui/semantic.css");
            //libraries.Add("~/Contents/styles/mixerp.css");


            //bundles.Add(new StyleBundle("~/bundles/style").Include(libraries.ToArray()));
        }
    }
}