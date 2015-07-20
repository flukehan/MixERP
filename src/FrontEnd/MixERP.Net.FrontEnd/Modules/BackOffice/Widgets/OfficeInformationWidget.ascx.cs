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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.BackOffice.Data;
using MixERP.Net.Entities.Audit;
using MixERP.Net.Framework.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

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


                using (HtmlGenericControl segment = HtmlControlHelper.GetSegment("ui attached segment"))
                {
                    using (HtmlGenericControl leftFloatedColumn = new HtmlGenericControl("div"))
                    {
                        leftFloatedColumn.Attributes.Add("class", "ui left floated column");
                        this.CreateHeader(leftFloatedColumn);
                        segment.Controls.Add(leftFloatedColumn);
                    }

                    using (HtmlGenericControl rightFloatedColumn = new HtmlGenericControl("div"))
                    {
                        rightFloatedColumn.Attributes.Add("class", "ui right floated column");


                        using (HtmlGenericControl i = HtmlControlHelper.GetIcon("expand disabled icon"))
                        {
                            rightFloatedColumn.Controls.Add(i);
                        }
                        using (HtmlGenericControl i = HtmlControlHelper.GetIcon("move icon"))
                        {
                            rightFloatedColumn.Controls.Add(i);
                        }
                        using (HtmlGenericControl i = HtmlControlHelper.GetIcon("help icon"))
                        {
                            rightFloatedColumn.Controls.Add(i);
                        }
                        using (HtmlGenericControl i = HtmlControlHelper.GetIcon("close icon"))
                        {
                            rightFloatedColumn.Controls.Add(i);
                        }

                        segment.Controls.Add(rightFloatedColumn);
                    }

                    widget.Controls.Add(segment);
                }

                using (HtmlGenericControl segment = HtmlControlHelper.GetSegment("ui attached segment"))
                {
                    this.CreateContent(segment);

                    widget.Controls.Add(segment);
                }

                container.Controls.Add(widget);
            }
        }

        private void CreateContent(HtmlGenericControl container)
        {
            int userId = AppUsers.GetCurrent().View.UserId.ToInt();

            if (userId.Equals(0))
            {
                return;
            }

            DbGetOfficeInformationModelResult model = Audit.GetOfficeInformationModel(AppUsers.GetCurrentUserDB(), userId);

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

        private void CreateHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                header.Attributes.Add("class", "ui header");
                header.InnerText = Titles.OfficeInformation;
                container.Controls.Add(header);
            }
        }
    }
}