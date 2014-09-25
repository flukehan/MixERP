using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("MixERP.Net.WebControls.PartyControl.PartyControl.js", "application/x-javascript")]

namespace MixERP.Net.WebControls.PartyControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PartyControl runat=server></{0}:PartyControl>")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public partial class PartyControl : CompositeControl
    {
        private bool disposed;
        public Panel container;

        public override sealed void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                base.Dispose();
            }
        }

        protected override void CreateChildControls()
        {
            //this.Validate();

            this.container = new Panel();
            this.container.Controls.Add(this.GetHeader());
            this.container.Controls.Add(this.GetTabs());
            this.container.Controls.Add(this.GetTabBody());
            this.AddHiddenField(this.container, "TotalDueAmountHidden");
            this.AddHiddenField(this.container, "OfficeDueAmountHidden");
            this.AddHiddenField(this.container, "AccruedInterestHidden");

            this.Controls.Add(this.container);
            this.AddScript();
        }

        private void AddScript()
        {
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.PartyControl.PartyControl.js", "party_control", typeof(PartyControl));
            //string script = JavaScriptHelper.GetEmbeddedScript("MixERP.Net.WebControls.PartyControl.PartyControl.js", Assembly.GetExecutingAssembly());
            //PageUtility.RegisterJavascript("partyControl", script, this.Page);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.container != null)
                    {
                        this.container.Dispose();
                        this.container = null;
                    }
                }

                this.disposed = true;
            }
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.container.RenderControl(w);
        }
    }
}