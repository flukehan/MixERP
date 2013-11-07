using AjaxControlToolkit;
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

namespace MixERP.Net.FrontEnd.Services
{
    /// <summary>
    /// Summary description for PartyData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class PartyData : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public CascadingDropDownNameValue[] GetParties(string knownCategoryValues, string category)
        {
            using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.FormHelper.GetTable("core", "parties"))
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter( "PartyDisplayField");
                table.Columns.Add("party", typeof(string), displayField);
                return this.GetValues(table);
            }

        }

        private CascadingDropDownNameValue[] GetValues(System.Data.DataTable table)
        {
            Collection<CascadingDropDownNameValue> values = new Collection<CascadingDropDownNameValue>();

            foreach(System.Data.DataRow dr in table.Rows)
            {
                values.Add(new CascadingDropDownNameValue(dr["party"].ToString(), dr["party_code"].ToString()));
            }

            return values.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetShippingAddresses(string knownCategoryValues, string category)
        {
            Collection<CascadingDropDownNameValue> values = new Collection<CascadingDropDownNameValue>();

            StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            string partyCode = kv["Party"];

            using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Core.ShippingAddresses.GetShippingAddressView(partyCode))
            {
                string displayField = MixERP.Net.Common.Helpers.ConfigurationHelper.GetDbParameter( "ShippingAddressDisplayField");
                table.Columns.Add("shipping_address", typeof(string), displayField);

                foreach(System.Data.DataRow dr in table.Rows)
                {
                    values.Add(new CascadingDropDownNameValue(dr["shipping_address_code"].ToString(), dr["shipping_address"].ToString()));
                }
            }

            return values.ToArray();

        }


    }
}
