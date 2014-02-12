using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Models.Office
{
    public class CashRepository
    {
        public int CashRepositoryId { get; set; }
        public int OfficeId { get; set; }

        public MixERP.Net.Common.Models.Office.Office Office { get; set; }
        public string CashRepositoryCode { get; set; }
        public string CashRepositoryName { get; set; }
        public int? ParentCashRepositoryId { get; set; }
        public MixERP.Net.Common.Models.Office.CashRepository ParentCashRepository { get; set; }
        public string Description { get; set; }
    }
}
