using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MixERP.Net.ReportManager
{
    internal class Parser
    {
        internal Parser(dynamic content)
        {
            this.Content = content;
        }

        internal dynamic Content { get; set; }

        internal IEnumerable<Report> Parse()
        {
            Collection<Report> reports = new Collection<Report>();
            foreach (dynamic item in this.Content)
            {
                Report report = new Report
                {
                    FileName = item[Config.FileNameKey],
                    DownloadUrl = item[Config.DownloadUrlKey]
                };
                reports.Add(report);
            }

            return reports;
        }
    }
}