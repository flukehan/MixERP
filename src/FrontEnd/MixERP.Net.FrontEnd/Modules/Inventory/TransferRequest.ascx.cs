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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI.WebControls;
using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.Entities.Transactions;
using MixERP.Net.Framework;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.TransactionGovernor;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.Flag;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Inventory
{
    public partial class TransferRequest : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.DateFromDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.DateToDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.AddGridView();
            this.BindGridView();
            this.AddFlagControl();
            this.AddHiddenField();
        }

        private void AddHiddenField()
        {
            this.selectedValuesHidden = new HiddenField();
            this.selectedValuesHidden.ID = "SelectedValuesHidden";
            this.GridViewPlaceholder.Controls.Add(selectedValuesHidden);
        }

        private void AddGridView()
        {
            this.grid = new MixERPGridView();
            this.grid.ID = "TransferRequestGridView";
            this.GridViewPlaceholder.Controls.Add(this.grid);
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGridView();
        }

        private void BindGridView()
        {
            this.grid.DataSource = this.GetDataSource();
            this.grid.DataBind();
        }

        private IEnumerable<DbGetInventoryTransferRequestViewResult> GetDataSource()
        {
            string catalog = AppUsers.GetCurrentUserDB();

            int userId = AppUsers.GetCurrent().View.UserId.ToInt();
            long loginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            DateTime from = Conversion.TryCastDate(this.DateFromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.DateToDateTextBox.Text);

            string office = this.OfficeTextBox.Value;
            string store = this.StoreTextBox.Value;
            string authorized = this.AuthorizedTextBox.Value;
            string delivered = this.DeliveredTextBox.Value;
            string received = this.ReceivedTextBox.Value;
            string user = this.UserTextBox.Value;
            string referenceNumber = this.ReferenceNumberTextBox.Value;
            string statementReference = this.StatementReferenceTextBox.Value;

            const string sql =
                @"SELECT * FROM transactions.get_inventory_transfer_request_view(@0::integer, @1::bigint, @2::integer,@3::date,@4::date,@5::text,@6::text,@7::text,@8::text,@9::text,@10::text,@11::text,@12::text);";
            return Factory.Get<DbGetInventoryTransferRequestViewResult>(catalog, sql, userId, loginId, officeId, from,
                to, office, store, authorized, delivered, received, user, referenceNumber, statementReference);
        }

        #region Flag

        private void AddFlagControl()
        {
            this.flag = new FlagControl();

            this.flag.ID = "FlagPopUnder";
            this.flag.AssociatedControlId = "FlagButton";
            this.flag.OnClientClick = "return getSelectedItems();";
            this.flag.CssClass = "ui form segment initially hidden";
            this.flag.Updated += this.Flag_Updated;
            this.flag.Catalog = AppUsers.GetCurrentUserDB();


            this.GridViewPlaceholder.Controls.Add(flag);
        }

        private void Flag_Updated(object sender, FlagUpdatedEventArgs e)
        {
            int flagTypeId = e.FlagId;

            const string resource = "transactions.inventory_transfer_requests";
            const string resourceKey = "inventory_transfer_request_id";

            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new MixERPException(Warnings.CannotCreateFlagTransactionTableNull);
            }


            Flags.CreateFlag(AppUsers.GetCurrentUserDB(), AppUsers.GetCurrent().View.UserId.ToInt(), flagTypeId,
                resource, resourceKey,
                this.GetSelectedValues().Select(t => Conversion.TryCastString(t)).ToList().ToCollection());

            this.BindGridView();
        }

        private Collection<long> GetSelectedValues()
        {
            string selectedValues = this.selectedValuesHidden.Value;

            //Check if something was selected.
            if (string.IsNullOrWhiteSpace(selectedValues))
            {
                return new Collection<long>();
            }

            //Create a collection object to store the IDs.
            Collection<long> values = new Collection<long>();

            //Iterate through each value in the selected values
            //and determine if each value is a number.
            foreach (string value in selectedValues.Split(','))
            {
                //Parse the value to integer.
                int val = Conversion.TryCastInteger(value);

                if (val > 0)
                {
                    values.Add(val);
                }
            }

            return values;
        }

        #endregion

        #region IDisposable

        private bool disposed;
        private MixERPGridView grid;
        private FlagControl flag;
        private HiddenField selectedValuesHidden;

        public override void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                base.Dispose();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.grid != null)
            {
                this.grid.Dispose();
                this.grid = null;
            }

            if (this.flag != null)
            {
                this.flag.Dispose();
                this.flag = null;
            }

            if (this.selectedValuesHidden != null)
            {
                this.selectedValuesHidden.Dispose();
                this.selectedValuesHidden = null;
            }

            this.disposed = true;
        }

        #endregion
    }
}