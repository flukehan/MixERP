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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Menu : WebService
    {
        [WebMethod]
        public string GetMenus()
        {
            LoginView view = AppUsers.GetCurrent().View;

            string catalog = AppUsers.GetCurrentUserDB();
            int userId = view.UserId.ToInt();
            int officeId = view.OfficeId.ToInt();
            string culture = view.Culture;

            Navigation nav = new Navigation(catalog, userId, officeId, culture);
            var menus = nav.GetMenus();
            return JsonConvert.SerializeObject(menus);
        }
    }
}