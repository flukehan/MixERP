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

using MixERP.Net.i18n.Resources;
using System.Data;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private Button cancelButton;
        private Panel form;
        private Panel formContainer;
        private Panel formPanel;
        private Literal requiredFieldDetailsLiteral;
        private Button saveButton;
        private Button useButton;

        private void AddFormContainer(Panel p)
        {
            this.formContainer = new Panel();
            p.Controls.Add(this.formContainer);
        }

        private void AddFormFooter(Panel p)
        {
            using (HtmlTable htmlTable = new HtmlTable())
            {
                using (HtmlTableRow row = new HtmlTableRow())
                {
                    using (HtmlTableCell labelCell = new HtmlTableCell())
                    {
                        labelCell.Attributes.Add("class", "label-cell");

                        row.Cells.Add(labelCell);
                    }

                    using (HtmlTableCell controlCell = new HtmlTableCell())
                    {
                        controlCell.Attributes.Add("class", "control-cell");

                        if (this.IsModal())
                        {
                            this.useButton = new Button();
                            this.useButton.ID = "UseButton";
                            this.useButton.Text = Titles.Use;
                            this.useButton.OnClientClick = "scrudDispalyLoading();";

                            this.useButton.Click += this.UseButton_Click;

                            this.useButton.CssClass = this.GetButtonCssClass();

                            controlCell.Controls.Add(this.useButton);
                        }

                        this.saveButton = new Button();
                        this.saveButton.ID = "SaveButton";
                        this.saveButton.Text = Titles.Save;
                        this.saveButton.OnClientClick = "return(scrudClientValidation());";

                        this.saveButton.Click += this.SaveButton_Click;

                        this.saveButton.CssClass = this.GetSaveButtonCssClass();

                        controlCell.Controls.Add(this.saveButton);

                        this.cancelButton = new Button();
                        this.cancelButton.ID = "CancelButton";
                        this.cancelButton.Text = Titles.Cancel;
                        this.cancelButton.CausesValidation = false;
                        this.cancelButton.OnClientClick = "$('#FormPanel').hide(500); $('#GridPanel').show(500);";
                        this.cancelButton.Click += this.CancelButton_Click;
                        this.cancelButton.CssClass = this.GetButtonCssClass();

                        controlCell.Controls.Add(this.cancelButton);

                        using (HtmlInputReset resetButton = new HtmlInputReset())
                        {
                            resetButton.Value = Titles.Reset;
                            resetButton.Attributes.Add("class", this.GetButtonCssClass());

                            controlCell.Controls.Add(resetButton);
                        }

                        row.Cells.Add(controlCell);

                        htmlTable.Rows.Add(row);
                        p.Controls.Add(htmlTable);
                    }
                }
            }
        }

        private void AddFormHeader(Panel p)
        {
            this.requiredFieldDetailsLiteral = new Literal();
            this.requiredFieldDetailsLiteral.Text = @"<div class='form-description'>" + Titles.RequiredFieldDetails + @"</div>"; //Todo:parameterize css class

            p.Controls.Add(this.requiredFieldDetailsLiteral);
        }

        private void CreateFormPanel()
        {
            this.formPanel = new Panel();
            this.formPanel.ID = "FormPanel";
            this.formPanel.Style.Add("display", "none");
            this.formPanel.CssClass = this.GetFormPanelCssClass();

            this.form = new Panel();
            this.form.CssClass = this.GetFormCssClass();
            this.form.Attributes.Add("role", "form");

            this.AddFormHeader(this.form);
            this.AddFormContainer(this.form);
            this.AddFormFooter(this.form);

            this.formPanel.Controls.Add(this.form);
        }

        private void InitializeScrudControl()
        {
            using (DataTable table = new DataTable())
            {
                table.Locale = Thread.CurrentThread.CurrentCulture;
                this.LoadForm(this.formContainer, table);
            }
        }

        private bool IsModal()
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if (page != null)
            {
                string modal = page.Request.QueryString["modal"];
                if (modal != null)
                {
                    if (modal.Equals("1"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}