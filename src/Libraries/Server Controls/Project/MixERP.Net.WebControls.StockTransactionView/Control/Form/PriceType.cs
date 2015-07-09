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
using MixERP.Net.Entities;
using MixERP.Net.i18n.Resources;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void CreatePriceTypeField(HtmlGenericControl container)
        {
            if (this.SubBook != SubTranBook.Receipt)
            {
                using (HtmlGenericControl field = HtmlControlHelper.GetField())
                {
                    this.priceTypeInputText = new HtmlInputText();
                    this.priceTypeInputText.ID = "PriceTypeTextBox";
                    this.priceTypeInputText.Attributes.Add("placeholder", Titles.PriceType);

                    field.Controls.Add(this.priceTypeInputText);
                    container.Controls.Add(field);
                }
            }
        }
    }
}