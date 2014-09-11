
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using Npgsql;
using System;
using System.Data;
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

        protected void LoadButton_Click(object sender, EventArgs e)
        {
            this.LoadSql();
        }

        private void LoadSql()
        {
            string sql = File.ReadAllText(this.Page.Server.MapPath("~/bundles/sql/mixerp-blank-db.sql"));
            this.QueryTextBox.Text = sql;
        }

        protected void LoadBlankDBButton_Click(object sender, EventArgs e)
        {
            string sql = File.ReadAllText(this.Page.Server.MapPath("~/bundles/sql/mixerp-blank-db.sql"));
            using (DataTable table = Data.Admin.QueryTool.GetDataTable(new NpgsqlCommand(sql)))
            {
                this.MessageLiteral.Text = string.Format("<div class='success'>{0} row(s) affected.</div>", table.Rows.Count);
                this.SQLGridView.DataSource = table;
                this.SQLGridView.DataBind();
            }
        }

        protected void LoadSampleData_Click(object sender, EventArgs e)
        {
            string sql = File.ReadAllText(this.Page.Server.MapPath("~/bundles/sql/mixerp-db-sample.sql"));
            using (DataTable table = Data.Admin.QueryTool.GetDataTable(new NpgsqlCommand(sql)))
            {
                this.MessageLiteral.Text = string.Format("<div class='success'>{0} row(s) affected.</div>", table.Rows.Count);
                this.SQLGridView.DataSource = table;
                this.SQLGridView.DataBind();
            }
        }

        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable table = Data.Admin.QueryTool.GetDataTable(new NpgsqlCommand(this.QueryTextBox.Text)))
                {
                    this.MessageLiteral.Text = string.Format("<div class='success'>{0} row(s) affected.</div>", table.Rows.Count);
                    this.SQLGridView.DataSource = table;
                    this.SQLGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.MessageLiteral.Text = @"<div class='error'>" + ex.Message + @"</div>";
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
    }
}