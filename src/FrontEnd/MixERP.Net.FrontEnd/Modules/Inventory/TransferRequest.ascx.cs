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
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.Entities.Transactions;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Cache;
using MixERP.Net.WebControls.Common;

namespace MixERP.Net.Core.Modules.Inventory
{
    public partial class TransferRequest : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.DateFromDateTextBox.OfficeId = AppUsers.GetCurrentLogin().View.OfficeId.ToInt();
            this.DateToDateTextBox.OfficeId = AppUsers.GetCurrentLogin().View.OfficeId.ToInt();
            this.AddGridView();
            this.BindGridView();
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
            ;
        }

        private void BindGridView()
        {
            this.grid.DataSource = this.GetDataSource();
            this.grid.DataBind();
        }

        private IEnumerable<DbGetInventoryTransferRequestViewResult> GetDataSource()
        {
            string catalog = AppUsers.GetCurrentUserDB();

            int userId = AppUsers.GetCurrentLogin().View.UserId.ToInt();
            long loginId = AppUsers.GetCurrentLogin().View.LoginId.ToLong();
            int officeId = AppUsers.GetCurrentLogin().View.OfficeId.ToInt();

            DateTime from = Conversion.TryCastDate(this.DateFromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.DateToDateTextBox.Text);

            string office = this.OfficeTextBox.Value;
            string store = this.StoreTextBox.Value;
            string authorized = this.AuthorizedTextBox.Value;
            string acknowledged = this.AcknowledgedTextBox.Value;
            string withdrawn = this.WithdrawnTextBox.Value;
            string user = this.UserTextBox.Value;
            string referenceNumber = this.ReferenceNumberTextBox.Value;
            string statementReference = this.StatementReferenceTextBox.Value;

            const string sql =
                @"SELECT * FROM transactions.get_inventory_transfer_request_view(@0::integer, @1::bigint, @2::integer,@3::date,@4::date,@5::text,@6::text,@7::text,@8::text,@9::text,@10::text,@11::text,@12::text);";
            return Factory.Get<DbGetInventoryTransferRequestViewResult>(catalog, sql, userId, loginId, officeId, from,
                to, office, store, authorized, acknowledged, withdrawn, user, referenceNumber, statementReference);
        }

        #region IDisposable

        private bool disposed;
        private MixERPGridView grid;

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

            this.disposed = true;
        }

        #endregion
    }
}