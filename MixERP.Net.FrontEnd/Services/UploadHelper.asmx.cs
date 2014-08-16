using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common.Models.Core;

namespace MixERP.Net.FrontEnd.Services
{
    /// <summary>
    /// Summary description for UploadHelper
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class UploadHelper : WebService
    {


        [WebMethod]
        public bool UndoUpload(string uploadedFilesJson)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Attachment[] uploads = js.Deserialize<Attachment[]>(uploadedFilesJson);

            string attachmentsDirectory = ConfigurationManager.AppSettings["AttachmentsDirectory"];

            foreach (var upload in uploads)
            {
                string path = Server.MapPath(attachmentsDirectory + upload.FilePath);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return true;
        }

    }
}
