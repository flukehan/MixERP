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
using System.Globalization;
using System.Threading;
using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.i18n.Resources;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPMasterPage : MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            this.RegisterJavascript();
            base.OnLoad(e);
        }

        private void RegisterJavascript()
        {
            string script = JSUtility.GetVar("culture", Thread.CurrentThread.CurrentCulture.Name);
            script += JSUtility.GetVar("language", Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            script += JSUtility.GetVar("jqueryUIi18nPath", this.Page.ResolveUrl("~/Scripts/jquery-ui/i18n/"));

            script += JSUtility.GetVar("today", DateTime.Now.ToShortDateString());
            script += JSUtility.GetVar("now", DateTime.Now.ToString(CultureInfo.InvariantCulture));

            script += JSUtility.GetVar("user", AppUsers.GetCurrentLogin().View.UserName);
            script += JSUtility.GetVar("office", AppUsers.GetCurrentLogin().View.OfficeName);

            script += JSUtility.GetVar("shortDateFormat", LocalizationHelper.GetShortDateFormat());
            script += JSUtility.GetVar("longDateFormat", LocalizationHelper.GetLongDateFormat());

            script += JSUtility.GetVar("thousandSeparator", LocalizationHelper.GetThousandSeparator());
            script += JSUtility.GetVar("decimalSeparator", LocalizationHelper.GetDecimalSeparator());
            script += JSUtility.GetVar("currencyDecimalPlaces", LocalizationHelper.GetCurrencyDecimalPlaces());
            script += JSUtility.GetVar("currencySymbol", LocalizationHelper.GetCurrencySymbol());

            script += JSUtility.GetVar("duplicateEntryLocalized", Warnings.DuplicateEntry);
            script += JSUtility.GetVar("selectLocalized", Titles.Select);
            script += JSUtility.GetVar("noneLocalized", Titles.None);
            script += JSUtility.GetVar("invalidDateWarningLocalized", Warnings.InvalidDate);
            script += JSUtility.GetVar("areYouSureLocalized", Questions.AreYouSure);
            script += JSUtility.GetVar("nothingSelectedLocalized", Warnings.NothingSelected);
            script += JSUtility.GetVar("yesLocalized", Titles.Yes);
            script += JSUtility.GetVar("noLocalized", Titles.No);

            script += JSUtility.GetVar("daysLowerCaseLocalized", Labels.DaysLowerCase);
            script += JSUtility.GetVar("gridViewEmptyWarningLocalized", Warnings.GridViewEmpty);

            script += JSUtility.GetVar("duplicateFileLocalized", Warnings.DuplicateFiles);

            script += JSUtility.GetVar("taskCompletedSuccessfullyLocalized", Labels.TaskCompletedSuccessfully);


            script += JSUtility.GetVar("today", DateTime.Now.ToShortDateString());
            script += JSUtility.GetVar("shortDateFormat", LocalizationHelper.GetShortDateFormat());
            script += JSUtility.GetVar("thousandSeparator", LocalizationHelper.GetThousandSeparator());
            script += JSUtility.GetVar("decimalSeparator", LocalizationHelper.GetDecimalSeparator());
            script += JSUtility.GetVar("currencyDecimalPlaces", LocalizationHelper.GetCurrencyDecimalPlaces());

            script += JSUtility.GetVar("duplicateEntryLocalized", Warnings.DuplicateEntry);

            script += JSUtility.GetVar("selectLocalized", Titles.Select);
            script += JSUtility.GetVar("noneLocalized", Titles.None);
            script += JSUtility.GetVar("invalidDateWarningLocalized", Warnings.InvalidDate);
            script += JSUtility.GetVar("areYouSureLocalized", Questions.AreYouSure);
            script += JSUtility.GetVar("nothingSelectedLocalized", Warnings.NothingSelected);
            script += JSUtility.GetVar("yesLocalized", Titles.Yes);
            script += JSUtility.GetVar("noLocalized", Titles.No);

            script += JSUtility.GetVar("daysLowerCaseLocalized", Labels.DaysLowerCase);
            script += JSUtility.GetVar("gridViewEmptyWarningLocalized", Warnings.GridViewEmpty);

            script += JSUtility.GetVar("duplicateFileLocalized", Warnings.DuplicateFiles);
            script += JSUtility.GetVar("taskCompletedSuccessfullyLocalized", Labels.TaskCompletedSuccessfully);
            script += JSUtility.GetVar("itemsLocalized", Titles.Items);
            script += JSUtility.GetVar("compoundItemsLocalized", Titles.CompoundItems);
            script += JSUtility.GetVar("addLocalized", Titles.Add);
            script += JSUtility.GetVar("updateLocalized", Titles.Update);

            script += JSUtility.GetVar("catalog", AppUsers.GetCurrentUserDB());

            if (Conversion.TryCastBoolean(Application["UpdateAvailable"]) && AppUsers.GetCurrentLogin().View.IsAdmin.ToBool())
            {
                script += JSUtility.GetVar("update", "1");
            }

            PageUtility.RegisterJavascript("MixERPMasterPage", script, this.Page, true);
        }
    }
}