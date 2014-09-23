using MixERP.Net.Common.Base;
using MixERP.Net.Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Common.Helpers
{
    public static class WidgetHelper
    {
        public static void LoadWidgets(IEnumerable<WidgetModel> widgetModels, PlaceHolder placeholder, Page page)
        {
            if (placeholder == null)
            {
                return;
            }

            if (page == null)
            {
                return;
            }

            if (widgetModels == null)
            {
                return;
            }

            var groups = widgetModels.OrderBy(x => x.RowNumber).ThenBy(x => x.ColumnNumber).GroupBy(x => new { x.RowNumber });

            foreach (var group in groups)
            {
                foreach (var item in group)
                {
                    using (HtmlGenericControl div = new HtmlGenericControl())
                    {
                        div.TagName = "div";
                        div.Attributes.Add("class", "sortable-item col-md-" + 12 / group.Count());

                        if (item.ColSpan > 1)
                        {
                            div.Attributes.Add("data-ss-colspan", item.ColSpan.ToString(CultureInfo.CurrentUICulture));
                        }

                        using (MixERPWidgetBase widget = page.LoadControl(item.WidgetSource) as MixERPWidgetBase)
                        {
                            if (widget != null)
                            {
                                div.Controls.Add(widget);
                            }
                        }

                        placeholder.Controls.Add(div);
                    }
                }
            }
        }
    }
}