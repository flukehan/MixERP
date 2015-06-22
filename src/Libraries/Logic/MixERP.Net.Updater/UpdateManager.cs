using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MixERP.Net.Updater.Api;

namespace MixERP.Net.Updater
{
    public class UpdateManager
    {
        private static Version GetCurrentVersion()
        {
            //Todo
            //return Assembly.GetExecutingAssembly().GetName().Version;
            return new Version("1.0.0.0");
        }

        public async Task<Release> GetLatestRelease()
        {
            IEnumerable<Release> releases = await Releases.GetReleases();

            if (releases == null)
            {
                return null;
            }

            Release release =
                releases.OrderBy(x => x.PublishedAt).First(x => new Version(x.TagName) > GetCurrentVersion());
            return release;
        }
    }
}