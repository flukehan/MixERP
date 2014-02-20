using System;
using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Setup.Admin
{
    public partial class DatabaseStatistics : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VacuumButton.OnClientClick = "return(confirm('" + Questions.ConfirmVacuum + "'));";
            this.FullVacuumButton.OnClientClick = "return(confirm('" + Questions.ConfirmVacuumFull + "'));";
            this.AnalyzeButton.OnClientClick = "return(confirm('" + Questions.ConfirmAnalyze + "'));";
            this.AddScrud();
            this.LocalizeButtons();
        }

        private void LocalizeButtons()
        {
            this.VacuumButton.Text = Titles.VacuumDatabase;
            this.FullVacuumButton.Text = Titles.VacuumFullDatabase;
            this.AnalyzeButton.Text = Titles.AnalyzeDatabse;
        }

        private void AddScrud()
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.DenyAdd = true;
                scrud.DenyDelete = true;
                scrud.DenyEdit = true;

                scrud.KeyColumn = "relname";
                scrud.PageSize = 500;

                scrud.TableSchema = "public";
                scrud.Table = "db_stat";
                scrud.ViewSchema = "public";
                scrud.View = "db_stat";

                scrud.Text = Titles.DatabaseStatistics;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        protected void VacuumButton_Click(object sender, EventArgs e)
        {
            Maintenance.Vacuum();
            this.DisplaySuccess();
        }

        protected void FullVacuumButton_Click(object sender, EventArgs e)
        {
            Maintenance.VacuumFull();
            this.DisplaySuccess();
        }

        protected void AnalyzeButton_Click(object sender, EventArgs e)
        {
            Maintenance.Analyze();
            this.DisplaySuccess();
        }

        private void DisplaySuccess()
        {
            this.MessageLiteral.Text = @"<div class='success'>" + Labels.TaskCompletedSuccessfully + @"</div>";
        }
    }
}