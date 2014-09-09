using System.Data;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Units
    {
        public static DataTable GetUnitViewByItemCode(string itemCode)
        {
            return DatabaseLayer.Core.Units.GetUnitViewByItemCode(itemCode);
        }

        public static bool UnitExistsByName(string unitName)
        {
            return DatabaseLayer.Core.Units.UnitExistsByName(unitName);
        }
    }
}