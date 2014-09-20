using MixERP.Net.FrontEnd.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class AttachmentManager : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.SetOverridePath();
            base.OnControlLoad(sender, e);
        }

        private void SetOverridePath()
        {
            string overridePath = this.Page.Request.QueryString["OverridePath"];

            if (!string.IsNullOrWhiteSpace(overridePath))
            {
                this.OverridePath = overridePath;
            }
        }
    }
}