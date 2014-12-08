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
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance
{
    public partial class JournalVoucher : MixERPUserControl
    {
        #region Properties

        public string AddNewPath { get; set; }

        public bool DisplayAddButton { get; set; }

        public bool DisplayApproveButton { get; set; }

        public bool DisplayFlagButton { get; set; }

        public bool DisplayPrintButton { get; set; }

        public bool DisplayRejectButton { get; set; }

        #endregion Properties

        public void AddColumns(GridView grid)
        {
            //grid.Columns.Clear();

            //AddTemplateFields(grid);

            AddDataBoundControl(grid, "transaction_master_id", Titles.Id);
            AddDataBoundControl(grid, "transaction_code", Titles.TranCode);
            AddDataBoundControl(grid, "book", Titles.Book);
            AddDataBoundControl(grid, "value_date", Titles.ValueDate, "{0:d}");
            AddDataBoundControl(grid, "reference_number", Titles.ReferenceNumber);
            AddDataBoundControl(grid, "statement_reference", Titles.StatementReference);

            AddDataBoundControl(grid, "posted_by", Titles.PostedBy);
            AddDataBoundControl(grid, "office", Titles.Office);

            AddDataBoundControl(grid, "status", Titles.Status);
            AddDataBoundControl(grid, "verified_by", Titles.VerifiedBy);
            AddDataBoundControl(grid, "verified_on", Titles.VerifiedOn, "{0:g}");
            AddDataBoundControl(grid, "reason", Titles.Reason);
            AddDataBoundControl(grid, "transaction_ts", Titles.TransactionTimestamp, "{0:g}");
            AddDataBoundControl(grid, "flag_bg", "flag_bg");
            AddDataBoundControl(grid, "flag_fg", "flag_fg");
        }

        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.DisplayAddButton = true;
            this.DisplayFlagButton = true;
            this.DisplayApproveButton = true;
            this.DisplayRejectButton = true;
            this.DisplayPrintButton = true;
            this.AddNewPath = "Entry/JournalVoucher.mix";

            this.AddHeading(this.Placeholder1);
            this.AddButtons(this.Placeholder1);
            this.AddTopPanel(this.Placeholder1);

            using (FlagControl flag = new FlagControl())
            {
                flag.ID = "FlagPopUnder";
                flag.AssociatedControlId = "FlagButton";
                flag.OnClientClick = "return getSelectedItems();";
                flag.CssClass = "ui form segment initially hidden";

                flag.Updated += Flag_Updated;

                FlagPlaceholder.Controls.Add(flag);
            }

            this.SetDefaultValues();
            this.BindGrid();
            base.OnControlLoad(sender, e);
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void TransactionGridView_DataBound(object sender, EventArgs e)
        {
            GridViewHelper.SetHeaderRow(this.TransactionGridView);
        }

        protected void TransactionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        private static void AddDataBoundControl(GridView grid, string dataField, string headerText, string dataFormatString = "")
        {
            BoundField field = new BoundField();
            field.DataField = dataField;
            field.HeaderText = headerText;
            field.DataFormatString = dataFormatString;

            grid.Columns.Add(field);
        }

        #region Top Panel

        private HtmlInputText bookInputText;
        private DateTextBox dateFromDateTextBox;
        private DateTextBox dateToDateTextBox;
        private HtmlInputText officeInputText;
        private HtmlInputText postedByInputText;
        private HtmlInputText reasonInputText;
        private HtmlInputText referenceNumberInputText;
        private Button showButton;
        private HtmlInputText statementReferenceInputText;
        private HtmlInputText statusInputText;
        private HtmlInputText tranCodeInputText;
        private HtmlInputText tranIdInputText;
        private HtmlInputText verifiedByInputText;

        private void AddBookInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.bookInputText = new HtmlInputText();
                this.bookInputText.Attributes.Add("placeholder", Titles.Book);

                field.Controls.Add(this.bookInputText);
                container.Controls.Add(field);
            }
        }

        private void AddDateFromDateTextBox(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl iconInput = new HtmlGenericControl("div"))
                {
                    iconInput.Attributes.Add("class", "ui icon input");

                    this.dateFromDateTextBox = new DateTextBox();
                    this.dateFromDateTextBox.ID = "DateFromDateTextBox";
                    this.dateFromDateTextBox.CssClass = "date";
                    this.dateFromDateTextBox.Mode = Frequency.FiscalYearStartDate;
                    this.dateFromDateTextBox.Required = true;

                    iconInput.Controls.Add(this.dateFromDateTextBox);

                    using (HtmlGenericControl icon = new HtmlGenericControl("i"))
                    {
                        icon.Attributes.Add("class", "icon calendar pointer");
                        icon.Attributes.Add("onclick", "$('#DateFromDateTextBox').datepicker('show');");
                        iconInput.Controls.Add(icon);
                    }

                    field.Controls.Add(iconInput);

                    container.Controls.Add(field);
                }
            }
        }

        private void AddDatetoDateTextBox(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl iconInput = new HtmlGenericControl("div"))
                {
                    iconInput.Attributes.Add("class", "ui icon input");

                    this.dateToDateTextBox = new DateTextBox();
                    this.dateToDateTextBox.ID = "DateToDateTextBox";
                    this.dateToDateTextBox.CssClass = "date";
                    this.dateToDateTextBox.Mode = Frequency.FiscalYearEndDate;
                    this.dateToDateTextBox.Required = true;

                    iconInput.Controls.Add(this.dateToDateTextBox);

                    using (HtmlGenericControl icon = new HtmlGenericControl("i"))
                    {
                        icon.Attributes.Add("class", "icon calendar pointer");
                        icon.Attributes.Add("onclick", "$('#DateToDateTextBox').datepicker('show');");
                        iconInput.Controls.Add(icon);
                    }
                    field.Controls.Add(iconInput);
                    container.Controls.Add(field);
                }
            }
        }

        private void AddOfficeInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.officeInputText = new HtmlInputText();
                this.officeInputText.Attributes.Add("placeholder", Titles.Office);

                field.Controls.Add(this.officeInputText);
                container.Controls.Add(field);
            }
        }

        private void AddPostedByInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.postedByInputText = new HtmlInputText();
                this.postedByInputText.Attributes.Add("placeholder", Titles.PostedBy);

                field.Controls.Add(this.postedByInputText);
                container.Controls.Add(field);
            }
        }

        private void AddReasonInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.reasonInputText = new HtmlInputText();
                this.reasonInputText.Attributes.Add("placeholder", Titles.Reason);

                field.Controls.Add(this.reasonInputText);
                container.Controls.Add(field);
            }
        }

        private void AddReferenceNumberInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.referenceNumberInputText = new HtmlInputText();
                this.referenceNumberInputText.Attributes.Add("placeholder", Titles.ReferenceNumber);

                field.Controls.Add(this.referenceNumberInputText);
                container.Controls.Add(field);
            }
        }

        private void AddShowbutton(HtmlGenericControl container)
        {
            this.showButton = new Button();
            this.showButton.Text = Titles.Show;
            this.showButton.CssClass = "blue ui button";
            this.showButton.Click += ShowButton_Click;

            container.Controls.Add(this.showButton);
        }

        private void AddStatementReferenceInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.statementReferenceInputText = new HtmlInputText();
                this.statementReferenceInputText.Attributes.Add("placeholder", Titles.StatementReference);

                field.Controls.Add(this.statementReferenceInputText);
                container.Controls.Add(field);
            }
        }

        private void AddStatusInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.statusInputText = new HtmlInputText();
                this.statusInputText.Attributes.Add("placeholder", Titles.Status);

                field.Controls.Add(this.statusInputText);
                container.Controls.Add(field);
            }
        }

        private void AddTopPanel(Control container)
        {
            using (HtmlGenericControl form = new HtmlGenericControl("div"))
            {
                form.Attributes.Add("class", "ui form segment");

                using (HtmlGenericControl fields = new HtmlGenericControl("div"))
                {
                    fields.Attributes.Add("class", "eight fields");

                    this.AddDateFromDateTextBox(fields);
                    this.AddDatetoDateTextBox(fields);
                    this.AddTranIdInputText(fields);
                    this.AddTranCodeInputText(fields);
                    this.AddBookInputText(fields);
                    this.AddReferenceNumberInputText(fields);
                    this.AddStatementReferenceInputText(fields);
                    this.AddPostedByInputText(fields);

                    form.Controls.Add(fields);
                }

                using (HtmlGenericControl fields = new HtmlGenericControl("div"))
                {
                    fields.Attributes.Add("class", "eight fields");

                    this.AddOfficeInputText(fields);
                    this.AddStatusInputText(fields);
                    this.AddVerifiedByInputText(fields);
                    this.AddReasonInputText(fields);
                    this.AddShowbutton(fields);

                    form.Controls.Add(fields);
                }

                container.Controls.Add(form);
            }
        }

        private void AddTranCodeInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.tranCodeInputText = new HtmlInputText();
                this.tranCodeInputText.Attributes.Add("placeholder", Titles.TranCode);

                field.Controls.Add(this.tranCodeInputText);
                container.Controls.Add(field);
            }
        }

        private void AddTranIdInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.tranIdInputText = new HtmlInputText();
                this.tranIdInputText.Attributes.Add("placeholder", Titles.TranId);
                this.tranIdInputText.Attributes.Add("class", "integer");

                field.Controls.Add(this.tranIdInputText);

                container.Controls.Add(field);
            }
        }

        private void AddVerifiedByInputText(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                this.verifiedByInputText = new HtmlInputText();
                this.verifiedByInputText.Attributes.Add("placeholder", Titles.VerifiedBy);

                field.Controls.Add(this.verifiedByInputText);
                container.Controls.Add(field);
            }
        }

        #endregion Top Panel

        #region Buttons

        private void AddAddButton(HtmlGenericControl container)
        {
            if (this.DisplayAddButton)
            {
                using (HtmlButton button = new HtmlButton())
                {
                    button.Attributes.Add("type", "button");
                    button.Attributes.Add("id", "AddNewButton");
                    button.Attributes.Add("class", "ui blue button");
                    button.Attributes.Add("onclick", string.Format(CultureInfo.InvariantCulture, "window.location='{0}'", this.AddNewPath));
                    button.InnerText = Titles.AddNew;

                    container.Controls.Add(button);
                }
            }
        }

        private void AddApproveButton(HtmlGenericControl container)
        {
            if (this.DisplayApproveButton)
            {
                using (HtmlButton button = new HtmlButton())
                {
                    button.Attributes.Add("type", "button");
                    button.Attributes.Add("id", "ApproveButton");
                    button.Attributes.Add("class", "ui positive button");
                    button.InnerText = Titles.Approve;

                    container.Controls.Add(button);
                }
            }
        }

        private void AddButtons(Control container)
        {
            using (HtmlGenericControl iconButtons = new HtmlGenericControl("div"))
            {
                iconButtons.Attributes.Add("class", "ui icon buttons");
                this.AddAddButton(iconButtons);
                this.AddFlagButton(iconButtons);
                this.AddApproveButton(iconButtons);
                this.AddRejectButton(iconButtons);
                this.AddPrintButton(iconButtons);

                container.Controls.Add(iconButtons);
            }
        }

        private void AddFlagButton(HtmlGenericControl container)
        {
            if (this.DisplayApproveButton)
            {
                using (HtmlButton button = new HtmlButton())
                {
                    button.Attributes.Add("type", "button");
                    button.Attributes.Add("id", "FlagButton");
                    button.Attributes.Add("class", "ui orange button");
                    button.InnerText = Titles.Flag;

                    container.Controls.Add(button);
                }
            }
        }

        private void AddPrintButton(HtmlGenericControl container)
        {
            if (this.DisplayApproveButton)
            {
                using (HtmlButton button = new HtmlButton())
                {
                    button.Attributes.Add("type", "button");
                    button.Attributes.Add("id", "PrintButton");
                    button.Attributes.Add("class", "ui teal button");
                    button.InnerText = Titles.Print;
                    container.Controls.Add(button);
                }
            }
        }

        private void AddRejectButton(HtmlGenericControl container)
        {
            if (this.DisplayApproveButton)
            {
                using (HtmlButton button = new HtmlButton())
                {
                    button.Attributes.Add("type", "button");
                    button.Attributes.Add("id", "RejectButton");
                    button.Attributes.Add("class", "ui negative button");
                    button.InnerText = Titles.Reject;
                    container.Controls.Add(button);
                }
            }
        }

        #endregion Buttons

        private void AddHeading(Control container)
        {
            using (HtmlGenericControl h1 = new HtmlGenericControl("h1"))
            {
                h1.InnerText = Titles.JournalVoucher;

                container.Controls.Add(h1);
            }
        }

        private void BindGrid()
        {
            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();

            DateTime from = Conversion.TryCastDate(this.dateFromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.dateToDateTextBox.Text);
            long tranId = Conversion.TryCastLong(this.tranIdInputText.Value);
            string tranCode = this.tranCodeInputText.Value;
            string book = this.bookInputText.Value;
            string referenceNumber = this.referenceNumberInputText.Value;
            string statementReference = this.statementReferenceInputText.Value;
            string postedBy = this.postedByInputText.Value;
            string office = this.officeInputText.Value;
            string status = this.statusInputText.Value;
            string verifiedBy = this.verifiedByInputText.Value;
            string reason = reasonInputText.Value;

            this.AddColumns(TransactionGridView);

            using (DataTable table = Data.Journal.GetJournalView(userId, officeId, from, to, tranId, tranCode, book, referenceNumber, statementReference, postedBy, office, status, verifiedBy, reason))
            {
                this.TransactionGridView.DataSource = table;
                this.TransactionGridView.DataBind();
            }
        }

        private void Flag_Updated(object sender, FlagUpdatedEventArgs e)
        {
            int flagTypeId = e.FlagId;

            const string resource = "transactions.transaction_master";
            const string resourceKey = "transaction_master_id";

            int userId = SessionHelper.GetUserId();

            TransactionGovernor.Flags.CreateFlag(userId, flagTypeId, resource, resourceKey, this.GetSelectedValues());

            this.BindGrid();
        }

        private Collection<int> GetSelectedValues()
        {
            string selectedValues = this.SelectedValuesHidden.Value;

            //Check if something was selected.
            if (string.IsNullOrWhiteSpace(selectedValues))
            {
                return new Collection<int>();
            }

            //Create a collection object to store the IDs.
            Collection<int> values = new Collection<int>();

            //Iterate through each value in the selected values
            //and determine if each value is a number.
            foreach (string value in selectedValues.Split(','))
            {
                //Parse the value to integer.
                int val = Conversion.TryCastInteger(value);

                if (val > 0)
                {
                    values.Add(val);
                }
            }

            return values;
        }

        private void SetDefaultValues()
        {
            this.bookInputText.Value = "Journal";
            this.postedByInputText.Value = SessionHelper.GetUserName();
            this.officeInputText.Value = SessionHelper.GetOfficeName();
            this.statusInputText.Value = "Approved";
        }
    }
}