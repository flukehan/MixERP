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
using System.Text;
using System.Web.UI;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

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
            string script = "var searchInput = $('#SearchInput');";

            script += this.GetVariable("today", DateTime.Now.ToShortDateString());
            script += this.GetVariable("now", DateTime.Now.ToString(CultureInfo.InvariantCulture));

            script += this.GetVariable("user", CurrentSession.GetUserName());
            script += this.GetVariable("office", CurrentSession.GetOfficeName());

            script += this.GetVariable("shortDateFormat", LocalizationHelper.GetShortDateFormat());
            script += this.GetVariable("longDateFormat", LocalizationHelper.GetLongDateFormat());

            script += this.GetVariable("thousandSeparator", LocalizationHelper.GetThousandSeparator());
            script += this.GetVariable("decimalSeparator", LocalizationHelper.GetDecimalSeparator());
            script += this.GetVariable("currencyDecimalPlaces", LocalizationHelper.GetCurrencyDecimalPlaces());
            script += this.GetVariable("currencySymbol", LocalizationHelper.GetCurrencySymbol());

            script += this.GetVariable("duplicateEntryLocalized", Resources.Warnings.DuplicateEntry);
            script += this.GetVariable("selectLocalized", Resources.Titles.Select);
            script += this.GetVariable("noneLocalized", Resources.Titles.None);
            script += this.GetVariable("invalidDateWarningLocalized", Resources.Warnings.InvalidDate);
            script += this.GetVariable("areYouSureLocalized", Resources.Questions.AreYouSure);
            script += this.GetVariable("nothingSelectedLocalized", Resources.Warnings.NothingSelected);
            script += this.GetVariable("yesLocalized", Resources.Titles.Yes);
            script += this.GetVariable("noLocalized", Resources.Titles.No);

            script += this.GetVariable("daysLowerCaseLocalized", Resources.Labels.DaysLowerCase);
            script += this.GetVariable("gridViewEmptyWarningLocalized", Resources.Warnings.GridViewEmpty);

            script += this.GetVariable("duplicateFileLocalized", Resources.Warnings.DuplicateFiles);

            script += this.GetVariable("taskCompletedSuccessfullyLocalized", Resources.Labels.TaskCompletedSuccessfully);


            script += this.GetVariable("today", DateTime.Now.ToShortDateString());
            script += this.GetVariable("shortDateFormat", LocalizationHelper.GetShortDateFormat());
            script += this.GetVariable("thousandSeparator", LocalizationHelper.GetThousandSeparator());
            script += this.GetVariable("decimalSeparator", LocalizationHelper.GetDecimalSeparator());
            script += this.GetVariable("currencyDecimalPlaces", LocalizationHelper.GetCurrencyDecimalPlaces());

            script += this.GetVariable("duplicateEntryLocalized", Resources.Warnings.DuplicateEntry);

            script += this.GetVariable("selectLocalized", Resources.Titles.Select);
            script += this.GetVariable("noneLocalized", Resources.Titles.None);
            script += this.GetVariable("invalidDateWarningLocalized", Resources.Warnings.InvalidDate);
            script += this.GetVariable("areYouSureLocalized", Resources.Questions.AreYouSure);
            script += this.GetVariable("nothingSelectedLocalized", Resources.Warnings.NothingSelected);
            script += this.GetVariable("yesLocalized", Resources.Titles.Yes);
            script += this.GetVariable("noLocalized", Resources.Titles.No);

            script += this.GetVariable("daysLowerCaseLocalized", Resources.Labels.DaysLowerCase);
            script += this.GetVariable("gridViewEmptyWarningLocalized", Resources.Warnings.GridViewEmpty);

            script += this.GetVariable("duplicateFileLocalized", Resources.Warnings.DuplicateFiles);

            script += this.GetVariable("taskCompletedSuccessfullyLocalized", Resources.Labels.TaskCompletedSuccessfully);

            PageUtility.RegisterJavascript("MixERP_MasterBase", script, this.Page, true);

        }

        private string GetVariable(string name, object value)
        {
            string script = "var {0}='{1}';";
            script = string.Format(CultureInfo.InvariantCulture, script, name, value);
            return script;
        }
    }
}