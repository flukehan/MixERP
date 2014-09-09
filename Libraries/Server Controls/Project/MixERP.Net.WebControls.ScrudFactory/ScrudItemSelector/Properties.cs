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

using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        public string ButtonCssClass { get; set; }

        public Unit ButtonHeight { get; set; }

        public Unit ButtonWidth { get; set; }

        public string FilterDropDownListCssClass { get; set; }

        public string FilterTextBoxCssClass { get; set; }

        public string GridPanelCssClass { get; set; }

        public Unit GridPanelHeight { get; set; }

        public Unit GridPanelWidth { get; set; }

        public string GridViewAlternateRowCssClass { get; set; }

        public string GridViewCssClass { get; set; }

        public string GridViewPagerCssClass { get; set; }

        public string GridViewRowCssClass { get; set; }

        public string TopPanelCssClass { get; set; }

        public string TopPanelTableCssClass { get; set; }
    }
}