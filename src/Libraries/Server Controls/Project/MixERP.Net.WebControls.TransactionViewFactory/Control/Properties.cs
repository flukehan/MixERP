using MixERP.Net.Entities;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
        public string AddNewPath { get; set; }
        public string Book { get; set; }
        public FrequencyType DateFromFromFrequencyType { get; set; } //= Frequency.FiscalYearStartDate;
        public FrequencyType DateToFrequencyType { get; set; } //= Frequency.FiscalYearEndDate;
        public bool DisplayAddButton { get; set; }
        public bool DisplayApproveButton { get; set; }
        public bool DisplayFlagButton { get; set; }
        public bool DisplayPrintButton { get; set; }
        public bool DisplayRejectButton { get; set; }
        public string GridViewCssClass { get; set; }
        public string OfficeName { get; set; }
        public string PostedBy { get; set; }
        public string Reason { get; set; }
        public string ReferenceNumber { get; set; }
        public string StatementReference { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string TranCode { get; set; }
        public long TranId { get; set; }
        public string VerifiedBy { get; set; }
        public int UserId { get; set; }
        public int OfficeId { get; set; }
    }
}