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

using MixERP.Net.FrontEnd.Base;
using System;
using MixERP.Net.WebControls.AttachmentFactory;

namespace MixERP.Net.Core.Modules.Finance.Entry
{
    public partial class JournalVoucher : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.TitleLabel.Text = Resources.Titles.JournalVoucherEntry;
            this.ValueDateLiteral.Text = Resources.Titles.ValueDate;
            this.ReferenceNumberLiteral.Text = Resources.Titles.ReferenceNumber;
            this.StatementReferenceLiteral.Text = Resources.Titles.StatementReference;
            this.AccountNumberLiteral.Text = Resources.Titles.AccountNumber;
            this.AccountLiteral.Text = Resources.Titles.Account;
            this.CashRepositoryLiteral.Text = Resources.Titles.CashRepository;
            this.CurrencyLiteral.Text = Resources.Titles.Currency;
            this.DebitLiteral.Text = Resources.Titles.Debit;
            this.CreditLiteral.Text = Resources.Titles.Credit;
            this.ERLiteral.Text = Resources.Titles.ER;
            this.LCDebitLiteral.Text = Resources.Titles.LCDebit;
            this.LCCreditLiteral.Text = Resources.Titles.LCCredit;
            this.ActionLiteral.Text = Resources.Titles.Action;
            this.CostCenterLiteral.Text = Resources.Titles.CostCenter;
            this.DebitTotalLiteral.Text = Resources.Titles.DebitTotal;
            this.CreditTotalLiteral.Text = Resources.Titles.CreditTotal;
            this.PostTransactionLiteral.Text = Resources.Titles.PostTransaction;
            this.AddInputButton.Value = Resources.Titles.Add;



            using (Attachment attachment = new Attachment())
            {
                this.Placeholder1.Controls.Add(attachment);
            }

            base.OnControlLoad(sender, e);
        }
    }
}