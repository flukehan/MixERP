using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        public string TopPanelCssClass { get; set; }
        public string TopPanelTableCssClass { get; set; }
        public string FilterDropDownListCssClass { get; set; }
        public string FilterTextBoxCssClass { get; set; }
        public string ButtonCssClass { get; set; }
        public Unit ButtonHeight { get; set; }
        public Unit ButtonWidth { get; set; }
        public string GridPanelCssClass { get; set; }
        public Unit GridPanelHeight { get; set; }
        public Unit GridPanelWidth { get; set; }
        public string GridViewCssClass { get; set; }
        public string GridViewPagerCssClass { get;set;}
        public string GridViewRowCssClass { get; set; }
        public string GridViewAlternateRowCssClass { get; set; }
    }
}
