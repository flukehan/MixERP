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

using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.StockTransactionFactory
{
    public partial class StockTransactionForm
    {
        private static void AddGrandTotalField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.GrandTotal, "GrandTotalInputTextInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText grandTotalInputTextInputText = new HtmlInputText())
                {
                    grandTotalInputTextInputText.ID = "GrandTotalInputText";
                    grandTotalInputTextInputText.Attributes.Add("class", "currency");
                    grandTotalInputTextInputText.Attributes.Add("readonly", "readonly");
                    field.Controls.Add(grandTotalInputTextInputText);
                }

                container.Controls.Add(field);
            }
        }

        private static void AddRunningTotalField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.RunningTotal, "RunningTotalInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText runningTotalInputText = new HtmlInputText())
                {
                    runningTotalInputText.ID = "RunningTotalInputText";
                    runningTotalInputText.Attributes.Add("class", "currency");
                    runningTotalInputText.Attributes.Add("readonly", "readonly");
                    field.Controls.Add(runningTotalInputText);
                }

                container.Controls.Add(field);
            }
        }

        private static void AddTaxTotalField(HtmlGenericControl container)
        {
            using (HtmlGenericControl field = HtmlControlHelper.GetField())
            {
                using (HtmlGenericControl label = HtmlControlHelper.GetLabel(Titles.TaxTotal, "TaxTotalInputText"))
                {
                    field.Controls.Add(label);
                }

                using (HtmlInputText taxTotalInputText = new HtmlInputText())
                {
                    taxTotalInputText.ID = "TaxTotalInputText";
                    taxTotalInputText.Attributes.Add("class", "currency");
                    taxTotalInputText.Attributes.Add("readonly", "readonly");
                    field.Controls.Add(taxTotalInputText);
                }

                container.Controls.Add(field);
            }
        }

        private static void AddTotalFields(HtmlGenericControl container)
        {
            using (HtmlGenericControl fields = HtmlControlHelper.GetFields("three fields"))
            {
                AddRunningTotalField(fields);
                AddTaxTotalField(fields);
                AddGrandTotalField(fields);

                container.Controls.Add(fields);
            }
        }
    }
}