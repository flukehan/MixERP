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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Localization;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.Core.Modules.BackOffice.Admin
{
    public partial class LocalizeMixERP : MixERPUserControl
    {
        public const string sessionKey = "LocalizationCulture";
        private string root;

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CultureSelect.DataSource = Data.Admin.LocalizeMixERP.GetCultures();
            this.CultureSelect.DataValueField = "CultureCode";
            this.CultureSelect.DataTextField = "CultureName";
            this.CultureSelect.DataBind();

            object cultureCode = this.Session[sessionKey];

            if (cultureCode != null)
            {
                this.AddGrid(cultureCode.ToString());
            }
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            string cultureCode = this.CultureSelect.SelectedValue;
            this.Session[sessionKey] = cultureCode;

            this.AddGrid(cultureCode);
        }

        private void AddGrid(string cultureCode)
        {
            string cultureName = new CultureInfo(cultureCode).NativeName;

            this.CultureLiteral.Text = string.Format("<h2 class='ui red header'>{0}</h2>", cultureName);

            using (MixERPGridView gridView = new MixERPGridView())
            {
                gridView.ID = "LocalizationGridView";
                gridView.CssClass = "ui table initially hidden";
                gridView.DataSource = Factory.Get<DbGetLocalizationTableResult>("SELECT * FROM localization.get_localization_table(@0::text)", cultureCode);
                gridView.DataBind();

                this.Placeholder1.Controls.Add(gridView);
            }
        }

        protected void LetsDoThatNowButton_OnClick(object sender, EventArgs e)
        {
            this.InsertResourceToDb();
        }

        private void InsertResourceToDb()
        {
            const string sql = "SELECT * FROM localization.add_resource(@0, @1, @2);";

            foreach (string file in this.GetFiles())
            {
                foreach (KeyValuePair<string, string> item in this.GetResources(file))
                {
                    Factory.NonQuery(sql, file, item.Key, item.Value);
                }
            }
        }

        private IEnumerable<string> GetFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.Page.Server.MapPath("~/")).Parent;

            if (directoryInfo != null && directoryInfo.Parent != null)
            {
                this.root = directoryInfo.Parent.FullName;

                string[] files = Directory.GetFiles(root, "*.resx", SearchOption.AllDirectories);
                return files.Select(s => s.Replace(root, "")).ToArray();
            }

            return null;
        }

        private IEnumerable<KeyValuePair<string, string>> GetResources(string file)
        {
            XDocument xDoc = XDocument.Load(this.root + file);

            IEnumerable<KeyValuePair<string, string>> result =
                from item in xDoc.Descendants("data")
                let xElement = item.Element("value")
                where xElement != null
                select new KeyValuePair<string, string>(item.Attribute("name").Value, xElement.Value);

            return result;
        }
    }
}