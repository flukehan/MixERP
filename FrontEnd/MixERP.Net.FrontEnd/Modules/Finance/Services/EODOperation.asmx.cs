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
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;

namespace MixERP.Net.Core.Modules.Finance.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class EODOperation : WebService
    {
        [WebMethod(EnableSession = true)]
        public bool InitializeEODOperation()
        {
            if (!SessionHelper.IsAdmin())
            {
                return false;
            }

            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();

            Data.EODOperation.Initialize(userId, officeId);

            this.ForceLogOff(officeId);

            return true;
        }

        [WebMethod(EnableSession = true)]
        public void StartNewDay()
        {
            this.SuggestDateReload();
        }

        private void ForceLogOff(int officeId)
        {
            Collection<ApplicationDateModel> applicationDates = ApplicationStateHelper.GetApplicationDates();
            DateTime forcedLogOffOn = DateTime.Now.AddMinutes(2);

            if (applicationDates != null)
            {
                ApplicationDateModel model = applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));

                if (model != null)
                {
                    applicationDates.Add(new ApplicationDateModel(model.OfficeId, model.Today, model.MonthStartDate, model.MonthEndDate, model.QuarterStartDate, model.QuarterEndDate, model.FiscalHalfStartDate, model.FiscalHalfEndDate, model.FiscalYearStartDate, model.FiscalYearEndDate, false, forcedLogOffOn));
                    applicationDates.Remove(model);

                    ApplicationStateHelper.SetApplicationDates(applicationDates);
                }
            }
        }

        private void SuggestDateReload()
        {
            int officeId = SessionHelper.GetOfficeId();
            Collection<ApplicationDateModel> applicationDates = ApplicationStateHelper.GetApplicationDates();

            if (applicationDates != null)
            {
                ApplicationDateModel model = applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));
                if (model != null)
                {
                    applicationDates.Add(new ApplicationDateModel(model.OfficeId, model.Today, model.MonthStartDate, model.MonthEndDate, model.QuarterStartDate, model.QuarterEndDate, model.FiscalHalfStartDate, model.FiscalHalfEndDate, model.FiscalYearStartDate, model.FiscalYearEndDate, true));
                    applicationDates.Remove(model);

                    ApplicationStateHelper.SetApplicationDates(applicationDates);
                }
            }
        }
    }
}