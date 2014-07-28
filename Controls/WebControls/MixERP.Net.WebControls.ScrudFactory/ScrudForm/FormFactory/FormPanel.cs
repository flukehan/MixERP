/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Data;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        Panel formPanel;
        Panel form;
        Literal addNewEntryLiteral;
        Label requiredFieldDetailsLabel;
        Panel formContainer;
        Button useButton;
        Button saveButton;
        Button cancelButton;

        private void CreateFormPanel()
        {
            this.formPanel = new Panel();
            this.formPanel.ID = "FormPanel";
            this.formPanel.Style.Add("display", "none");

            this.form = new Panel();
            this.form.CssClass = "form";

            this.AddFormHeader(this.form);
            this.AddFormContainer(this.form);
            this.AddFormFooter(this.form);

            this.formPanel.Controls.Add(this.form);
        }

        private void AddFormHeader(Panel p)
        {
            using (var h3 = new HtmlGenericControl("h3"))
            {
                this.addNewEntryLiteral = new Literal();
                this.addNewEntryLiteral.Text = ScrudResource.AddNew;
                h3.Controls.Add(this.addNewEntryLiteral);

                p.Controls.Add(h3);
            }

            using (var ruler = new HtmlGenericControl("hr"))
            {
                ruler.Attributes.Add("class", "hr");

                p.Controls.Add(ruler);
            }

            this.requiredFieldDetailsLabel = new Label();
            this.requiredFieldDetailsLabel.CssClass = "info";
            this.requiredFieldDetailsLabel.Style.Add("text-align", "left");
            this.requiredFieldDetailsLabel.Style.Add("font-weight", "bold");
            this.requiredFieldDetailsLabel.Text = ScrudResource.RequiredFieldDetails;

            p.Controls.Add(this.requiredFieldDetailsLabel);


        }

        private bool IsModal()
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if (page != null)
            {
                var modal = page.Request.QueryString["modal"];
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

        private void AddFormContainer(Panel p)
        {
            this.formContainer = new Panel();
            p.Controls.Add(this.formContainer);
        }

        private void AddFormFooter(Panel p)
        {
            using (var htmlTable = new HtmlTable())
            {
                using (var row = new HtmlTableRow())
                {
                    using (var labelCell = new HtmlTableCell())
                    {
                        labelCell.Attributes.Add("class", "label-cell");

                        row.Cells.Add(labelCell);
                    }

                    using (var controlCell = new HtmlTableCell())
                    {
                        if (this.IsModal())
                        {
                            this.useButton = new Button();
                            this.useButton.ID = "UseButton";
                            this.useButton.Text = ScrudResource.Use;
                            this.useButton.OnClientClick = "adjustSpinnerSize();";

                            this.useButton.Click += this.UseButton_Click;

                            this.useButton.CssClass = this.GetButtonCssClass();

                            controlCell.Controls.Add(this.useButton);
                        }


                        this.saveButton = new Button();
                        this.saveButton.ID = "SaveButton";
                        this.saveButton.Text = ScrudResource.Save;
                        this.saveButton.OnClientClick = "adjustSpinnerSize();";

                        this.saveButton.Click += this.SaveButton_Click;

                        this.saveButton.CssClass = this.GetButtonCssClass();

                        controlCell.Controls.Add(this.saveButton);


                        this.cancelButton = new Button();
                        this.cancelButton.ID = "CancelButton";
                        this.cancelButton.Text = ScrudResource.Cancel;
                        this.cancelButton.CausesValidation = false;
                        this.cancelButton.OnClientClick = "$('#FormPanel').hide(500); $('#GridPanel').show(500);";
                        this.cancelButton.Click += this.CancelButton_Click;
                        this.cancelButton.CssClass = this.GetButtonCssClass();

                        controlCell.Controls.Add(this.cancelButton);

                        using (var resetButton = new HtmlInputReset())
                        {
                            resetButton.Value = ScrudResource.Reset;
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

        private void InitializeScrudControl()
        {
            using (var table = new DataTable())
            {
                table.Locale = Thread.CurrentThread.CurrentCulture;
                this.LoadForm(this.formContainer, table);
            }
        }
    }
}
