using MixERP.Net.Common.Base;
using MixERP.Net.FrontEnd.Base;
using System;
using System.IO;

namespace MixERP.Net.FrontEnd.Modules
{
    public partial class Default : MixERPWebpage
    {
        private MixERPUserControlBase plugin;

        private void InitializeControl()
        {
            if (plugin == null)
            {
                string path = @"~/Modules/" + this.RouteData.Values["path"].ToString().Replace(".mix", "") + ".ascx";

                if (!File.Exists(Server.MapPath(path)))
                {
                    throw new FileNotFoundException("Invalid path : " + path);
                }

                this.plugin = this.Page.LoadControl(path) as MixERPUserControlBase;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.InitializeControl();

            if (plugin != null)
            {
                if (!string.IsNullOrWhiteSpace(plugin.MasterPageId))
                {
                    this.MasterPageFile = "~/" + plugin.MasterPageId;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (plugin != null)
            {
                this.DefaultContentPlaceholder.Controls.Add(plugin);

                plugin.OnControlLoad(sender, e);

                if (!string.IsNullOrWhiteSpace(plugin.OverridePath))
                {
                    this.OverridePath = plugin.OverridePath;
                }
            }
        }
    }
}