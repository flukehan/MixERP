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
using System.Linq;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;

namespace MixERP.Net.WebControls.Common.Helpers
{
    internal static class DatePersister
    {
        public static DateTime GetDate()
        {
            ApplicationDateModel model = GetApplicationDates();

            return (model == null) ? DateTime.Today : model.Today;
        }

        public static DateTime GetFiscalHalfEndDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.FiscalHalfEndDate;
        }

        public static DateTime GetFiscalHalfStartDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.FiscalHalfStartDate;
        }

        public static DateTime GetFiscalYearEndDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.FiscalYearEndDate;
        }

        public static DateTime GetFiscalYearStartDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.FiscalYearStartDate;
        }

        public static DateTime GetMonthEndtDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.MonthEndDate;
        }

        public static DateTime GetMonthStartDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.MonthStartDate;
        }

        public static DateTime GetQuarterEndDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.QuarterEndDate;
        }

        public static DateTime GetQuarterStartDate()
        {
            ApplicationDateModel model = GetApplicationDates();
            return (model == null) ? DateTime.Today : model.QuarterStartDate;
        }

        private static ApplicationDateModel GetApplicationDates()
        {
            int officeId = SessionHelper.GetOfficeId();
            Collection<ApplicationDateModel> applicationDates = ApplicationStateHelper.GetApplicationDates();
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
                ApplicationStateHelper.SetApplicationDates(applicationDates);
            }

            return applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));
        }
    }
}