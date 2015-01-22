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
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.SignalR;
using MixERP.Net.Common;
using MixERP.Net.Common.Models;
using MixERP.Net.Core.Modules.BackOffice.Resources;

namespace MixERP.Net.Core.Modules.BackOffice.Hubs
{
    public class DbHub : Hub
    {
        public void BackupDatabase(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                this.Clients.Caller.backupFailed(Warnings.NoFileSpecified);
                return;
            }

            PostgreSQLServer server = new PostgreSQLServer();
            server.Validate();

            if (server.IsValid && !string.IsNullOrWhiteSpace(server.BinDirectory) && !string.IsNullOrWhiteSpace(server.DatabaseBackupDirectory))
            {
                this.Backup(server, fileName);
                return;
            }

            this.Clients.Caller.backupFailed(Warnings.ConfigurationError);
        }

        private void Backup(PostgreSQLServer server, string fileName)
        {
            string pgdumpPath = Path.Combine(server.BinDirectory, "pg_dump.exe");
            string backupDirectory = HostingEnvironment.MapPath(server.DatabaseBackupDirectory);

            if (backupDirectory != null)
            {
                string path = Path.Combine(backupDirectory, fileName + ".bakup");
                var result=this.BackupDatabase(pgdumpPath, server, path);

                if (result)
                {
                    StringBuilder message = new StringBuilder();
                    message.Append(Labels.DatabaseBackupSuccessful);
                    message.Append("&nbsp;");
                    message.Append("<a href='");
                    message.Append(PageUtility.ResolveUrl(Path.Combine(server.DatabaseBackupDirectory, fileName + ".bakup")));
                    message.Append("'");
                    message.Append(" target='_blank'>");
                    message.Append(Labels.ClickHereToDownload);
                    message.Append("</a>");

                    this.Clients.Caller.backupCompleted(message.ToString());
                    return;
                }

                this.Clients.Caller.backupFailed(Warnings.CannotCreateABackup);
            }
        }

        private bool BackupDatabase(string pgDumpPath, PostgreSQLServer server, string fileName)
        {

            var batchFile = this.CreateBatchFile(server, pgDumpPath, fileName);

            try
            {

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = batchFile;

                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.ErrorDialog = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.ErrorDataReceived += this.Data_Received;
                    process.OutputDataReceived += this.Data_Received;
                    process.Disposed += this.Completed;

                    process.Start();

                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();

                    process.WaitForExit();
                    return true;

                }
            }
            finally
            {
                this.RemoveFile(batchFile);
            }
        }

        private void RemoveFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch
            {
                //Yeah! Ignore the exception.
            }
        }

        private string CreateBatchFile(PostgreSQLServer server, string pgDumpPath, string fileName)
        {
            Collection<string> commands = new Collection<string>();
            commands.Add("@echo off");
            commands.Add("SET PGPASSWORD=" + server.Password);            
            string command = @"""{0}"" --host ""{1}"" --port {2} --username ""{3}"" --format custom --blobs --verbose --file ""{4}"" ""{5}""";
            command = string.Format(CultureInfo.InvariantCulture, command, pgDumpPath, server.HostName, server.PortNumber, server.UserId, fileName, server.DatabaseName);
            commands.Add(command);
            commands.Add("exit");

            string batchFilePath = fileName + ".bat";

            File.WriteAllText(batchFilePath, string.Join(Environment.NewLine, commands));
            return batchFilePath;
        }

        private void Completed(object sender, EventArgs e)
        {
            this.Clients.Caller.backupCompleted(string.Empty);
        }

        private void Data_Received(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                this.Clients.Caller.getNotification(e.Data);
            }
        }





    }
}