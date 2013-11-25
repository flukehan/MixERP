/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        Panel formPanel;
        Panel form;
        Literal addNewEntryLiteral;
        Label requiredFieldDetailsLabel;
        Panel formContainer;
        Button saveButton;
        Button cancelButton;

        private void CreateFormPanel()
        {
            formPanel = new Panel();
            formPanel.ID = "FormPanel";
            formPanel.Style.Add("display", "none");

            form = new Panel();
            form.CssClass = "form";

            this.AddFormHeader(form);
            this.AddFormContainer(form);
            this.AddFormFooter(form);

            formPanel.Controls.Add(form);
        }

        private void AddFormHeader(Panel p)
        {
            using (HtmlGenericControl h3 = new HtmlGenericControl("h3"))
            {
                addNewEntryLiteral = new Literal();
                addNewEntryLiteral.Text = Resources.ScrudResource.AddNew;
                h3.Controls.Add(addNewEntryLiteral);

                p.Controls.Add(h3);
            }

            using (HtmlGenericControl ruler = new HtmlGenericControl("hr"))
            {
                ruler.Attributes.Add("class", "hr");

                p.Controls.Add(ruler);
            }

            requiredFieldDetailsLabel = new Label();
            requiredFieldDetailsLabel.CssClass = "info";
            requiredFieldDetailsLabel.Style.Add("text-align", "left");
            requiredFieldDetailsLabel.Style.Add("font-weight", "bold");
            requiredFieldDetailsLabel.Text = Resources.ScrudResource.RequiredFieldDetails;

            p.Controls.Add(requiredFieldDetailsLabel);


        }

        private void AddFormContainer(Panel p)
        {
            formContainer = new Panel();
            p.Controls.Add(formContainer);
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
                        saveButton = new Button();
                        saveButton.ID = "SaveButton";
                        saveButton.Text = Resources.ScrudResource.Save;
                        saveButton.OnClientClick = "adjustSpinnerSize();";
                        saveButton.Click += SaveButton_Click;
                        saveButton.CssClass = this.GetButtonCssClass();

                        controlCell.Controls.Add(saveButton);


                        cancelButton = new Button();
                        cancelButton.ID = "CancelButton";
                        cancelButton.Text = Resources.ScrudResource.Cancel;
                        cancelButton.CausesValidation = false;
                        cancelButton.OnClientClick = "$('#FormPanel').hide(500); $('#GridPanel').show(500);";
                        cancelButton.Click += CancelButton_Click;
                        cancelButton.CssClass = this.GetButtonCssClass();

                        controlCell.Controls.Add(cancelButton);

                        using (HtmlInputReset resetButton = new HtmlInputReset())
                        {
                            resetButton.Value = Resources.ScrudResource.Reset;
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
            using (System.Data.DataTable table = new System.Data.DataTable())
            {
                table.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                this.LoadForm(this.formContainer, table);
            }
        }
    }
}
