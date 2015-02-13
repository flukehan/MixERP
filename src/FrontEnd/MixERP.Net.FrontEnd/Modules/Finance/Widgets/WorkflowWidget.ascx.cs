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
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.Entities.Core;
using MixERP.Net.FrontEnd.Base;

namespace MixERP.Net.Core.Modules.Finance.Widgets
{
    public partial class WorkflowWidget : MixERPWidget
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateWidget(this.Placeholder1);
        }

        private void CreateWidget(Control container)
        {
            using (HtmlGenericControl widget = new HtmlGenericControl("div"))
            {
                widget.ID = "WorkflowWidget";
                widget.Attributes.Add("class", "eight wide column widget");

                using (HtmlGenericControl segment = HtmlControlHelper.GetSegment())
                {
                    this.CreateHeader(segment);
                    this.CreateDivider(segment);
                    this.CreateList(segment);

                    widget.Controls.Add(segment);
                }

                container.Controls.Add(widget);
            }
        }

        private void CreateList(Control container)
        {
            DbGetWorkflowModelResult model = Data.Core.Workflow.GetWorkflowModel();

            using (HtmlGenericControl ul = new HtmlGenericControl("ul"))
            {
                HtmlControlHelper.AddListItem(ul, Titles.FlaggedTransactions, model.FlaggedTransactions);
                HtmlControlHelper.AddListItem(ul, Titles.InVerificationStack, model.InVerificationStack);
                HtmlControlHelper.AddListItem(ul, Titles.AutomaticallyApprovedByWorkflow, model.AutoApproved);
                HtmlControlHelper.AddListItem(ul, Titles.ApprovedTransactions, model.Approved);
                HtmlControlHelper.AddListItem(ul, Titles.RejectedTransactions, model.Rejected);
                HtmlControlHelper.AddListItem(ul, Titles.ClosedTransactions, model.Closed);
                HtmlControlHelper.AddListItem(ul, Titles.WithdrawnTransactions, model.Withdrawn);
                container.Controls.Add(ul);
            }
        }

        private void CreateDivider(Control container)
        {
            using (HtmlGenericControl divider = HtmlControlHelper.GetDivider())
            {
                container.Controls.Add(divider);
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.Attributes.Add("class", "ui purple header");
                header.InnerText = Titles.Workflow;

                container.Controls.Add(header);
            }
        }
    }
}