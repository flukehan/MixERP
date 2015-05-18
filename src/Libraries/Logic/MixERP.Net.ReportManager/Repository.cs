using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MixERP.Net.ReportManager
{
    public class Repository
    {
        public static async void DownloadAndInstallReports()
        {
            Client client = new Client(Config.ApiUrl);
            IEnumerable<Report> reports = await client.FetchReports();
            Collection<ReportMenu> menus = new Collection<ReportMenu>();

            foreach (Report report in reports)
            {
                client.Url = report.DownloadUrl;
                string content = client.GetContent();

                MenuParser parser = new MenuParser(content);
                menus.Add(parser.Parse());

                Serializer serializer = new Serializer(report.FileName, content);
                serializer.Serialize();
            }

            DataLayer.AddMenus(menus);
        }
    }
}