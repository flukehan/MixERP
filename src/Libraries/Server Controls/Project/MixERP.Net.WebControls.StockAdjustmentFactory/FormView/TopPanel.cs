using MixERP.Net.Entities;
using MixERP.Net.i18n.Resources;
using MixERP.Net.WebControls.Common;
using MixERP.Net.WebControls.StockAdjustmentFactory.Helpers;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockAdjustmentFactory
{
    public partial class FormView
    {
        private void AddDateTextBox(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = FormHelper.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "ValueDateTextBox");
                    label.InnerText = Titles.ValueDate;
                    field.Controls.Add(label);
                }

                this.dateTextBox = new DateTextBox();
                this.dateTextBox.ID = "ValueDateTextBox";
                this.dateTextBox.Mode = FrequencyType.Today;
                this.dateTextBox.Catalog = this.Catalog;
                this.dateTextBox.OfficeId = this.OfficeId;

                field.Controls.Add(this.dateTextBox);

                fields.Controls.Add(field);
            }
        }

        private void AddReferenceNumberTextBox(HtmlGenericControl fields)
        {
            using (HtmlGenericControl field = FormHelper.GetField())
            {
                using (HtmlGenericControl label = new HtmlGenericControl())
                {
                    label.TagName = "label";
                    label.Attributes.Add("for", "ReferenceNumberInputText");
                    label.InnerText = Titles.RefererenceNumberAbbreviated;
                    field.Controls.Add(label);
                }

                using (HtmlInputText referenceNumberInputText = new HtmlInputText())
                {
                    referenceNumberInputText.ID = "ReferenceNumberInputText";
                    referenceNumberInputText.MaxLength = 24;

                    field.Controls.Add(referenceNumberInputText);
                }
                fields.Controls.Add(field);
            }
        }

        private void CreateTopPanel()
        {
            using (HtmlGenericControl form = new HtmlGenericControl())
            {
                form.TagName = "div";
                form.Attributes.Add("class", "ui form");

                using (HtmlGenericControl fields = FormHelper.GetFields())
                {
                    this.AddDateTextBox(fields);
                    this.AddReferenceNumberTextBox(fields);

                    form.Controls.Add(fields);
                }
                this.container.Controls.Add(form);
            }
        }
    }
}