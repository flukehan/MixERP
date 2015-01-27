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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.DbFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Office;
using CashRepository = MixERP.Net.Entities.Office.CashRepository;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class CashRepositories
    {
        public static bool CashRepositoryCodeExists(string cashRepositoryCode)
        {
            return Factory.Get<CashRepository>("SELECT * FROM office.cash_repositories WHERE cash_repository_code=@0;", cashRepositoryCode).Count().Equals(1);
        }

        public static decimal GetBalance(int cashRepositoryId, string currencyCode)
        {
            return Factory.Scalar<decimal>("SELECT transactions.get_cash_repository_balance(@0, @1);", cashRepositoryId, currencyCode);
        }

        public static decimal GetBalance(int cashRepositoryId)
        {
            return Factory.Scalar<decimal>("SELECT transactions.get_cash_repository_balance(@0);", cashRepositoryId);
        }

        public static decimal GetBalance(string cashRepositoryCode)
        {
            return Factory.Scalar<decimal>("SELECT transactions.get_cash_repository_balance(office.get_cash_repository_id_by_cash_repository_code(@0));", cashRepositoryCode);
        }

        public static decimal GetBalance(string cashRepositoryCode, string currencyCode)
        {
            return Factory.Scalar<decimal>("SELECT transactions.get_cash_repository_balance(office.get_cash_repository_id_by_cash_repository_code(@0), @1);", cashRepositoryCode, currencyCode);
        }

        public static IEnumerable<CashRepository> GetCashRepositories()
        {
            return Factory.Get<CashRepository>("SELECT * FROM office.cash_repositories ORDER BY cash_repository_id;");
        }

        public static IEnumerable<CashRepository> GetCashRepositories(int officeId)
        {
            return Factory.Get<CashRepository>("SELECT * FROM office.cash_repositories WHERE office_id=@0 ORDER BY cash_repository_id;", officeId);
        }

        public static CashRepository GetCashRepository(int? cashRepositoryId)
        {
            return Factory.Get<CashRepository>("SELECT * FROM office.cash_repositories WHERE cash_repository_id=@0;", cashRepositoryId).FirstOrDefault();
        }

        private static Office GetOffice(int? officeId)
        {
            return Factory.Get<Office>("SELECT * FROM office.offices WHERE office_id=@0;", officeId).FirstOrDefault();
        }

    }
}