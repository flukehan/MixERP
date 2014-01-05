using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public class ScrudItemSelectorSelectTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            using (HtmlAnchor selectAnchor = new HtmlAnchor())
            {
                selectAnchor.HRef = "#";
                selectAnchor.Attributes.Add("class", "linkbutton");//Todo: parameterize
                selectAnchor.DataBinding += this.BindData;
                selectAnchor.InnerText = Resources.ScrudResource.Select;
            }

        }

        public void BindData(object sender, EventArgs e)
        {
            using (HtmlAnchor selectAnchor = new HtmlAnchor())
            {
                DataGridItem container = (DataGridItem)selectAnchor.NamingContainer;
                selectAnchor.Attributes.Add("onclick", "updateValue(" + ((DataRowView)container.DataItem)[0].ToString() + ");");
            }
        }
    }
}
