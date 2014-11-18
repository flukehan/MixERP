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

using MixERP.Net.Common.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Sales.Services.Receipt
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Accounts : WebService
    {
        [WebMethod]
        public Collection<ListItem> GetCashRepositories()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Data.Helpers.Accounts.GetCashRepositories())
            {
                string displayField = ConfigurationHelper.GetDbParameter("CashRepositoryDisplayField");
                table.Columns.Add("cash_repository", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["cash_repository"].ToString(), dr["cash_repository_id"].ToString()));
                }
            }
            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetFlags()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Data.Helpers.Accounts.GetFlags())
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["flag_type_name"].ToString(), dr["flag_type_id"].ToString()));
                }

                return values;
            }
        }

        [WebMethod]
        public Collection<ListItem> GetCostCenters()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Data.Helpers.Accounts.GetCostCenters())
            {
                string displayField = ConfigurationHelper.GetDbParameter("CostCenterDisplayField");
                table.Columns.Add("cost_center", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["cost_center"].ToString(), dr["cost_center_id"].ToString()));
                }
            }
            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetBankAccounts()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Data.Helpers.Accounts.GetBankAccounts())
            {
                string displayField = ConfigurationHelper.GetDbParameter("BankAccountDisplayField");
                table.Columns.Add("bank_account", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["bank_account"].ToString(), dr["account_id"].ToString()));
                }
            }
            return values;
        }
    }
}