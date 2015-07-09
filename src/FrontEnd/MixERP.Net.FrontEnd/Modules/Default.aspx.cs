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

using MixERP.Net.FrontEnd.Base;
using System;
using System.IO;

namespace MixERP.Net.FrontEnd.Modules
{
    public partial class Default : MixERPWebpage
    {
        private MixERPUserControl plugin;

        protected override void OnInit(EventArgs e)
        {
            if (this.plugin != null)
            {
                this.DefaultContentPlaceholder.Controls.Add(this.plugin);

                this.plugin.Initialize();
                this.ValidateRequestMode = plugin.ValidDateRequest;

                if (!string.IsNullOrWhiteSpace(this.plugin.OverridePath))
                {
                    this.OverridePath = this.plugin.OverridePath;
                }

                this.IsLandingPage = this.plugin.IsLandingPage;
            }

            base.OnInit(e);
        }

        protected override void OnPreInit(EventArgs e)
        {
            this.InitializeControl();

            if (this.plugin != null)
            {
                if (!string.IsNullOrWhiteSpace(this.plugin.MasterPageId))
                {
                    this.MasterPageFile = "~/" + this.plugin.MasterPageId;
                }

                if (this.plugin.RemoveTheme)
                {
                    this.Page.Theme = null;
                }
            }

            base.OnPreInit(e);
        }


        private void InitializeControl()
        {
            if (this.plugin == null)
            {
                if (this.RouteData.Values["path"] != null)
                {
                    string path = @"~/Modules/" + this.RouteData.Values["path"].ToString().Replace(".mix", "") + ".ascx";

                    if (File.Exists(this.Server.MapPath(path)))
                    {
                        this.plugin = this.Page.LoadControl(path) as MixERPUserControl;
                    }
                }
            }
        }
    }
}