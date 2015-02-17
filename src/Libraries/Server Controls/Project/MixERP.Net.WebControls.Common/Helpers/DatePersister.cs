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
using System.Linq;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;

namespace MixERP.Net.WebControls.Common.Helpers
{
    internal static class DatePersister
    {
        public static ApplicationDateModel GetApplicationDates(int officeId)
        {
            Collection<ApplicationDateModel> applicationDates = CacheFactory.GetApplicationDates();
            bool persist = false;

            if (applicationDates == null || applicationDates.Count.Equals(0))
            {
                applicationDates = Data.Frequency.GetApplicationDates();
                persist = true;
            }
            else
            {
                for (int i = 0; i < applicationDates.Count; i++)
                {
                    if (applicationDates[i].NewDayStarted)
                    {
                        int modelOfficeId = applicationDates[i].OfficeId;

                        applicationDates.Remove(applicationDates[i]);
                        applicationDates.Add(Data.Frequency.GetApplicationDates(modelOfficeId));
                        persist = true;
                    }
                }
            }

            if (persist)
            {
                CacheFactory.SetApplicationDates(applicationDates);
            }

            return applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));
        }
    }
}