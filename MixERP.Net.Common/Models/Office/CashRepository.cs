namespace MixERP.Net.Common.Models.Office
{
    public class CashRepository
    {
        public int CashRepositoryId { get; set; }
        public int OfficeId { get; set; }

        public Office Office { get; set; }
        public string CashRepositoryCode { get; set; }
        public string CashRepositoryName { get; set; }
        public int? ParentCashRepositoryId { get; set; }
        public CashRepository ParentCashRepository { get; set; }
        public string Description { get; set; }
    }
}
