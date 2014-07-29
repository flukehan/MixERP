/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.DatabaseLayer.Core
{
    public static class Parties
    {
        public static bool IsCreditAllowed(string partyCode)
        {
            const string sql = "SELECT 1 FROM core.parties WHERE party_code=@PartyCode and allow_credit=true;";

            using(NpgsqlCommand command = new NpgsqlCommand(sql))
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
    }
}
