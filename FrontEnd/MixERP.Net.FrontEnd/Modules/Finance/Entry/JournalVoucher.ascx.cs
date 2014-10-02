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