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

using MixERP.Net.i18n.Resources;
using System;
using System.Collections.ObjectModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void CreateMergeToOrderButton(HtmlGenericControl container)
        {
            if (this.ShowMergeToOrderButton)
            {
                this.mergeToOrderButton = new Button();
                this.mergeToOrderButton.ID = "MergeToOrderButton";
                this.mergeToOrderButton.CssClass = "ui button";
                this.mergeToOrderButton.Text = Titles.MergeBatchToSalesOrder;
                this.mergeToOrderButton.OnClientClick = "if(!getSelectedItems()){return false;};";
                this.mergeToOrderButton.Click += this.MergeToOrderButton_Click;
                container.Controls.Add(this.mergeToOrderButton);
            }
        }


        private void MergeToOrderButton_Click(object sender, EventArgs e)
        {
            Collection<long> values = this.GetSelectedValues();

            if (this.IsValid(this.Catalog))
            {
                if (string.IsNullOrWhiteSpace(this.MergeToOrderButtonUrl))
                {
                    throw new Exception(Warnings.CannotMergeUrlNull);
                }

                this.Merge(values, MergeToOrderButtonUrl);
            }
        }

    }
}