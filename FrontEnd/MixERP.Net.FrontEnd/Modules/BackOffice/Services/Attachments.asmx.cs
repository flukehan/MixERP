using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
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

        [WebMethod]
        public bool DeleteAttachment(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id");
            }

            return DeleteImage(Data.Attachments.DeleteReturningPath(id));
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
                catch
                {
                    //Swallow
                }
            }

            return false;
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
    }
}