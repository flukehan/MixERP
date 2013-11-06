using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Models
{
    public class MergeModel
    {
        public DateTime ValueDate { get; set; }
        public string PartyCode { get; set; }
        public int PriceTypeId { get; set; }
        public string ReferenceNumber { get; set; }
        public int AgentId { get; set; }
        public Collection<MixERP.Net.Common.Models.Transactions.ProductDetailsModel> View { get; set; }
        public string StatementReference { get; set; }

        public MixERP.Net.Common.Models.Transactions.TranBook Book { get; set; }
        public MixERP.Net.Common.Models.Transactions.SubTranBook SubBook { get; set; }
        public Collection<int> TransactionIdCollection { get; set; }
    }
}
