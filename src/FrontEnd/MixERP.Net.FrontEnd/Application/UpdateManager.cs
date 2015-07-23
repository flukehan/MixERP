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
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Updater.Api;
using Serilog;

namespace MixERP.Net.FrontEnd.Application
{
    internal static class UpdateManager
    {
        internal static async void CheckForUpdates(HttpApplicationState application)
        {
            bool autoSuggestUpdate =
                Conversion.TryCastBoolean(ConfigurationHelper.GetUpdaterParameter("AutoSuggestUpdate"));

            if (autoSuggestUpdate)
            {
                try
                {
                    Updater.UpdateManager updater = new Updater.UpdateManager();
                    Release release = await updater.GetLatestRelease();

                    if (release != null)
                    {
                        application["UpdateAvailable"] = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Exception occurred. {Exception}.", ex);
                }
            }
        }
    }
}