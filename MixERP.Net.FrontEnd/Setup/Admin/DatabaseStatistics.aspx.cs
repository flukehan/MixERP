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