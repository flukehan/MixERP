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
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentNullException("id");
                }

                if (AppUsers.GetCurrent().View.UserId.ToInt() > 0)
                {
                    return this.DeleteImage(Data.Attachments.DeleteReturningPath(AppUsers.GetCurrentUserDB(), id));
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Warning("Could not delete attachment. {Exception}", ex);
                throw;
            }
        }

        [WebMethod]
        public IEnumerable<Attachment> GetAttachments(string book, long id)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                throw new ArgumentNullException("book");
            }

            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            return Data.Attachments.GetAttachments(AppUsers.GetCurrentUserDB(), "/Resource/Static/Attachments/", book, id);
        }

        [WebMethod]
        public bool Save(string book, long id, string attachmentsJSON)
        {
            try
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

                Collection<Attachment> attachments = CollectionHelper.GetAttachmentCollection(attachmentsJSON);
                int userId = AppUsers.GetCurrent().View.UserId.ToInt();

                return Data.Attachments.Save(AppUsers.GetCurrentUserDB(), userId, book, id, attachments);
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save attachment. {Exception}", ex);
                throw;
            }
        }

        private bool DeleteImage(string filePath)
        {
            filePath = this.Server.MapPath("~/Resource/Static/Attachments/" + filePath);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (IOException)
                {
                    Log.Warning("Could not delete file: {FilePath}.", filePath);
                }
            }

            return false;
        }
    }
}