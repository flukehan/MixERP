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

using MixERP.Net.Common.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    [ToolboxData("<{0}:StockTransactionForm runat=server></{0}:StockTransactionForm>")]
    public partial class StockTransactionForm : CompositeControl
    {
        protected override void CreateChildControls()
        {
            this.placeHolder = new PlaceHolder();

            this.CreateTitle(placeHolder);
            this.CreateTopFormPanel(placeHolder);

            if (!this.Page.IsPostBack)
            {
                SessionHelper.RemoveSessionKey(this.ID);
            }

            this.AddStylesheet();
            this.CreateGrid(this.placeHolder);
            CreateErrorLabel(this.placeHolder);
            CreateAttachmentPanel(this.placeHolder);
            this.CreateBottomFormPanel(this.placeHolder);
            this.CreateHiddenFields(this.placeHolder);
            CreateErrorLabelBottom(this.placeHolder);
            this.AddJavascript();
            this.RegisterJavascript();
            this.LoadValuesFromSession();
            this.BindGridView();
            this.ClearSession();

            this.Controls.Add(this.placeHolder);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            this.placeHolder.RenderControl(writer);
        }
    }
}