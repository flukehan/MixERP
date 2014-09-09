using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.PartyControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PartyControl runat=server></{0}:PartyControl>")]
    public partial class PartyControl : CompositeControl
    {
        private bool disposed;
        public Panel conatiner;

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

            this.conatiner = new Panel();
            this.conatiner.Controls.Add(this.GetHeader());
            this.conatiner.Controls.Add(this.GetTabs());
            this.conatiner.Controls.Add(this.GetTabBody());
            this.Controls.Add(this.conatiner);
            this.AddScript();
        }

        private void AddScript()
        {
            string script = JavaScriptHelper.GetEmbeddedScript("MixERP.Net.WebControls.PartyControl.PartyControl.js", Assembly.GetExecutingAssembly());
            PageUtility.RegisterJavascript("partyControl", script, this.Page);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //if (this.scrudContainer != null)
                    //{
                    //    this.scrudContainer.Dispose();
                    //    this.scrudContainer = null;
                    //}
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
            this.conatiner.RenderControl(w);
        }
    }
}