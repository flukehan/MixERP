/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace MixERP.Net.Core.Modules.BackOffice.Models
{
    public sealed class ReportDefinition
    {
        private static readonly string reportContainer =
            HostingEnvironment.MapPath(ConfigurationHelper.GetReportParameter("ReportContainer"));

        public string Title { get; set; }
        public string FileName { get; set; }
        public string MenuCode { get; set; }
        public string ParentMenuCode { get; set; }
        public string TopSection { get; set; }
        public string Body { get; set; }
        public string BottomSection { get; set; }
        public IEnumerable<Data.ReportWriter.DataSource> DataSources { get; set; }
        public IEnumerable<Data.ReportWriter.Grid> GridViews { get; set; }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                throw new MixERPException("Invalid file name \"" + this.FileName + "\".");
            }

            if (string.IsNullOrWhiteSpace(reportContainer) || !Directory.Exists(reportContainer))
            {
                throw new DirectoryNotFoundException(reportContainer);
            }

            if (!this.FileName.EndsWith(".xml"))
            {
                this.FileName += ".xml";
            }

            string path = Path.Combine(reportContainer, this.FileName);


            ReportBuilder builder = new ReportBuilder(this);

            string contents = builder.Build();

            File.WriteAllText(path, contents);
        }

        public ReportDefinition Get(string fileName)
        {
            string file = Path.Combine(reportContainer, fileName);

            if (File.Exists(file))
            {
                ReportParser parser = new ReportParser(fileName);

                return parser.Definition;
            }

            return new ReportDefinition();
        }
    }
}