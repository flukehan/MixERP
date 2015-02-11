using System.Linq;
using Microsoft.Web.Administration;
using MixERP.Net.Utility.Installer.Helpers;

namespace MixERP.Net.Utility.Installer.Domains
{
    public class Site
    {
        public Site(string siteName)
        {
            this.SiteName = siteName;

            ServerManager iisManager = new ServerManager();
            foreach (
                Microsoft.Web.Administration.Site site in
                    iisManager.Sites.Where(site => site.Name.ToUpperInvariant().Equals(this.SiteName.ToUpperInvariant())))
            {
                this.Path = site.Applications["/"].VirtualDirectories["/"].PhysicalPath;
                this.IsInstalled = true;
            }
        }

        public bool IsInstalled { get; private set; }
        public string Path { get; private set; }
        public string SiteName { get; set; }
    }
}