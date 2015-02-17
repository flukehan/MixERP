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
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Cache;

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

            script += JSUtility.GetVar("user", CurrentUser.GetSignInView().UserName);
            script += JSUtility.GetVar("office", CurrentUser.GetSignInView().OfficeName);

            script += JSUtility.GetVar("shortDateFormat", LocalizationHelper.GetShortDateFormat());
            script += JSUtility.GetVar("longDateFormat", LocalizationHelper.GetLongDateFormat());

            script += JSUtility.GetVar("thousandSeparator", LocalizationHelper.GetThousandSeparator());
            script += JSUtility.GetVar("decimalSeparator", LocalizationHelper.GetDecimalSeparator());
            script += JSUtility.GetVar("currencyDecimalPlaces", LocalizationHelper.GetCurrencyDecimalPlaces());
            script += JSUtility.GetVar("currencySymbol", LocalizationHelper.GetCurrencySymbol());

            script += JSUtility.GetVar("duplicateEntryLocalized", Resources.Warnings.DuplicateEntry);
            script += JSUtility.GetVar("selectLocalized", Resources.Titles.Select);
            script += JSUtility.GetVar("noneLocalized", Resources.Titles.None);
            script += JSUtility.GetVar("invalidDateWarningLocalized", Resources.Warnings.InvalidDate);
            script += JSUtility.GetVar("areYouSureLocalized", Resources.Questions.AreYouSure);
            script += JSUtility.GetVar("nothingSelectedLocalized", Resources.Warnings.NothingSelected);
            script += JSUtility.GetVar("yesLocalized", Resources.Titles.Yes);
            script += JSUtility.GetVar("noLocalized", Resources.Titles.No);

            script += JSUtility.GetVar("daysLowerCaseLocalized", Resources.Labels.DaysLowerCase);
            script += JSUtility.GetVar("gridViewEmptyWarningLocalized", Resources.Warnings.GridViewEmpty);

            script += JSUtility.GetVar("duplicateFileLocalized", Resources.Warnings.DuplicateFiles);

            script += JSUtility.GetVar("taskCompletedSuccessfullyLocalized", Resources.Labels.TaskCompletedSuccessfully);


            script += JSUtility.GetVar("today", DateTime.Now.ToShortDateString());
            script += JSUtility.GetVar("shortDateFormat", LocalizationHelper.GetShortDateFormat());
            script += JSUtility.GetVar("thousandSeparator", LocalizationHelper.GetThousandSeparator());
            script += JSUtility.GetVar("decimalSeparator", LocalizationHelper.GetDecimalSeparator());
            script += JSUtility.GetVar("currencyDecimalPlaces", LocalizationHelper.GetCurrencyDecimalPlaces());

            script += JSUtility.GetVar("duplicateEntryLocalized", Resources.Warnings.DuplicateEntry);

            script += JSUtility.GetVar("selectLocalized", Resources.Titles.Select);
            script += JSUtility.GetVar("noneLocalized", Resources.Titles.None);
            script += JSUtility.GetVar("invalidDateWarningLocalized", Resources.Warnings.InvalidDate);
            script += JSUtility.GetVar("areYouSureLocalized", Resources.Questions.AreYouSure);
            script += JSUtility.GetVar("nothingSelectedLocalized", Resources.Warnings.NothingSelected);
            script += JSUtility.GetVar("yesLocalized", Resources.Titles.Yes);
            script += JSUtility.GetVar("noLocalized", Resources.Titles.No);

            script += JSUtility.GetVar("daysLowerCaseLocalized", Resources.Labels.DaysLowerCase);
            script += JSUtility.GetVar("gridViewEmptyWarningLocalized", Resources.Warnings.GridViewEmpty);

            script += JSUtility.GetVar("duplicateFileLocalized", Resources.Warnings.DuplicateFiles);

            script += JSUtility.GetVar("taskCompletedSuccessfullyLocalized", Resources.Labels.TaskCompletedSuccessfully);

            PageUtility.RegisterJavascript("MixERPMasterPage", script, this.Page, true);
        }
    }
}