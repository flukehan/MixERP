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
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

[assembly: WebResource("MixERP.Net.WebControls.StockTransactionFactory.Includes.Script.StockTransactionForm.js", "application/x-javascript", PerformSubstitution = true)]

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private void RegisterJavascript()
        {
            string isSales = (this.Book.Equals(TranBook.Sales)) ? "true" : "false";
            string tranBook = this.GetTranBook();
            string verifyStock = (this.VerifyStock) ? "true" : "false";

            string js = string.Empty;
            js += JSUtility.GetVar("isSales", isSales, false);
            js += JSUtility.GetVar("tranBook", tranBook);
            js += JSUtility.GetVar("verifyStock", verifyStock, false);

            PageUtility.RegisterJavascript("StockTransactionForm_Vars", js, this.Page, true);
        }

        public string GetTranBook()
        {
            if (this.Book == TranBook.Sales)
            {
                return "Sales";
            }

            return "Purchase";
        }

        [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
        private void AddJavascript()
        {
            JSUtility.AddJSReference(this.Page, "MixERP.Net.WebControls.StockTransactionFactory.Includes.Script.StockTransactionForm.js", "StockTransactionForm", typeof(StockTransactionForm));
        }
    }
}