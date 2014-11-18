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

using MixERP.Net.Common.Models.Transactions;

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
using System.Collections.ObjectModel;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Models
{
    public sealed class MergeModel
    {
        public DateTime ValueDate { get; set; }

        public string PartyCode { get; set; }

        public int PriceTypeId { get; set; }

        public string ReferenceNumber { get; set; }

        public int AgentId { get; set; }

        private readonly Collection<ProductDetailsModel> view = new Collection<ProductDetailsModel>();

        public Collection<ProductDetailsModel> View
        {
            get
            {
                return this.view;
            }
        }

        public void AddViewToCollection(ProductDetailsModel product)
        {
            this.view.Add(product);
        }

        public string StatementReference { get; set; }

        public TranBook Book { get; set; }

        public SubTranBook SubBook { get; set; }

        private readonly Collection<int> transactionIdCollection = new Collection<int>();

        public Collection<int> TransactionIdCollection
        {
            get
            {
                return this.transactionIdCollection;
            }
        }

        public void AddTransactionIdToCollection(int transactionId)
        {
            this.transactionIdCollection.Add(transactionId);
        }
    }
}