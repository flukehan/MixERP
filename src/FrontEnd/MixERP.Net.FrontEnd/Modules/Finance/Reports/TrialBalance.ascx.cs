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

using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Finance.Reports
{
    public partial class TrialBalance : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.CreateHeader(this.Placeholder1);
            this.CreateForm(this.Placeholder1);
            this.CreateGrid(this.Placeholder1);

            if (!this.Page.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void CreateHeader(Control container)
        {
            using (HtmlGenericControl header = new HtmlGenericControl("h2"))
            {
                header.InnerText = Titles.TrialBalance;
                container.Controls.Add(header);
            }
        }

        #region GridView

        private GridView trialBalanceGridView;

        private void CreateColumns()
        {
            this.trialBalanceGridView.AutoGenerateColumns = false;

            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "account_number", Titles.AccountNumber);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "account", Titles.Account);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "previous_debit", Titles.Debit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "previous_credit", Titles.Credit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "debit", Titles.Debit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "credit", Titles.Credit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "closing_debit", Titles.Debit, "{0:N}", true);
            GridViewHelper.AddDataBoundControl(this.trialBalanceGridView, "closing_credit", Titles.Credit, "{0:N}", true);
        }

        private void CreateGrid(Control container)
        {
            using (HtmlGenericControl gridPanel = new HtmlGenericControl("div"))
            {
                gridPanel.Attributes.Add("style", "width:100%;");

                this.trialBalanceGridView = new GridView();
                this.trialBalanceGridView.ID = "TrialBalanceGridView";
                this.trialBalanceGridView.GridLines = GridLines.None;
                this.trialBalanceGridView.CssClass = "ui celled segment table nowrap";
                this.trialBalanceGridView.DataBound += this.TrialBalanceGridView_DataBound;

                this.CreateColumns();

                gridPanel.Controls.Add(this.trialBalanceGridView);

                container.Controls.Add(gridPanel);
            }
        }

        private void TrialBalanceGridView_DataBound(object sender, EventArgs eventArgs)
        {
            if (this.trialBalanceGridView.HeaderRow != null)
            {
                this.trialBalanceGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        #endregion

        #region Form

        private HiddenField changeSideWhenNegativeHidden;
        private HtmlInputText factorInputText;
        private DateTextBox fromDateTextBox;
        private HiddenField includeZeroBalanceAccountHidden;
        private HiddenField isCompactHidden;
        private Button showButton;
        private DateTextBox toDateTextBox;

        private void AddCompactCheckBoxField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl slider = new HtmlGenericControl("div"))
                {
                    slider.Attributes.Add("class", "ui checkbox");

                    using (HtmlInputCheckBox checkBox = new HtmlInputCheckBox())
                    {
                        checkBox.ID = "CompactCheckBox";
                        checkBox.Checked = false;

                        slider.Controls.Add(checkBox);
                        this.isCompactHidden = new HiddenField();
                        this.isCompactHidden.ID = "IsCompactHidden";
                        this.isCompactHidden.Value = "0";

                        slider.Controls.Add(this.isCompactHidden);
                    }

                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ShowCompact))
                    {
                        slider.Controls.Add(label);
                    }

                    field.Controls.Add(slider);
                }

                container.Controls.Add(field);
            }
        }

        private void AddFactorField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.Factor, "FactorInputText"))
                {
                    field.Controls.Add(label);
                }
                this.factorInputText = new HtmlInputText();
                this.factorInputText.ID = "FactorInputText";
                this.factorInputText.Attributes.Add("class", "small input integer");
                this.factorInputText.Value = "1000";

                field.Controls.Add(this.factorInputText);

                container.Controls.Add(field);
            }
        }

        private void AddFromDateTextBoxField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.From, "FromDateTextBox"))
                {
                    field.Controls.Add(label);
                }
                this.fromDateTextBox = new DateTextBox();
                this.fromDateTextBox.ID = "FromDateTextBox";
                this.fromDateTextBox.Mode = FrequencyType.QuarterStartDate;
                this.fromDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.fromDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();


                field.Controls.Add(this.fromDateTextBox);

                container.Controls.Add(field);
            }
        }

        private void AddPrintButton(HtmlGenericControl container)
        {
            using (HtmlInputButton printButton = new HtmlInputButton())
            {
                printButton.ID = "PrintButton";
                printButton.Attributes.Add("class", "ui orange button");

                printButton.Value = Titles.Print;

                container.Controls.Add(printButton);
            }
        }

        private void AddShowButton(HtmlGenericControl container)
        {
            this.showButton = new Button();
            this.showButton.ID = "ShowButton";
            this.showButton.Text = Titles.Show;
            this.showButton.CssClass = "ui positive button";
            this.showButton.Click += this.ShowButton_Click;

            container.Controls.Add(this.showButton);
        }

        private void AddToDateTextBoxField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.To, "ToDateTextBox"))
                {
                    field.Controls.Add(label);
                }

                this.toDateTextBox = new DateTextBox();
                this.toDateTextBox.ID = "ToDateTextBox";
                this.toDateTextBox.Mode = FrequencyType.QuarterEndDate;
                this.toDateTextBox.Catalog = AppUsers.GetCurrentUserDB();
                this.toDateTextBox.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

                field.Controls.Add(this.toDateTextBox);

                container.Controls.Add(field);
            }
        }

        private void BindGrid()
        {
            DateTime from = Conversion.TryCastDate(this.fromDateTextBox.Text);
            DateTime to = Conversion.TryCastDate(this.toDateTextBox.Text);
            decimal factor = Conversion.TryCastDecimal(this.factorInputText.Value);

            bool compact = this.ShowCompact();
            bool changeSide = this.ChangeSideWhenNegative();
            bool includeZeroBalanceAccounts = this.IncludeZeroBalanceAccounts();

            int userId = AppUsers.GetCurrent().View.UserId.ToInt();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            using (DataTable table = Data.Reports.TrialBalance.GetTrialBalance(AppUsers.GetCurrentUserDB(), from, to, userId, officeId, compact, factor, changeSide, includeZeroBalanceAccounts))
            {
                this.trialBalanceGridView.DataSource = table;
                this.trialBalanceGridView.DataBind();
            }
        }

        private void ChangeSideCheckBoxField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl slider = new HtmlGenericControl("div"))
                {
                    slider.Attributes.Add("class", "ui checkbox");

                    using (HtmlInputCheckBox checkBox = new HtmlInputCheckBox())
                    {
                        checkBox.ID = "ChangeSideCheckBox";
                        checkBox.Checked = true;

                        slider.Controls.Add(checkBox);
                        this.changeSideWhenNegativeHidden = new HiddenField();
                        this.changeSideWhenNegativeHidden.ID = "ChangeSideWhenNegativeHidden";
                        this.changeSideWhenNegativeHidden.Value = "1";

                        slider.Controls.Add(this.changeSideWhenNegativeHidden);
                    }

                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.ChangeSideWhenNegative))
                    {
                        slider.Controls.Add(label);
                    }

                    field.Controls.Add(slider);
                }

                container.Controls.Add(field);
            }
        }

        private bool ChangeSideWhenNegative()
        {
            return this.changeSideWhenNegativeHidden.Value.Equals("1");
        }

        private void CreateForm(Control container)
        {
            using (HtmlGenericControl formSegment = new HtmlGenericControl("div"))
            {
                formSegment.Attributes.Add("class", "ui form segment");

                using (HtmlGenericControl fields = HtmlControlHelper.GetFields("eight inline fields"))
                {
                    this.AddFromDateTextBoxField(fields);
                    this.AddToDateTextBoxField(fields);
                    this.AddFactorField(fields);
                    this.AddCompactCheckBoxField(fields);
                    this.ChangeSideCheckBoxField(fields);
                    this.IncludeZeroBalanceAccountsCheckBoxField(fields);
                    this.AddShowButton(fields);
                    this.AddPrintButton(fields);
                    formSegment.Controls.Add(fields);
                }
                container.Controls.Add(formSegment);
            }
        }

        private bool IncludeZeroBalanceAccounts()
        {
            return this.includeZeroBalanceAccountHidden.Value.Equals("1");
        }

        private void IncludeZeroBalanceAccountsCheckBoxField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl slider = new HtmlGenericControl("div"))
                {
                    slider.Attributes.Add("class", "ui checkbox");

                    using (HtmlInputCheckBox checkBox = new HtmlInputCheckBox())
                    {
                        checkBox.ID = "ZeroBalanceCheckBox";
                        checkBox.Checked = true;

                        slider.Controls.Add(checkBox);
                        this.includeZeroBalanceAccountHidden = new HiddenField();
                        this.includeZeroBalanceAccountHidden.ID = "IncludeZeroBalanceAccountHidden";
                        this.includeZeroBalanceAccountHidden.Value = "1";

                        slider.Controls.Add(this.includeZeroBalanceAccountHidden);
                    }

                    using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.IncludeZeroBalanceAccounts))
                    {
                        slider.Controls.Add(label);
                    }

                    field.Controls.Add(slider);
                }

                container.Controls.Add(field);
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private bool ShowCompact()
        {
            return this.isCompactHidden.Value.Equals("1");
        }

        #endregion
    }
}