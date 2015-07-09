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
using MixERP.Net.Framework;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Flag;
using MixERP.Net.WebControls.StockTransactionViewFactory.Helpers;
using System.Linq;
using System.Web.UI;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private void AddFlag(Control contianer)
        {
            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui form segment initially hidden";
                flag.Updated += this.Flag_Updated;
                flag.Catalog = this.Catalog;

                contianer.Controls.Add(flag);
            }
        }

        private void Flag_Updated(object sender, FlagUpdatedEventArgs e)
        {
            int flagTypeId = e.FlagId;

            string resource = this.DbTableName;
            string resourceKey = this.PrimaryKey;

            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new MixERPException(Warnings.CannotCreateFlagTransactionTableNull);
            }

            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                throw new MixERPException(Warnings.CannotCreateFlagTransactionTablePrimaryKeyNull);
            }


            Flags.CreateFlag(this.Catalog, this.UserId, flagTypeId, resource, resourceKey, this.GetSelectedValues().Select(t => Conversion.TryCastString(t)).ToList().ToCollection());

            this.LoadGridView();
        }
    }
}