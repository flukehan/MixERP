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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n;
using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;

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

            script += JSUtility.GetVar("user", AppUsers.GetCurrent().View.UserName);
            script += JSUtility.GetVar("office", AppUsers.GetCurrent().View.OfficeName);

            script += JSUtility.GetVar("shortDateFormat", CurrentCulture.GetShortDateFormat());
            script += JSUtility.GetVar("longDateFormat", CurrentCulture.GetLongDateFormat());

            script += JSUtility.GetVar("thousandSeparator", CurrentCulture.GetThousandSeparator());
            script += JSUtility.GetVar("decimalSeparator", CurrentCulture.GetDecimalSeparator());
            script += JSUtility.GetVar("currencyDecimalPlaces", CurrentCulture.GetCurrencyDecimalPlaces());
            script += JSUtility.GetVar("currencySymbol", CurrentCulture.GetCurrencySymbol());


            script += JSUtility.GetVar("today", DateTime.Now.ToShortDateString());
            script += JSUtility.GetVar("shortDateFormat", CurrentCulture.GetShortDateFormat());
            script += JSUtility.GetVar("thousandSeparator", CurrentCulture.GetThousandSeparator());
            script += JSUtility.GetVar("decimalSeparator", CurrentCulture.GetDecimalSeparator());
            script += JSUtility.GetVar("currencyDecimalPlaces", CurrentCulture.GetCurrencyDecimalPlaces());
            script += JSUtility.GetVar("baseCurrencyCode", AppUsers.GetCurrent().View.CurrencyCode);


            script += JSUtility.GetVar("catalog", AppUsers.GetCurrentUserDB());

            script += JSUtility.GetVar("update", this.Update());

            PageUtility.RegisterJavascript("MixERPMasterPage", script, this.Page, true);
        }

        private int Update()
        {
            if (Conversion.TryCastBoolean(Application["UpdateAvailable"]) &&
                AppUsers.GetCurrent().View.IsAdmin.ToBool())
            {
                return 1;
            }

            return 0;
        }
    }
}