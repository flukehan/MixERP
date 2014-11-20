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

using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using Npgsql;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace MixERP.Net.Core.Modules.BackOffice.Admin
{
    public partial class QueryTool : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            ExecuteButton.Text = Titles.Execute;
            LoadButton.Text = Titles.Load;
            ClearButton.Text = Titles.Clear;
            SaveButton.Text = Titles.Save;
            GoToTopButton.Text = Titles.GoToTop;

            base.OnControlLoad(sender, e);
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            this.QueryTextBox.Text = "";
        }

        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(this.QueryTextBox.Text))
                {
                    using (DataTable table = Data.Admin.QueryTool.GetDataTable(command))
                    {
                        this.MessageLiteral.Text = string.Format(CultureInfo.CurrentCulture, "<div class='success'>" + Labels.NumRowsAffected + "</div>", table.Rows.Count);
                        this.SQLGridView.DataSource = table;
                        this.SQLGridView.DataBind();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                this.MessageLiteral.Text = @"<div class='error'>" + ex.Message + @"</div>";
            }
        }

        protected void LoadBlankDBButton_Click(object sender, EventArgs e)
        {
            string sql = File.ReadAllText(this.Page.Server.MapPath("~/bundles/sql/mixerp-blank-db.sql"));
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = Data.Admin.QueryTool.GetDataTable(command))
                {
                    this.MessageLiteral.Text = string.Format(CultureInfo.CurrentCulture, "<div class='success'>" + Labels.NumRowsAffected + "</div>", table.Rows.Count);
                    this.SQLGridView.DataSource = table;
                    this.SQLGridView.DataBind();
                }
            }
        }

        protected void LoadButton_Click(object sender, EventArgs e)
        {
            this.LoadSql();
        }

        protected void LoadSampleData_Click(object sender, EventArgs e)
        {
            string sql = File.ReadAllText(this.Page.Server.MapPath("~/bundles/sql/mixerp-db-sample.sql"));
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = Data.Admin.QueryTool.GetDataTable(command))
                {
                    this.MessageLiteral.Text = string.Format(CultureInfo.CurrentCulture, "<div class='success'>" + Labels.NumRowsAffected + "</div>", table.Rows.Count);
                    this.SQLGridView.DataSource = table;
                    this.SQLGridView.DataBind();
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string sql = this.QueryHidden.Value;

            if (!string.IsNullOrWhiteSpace(sql))
            {
                string path = this.Page.Server.MapPath("~/db/en-US/mixerp.postgresql.bak.sql");
                File.Delete(path);
                File.WriteAllText(path, sql, Encoding.UTF8);
            }
        }

        private void LoadSql()
        {
            string sql = File.ReadAllText(this.Page.Server.MapPath("~/bundles/sql/mixerp-blank-db.sql"));
            this.QueryTextBox.Text = sql;
        }
    }
}