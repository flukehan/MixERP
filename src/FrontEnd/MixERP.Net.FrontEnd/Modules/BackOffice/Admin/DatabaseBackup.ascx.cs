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
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Domains;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.Core.Modules.BackOffice.Admin
{
    public partial class DatabaseBackup : MixERPUserControl
    {
        private readonly PostgreSQLServer server = new PostgreSQLServer();

        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.AdminOnly; }
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateFormSegment(this.Placeholder1);
            this.CreateGridModal(this.Placeholder1);
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h1"))
            {
                header.InnerText = Titles.BackupDatabase;
                container.Controls.Add(header);
            }
        }

        #region Form Segment

        private void CreateBackupButton(HtmlGenericControl container)
        {
            using (HtmlInputButton backupButton = new HtmlInputButton())
            {
                backupButton.Attributes.Add("class", "ui red button disabled loading");
                backupButton.Value = Titles.BackupNow;
                backupButton.ID = "BackupButton";
                container.Controls.Add(backupButton);
            }
        }

        private void CreateBackupConsole(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.Attributes.Add("class", "ui blue header initially hidden");
                header.InnerText = Titles.BackupConsole;

                container.Controls.Add(header);
            }

            using (HtmlGenericControl messages = new HtmlGenericControl("div"))
            {
                messages.Attributes.Add("style", "overflow:auto;");
                messages.ID = "Messages";

                using (HtmlGenericControl celledList = new HtmlGenericControl("div"))
                {
                    celledList.Attributes.Add("class", "ui celled list");
                    messages.Controls.Add(celledList);
                }
                container.Controls.Add(messages);
            }
        }

        private void CreateBackupNameField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.EnterBackupName, "BackupNameInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText backupNameInputText = new HtmlInputText())
                {
                    this.server.Validate();
                    backupNameInputText.ID = "BackupNameInputText";
                    if (this.server.IsValid)
                    {
                        backupNameInputText.Value = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", this.server.DatabaseName, CurrentSession.GetOfficeId(), CurrentSession.GetUserName(), DateTime.Now.ToFileTime());
                    }

                    field.Controls.Add(backupNameInputText);
                }

                container.Controls.Add(field);
            }
        }

        private void CreateFormSegment(Control container)
        {
            using (HtmlGenericControl formSegment = HtmlControlHelper.GetFormSegment())
            {
                using (HtmlGenericControl inlineFields = HtmlControlHelper.GetInlineFields())
                {
                    this.CreateBackupNameField(inlineFields);
                    this.CreateBackupButton(inlineFields);
                    this.CreateViewBackupButton(inlineFields);

                    formSegment.Controls.Add(inlineFields);
                }

                this.CreateBackupConsole(formSegment);
                container.Controls.Add(formSegment);
            }
        }

        private void CreateViewBackupButton(HtmlGenericControl container)
        {
            using (HtmlInputButton backupButton = new HtmlInputButton())
            {
                backupButton.Attributes.Add("class", "ui green button");
                backupButton.Attributes.Add("onclick", "$('.modal').modal('show');");
                backupButton.Value = Titles.ViewBackups;
                container.Controls.Add(backupButton);
            }
        }

        #endregion

        #region GridPanel

        private void BindGridView()
        {
            string backupDirectory = HostingEnvironment.MapPath(this.server.DatabaseBackupDirectory);
            if (backupDirectory != null)
            {
                if (Directory.Exists(backupDirectory))
                {
                    DirectoryInfo directory = new DirectoryInfo(backupDirectory);
                    Collection<BackupFile> files = new Collection<BackupFile>();

                    foreach (FileInfo fileInfo in directory.GetFiles("*.backup"))
                    {
                        if (fileInfo != null)
                        {
                            BackupFile file = new BackupFile();
                            string downloadPath = this.ResolveUrl(Path.Combine(this.server.DatabaseBackupDirectory, fileInfo.Name));

                            file.Name = string.Format(CultureInfo.InvariantCulture, "<a href='{0}' target='_blank'>{1}</a>", downloadPath, fileInfo.Name);
                            file.CreatedOn = fileInfo.CreationTime;
                            file.LastWrittenOn = fileInfo.LastWriteTime;
                            file.LastAccessedOn = fileInfo.LastAccessTime;

                            files.Add(file);
                        }
                    }

                    this.grid.RowDataBound += this.Grid_RowDataBound;
                    this.grid.DataSource = files;
                    this.grid.DataBind();
                }
            }
        }

        private void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Assembly ass = Assembly.GetAssembly(typeof (DatabaseBackup));

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = LocalizationHelper.GetResourceString(ass, "MixERP.Net.Core.Modules.BackOffice.Resources.Titles", e.Row.Cells[i].Text);
                }
            }
        }

        private void CreateCloseIcon(HtmlGenericControl container)
        {
            using (HtmlGenericControl closeIcon = new HtmlGenericControl("i"))
            {
                closeIcon.Attributes.Add("class", "close icon");
                container.Controls.Add(closeIcon);
            }
        }

        private void CreateGridModal(Control container)
        {
            using (HtmlGenericControl largeModal = new HtmlGenericControl("div"))
            {
                largeModal.Attributes.Add("class", "ui large modal");

                this.CreateCloseIcon(largeModal);
                this.CreateModalHeader(largeModal);
                this.CreateModalContent(largeModal);
                this.CreateModalActions(largeModal);

                container.Controls.Add(largeModal);
            }
        }

        private void CreateModalActions(HtmlGenericControl container)
        {
            using (HtmlGenericControl actions = new HtmlGenericControl("div"))
            {
                actions.Attributes.Add("class", "actions");
                using (HtmlGenericControl closeButton = new HtmlGenericControl("div"))
                {
                    closeButton.Attributes.Add("class", "ui black button");
                    closeButton.InnerText = Titles.Close;
                    actions.Controls.Add(closeButton);
                }

                container.Controls.Add(actions);
            }
        }

        private void CreateModalContent(HtmlGenericControl container)
        {
            using (HtmlGenericControl content = new HtmlGenericControl("div"))
            {
                content.Attributes.Add("class", "content");

                this.grid = new MixERPGridView(false);
                this.grid.CssClass += "relaxed";
                this.BindGridView();

                content.Controls.Add(this.grid);
                container.Controls.Add(content);
            }
        }

        private void CreateModalHeader(HtmlGenericControl container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("div"))
            {
                header.Attributes.Add("class", "header");

                using (HtmlGenericControl databaseIcon = new HtmlGenericControl("i"))
                {
                    databaseIcon.Attributes.Add("class", "database icon");
                    header.Controls.Add(databaseIcon);
                }

                header.InnerText = Titles.DatabaseBackups;
                container.Controls.Add(header);
            }
        }

        internal class BackupFile
        {
            public DateTime CreatedOn { get; set; }
            public DateTime LastAccessedOn { get; set; }
            public DateTime LastWrittenOn { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region IDisposable

        private bool disposed;
        private MixERPGridView grid;

        public override sealed void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                base.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.grid != null)
            {
                this.grid.RowDataBound -= this.Grid_RowDataBound;
                this.grid.Dispose();
                this.grid = null;
            }

            this.disposed = true;
        }

        #endregion
    }
}