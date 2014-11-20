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

using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.BackOffice.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Attachments : WebService
    {
        [WebMethod]
        public bool DeleteAttachment(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            return DeleteImage(Data.Attachments.DeleteReturningPath(id));
        }

        [WebMethod]
        public Collection<AttachmentModel> GetAttachments(string book, long id)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                throw new ArgumentNullException("book");
            }

            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            return Data.Attachments.GetAttachments("/Resource/Static/Attachments/", book, id);
        }

        [WebMethod(EnableSession = true)]
        public bool Save(string book, long id, string attachmentsJSON)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                throw new ArgumentNullException("book");
            }

            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            if (string.IsNullOrWhiteSpace(attachmentsJSON))
            {
                throw new ArgumentNullException("attachmentsJSON");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<AttachmentModel> attachments = js.Deserialize<Collection<AttachmentModel>>(attachmentsJSON);
            int userId = SessionHelper.GetUserId();

            return Data.Attachments.Save(userId, book, id, attachments);
        }

        private bool DeleteImage(string filePath)
        {
            filePath = Server.MapPath("~/Resource/Static/Attachments/" + filePath);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (IOException)
                {
                    //Swallow
                }
            }

            return false;
        }
    }
}