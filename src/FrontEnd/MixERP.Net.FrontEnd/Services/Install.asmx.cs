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
using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using Serilog;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Install : WebService
    {
        [WebMethod]
        public bool SaveOffice(string officeCode, string officeName, string nickName, string registrationDate,
            string currencyCode, string currencySymbol, string currencyName, string hundredthName, string fiscalYearCode,
            string fiscalYearName, string startsFrom, string endsOn, string adminName,
            string username, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(officeName) || string.IsNullOrWhiteSpace(officeCode) ||
                string.IsNullOrWhiteSpace(nickName) || string.IsNullOrWhiteSpace(registrationDate) ||
                string.IsNullOrWhiteSpace(currencyCode) || string.IsNullOrWhiteSpace(currencySymbol) ||
                string.IsNullOrWhiteSpace(currencyName) || string.IsNullOrWhiteSpace(hundredthName) ||
                string.IsNullOrWhiteSpace(fiscalYearCode) || string.IsNullOrWhiteSpace(fiscalYearName) ||
                string.IsNullOrWhiteSpace(startsFrom) || string.IsNullOrWhiteSpace(endsOn) ||
                string.IsNullOrWhiteSpace(adminName) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                throw new MixERPException(Labels.AllFieldsRequired);
            }

            if (password != confirmPassword)
            {
                throw new MixERPException(Warnings.ConfirmationPasswordDoesNotMatch);
            }


            DateTime dateOfRegistration = Convert.ToDateTime(registrationDate);
            DateTime fiscalYearStartDate = Convert.ToDateTime(startsFrom);
            DateTime fiscalYearEndDate = Convert.ToDateTime(endsOn);

            if (fiscalYearStartDate > fiscalYearEndDate)
            {
                throw new MixERPException(Warnings.StartDateGreaterThanEndDate);
            }

            try
            {
                Data.Office.Offices.SaveOffice(AppUsers.GetCurrentUserDB(), officeCode, officeName, nickName,
                    dateOfRegistration, currencyCode,
                    currencySymbol, currencyName, hundredthName, fiscalYearCode, fiscalYearName, fiscalYearStartDate, fiscalYearEndDate, adminName, username, password);

                return true;
            }
            catch (Exception ex)
            {
                Log.Warning("Could not save office. {Exception}", ex);
                throw;
            }
        }
    }
}