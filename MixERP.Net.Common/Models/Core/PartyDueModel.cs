using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Models.Core
{
    public class PartyDueModel
    {
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal TotalDueAmount { get; set; }

        public decimal OfficeDueAmount { get; set; }

        public decimal AccruedInterest { get; set; }
        public DateTime LastReceiptDate { get; set; }
        public decimal TransactionValue { get; set; }
    }
}
