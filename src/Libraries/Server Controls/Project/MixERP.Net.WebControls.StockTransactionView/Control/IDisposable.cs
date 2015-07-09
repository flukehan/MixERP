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

using MixERP.Net.WebControls.Common;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        private DateTextBox dateFromDateTextBox;
        private DateTextBox dateToDateTextBox;
        private HtmlGenericControl errorLabel;
        private Button mergeToDeliveryButton; //MergeToDeliveryButton_Click
        private Button mergeToGRNButton; //MergeToGRNButton_Click
        private Button mergeToOrderButton; //MergeToOrderButton_Click
        private HtmlInputText officeInputText;
        private HtmlInputText partyInputText;
        private PlaceHolder placeHolder;
        private HtmlInputText priceTypeInputText;
        private MixERPGridView productViewGridView;
        private HtmlInputText referenceNumberInputText;
        private Button returnButton; //ReturnButton_Click
        private HiddenField selectedValuesHidden;
        private HtmlButton showButton; //ShowButton_Click
        private HtmlInputText statementReferenceInputText;
        private HtmlInputText userInputText;
    }
}