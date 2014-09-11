using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class ShippingAddresses
    {
        public static DataTable GetShippingAddressView(string partyCode)
        {
            const string sql = "SELECT * FROM core.shipping_address_view WHERE party_id = core.get_party_id_by_party_code(@PartyCode);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return DbOperations.GetDataTable(command);
            }
        }
    }
}