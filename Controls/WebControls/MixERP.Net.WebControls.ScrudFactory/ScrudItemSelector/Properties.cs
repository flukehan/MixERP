/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

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
