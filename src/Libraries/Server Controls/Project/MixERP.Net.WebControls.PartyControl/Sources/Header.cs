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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public partial class PartyControl
    {
        private static Control GetHeader()
        {
            using (HtmlGenericControl form = ControlHelper.GetGenericControl(@"div", @"ui form"))
            {
                using (HtmlGenericControl fields = ControlHelper.GetGenericControl("div", "inline fields"))
                {
                    using (HtmlGenericControl partyCodeField = ControlHelper.GetGenericControl(@"div", @"small field"))
                    {
                        using (Literal partyDropDownListLabelLiteral = new Literal())
                        {
                            partyDropDownListLabelLiteral.Text = ControlHelper.GetLabelText(@"PartyCodeTextBox", Titles.SelectCustomer);

                            using (HtmlInputText partyCodeTextBox = ControlHelper.GetInputText(@"PartyCodeTextBox", string.Empty))
                            {
                                partyCodeField.Controls.Add(partyDropDownListLabelLiteral);
                                partyCodeField.Controls.Add(partyCodeTextBox);
                            }

                            fields.Controls.Add(partyCodeField);
                        }
                    }

                    using (HtmlGenericControl partyDropDownListField = ControlHelper.GetGenericControl("div", "medium field"))
                    {
                        using (HtmlSelect partyDropDownList = ControlHelper.GetSelect(@"PartyDropDownList", string.Empty))
                        {
                            partyDropDownListField.Controls.Add(partyDropDownList);
                        }

                        fields.Controls.Add(partyDropDownListField);
                    }

                    using (HtmlButton goButton = ControlHelper.GetButton(@"GoButton", @"ui small teal button", Titles.Go))
                    {
                        fields.Controls.Add(goButton);
                    }

                    form.Controls.Add(fields);
                    return form;
                }
            }
        }
    }
}