using MixERP.Net.Common.Helpers;
using System.IO;
using System.Web.Hosting;

namespace MixERP.Net.ReportManager
{
    internal class Serializer
    {
        internal bool IsValid { get; set; }

        internal Serializer(string fileName, string content)
        {
            string targetDirectory = HostingEnvironment.MapPath(ConfigurationHelper.GetReportParameter("ReportContainer"));
            this.Content = content;

            if (targetDirectory == null || !Directory.Exists(targetDirectory))
            {
                this.IsValid = false;
                return;
            }

            this.IsValid = true;
            this.Path = System.IO.Path.Combine(targetDirectory, fileName);
        }

        internal string Path { get; set; }
        internal string Content { get; set; }

        internal void Serialize()
        {
            if (this.IsValid)
            {
                File.WriteAllText(this.Path, this.Content);
            }
        }
    }
}