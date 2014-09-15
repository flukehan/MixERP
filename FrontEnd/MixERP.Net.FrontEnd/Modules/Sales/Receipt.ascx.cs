using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.TransactionGovernor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MixERP.Net.Core.Modules.Sales
{
    public partial class Receipt : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.LoadLocalizedTexts();
            this.LoadGridView();
            base.OnControlLoad(sender, e);
        }

        private void LoadLocalizedTexts()
        {
            this.AddNewLiteral.Text = Resources.Titles.AddNew;
            this.TitleLiteral.Text = Resources.Titles.SalesReceipt;
            this.FlagLiteral.Text = Resources.Titles.Flag;
            this.PrintLiteral.Text = Resources.Titles.Print;
            this.FlagThisTransactionLiteral.Text = Resources.Titles.FlagThisTransaction;

            this.FlagDescriptionLiteral.Text = Resources.Titles.FlagDescription;
            this.SelectFlagLiteral.Text = Resources.Titles.SelectFlag;
            this.UpdateButton.Text = Resources.Titles.Update;
            this.CloseLiteral.Text = Resources.Titles.Close;
        }

        private Collection<int> GetSelectedValues()
        {
            //Get the comma separated selected values.
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

                //If the object "val" has a greater than zero,
                //add it to the collection.
                if (val > 0)
                {
                    values.Add(val);
                }
            }

            return values;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int flagTypeId = Conversion.TryCastInteger(this.FlagTypeHidden.Value);
            const string resource = "transactions.transaction_master";
            const string resourceKey = "transaction_master_id";
            Collection<int> resourceIds = this.GetSelectedValues();
            int userId = SessionHelper.GetUserId();

            Flags.CreateFlag(userId, flagTypeId, resource, resourceKey, resourceIds);
            this.LoadGridView();
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            this.LoadGridView();
        }

        private void LoadGridView()
        {
            DateTime dateFrom = Conversion.TryCastDate(this.DateFromDateTextBox.Text);
            DateTime dateTo = Conversion.TryCastDate(this.DateToDateTextBox.Text);
            string office = this.OfficeTextBox.Text;
            string party = this.PartyTextBox.Text;
            string user = this.UserTextBox.Text;
            string referenceNumber = this.ReferenceNumberTextBox.Text;
            string statementReference = this.StatementReferenceTextBox.Text;
            int userId = SessionHelper.GetUserId();
            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = Data.Helpers.Receipt.GetView(userId, officeId, dateFrom, dateTo, office, party, user, referenceNumber, statementReference))
            {
                this.ReceiptViewGridView.DataSource = table;
                this.ReceiptViewGridView.DataBind();
            }
        }

        private static string GetResourceString(string key)
        {
            const string className = "MixERP.Net.Core.Modules.Sales.Resources.DbResource";
            return LocalizationHelper.GetResourceString(className, key, Assembly.GetExecutingAssembly());
        }

        protected void ReceiptGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text.Replace("&nbsp;", " ").Trim();

                    if (!string.IsNullOrWhiteSpace(cellText))
                    {
                        cellText = GetResourceString(cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                }
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string id = e.Row.Cells[2].Text;
            //    //Todo: Fix 403 errors.
            //    if (!string.IsNullOrWhiteSpace(id))
            //    {
            //        if (!string.IsNullOrWhiteSpace(this.PreviewUrl))
            //        {
            //            string popUpQuotationPreviewUrl = this.Page.ResolveUrl(this.PreviewUrl + "?TranId=" + id);

            //            using (HtmlAnchor previewAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("PreviewAnchor"))
            //            {
            //                if (previewAnchor != null)
            //                {
            //                    previewAnchor.HRef = popUpQuotationPreviewUrl;
            //                }
            //            }

            //            using (HtmlAnchor printAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("PrintAnchor"))
            //            {
            //                if (printAnchor != null)
            //                {
            //                    printAnchor.Attributes.Add("onclick", "showWindow('" + popUpQuotationPreviewUrl + "');return false;");
            //                }
            //            }
            //        }

            //        using (HtmlAnchor checklistAnchor = (HtmlAnchor)e.Row.Cells[0].FindControl("ChecklistAnchor"))
            //        {
            //            if (!string.IsNullOrWhiteSpace(this.ChecklistUrl))
            //            {
            //                if (checklistAnchor != null)
            //                {
            //                    string checkListUrl = this.Page.ResolveUrl(this.ChecklistUrl + "?TranId=" + id);
            //                    checklistAnchor.HRef = checkListUrl;
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}