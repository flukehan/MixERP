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
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models;
using MixERP.Net.FrontEnd.Cache;
using Serilog;

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
            try
            {
                if (!CurrentUser.GetSignInView().IsAdmin.ToBool())
                {
                    return false;
                }

                int userId = CurrentUser.GetSignInView().UserId.ToInt();
                int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();

                Data.EODOperation.Initialize(userId, officeId);

                ForceLogOff(officeId);

                return true;
            }
            catch (Exception ex)
            {
                Log.Warning("Could not initialize eod operation. {Exception}", ex);
                throw;
            }
        }

        [WebMethod(EnableSession = true)]
        public void StartNewDay()
        {
            try
            {
                SuggestDateReload();
            }
            catch (Exception ex)
            {
                Log.Warning("Could not start a new day. {Exception}", ex);
                throw;
            }
        }

        private static void ForceLogOff(int officeId)
        {
            Collection<ApplicationDateModel> applicationDates = CacheFactory.GetApplicationDates();
            DateTime forcedLogOffOn = DateTime.Now.AddMinutes(2);

            if (applicationDates != null)
            {
                ApplicationDateModel model = applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));

                if (model != null)
                {
                    ApplicationDateModel item = model.Clone() as ApplicationDateModel;
                    if (item != null)
                    {
                        item.ForcedLogOffTimestamp = forcedLogOffOn;
                        item.NewDayStarted = false;

                        applicationDates.Add(item);
                        applicationDates.Remove(model);
                    }


                    CacheFactory.SetApplicationDates(applicationDates);
                }
            }
        }

        private static void SuggestDateReload()
        {
            int officeId = CurrentUser.GetSignInView().OfficeId.ToInt();
            Collection<ApplicationDateModel> applicationDates = CacheFactory.GetApplicationDates();

            if (applicationDates != null)
            {
                ApplicationDateModel model = applicationDates.FirstOrDefault(c => c.OfficeId.Equals(officeId));
                if (model != null)
                {
                    ApplicationDateModel item = model.Clone() as ApplicationDateModel;
                    if (item != null)
                    {
                        item.NewDayStarted = true;

                        applicationDates.Add(item);
                        applicationDates.Remove(model);
                    }


                    CacheFactory.SetApplicationDates(applicationDates);
                }
            }
        }
    }
}