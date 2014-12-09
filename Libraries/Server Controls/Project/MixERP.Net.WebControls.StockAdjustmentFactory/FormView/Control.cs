using System;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: WebResource("MixERP.Net.WebControls.StockAdjustmentFactory.FormView.js", "application/x-javascript")]

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    [ToolboxData("<{0}:FormView runat=server></{0}:FormView>")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public partial class FormView : CompositeControl
    {
        private HtmlGenericControl container;
        private bool disposed;

        public string ItemIdQuerySericeUrl { get; set; }

        public string ItemPopupUrl { get; set; }

        public string ItemServiceUrl { get; set; }

        public string StoreServiceUrl { get; set; }

        public string Text { get; set; }

        public string UnitServiceUrl { get; set; }

        public bool ValidateSides { get; set; }

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
            this.container = new HtmlGenericControl();
            this.CreateHeader();
            this.AddRuler();
            this.CreateTopPanel();
            this.CreateGridPanel();
            this.AddErrorLabelBottom();
            this.CreateBottomPanel();
            this.AddInlineScript();
            this.AddJavaScript();

            this.Controls.Add(this.container);
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