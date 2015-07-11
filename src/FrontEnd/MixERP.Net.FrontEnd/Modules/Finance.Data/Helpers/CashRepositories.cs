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
using System.Linq;
using MixERP.Net.Entities.Office;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class CashRepositories
    {
        public static bool CashRepositoryCodeExists(string catalog, string cashRepositoryCode)
        {
            const string sql = "SELECT * FROM office.cash_repositories WHERE cash_repository_code=@0;";
            return Factory.Get<CashRepository>(catalog, sql, cashRepositoryCode).Count().Equals(1);
        }

        public static decimal GetBalance(string catalog, int? cashRepositoryId, string currencyCode)
        {
            const string sql =
                "SELECT transactions.get_cash_repository_balance(@0::integer, @1::national character varying(12));";
            return Factory.Scalar<decimal>(catalog, sql, cashRepositoryId, currencyCode);
        }

        public static decimal GetBalance(string catalog, int cashRepositoryId)
        {
            const string sql = "SELECT transactions.get_cash_repository_balance(@0::integer);";
            return Factory.Scalar<decimal>(catalog, sql, cashRepositoryId);
        }

        private static decimal GetBalance(string catalog, string cashRepositoryCode)
        {
            const string sql =
                "SELECT transactions.get_cash_repository_balance(office.get_cash_repository_id_by_cash_repository_code(@0)::integer);";

            return Factory.Scalar<decimal>(catalog, sql, cashRepositoryCode);
        }

        public static decimal GetBalance(string catalog, string cashRepositoryCode, string currencyCode)
        {
            const string sql =
                "SELECT transactions.get_cash_repository_balance(office.get_cash_repository_id_by_cash_repository_code(@0)::integer, @1::national character varying(12));";
            return Factory.Scalar<decimal>(catalog, sql, cashRepositoryCode, currencyCode);
        }

        public static IEnumerable<CashRepository> GetCashRepositories(string catalog)
        {
            const string sql = "SELECT * FROM office.cash_repositories ORDER BY cash_repository_id;";
            return Factory.Get<CashRepository>(catalog, sql);
        }

        public static IEnumerable<CashRepository> GetCashRepositories(string catalog, int officeId)
        {
            const string sql = "SELECT * FROM office.cash_repositories WHERE office_id=@0 ORDER BY cash_repository_id;";
            return Factory.Get<CashRepository>(catalog, sql, officeId);
        }

        public static CashRepository GetCashRepository(string catalog, int? cashRepositoryId)
        {
            const string sql = "SELECT * FROM office.cash_repositories WHERE cash_repository_id=@0;";
            return Factory.Get<CashRepository>(catalog, sql, cashRepositoryId).FirstOrDefault();
        }

        public static int GetCashRepositoryIdByCashRepositoryCode(string catalog, string cashRepositoryCode)
        {
            const string sql = "SELECT cash_repository_id FROM office.cash_repositories WHERE cash_repository_code=@0;";
            return Factory.Scalar<int>(catalog, sql, cashRepositoryCode);
        }
    }
}