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

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using MixERP.Net.Common.Domains;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.Entities.Audit;
using MixERP.Net.FrontEnd.Base;

namespace MixERP.Net.Core.Modules.BackOffice.Widgets
{
    public partial class OfficeInformationWidget : MixERPWidget
    {
        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.Everyone; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateWidget(this.Placeholder1);
        }

        private void CreateWidget(Control container)
        {
            using (HtmlGenericControl widget = new HtmlGenericControl("div"))
            {
                widget.ID = "OfficeInformationWidget";
                widget.Attributes.Add("class", "four wide column widget");

                using (HtmlGenericControl segment = HtmlControlHelper.GetSegment())
                {
                    this.CreateHeader(segment);
                    this.CreateDivider(segment);
                    this.CreateContent(segment);

                    widget.Controls.Add(segment);
                }

                container.Controls.Add(widget);
            }
        }

        private void CreateContent(HtmlGenericControl container)
        {
            int userId = CurrentSession.GetUserId();

            DbGetOfficeInformationModelResult model = Data.Audit.GetOfficeInformationModel(userId);

            using (HtmlGenericControl ul = new HtmlGenericControl("ul"))
            {
                HtmlControlHelper.AddListItem(ul, Titles.YourOffice, model.Office);
                HtmlControlHelper.AddListItem(ul, Titles.LoggedInTo, model.LoggedInTo);
                HtmlControlHelper.AddListItem(ul, Titles.LastLoginIP, model.LastLoginIp);
                HtmlControlHelper.AddListItem(ul, Titles.LastLoginOn, model.LastLoginOn);
                HtmlControlHelper.AddListItem(ul, Titles.CurrentIP, model.CurrentIp);
                HtmlControlHelper.AddListItem(ul, Titles.CurrentLoginOn, model.CurrentLoginOn);
                HtmlControlHelper.AddListItem(ul, Titles.Role, model.Role);
                HtmlControlHelper.AddListItem(ul, Titles.Department, model.Department);

                container.Controls.Add(ul);
            }
        }

        private void CreateDivider(HtmlGenericControl container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreateHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.Attributes.Add("class", "ui purple header");
                header.InnerText = Resources.Titles.OfficeInformation;
                container.Controls.Add(header);
            }
        }
    }
}