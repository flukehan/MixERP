using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Data.Helpers;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Inventory.Services
{
    /// <summary>
    /// Summary description for ItemData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class ItemData : WebService
    {
        [WebMethod]
        public Collection<ListItem> GetItems(string tranBook)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            switch (tranBook)
            {
                case "Sales":
                    values = this.GetItems();
                    break;

                case "Purchase":
                    values = this.GetStockItems();
                    break;
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetStores()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Stores.GetStoreDataTable())
            {
                string displayField = ConfigurationHelper.GetDbParameter("StoreDisplayField");
                table.Columns.Add("store", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["store"].ToString(), dr["store_id"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public Collection<ListItem> GetAgents()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Agents.GetAgentDataTable())
            {
                string displayField = ConfigurationHelper.GetDbParameter("AgentDisplayField");
                table.Columns.Add("agent", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["agent"].ToString(), dr["agent_id"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public Collection<ListItem> GetShippers()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Shippers.GetShipperDataTable())
            {
                string displayField = ConfigurationHelper.GetDbParameter("ShipperDisplayField");
                table.Columns.Add("shipper", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["shipper"].ToString(), dr["shipper_id"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public bool IsStockItem(string itemCode)
        {
            return Items.IsStockItem(itemCode);
        }

        private Collection<ListItem> GetItems()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Items.GetItemDataTable())
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["item_name"].ToString(), dr["item_code"].ToString()));
                }

                return values;
            }
        }

        private Collection<ListItem> GetStockItems()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Items.GetStockItemDataTable())
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["item_name"].ToString(), dr["item_code"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public decimal CountItemInStock(string itemCode, int unitId, int storeId)
        {
            return Items.CountItemInStock(itemCode, unitId, storeId);
        }

        [WebMethod]
        public Collection<ListItem> GetUnits(string itemCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Units.GetUnitViewByItemCode(itemCode))
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["unit_name"].ToString(), dr["unit_id"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public Collection<ListItem> GetPriceTypes()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = PriceTypes.GetPriceTypeDataTable())
            {
                string displayField = ConfigurationHelper.GetDbParameter("PriceTypeDisplayField");
                table.Columns.Add("price_type", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["price_type"].ToString(), dr["price_type_id"].ToString()));
                }
            }

            return values;
        }

        [WebMethod]
        public bool UnitNameExists(string unitName)
        {
            return Units.UnitExistsByName(unitName);
        }

        [WebMethod]
        public decimal GetPrice(string tranBook, string itemCode, string partyCode, int priceTypeId, int unitId)
        {
            decimal price = 0;

            switch (tranBook)
            {
                case "Sales":
                    price = Items.GetItemSellingPrice(itemCode, partyCode, priceTypeId, unitId);
                    break;

                case "Purchase":
                    price = Items.GetItemCostPrice(itemCode, partyCode, unitId);
                    break;
            }

            return price;
        }

        [WebMethod]
        public decimal GetTaxRate(string itemCode)
        {
            return Items.GetTaxRate(itemCode);
        }

        [WebMethod]
        public string GetItemCodeByItemId(int itemId)
        {
            return Items.GetItemCodeByItemId(itemId);
        }

        [WebMethod]
        public bool ItemCodeExists(string itemCode)
        {
            return Items.ItemExistsByCode(itemCode);
        }
    }
}