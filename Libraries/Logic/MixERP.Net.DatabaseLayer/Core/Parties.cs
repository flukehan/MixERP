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
using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Parties
    {
        public static bool IsCreditAllowed(string partyCode)
        {
            const string sql = "SELECT 1 FROM core.parties WHERE party_code=@PartyCode and allow_credit=true;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyCode", partyCode);
                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static string GetPartyCodeByPartyId(int partyId)
        {
            const string sql = "SELECT party_code FROM core.parties WHERE party_id=@PartyId;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyId", partyId);
                return Conversion.TryCastString(DbOperations.GetScalarValue((command)));
            }
        }

        public static DataTable GetShippingAddresses(string partyCode)
        {
            const string sql = "SELECT * FROM core.shipping_addresses WHERE party_id=core.get_party_id_by_party_code(@PartyCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetShippingAddresses(int partyId)
        {
            const string sql = "SELECT * FROM core.shipping_addresses WHERE party_id=@PartyId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyId", partyId);

                return DbOperations.GetDataTable(command);
            }
        }

        public static DataTable GetPartyDue(int officeId, string partyCode)
        {
            const string sql = "SELECT * FROM transactions.get_party_transaction_summary(@OfficeId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return DbOperations.GetDataTable(command);
            }
        }
    }
}