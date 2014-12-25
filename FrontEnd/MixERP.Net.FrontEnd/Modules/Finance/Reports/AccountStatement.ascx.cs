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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.Flag;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class AccountStatement : MixERPUserControl
    {
        private HtmlInputText accountNumberInputText;
        private HtmlSelect accountNumberSelect;
        private DateTextBox fromDateTextBox;
        private Button saveButton;
        private DateTextBox toDateTextBox;

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateTopPanel(this.Placeholder1);

            this.CreateFormPanel(this.FormPlaceholder);

            this.CreateFlagPanel(this.FlagPlaceholder);

            base.OnControlLoad(sender, e);
        }

        private void CreateFlagPanel(Control placeHolder)
        {
            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui form segment initially hidden";
                flag.Updated += FlagUpdated;

                placeHolder.Controls.Add(flag);
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.AccountStatement;
                container.Controls.Add(header);
            }
        }

        private void FlagUpdated(object sender, FlagUpdatedEventArgs flagUpdatedEventArgs)
        {
            throw new NotImplementedException();
        }

        #region Form

        private void AddAccountNumberInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.AccountNumber, "AccountNumberInputText"))
                {
                    field.Controls.Add(label);
                }

                accountNumberInputText = new HtmlInputText();
                accountNumberInputText.ID = "AccountNumberInputText";
                field.Controls.Add(accountNumberInputText);

                container.Controls.Add(field);
            }
        }

        private void AddAccountNumberSelect(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField("four wide field"))
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Select, "AccountNumberSelect"))
                {
                    field.Controls.Add(label);
                }

                accountNumberSelect = new HtmlSelect();
                accountNumberSelect.ID = "AccountNumberSelect";
                field.Controls.Add(accountNumberSelect);
                container.Controls.Add(field);
            }
        }

        private void AddFromDateTextBox(HtmlGenericControl container)
        {
            fromDateTextBox = new DateTextBox();
            fromDateTextBox.ID = "FromDateTextBox";
            fromDateTextBox.Mode = Frequency.FiscalYearStartDate;

            using (HtmlGenericControl field = this.GetDateField(Titles.From, fromDateTextBox))
            {
                container.Controls.Add(field);
            }
        }

        private void AddShowButton(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Prepare, "ShowButton"))
                {
                    field.Controls.Add(label);
                }

                saveButton = new Button();
                saveButton.ID = "ShowButton";
                saveButton.Attributes.Add("class", "ui positive button");
                //saveButton.Attributes.Add("onclick", "alert('foo');");
                saveButton.Text = Titles.Show;
                saveButton.Click += button_Click;
                field.Controls.Add(saveButton);

                container.Controls.Add(field);
            }
        }

        private void AddToDateTextBox(HtmlGenericControl container)
        {
            toDateTextBox = new DateTextBox();
            toDateTextBox.ID = "ToDateTextBox";
            toDateTextBox.Mode = Frequency.FiscalYearEndDate;

            using (HtmlGenericControl field = this.GetDateField(Titles.To, toDateTextBox))
            {
                container.Controls.Add(field);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            DateTime from = Conversion.TryCastDate(fromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(toDateTextBox.Text);
            int userId = SessionHelper.GetUserId();
            string accountNumber = accountNumberInputText.Value;
            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = Data.Reports.AccountStatement.GetAccountStatement(from, to, userId, accountNumber, officeId))
            {
                StatementGridView.DataSource = table;
                StatementGridView.DataBound += StatementGridViewDataBound;
                StatementGridView.DataBind();
            }
        }

        private void CreateFormPanel(Control container)
        {
            using (HtmlGenericControl fields = new HtmlGenericControl("div"))
            {
                fields.Attributes.Add("class", "fields");

                this.AddAccountNumberInputText(fields);
                this.AddAccountNumberSelect(fields);
                this.AddFromDateTextBox(fields);
                this.AddToDateTextBox(fields);
                this.AddShowButton(fields);

                container.Controls.Add(fields);
            }
        }

        private HtmlGenericControl GetDateField(string labelText, DateTextBox dateTextBox)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(labelText, dateTextBox.ID))
                {
                    field.Controls.Add(label);
                }

                using (HtmlGenericControl iconInput = new HtmlGenericControl("div"))
                {
                    iconInput.Attributes.Add("class", "ui icon input");

                    iconInput.Controls.Add(dateTextBox);

                    using (HtmlGenericControl icon = new HtmlGenericControl("i"))
                    {
                        icon.Attributes.Add("class", "icon calendar pointer");
                        icon.Attributes.Add("onclick", string.Format(CultureInfo.InvariantCulture, "$('#{0}').datepicker('show');", dateTextBox.ID));
                    }

                    field.Controls.Add(iconInput);
                }

                return field;
            }
        }

        private void StatementGridViewDataBound(object sender, EventArgs eventArgs)
        {
            StatementGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion Form

        #region Top Panel

        private void AddFlagButton(HtmlGenericControl container)
        {
            using (HtmlButton flagButton = new HtmlButton())
            {
                flagButton.ID = "FlagButton";
                flagButton.Attributes.Add("class", "ui button");
                flagButton.Attributes.Add("onclick", "return false");
                flagButton.InnerHtml = "<i class='icon flag'></i>" + Titles.Flag;
                container.Controls.Add(flagButton);
            }
        }

        private void AddNewButton(HtmlGenericControl container)
        {
            using (HtmlButton newButton = new HtmlButton())
            {
                newButton.ID = "AddNewButton";
                newButton.Attributes.Add("class", "ui button");
                newButton.Attributes.Add("onclick", "window.location='/Modules/Finance/Entry/JournalVoucher.mix';return false;");
                newButton.InnerHtml = "<i class='icon plus'></i>" + Titles.NewJournalEntry;
                container.Controls.Add(newButton);
            }
        }

        private void AddPrintButton(HtmlGenericControl container)
        {
            using (HtmlButton printButton = new HtmlButton())
            {
                printButton.ID = "PrintButton";
                printButton.Attributes.Add("class", "ui button");
                printButton.Attributes.Add("onclick", "return false");
                printButton.InnerHtml = "<i class='icon print'></i>" + Titles.Print;
                container.Controls.Add(printButton);
            }
        }

        private void CreateTopPanel(Control container)
        {
            using (HtmlGenericControl buttons = new HtmlGenericControl("div"))
            {
                buttons.Attributes.Add("class", "ui icon buttons bpad16");

                this.AddNewButton(buttons);
                this.AddFlagButton(buttons);
                this.AddPrintButton(buttons);

                container.Controls.Add(buttons);
            }
        }

        #endregion Top Panel
    }
}