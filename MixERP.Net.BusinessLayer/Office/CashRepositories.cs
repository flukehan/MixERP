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
using System.Collections.ObjectModel;
using MixERP.Net.Common.Models.Office;

namespace MixERP.Net.BusinessLayer.Office
{
    public static class CashRepositories
    {
        public static Collection<CashRepository> GetCashRepositories()
        {
            return DatabaseLayer.Office.CashRepositories.GetCashRepositories();
        }

        public static Collection<CashRepository> GetCashRepositories(int officeId)
        {
            //TODO: Bind this instance to a collection of entities.
            return DatabaseLayer.Office.CashRepositories.GetCashRepositories(officeId);
        }

        public static decimal GetBalance(int cashRepositoryId)
        {
            return DatabaseLayer.Office.CashRepositories.GetBalance(cashRepositoryId);
        }

        public static decimal GetBalance(string cashRepositoryCode)
        {
            return DatabaseLayer.Office.CashRepositories.GetBalance(cashRepositoryCode);
        }

        public static bool CashRepositoryCodeExists(string cashRepositoryCode)
        {
            return DatabaseLayer.Office.CashRepositories.CashRepositoryCodeExists(cashRepositoryCode);
        }
    }
}
