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

using MixERP.Net.ApplicationState;
using System.Collections.ObjectModel;
using System.Linq;

namespace MixERP.Net.WebControls.Common.Helpers
{
    public static class DatePersister
    {
        public static FrequencyDates GetFrequencyDates(string catalog, int officeId)
        {
            Collection<FrequencyDates> applicationDates = Dates.GetFrequencyDates(catalog);
            bool persist = false;

            if (applicationDates == null || applicationDates.Count.Equals(0))
            {
                applicationDates = Data.Frequency.GetFrequencyDates(catalog);
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
                        applicationDates.Add(Data.Frequency.GetFrequencyDates(catalog, modelOfficeId));
                        persist = true;
                    }
                }
            }

            if (persist)
            {
                Dates.SetApplicationDates(catalog, applicationDates);
            }

            return applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));
        }
    }
}