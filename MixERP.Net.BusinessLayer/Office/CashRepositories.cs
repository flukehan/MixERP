/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
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
    }
}
