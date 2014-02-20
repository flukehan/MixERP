/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Collections.ObjectModel;
using System.ComponentModel;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.BusinessLayer.Helpers;

namespace MixERP.Net.FrontEnd.Services
{
    /// <summary>
    /// Summary description for ItemData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class ItemData : WebService
    {

        [WebMethod]
        public Collection<ListItem> GetItems()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using(DataTable table = FormHelper.GetTable("core", "items"))
            {
                foreach(DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["item_name"].ToString(), dr["item_code"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public Collection<ListItem> GetStockItems()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using(DataTable table = FormHelper.GetTable("core", "items", "maintain_stock", "true"))
            {
                foreach(DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["item_name"].ToString(), dr["item_code"].ToString()));
                }

                return values;
            }
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

    }
}
