namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class ShippingAddresses
    {
        public static System.Data.DataTable GetShippingAddressView(string partyCode)
        {
            return DatabaseLayer.Core.ShippingAddresses.GetShippingAddressView(partyCode);
        }
    }
}