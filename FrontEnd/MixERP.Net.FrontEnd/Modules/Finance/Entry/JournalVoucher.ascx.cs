
using MixERP.Net.FrontEnd.Base;
using System;

namespace MixERP.Net.Core.Modules.Finance.Entry
{
    public partial class JournalVoucher : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            TitleLabel.Text = Resources.Titles.JournalVoucherEntry;
            ValueDateLiteral.Text = Resources.Titles.ValueDate;
            ReferenceNumberLiteral.Text = Resources.Titles.ReferenceNumber;
            StatementReferenceLiteral.Text = Resources.Titles.StatementReference;
            AccountCodeLiteral.Text = Resources.Titles.AccountCode;
            AccountLiteral.Text = Resources.Titles.Account;
            CashRepositoryLiteral.Text = Resources.Titles.CashRepository;
            CurrencyLiteral.Text = Resources.Titles.Currency;
            DebitLiteral.Text = Resources.Titles.Debit;
            CreditLiteral.Text = Resources.Titles.Credit;
            ERLiteral.Text = Resources.Titles.ER;
            LCDebitLiteral.Text = Resources.Titles.LCDebit;
            LCCreditLiteral.Text = Resources.Titles.LCCredit;
            ActionLiteral.Text = Resources.Titles.Action;
            CostCenterLiteral.Text = Resources.Titles.CostCenter;
            DebitTotalLiteral.Text = Resources.Titles.DebitTotal;
            CreditTotalLiteral.Text = Resources.Titles.CreditTotal;
            PostTransactionLiteral.Text = Resources.Titles.PostTransaction;
            AddButton.Value = Resources.Titles.Add;

            base.OnControlLoad(sender, e);
        }
    }
}