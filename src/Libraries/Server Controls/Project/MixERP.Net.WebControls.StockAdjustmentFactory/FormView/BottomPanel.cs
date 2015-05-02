using System.Web.UI.HtmlControls;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.StockAdjustmentFactory.Helpers;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void AddErrorLabelBottom()
        {
            using (HtmlGenericControl errorLabel = new HtmlGenericControl())
            {
                errorLabel.TagName = "div";
                errorLabel.ID = "ErrorLabel";
                errorLabel.Attributes.Add("class", "big error vpad16");
                this.container.Controls.Add(errorLabel);
            }
        }

        private void AddSaveButton(HtmlGenericControl div)
        {
            using (HtmlInputButton button = new HtmlInputButton())
            {
                button.ID = "SaveButton";
                button.Value = Titles.Save;
                button.Attributes.Add("class", "ui small blue button");
                div.Controls.Add(button);
            }
        }

        private void AddStatementReferenceTextArea(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = FormHelper.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "StatementReferenceTextArea");
                    label.InnerText = Titles.StatementReference;
                    field.Controls.Add(label);
                }

                using (HtmlTextArea statementReferenceTextArea = new HtmlTextArea())
                {
                    statementReferenceTextArea.ID = "StatementReferenceTextArea";
                    statementReferenceTextArea.Rows = 4;

                    field.Controls.Add(statementReferenceTextArea);
                }

                fields.Controls.Add(field);
            }
        }

        private void CreateBottomPanel()
        {
            using (HtmlGenericControl fields = FormHelper.GetFields())
            {
                fields.TagName = "div";
                fields.Attributes.Add("class", "ui form");
                fields.Attributes.Add("style", "width:290px;");

                this.AddStatementReferenceTextArea(fields);
                this.AddSaveButton(fields);

                this.container.Controls.Add(fields);
            }
        }
    }
}