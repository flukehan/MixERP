using MixERP.Net.ApplicationState.Cache;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MixERP.Net.WebControls.AttachmentFactory.Handlers
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public class UploadService : WebService, IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            WebServiceHandlerFactory factory = new WebServiceHandlerFactory();
            MethodInfo method = typeof (WebServiceHandlerFactory).GetMethod("CoreGetHandler",
                BindingFlags.NonPublic | BindingFlags.Instance, null,
                new Type[] {typeof (Type), typeof (HttpContext), typeof (HttpRequest), typeof (HttpResponse)}, null);

            return (IHttpHandler) method.Invoke(factory, new object[]
            {
                this.GetType(),
                context,
                context.Request,
                context.Response
            });
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public bool UndoUpload(string uploadedFilesJson)
        {
            Collection<Entities.Core.Attachment> uploads = GetAttachmentCollection(uploadedFilesJson);

            string attachmentsDirectory =
                Helpers.ConfigurationHelper.GetAttachmentsDirectory(AppUsers.GetCurrentUserDB());

            foreach (Entities.Core.Attachment upload in uploads)
            {
                string path = this.Server.MapPath(attachmentsDirectory + upload.FilePath);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return true;
        }

        public static Collection<Entities.Core.Attachment> GetAttachmentCollection(string json)
        {
            Collection<Entities.Core.Attachment> details = new Collection<Entities.Core.Attachment>();
            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            if (result != null)
            {
                foreach (dynamic item in result)
                {
                    Entities.Core.Attachment detail = new Entities.Core.Attachment();
                    detail.Comment = item["Comment"];
                    detail.FilePath = item["FilePath"];
                    detail.OriginalFileName = item["OriginalFileName"];

                    details.Add(detail);
                }
            }

            return details;
        }
    }
}