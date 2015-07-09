using MixERP.Net.i18n.Resources;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.TransactionViewFactory
{
    public partial class TransactionView
    {
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
                    button.Attributes.Add("title", "CTRL + K");
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
            if (this.DisplayFlagButton)
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
            if (this.DisplayPrintButton)
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
            if (this.DisplayRejectButton)
            {
                using (HtmlButton button = new HtmlButton())
                {
                    button.Attributes.Add("type", "button");
                    button.Attributes.Add("id", "RejectButton");
                    button.Attributes.Add("title", "CTRL + SHIFT + K");
                    button.Attributes.Add("class", "ui negative button");
                    button.InnerText = Titles.Reject;
                    container.Controls.Add(button);
                }
            }
        }
    }
}