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

using MixERP.Net.Entities;

namespace MixERP.Net.WebControls.StockTransactionViewFactory
{
    public partial class StockTransactionView
    {
        public string AddNewUrl { get; set; }
        public string Catalog { get; set; }
        public TranBook Book { get; set; }
        public string ChecklistUrl { get; set; }
        public string PreviewUrl { get; set; }
        public bool ShowMergeToDeliveryButton { get; set; }
        public string MergeToDeliveryButtonUrl { get; set; }
        public bool ShowMergeToGRNButton { get; set; }
        public string MergeToGRNButtonUrl { get; set; }
        public bool ShowMergeToOrderButton { get; set; }
        public string MergeToOrderButtonUrl { get; set; }
        public SubTranBook SubBook { get; set; }
        public string Text { get; set; }
        public string DbTableName { get; set; }
        public string PrimaryKey { get; set; }
        public bool IsNonGlTransaction { get; set; }
        public bool ShowReturnButton { get; set; }
        public string ReturnButtonUrl { get; set; }
        public int UserId { get; set; }
        public int OfficeId { get; set; }
    }
}