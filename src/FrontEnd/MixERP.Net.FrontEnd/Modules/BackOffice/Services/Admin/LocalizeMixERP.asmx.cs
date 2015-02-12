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

using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.Core.Modules.BackOffice.Services.Admin
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class LocalizeMixERP : WebService
    {
        [WebMethod(EnableSession = true)]
        public void Save(string key, string value)
        {
            int userId = CurrentSession.GetUserId();
            if (userId.Equals(0))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            const string sessionKey = BackOffice.Admin.LocalizeMixERP.sessionKey;
            string cultureCode = Conversion.TryCastString(SessionHelper.GetSessionKey(sessionKey));

            if (string.IsNullOrWhiteSpace(cultureCode))
            {
                return;
            }

            Data.Admin.LocalizeMixERP.Save(cultureCode, key, value);
        }
    }
}