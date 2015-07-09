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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Core.Modules.BackOffice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;

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
            Collection<Data.ReportWriter.ReportParameter> parameterCollection =
                JsonConvert.DeserializeObject<Collection<Data.ReportWriter.ReportParameter>>(parameters);

            JsonSerializer serializer = new JsonSerializer();
            TextWriter writer = new StringWriter();

            using (DataTable table = Data.ReportWriter.GetTable(AppUsers.GetCurrentUserDB(), sql, parameterCollection))
            {
                serializer.Serialize(writer, table);
                return writer.ToString();
            }
        }

        [WebMethod]
        public string SaveReport(string title, string fileName, string menuCode, string parentMenuCode, string topSection, string body, string bottomSection,
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

            if (string.IsNullOrWhiteSpace(menuCode))
            {
                throw new ArgumentNullException(menuCode);
            }

            if (string.IsNullOrWhiteSpace(parentMenuCode))
            {
                throw new ArgumentNullException(parentMenuCode);
            }

            IEnumerable<Data.ReportWriter.DataSource> dataSourceCollection =
                JsonConvert.DeserializeObject<IEnumerable<Data.ReportWriter.DataSource>>(dataSources);
            IEnumerable<Data.ReportWriter.Grid> gridViewCollection =
                JsonConvert.DeserializeObject<IEnumerable<Data.ReportWriter.Grid>>(gridViews);

            this.SaveReport(title, fileName, menuCode, parentMenuCode, topSection, body, bottomSection,
                dataSourceCollection, gridViewCollection);

            return "OK";
        }

        private void SaveReport(string title, string fileName, string menuCode, string parentMenuCode, string topSection, string body, string bottomSection,
            IEnumerable<Data.ReportWriter.DataSource> dataSources, IEnumerable<Data.ReportWriter.Grid> gridViews)
        {
            ReportDefinition definition = new ReportDefinition
            {
                Title = title,
                FileName = fileName,
                MenuCode = menuCode,
                ParentMenuCode = parentMenuCode,
                TopSection = topSection,
                Body = body,
                BottomSection = bottomSection,
                DataSources = dataSources,
                GridViews = gridViews
            };

            definition.Save();
        }

        [WebMethod]
        public IEnumerable<Report> GetReports()
        {
            return Report.GetReports();
        }

        [WebMethod]
        public ReportDefinition GetDefinition(string fileName)
        {
            ReportDefinition definition = new ReportDefinition();
            return definition.Get(fileName);
        }
    }
}