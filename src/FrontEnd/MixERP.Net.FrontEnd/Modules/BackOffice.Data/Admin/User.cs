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

using System.Collections.Generic;
using System.Data.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Entities;

namespace MixERP.Net.Core.Modules.BackOffice.Data.Admin
{
    /// <summary>The user class.</summary>
    public class User
    {
        /// <summary>Gets the collection of all users.</summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the users in this collection.
        /// </returns>

        public static IEnumerable<Entities.Office.UserSelectorView> GetUserSelectorView()
        {
            return Factory.Get<Entities.Office.UserSelectorView>("SELECT * FROM office.user_selector_view ORDER BY user_name;");
        }

        /// <summary>Change password of the user to a new one.</summary>
        /// <exception cref="MixERPException">Thrown when a MixERPException occurs.</exception>
        /// <param name="adminUserId">The UserId of the administration who is changing password for this user.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The new password.</param>
        public void SetNewPassword(int adminUserId, string username, string password)
        {
            try
            {
                Factory.NonQuery("SELECT * FROM policy.change_password(@0, @1, @2);", adminUserId, username, password);
            }
            catch (DbException ex)
            {
                throw new MixERPException(ex.Message, ex);
            }
        }
    }
}