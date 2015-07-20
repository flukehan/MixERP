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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Contracts;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.AttachmentFactory;
using System;

namespace MixERP.Net.Core.Modules.Finance.Entry
{
    public partial class JournalVoucher : MixERPUserControl, ITransaction
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.TitleLabel.Text = Titles.JournalVoucherEntry;
            this.ValueDateLiteral.Text = Titles.ValueDate;
            this.ReferenceNumberLiteral.Text = Titles.ReferenceNumber;
            this.StatementReferenceLiteral.Text = Titles.StatementReference;
            this.AccountNumberLiteral.Text = Titles.AccountNumber;
            this.AccountLiteral.Text = Titles.Account;
            this.CashRepositoryLiteral.Text = Titles.CashRepository;
            this.CurrencyLiteral.Text = Titles.Currency;
            this.DebitLiteral.Text = Titles.Debit;
            this.CreditLiteral.Text = Titles.Credit;
            this.ERLiteral.Text = Titles.ER;
            this.LCDebitLiteral.Text = Titles.LCDebit;
            this.LCCreditLiteral.Text = Titles.LCCredit;
            this.ActionLiteral.Text = Titles.Action;
            this.CostCenterLiteral.Text = Titles.CostCenter;
            this.DebitTotalLiteral.Text = Titles.DebitTotal;
            this.CreditTotalLiteral.Text = Titles.CreditTotal;
            this.PostTransactionLiteral.Text = Titles.PostTransaction;
            this.AddInputButton.Value = Titles.Add;
            this.ValueDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.BookDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            using (Attachment attachment = new Attachment(AppUsers.GetCurrentUserDB()))
            {
                this.Placeholder1.Controls.Add(attachment);
            }
        }
    }
}