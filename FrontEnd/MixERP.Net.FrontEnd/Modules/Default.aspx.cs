using MixERP.Net.BusinessLayer;
using System;
using System.IO;

namespace MixERP.Net.FrontEnd.Modules
{
    public partial class Default : MixERPWebpage
    {
        private MixERPUserControl plugin;

        private void InitializeControl()
        {
            if (plugin == null)
            {
                string path = @"~/Modules/" + this.RouteData.Values["path"].ToString().Replace(".html", "") + ".ascx";

                if (!File.Exists(Server.MapPath(path)))
                {
                    throw new FileNotFoundException("Invalid path : " + path);
                }

                this.plugin = this.Page.LoadControl(path) as MixERPUserControl;
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

                if (!string.IsNullOrWhiteSpace(plugin.OverridePath))
                {
                    this.OverridePath = plugin.OverridePath;
                }

                plugin.OnControlLoad(sender, e);
            }
        }
    }
}