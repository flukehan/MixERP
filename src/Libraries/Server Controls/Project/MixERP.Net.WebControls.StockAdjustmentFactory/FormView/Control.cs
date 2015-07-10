using MixERP.Net.WebControls.Common;
using System;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    [ToolboxData("<{0}:FormView runat=server></{0}:FormView>")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public partial class FormView : CompositeControl
    {
        private HtmlGenericControl container;
        private DateTextBox dateTextBox;
        public string ItemIdQuerySericeUrl { get; set; }
        public string ItemPopupUrl { get; set; }
        public string ItemServiceUrl { get; set; }
        public string StoreServiceUrl { get; set; }
        public string ShippingCompanyServiceUrl { get; set; }
        public string Text { get; set; }
        public string UnitServiceUrl { get; set; }
        public bool ValidateSides { get; set; }
        public bool HideSides { get; set; }

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
            this.AddJavascript();

            this.Controls.Add(this.container);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter w)
        {
            this.container.RenderControl(w);
        }

        #region IDisposable

        private bool disposed;

        public override sealed void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                base.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
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

            if (this.dateTextBox != null)
            {
                this.dateTextBox.Dispose();
                this.dateTextBox = null;
            }

            this.disposed = true;
        }

        #endregion
    }
}