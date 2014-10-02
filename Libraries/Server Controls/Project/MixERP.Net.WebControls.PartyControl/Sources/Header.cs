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

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.PartyControl
{
    public partial class PartyControl
    {
        private Control GetHeader()
        {
            using (HtmlGenericControl form = ControlHelper.GetGenericControl(@"div", @"grey form-inline", @"form"))
            {
                using (HtmlGenericControl formGroup = ControlHelper.GetGenericControl(@"div", @"form-group form-group-sm"))
                {
                    using (Literal partyDropDownListLabelLiteral = new Literal())
                    {
                        partyDropDownListLabelLiteral.Text = ControlHelper.GetLabelText(@"PartyDropDownList", "Select Customer");

                        using (HtmlGenericControl partyCodeTextBoxFormGroup = ControlHelper.GetGenericControl(@"div", @"form-group form-group-sm"))
                        {
                            using (HtmlInputText partyCodeTextBox = ControlHelper.GetInputText(@"PartyCodeTextBox", @"form-control input-sm"))
                            {
                                partyCodeTextBox.Style.Add(@"width", @"100px");
                                partyCodeTextBoxFormGroup.Controls.Add(partyCodeTextBox);
                            }

                            formGroup.Controls.Add(partyCodeTextBoxFormGroup);
                        }

                        using (HtmlGenericControl partyDropDownListFormGroup = ControlHelper.GetGenericControl(@"div", @"input-group input-group-sm"))
                        {
                            using (HtmlSelect partyDropDownList = ControlHelper.GetSelect(@"PartyDropDownList", @"form-control"))
                            {
                                partyDropDownListFormGroup.Controls.Add(partyDropDownList);
                            }

                            using (HtmlGenericControl goButtonSpan = ControlHelper.GetGenericControl(@"span", @"input-group-btn"))
                            {
                                using (HtmlButton goButton = ControlHelper.GetButton(@"GoButton", @"btn btn-primary", "Go"))
                                {
                                    goButtonSpan.Controls.Add(goButton);
                                }

                                partyDropDownListFormGroup.Controls.Add(goButtonSpan);
                            }

                            formGroup.Controls.Add(partyDropDownListFormGroup);
                        }
                    }

                    form.Controls.Add(formGroup);

                    return form;
                }
            }
        }
    }
}