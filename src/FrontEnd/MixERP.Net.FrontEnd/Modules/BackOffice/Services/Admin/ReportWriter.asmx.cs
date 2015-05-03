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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

namespace MixERP.Net.Core.Modules.BackOffice.Services.Admin
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class ReportWriter : WebService
    {
        [WebMethod]
        public string GetTable(string sql, string parameters)
        {
            Collection<Data.ReportWriter.ReportParameters> parameterCollection =
                JsonConvert.DeserializeObject<Collection<Data.ReportWriter.ReportParameters>>(parameters);

            JsonSerializer serializer = new JsonSerializer();
            TextWriter writer = new StringWriter();

            using (DataTable table = Data.ReportWriter.GetTable(sql, parameterCollection))
            {
                serializer.Serialize(writer, table);
                return writer.ToString();
            }
        }

        [WebMethod]
        public string SaveReport(string title, string fileName, string topSection, string body, string bottomSection,
            string dataSources, string gridViews)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(title);
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(fileName);
            }

            IEnumerable<Data.ReportWriter.DataSource> dataSourceCollection =
                JsonConvert.DeserializeObject<IEnumerable<Data.ReportWriter.DataSource>>(dataSources);
            IEnumerable<Data.ReportWriter.Grid> gridViewCollection =
                JsonConvert.DeserializeObject<IEnumerable<Data.ReportWriter.Grid>>(gridViews);


            return "OK";
        }
    }
}