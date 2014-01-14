/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.Common.Models.Transactions;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Models
{
    public class MergeModel
    {
        public DateTime ValueDate { get; set; }
        public string PartyCode { get; set; }
        public int PriceTypeId { get; set; }
        public string ReferenceNumber { get; set; }
        public int AgentId { get; set; }

        private Collection<ProductDetailsModel> view = new Collection<ProductDetailsModel>();
        public Collection<ProductDetailsModel> View
        {
            get
            {
                return this.view;
            }
        }

        public void AddViewToCollection(ProductDetailsModel product)
        {
            view.Add(product);
        }

        public string StatementReference { get; set; }

        public MixERP.Net.Common.Models.Transactions.TranBook Book { get; set; }
        public MixERP.Net.Common.Models.Transactions.SubTranBook SubBook { get; set; }

        private Collection<int> transactionIdCollection = new Collection<int>();
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
