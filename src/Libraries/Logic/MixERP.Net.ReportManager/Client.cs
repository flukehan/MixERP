using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace MixERP.Net.ReportManager
{
    internal class Client
    {
        internal Client(string url)
        {
            this.Url = url;
        }

        internal string Url { get; set; }

        internal async Task<IEnumerable<Report>> FetchReports()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Config.MediaType));
                    client.DefaultRequestHeaders.Add("User-Agent", Config.UserAgent);

                    HttpResponseMessage response = await client.GetAsync(this.Url);

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic content = response.Content.ReadAsAsync<dynamic>();

                        Parser parser = new Parser(content.Result);
                        IEnumerable<Report> reports = parser.Parse();

                        return reports;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //Swallow
            }

            return null;
        }

        internal string GetContent()
        {
            using (WebClient client = new WebClient())
            {
                string content = client.DownloadString(this.Url);
                Console.WriteLine("{0}", content);
                return content;
            }
        }
    }
}