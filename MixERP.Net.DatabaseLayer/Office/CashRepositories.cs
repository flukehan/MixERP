/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Office;
using MixERP.Net.DatabaseLayer.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.DatabaseLayer.Office
{
    public static class CashRepositories
    {
        public static CashRepository GetCashRepository(int? cashRepositoryId)
        {
            CashRepository cashRepository = new CashRepository();

            if (cashRepositoryId != null || cashRepositoryId != 0)
            {
                string sql = "SELECT * FROM office.cash_repositories WHERE cash_repository_id=@CashRepositoryId;";
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.Add("@CashRepositoryId", cashRepositoryId);

                    using (DataTable table = DBOperations.GetDataTable(command))
                    {
                        if (table != null)
                        {
                            if (table.Rows.Count.Equals(1))
                            {
                                cashRepository = GetCashRepository(table.Rows[0]);
                            }
                        }
                    }
                }
            }

            return cashRepository;
        }

        public static Collection<CashRepository> GetCashRepositories()
        {
            string sql = "SELECT * FROM office.cash_repositories;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return GetCashRepositories(DBOperations.GetDataTable(command));
            }
        }

        public static Collection<CashRepository> GetCashRepositories(int officeId)
        {
            string sql = "SELECT * FROM office.cash_repositories WHERE office_id=@OfficeId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@OfficeId", officeId);
                return GetCashRepositories(DBOperations.GetDataTable(command));
            }
        }

        private static Collection<CashRepository> GetCashRepositories(DataTable table)
        {
            Collection<CashRepository> cashRepositoryCollection = new Collection<CashRepository>();

            if (table == null || table.Rows.Count.Equals(0))
            {
                return cashRepositoryCollection;
            }

            foreach (DataRow row in table.Rows)
            {
                if (row != null)
                {
                    CashRepository cashRepository = GetCashRepository(row);

                    cashRepositoryCollection.Add(cashRepository);
                }
            }

            return cashRepositoryCollection;
        }

        private static CashRepository GetCashRepository(DataRow row)
        {
            CashRepository cashRepository = new CashRepository();

            cashRepository.CashRepositoryId = Conversion.TryCastInteger(ConversionHelper.GetColumnValue(row, "cash_repository_id"));
            cashRepository.OfficeId = Conversion.TryCastInteger(ConversionHelper.GetColumnValue(row, "office_id"));
            cashRepository.Office = Offices.GetOffice(cashRepository.OfficeId);
            cashRepository.CashRepositoryCode = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "cash_repository_code"));
            cashRepository.CashRepositoryName = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "cash_repository_name"));
            cashRepository.ParentCashRepositoryId = Conversion.TryCastInteger(ConversionHelper.GetColumnValue(row, "parent_cash_repository_id"));
            cashRepository.ParentCashRepository = GetCashRepository(cashRepository.ParentCashRepositoryId);
            cashRepository.Description = Conversion.TryCastString(ConversionHelper.GetColumnValue(row, "description"));

            return cashRepository;

        }


        public static decimal GetBalance(int cashRepositoryId)
        {
            string sql = "SELECT transactions.get_cash_repository_balance(@CashRepositoryId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@CashRepositoryId", cashRepositoryId);
                return Conversion.TryCastDecimal(DBOperations.GetScalarValue(command));
            }
        }

    }
}
