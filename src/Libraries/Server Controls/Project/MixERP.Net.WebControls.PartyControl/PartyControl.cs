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
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("MixERP.Net.WebControls.PartyControl.PartyControl.js", "application/x-javascript")]

namespace MixERP.Net.WebControls.PartyControl
{
    [ToolboxData("<{0}:PartyControl runat=server></{0}:PartyControl>")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed partial class PartyControl : CompositeControl
    {
        public Panel container;

        protected override void CreateChildControls()
        {
            //this.Validate();

            this.container = new Panel();
            this.container.Controls.Add(GetHeader());
            this.container.Controls.Add(this.GetTabs());
            this.AddTabBody(this.container);
            this.AddHiddenField(this.container, "TotalDueAmountHidden");
            this.AddHiddenField(this.container, "OfficeDueAmountHidden");
            this.AddHiddenField(this.container, "AccruedInterestHidden");

            this.Controls.Add(this.container);
            this.AddScript();
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.container.RenderControl(w);
        }

        private void AddScript()
        {
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.PartyControl.PartyControl.js", "PartyControl", typeof(PartyControl));
        }

        #region IDisposable

        private bool disposed;

        public override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            if (this.container != null)
            {
                this.container.Dispose();
                this.container = null;
            }

            this.disposed = true;
        }

        #endregion
    }
}