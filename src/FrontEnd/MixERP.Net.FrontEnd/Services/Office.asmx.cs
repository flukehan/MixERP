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

using MixERP.Net.Entities.Office;
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Office : WebService
    {
        [WebMethod]
        public IEnumerable<DbGetOfficesResult> GetOffices(string catalog)
        {
            IEnumerable<DbGetOfficesResult> offices;

            try
            {
                offices = Data.Office.Offices.GetOffices(catalog);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new MixERPException(Titles.AccessIsDenied);
            }

            return offices;
        }
    }
}