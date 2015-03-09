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

using System.Collections.Generic;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.HtmlParser.ImageSerializer;
using MixERP.Net.Messaging.Email;
using MixERP.Net.WebControls.TransactionChecklist.Resources;

namespace MixERP.Net.WebControls.TransactionChecklist.Helpers
{
    internal sealed class EmailHelper
    {
        public EmailHelper(string html, string subject, string recipient)
        {
            this.EmailBody = Labels.EmailBody;
            this.Html = html;
            this.Subject = subject;
            this.Recipient = recipient;
        }

        internal string EmailBody { get; set; }

        internal string Html { get; set; }
        internal string Recipient { get; set; }

        internal string Subject { get; set; }
        internal void SendEmail()
        {

            IHtmlImageSerializer serializer = new HtmlRendererImageSerializer();

            string extension = ConfigurationHelper.GetTransactionChecklistParameter("EmailImageExtension") ?? ".png";

            serializer.Html = this.Html;
            serializer.ImageFormat = ImageHelper.GetImageFormat(extension);
            serializer.ImageSaved += this.Serializer_ImageSaved;
            serializer.TempDirectory = "~/Resource/Temp/Images/";
            serializer.Serialize();
        }


        private void Serializer_ImageSaved(object sender, ImageSavedEventArgs e)
        {
            Processor processor = new Processor();
            processor.Send(this.Recipient, this.Subject, this.EmailBody, AttachmentFactory.GetAttachments(e.ImagePath));
        }
    }
}