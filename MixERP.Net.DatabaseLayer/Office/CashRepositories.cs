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
using Npgsql;
using System.Collections.ObjectModel;

namespace MixERP.Net.DatabaseLayer.Office
{
    public static class CashRepositories
    {
        public static MixERP.Net.Common.Models.Office.CashRepository GetCashRepository(int? cashRepositoryId)
        {
            MixERP.Net.Common.Models.Office.CashRepository cashRepository = new Common.Models.Office.CashRepository();

            if (cashRepositoryId != null || cashRepositoryId != 0)
            {
                string sql = "SELECT * FROM office.cash_repositories WHERE cash_repository_id=@CashRepositoryId;";
                using (NpgsqlCommand command = new NpgsqlCommand(sql))
                {
                    command.Parameters.Add("@CashRepositoryId", cashRepositoryId);

                    using (DataTable table = MixERP.Net.DBFactory.DBOperations.GetDataTable(command))
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

        public static Collection<MixERP.Net.Common.Models.Office.CashRepository> GetCashRepositories()
        {
            string sql = "SELECT * FROM office.cash_repositories;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                return GetCashRepositories(MixERP.Net.DBFactory.DBOperations.GetDataTable(command));
            }
        }

        public static Collection<MixERP.Net.Common.Models.Office.CashRepository> GetCashRepositories(int officeId)
        {
            string sql = "SELECT * FROM office.cash_repositories WHERE office_id=@OfficeId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@OfficeId", officeId);
                return GetCashRepositories(MixERP.Net.DBFactory.DBOperations.GetDataTable(command));
            }
        }

        private static Collection<MixERP.Net.Common.Models.Office.CashRepository> GetCashRepositories(DataTable table)
        {
            Collection<MixERP.Net.Common.Models.Office.CashRepository> cashRepositoryCollection = new Collection<Common.Models.Office.CashRepository>();

            if (table == null || table.Rows.Count.Equals(0))
            {
                return cashRepositoryCollection;
            }

            foreach (DataRow row in table.Rows)
            {
                if (row != null)
                {
                    MixERP.Net.Common.Models.Office.CashRepository cashRepository = GetCashRepository(row);

                    cashRepositoryCollection.Add(cashRepository);
                }
            }

            return cashRepositoryCollection;
        }

        private static MixERP.Net.Common.Models.Office.CashRepository GetCashRepository(DataRow row)
        {
            MixERP.Net.Common.Models.Office.CashRepository cashRepository = new Common.Models.Office.CashRepository();

            cashRepository.CashRepositoryId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "cash_repository_id"));
            cashRepository.OfficeId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "office_id"));
            cashRepository.Office = Office.Offices.GetOffice(cashRepository.OfficeId);
            cashRepository.CashRepositoryCode = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "cash_repository_code"));
            cashRepository.CashRepositoryName = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "cash_repository_name"));
            cashRepository.ParentCashRepositoryId = MixERP.Net.Common.Conversion.TryCastInteger(Helpers.ConversionHelper.GetColumnValue(row, "parent_cash_repository_id"));
            cashRepository.ParentCashRepository = Office.CashRepositories.GetCashRepository(cashRepository.ParentCashRepositoryId);
            cashRepository.Description = MixERP.Net.Common.Conversion.TryCastString(Helpers.ConversionHelper.GetColumnValue(row, "description"));

            return cashRepository;

        }


        public static decimal GetBalance(int cashRepositoryId)
        {
            string sql = "SELECT transactions.get_cash_repository_balance(@CashRepositoryId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.Add("@CashRepositoryId", cashRepositoryId);
                return MixERP.Net.Common.Conversion.TryCastDecimal(MixERP.Net.DBFactory.DBOperations.GetScalarValue(command));
            }
        }

    }
}
