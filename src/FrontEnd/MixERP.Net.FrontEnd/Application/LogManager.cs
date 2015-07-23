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
using System.IO;
using System.Web.Hosting;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MixERP.Net.FrontEnd.Application
{
    internal static class LogManager
    {
        private static string GetLogDirectory()
        {
            string path = DbConfig.GetMixERPParameter(AppUsers.GetCurrentUserDB(), "ApplicationLogDirectory");

            if (string.IsNullOrWhiteSpace(path))
            {
                return HostingEnvironment.MapPath("~/Resource/Temp");
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private static string GetLogFileName()
        {
            string applicationLogDirectory = GetLogDirectory();
            string filePath = Path.Combine(applicationLogDirectory,
                DateTime.Now.Date.ToShortDateString().Replace(@"/", "-"), "log.txt");
            return filePath;
        }

        private static LoggerConfiguration GetConfiguration()
        {
            string minimumLogLevel = DbConfig.GetMixERPParameter(AppUsers.GetCurrentUserDB(), "MinimumLogLevel");

            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch();

            LogEventLevel logLevel;
            Enum.TryParse(minimumLogLevel, out logLevel);

            levelSwitch.MinimumLevel = logLevel;

            return
                new LoggerConfiguration().MinimumLevel.ControlledBy(levelSwitch)
                    .WriteTo.RollingFile(GetLogFileName());
        }

        internal static void IntializeLogger()
        {
            Log.Logger = GetConfiguration().CreateLogger();

            Log.Information("Application started.");
        }
    }
}