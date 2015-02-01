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
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.Common.Base;
using MixERP.Net.Entities;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Admin
{
    public static class User
    {
        public static IEnumerable<Entities.Office.User> GetUsers()
        {
            return Factory.Get<Entities.Office.User>("SELECT * FROM office.users ORDER BY user_name;");
        }

        public static void ChangePassword(string userName, string password)
        {
            try
            {
                Factory.NonQuery("SELECT * FROM policy.change_password(@0, @1);", userName, password);
            }
            catch (DbException ex)
            {                
                throw new MixERPException(ex.Message, ex);
            }
        }
    }
}
