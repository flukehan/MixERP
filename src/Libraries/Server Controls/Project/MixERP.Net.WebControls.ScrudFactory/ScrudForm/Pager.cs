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

using MixERP.Net.Common;
using MixERP.Net.WebControls.ScrudFactory.Controls;
using MixERP.Net.WebControls.ScrudFactory.Data;
using System.Web.UI;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void CreatePager(Control container)
        {
            int pageSize = this.GetPageSize();
            int totalRecords = FormHelper.GetTotalRecords(this.Catalog, this.ViewSchema, this.View);

            int currentPage = Conversion.TryCastInteger(this.Page.Request.QueryString["page"]);
            if (currentPage.Equals(0))
            {
                currentPage = 1;
            }

            string cssClass = this.GetPagerCssClass();
            string activeCssClass = this.GetPagerCurrentPageCssClass();
            string itemCssClass = this.GetPagerPageButtonCssClass();

            if (totalRecords == 0 || pageSize > totalRecords)
            {
                return;
            }

            const int blockCount = 10; //This can be parameterized.

            this.CreatePager(container, pageSize, totalRecords, currentPage, blockCount, cssClass, activeCssClass, itemCssClass);
        }

        private void CreatePager(Control container, int pageSize, int totalRecords, int currentPage, int blockCount, string cssClass, string activeCssClass, string itemCssClass)
        {
            string queryString = this.Page.Request.QueryString.ToString();
           
            ScrudPager scrudPager = new ScrudPager
             {
                 TotalRecords = totalRecords,
                 CurrentPage = currentPage,
                 PageSize = pageSize,
                 BlockCount = blockCount,
                 CssClass = cssClass,
                 ActiveCssClass = activeCssClass,
                 ItemCssClass = itemCssClass,
                 CurrentPageUrl = this.Page.Request.Url.AbsolutePath,
                 QueryString=queryString
             };

            container.Controls.Add(scrudPager.GetPager());
        }
    }
}