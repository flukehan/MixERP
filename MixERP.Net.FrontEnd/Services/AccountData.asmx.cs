using System.Collections.ObjectModel;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Services
{
    /// <summary>
    /// Summary description for AccountData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class AccountData : System.Web.Services.WebService
    {
        [WebMethod(EnableSession=true)]
        public Collection<ListItem> GetAccounts()
        {
            if(MixERP.Net.Common.Helpers.Switches.AllowParentAccountInGLTransaction())
            {
                if(MixERP.Net.BusinessLayer.Helpers.SessionHelper.IsAdmin())
                {
                    using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("core", "accounts"))
                    {
                        return GetValues(table);
                    }
                }
                else
                {
                    using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("core", "accounts", "confidential", "0"))
                    {
                        return GetValues(table);
                    }
                }
            }
            else
            {
                if(MixERP.Net.BusinessLayer.Helpers.SessionHelper.IsAdmin())
                {
                    using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("core", "account_view", "has_child", "0"))
                    {
                        return GetValues(table);
                    }
                }
                else
                {
                    {
                        using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("core", "account_view", "has_child, confidential", "0, 0"))
                        {
                            return GetValues(table);
                        }
                    }
                }
            }

        }

        private static Collection<ListItem> GetValues(System.Data.DataTable table)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach(System.Data.DataRow dr in table.Rows)
            {
                values.Add(new ListItem(dr["account_name"].ToString(), dr["account_code"].ToString()));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetCashRepositories(string accountCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if(MixERP.Net.BusinessLayer.Core.Accounts.IsCashAccount(accountCode))
            {
                using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("office", "cash_repositories"))
                {
                    foreach(System.Data.DataRow dr in table.Rows)
                    {
                        values.Add(new ListItem(dr["cash_repository_name"].ToString(), dr["cash_repository_code"].ToString()));
                    }
                }
            }

            return values;
        }
    }
}