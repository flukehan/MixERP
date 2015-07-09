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
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Framework;
using MixERP.Net.ReportManager;
using MixERP.Net.Updater;
using MixERP.Net.Updater.Api;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Routing;
using GlobalLogin = MixERP.Net.Entities.Office.GlobalLogin;

namespace MixERP.Net.FrontEnd
{
    public class Global : HttpApplication
    {
        protected static void RegisterRoutes(RouteCollection routes)
        {
            if (routes != null)
            {
                Log.Information("Registering routes.");
                routes.Ignore("{resource}.axd");
                routes.MapPageRoute("DefaultRoute", "", "~/SignIn.aspx");
                routes.MapPageRoute("Reporting", "Reports/{path}", "~/Reports/ReportMaster.aspx");
                routes.MapPageRoute("Modules", "Modules/{*path}", "~/Modules/Default.aspx");
            }
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Server.GetLastError();

            if (ex == null)
            {
                return;
            }

            if (ex is ThreadAbortException)
            {
                Log.Verbose("The thread was being aborted. {Exception}.", ex);
                return;
            }


            MixERPException exception = ex as MixERPException;

            if (exception != null)
            {
                Log.Verbose("Handling exception.");

                MixERPException.Handle(exception);
                return;
            }

            Log.Error("Exception occurred. {Exception}.", ex);

            var innerException = ex.InnerException as MixERPException;

            if (innerException != null)
            {
                MixERPException.Handle(innerException);
                return;
            }

            if (ex.InnerException != null)
            {
                Log.Error("Inner Exception. {InnerException}.", ex.InnerException);
            }

            throw ex;
        }

        private void Application_Start(object sender, EventArgs e)
        {
            this.IntializeLogger();
            this.CheckForUpdates();
            RegisterRoutes(RouteTable.Routes);

            GlobalLogin.CreateTable();
            Repository.DownloadAndInstallReports();
        }

        private async void CheckForUpdates()
        {
            bool autoSuggestUpdate =
                Conversion.TryCastBoolean(ConfigurationHelper.GetUpdaterParameter("AutoSuggestUpdate"));

            if (autoSuggestUpdate)
            {
                UpdateManager updater = new UpdateManager();
                Release release = await updater.GetLatestRelease();

                if (release != null)
                {
                    Application["UpdateAvailable"] = true;
                }
            }
        }

        private string GetLogDirectory()
        {
            string path = DbConfig.GetMixERPParameter(AppUsers.GetCurrentUserDB(), "ApplicationLogDirectory");

            if (string.IsNullOrWhiteSpace(path))
            {
                return Server.MapPath("~/Resource/Temp");
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private string GetLogFileName()
        {
            string applicationLogDirectory = this.GetLogDirectory();
            string filePath = Path.Combine(applicationLogDirectory,
                DateTime.Now.Date.ToShortDateString().Replace(@"/", "-"), "log.txt");
            return filePath;
        }

        private LoggerConfiguration GetConfiguration()
        {
            string minimumLogLevel = DbConfig.GetMixERPParameter(AppUsers.GetCurrentUserDB(), "MinimumLogLevel");

            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch();

            LogEventLevel logLevel;
            Enum.TryParse(minimumLogLevel, out logLevel);

            levelSwitch.MinimumLevel = logLevel;

            return
                new LoggerConfiguration().MinimumLevel.ControlledBy(levelSwitch)
                    .WriteTo.RollingFile(this.GetLogFileName());
        }

        private void IntializeLogger()
        {
            Log.Logger = this.GetConfiguration().CreateLogger();

            Log.Information("Application started.");
            //Log.Verbose("Appending to log directory: {Directory}.", applicationLogDirectory);
        }
    }
}