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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.AttachmentFactory;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private static void CreateAttachmentPanel(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.ID = "attachmentToggler";
                header.InnerText = Titles.AttachmentsPlus;
                container.Controls.Add(header);
            }

            using (HtmlGenericControl attachmentContainer = new HtmlGenericControl("div"))
            {
                attachmentContainer.ID = "attachment";
                attachmentContainer.Attributes.Add("class", "ui segment initially hidden");


                using (Attachment attachment = new Attachment(AppUsers.GetCurrentUserDB()))
                {
                    attachment.ShowSaveButton = false;
                    attachmentContainer.Controls.Add(attachment);
                }
                container.Controls.Add(attachmentContainer);
            }
        }
    }
}