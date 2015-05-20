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

using MixERP.Net.Common;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    [ToolboxData("<{0}:ScrudForm runat=server></{0}:ScrudForm>")]
    public partial class ScrudForm
    {
        private string imageColumn = string.Empty;
        private Panel scrudContainer;

        protected override void CreateChildControls()
        {
            this.Validate();

            this.scrudContainer = new Panel();

            this.LoadScrudContainer(this.scrudContainer);

            this.LoadTitle();
            this.LoadDescription();

            this.LoadGrid();
            this.CreatePager(this.gridPanel);

            this.InitializeScrudControl();

            PageUtility.AddMeta(this.Page, "generator", Assembly.GetAssembly(typeof(ScrudForm)).GetName().Name);

            this.Controls.Add(this.scrudContainer);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.scrudContainer.RenderControl(w);
        }
    }
}