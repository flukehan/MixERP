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

using System.Collections.ObjectModel;
using System.Web;
using MixERP.Net.Common.Models.Core;

namespace MixERP.Net.Common.Helpers
{
    public static class ApplicationStateHelper
    {
        private const string appDatesKey = "ApplicationDates";

        public static Collection<ApplicationDateModel> GetApplicationDates()
        {
            return (Collection<ApplicationDateModel>)GetApplicationValueByKey(appDatesKey);
        }

        public static void SetApplicationDates(Collection<ApplicationDateModel> applicationDates)
        {
            AddApplicationKey(appDatesKey, applicationDates);
        }

        private static void AddApplicationKey(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            HttpApplicationState application = HttpContext.Current.Application;
            {
                if (application[key] == null)
                {
                    application[key] = value;
                }
                else
                {
                    application.Add(key, value);
                }
            }
        }

        private static object GetApplicationValueByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            HttpApplicationState application = HttpContext.Current.Application;
            {
                if (application[key] != null)
                {
                    return application[key];
                }
            }

            return null;
        }
    }
}