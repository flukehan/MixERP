using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MixERP.Net.Updater.Api
{
    public static class Releases
    {
        public static async Task<IEnumerable<Release>> GetReleases()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Config.MediaType));
                    client.DefaultRequestHeaders.Add("User-Agent", Config.UserAgent);

                    HttpResponseMessage response = await client.GetAsync(Config.ApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic content = await response.Content.ReadAsAsync<dynamic>();

                        List<Release> releases = new List<Release>();

                        foreach (dynamic release in content)
                        {
                            releases.Add(Release.Parse(release, Config.AssetsKey));
                        }


                        return releases;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}