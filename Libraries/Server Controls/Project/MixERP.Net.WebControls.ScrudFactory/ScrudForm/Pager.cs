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

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.WebControls.ScrudFactory.Data;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void CreatePager(Control container)
        {
            int totalRecords = FormHelper.GetTotalRecords(this.ViewSchema, this.View);
            int currentPage = Conversion.TryCastInteger(this.Page.Request.QueryString["page"]);
            int pageSize = this.GetPageSize();
            string cssClass = this.GetPagerCssClass();
            string activeCssClass = this.GetPagerCurrentPageCssClass();
            string itemCssClass = this.GetPagerPageButtonCssClass();

            if (currentPage.Equals(0))
            {
                currentPage = 1;
            }

            Pager pager = new Pager
            {
                TotalRecords = totalRecords,
                CurrentPage = currentPage,
                PageSize = pageSize,
                CssClass = cssClass,
                ActiveCssClass = activeCssClass,
                ItemCssClass = itemCssClass,
                CurrentPageUrl = this.Page.Request.Url.AbsolutePath
            };


            container.Controls.Add(pager.GetPager());
        }
    }


    internal class Pager
    {
        internal int TotalRecords { get; set; }
        private int TotalPages;
        internal int CurrentPage { get; set; }
        internal int PageSize { get; set; }

        internal string CssClass { get; set; }
        internal string ActiveCssClass { get; set; }
        internal string ItemCssClass { get; set; }

        internal string CurrentPageUrl { get; set; }

        private int start;
        private int end;


        internal HtmlGenericControl GetPager()
        {
            if (this.PageSize.Equals(0))
            {
                this.PageSize = 10;
            }

            this.TotalPages =(int)Decimal.Ceiling(Decimal.Divide(this.TotalRecords, this.PageSize));
            this.start = 1 + (int) Decimal.Ceiling(Decimal.Divide(this.CurrentPage, this.PageSize) - 1) * this.PageSize;
            this.end = this.start + 10 -1;

            if(this.end > this.TotalPages)
            {
                this.end = this.TotalPages;
            }


            using (HtmlGenericControl paginationMenu = new HtmlGenericControl("div"))
            {
                paginationMenu.Attributes.Add("class", this.CssClass);

                paginationMenu.Controls.Add(this.FirstItem());
                paginationMenu.Controls.Add(this.PreviousItem());
                paginationMenu.Controls.Add(this.GetAnchor(1));


                if (start - this.PageSize > 0)
                {
                    using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "backward blue icon", start - 1))
                    {
                        paginationMenu.Controls.Add(anchor);
                    }
                }

                for (int i = start; i <= end; i++)
                {
                    if (i.Equals(1) || i.Equals(this.TotalPages))
                    {
                        continue;
                    }

                    paginationMenu.Controls.Add(this.GetAnchor(i));
                }


                if (start + this.PageSize < this.TotalPages)
                {
                    using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "forward blue icon", end + 1))
                    {
                        paginationMenu.Controls.Add(anchor);
                    }
                }

                paginationMenu.Controls.Add(this.GetAnchor(this.TotalPages));
                paginationMenu.Controls.Add(this.NextItem());
                paginationMenu.Controls.Add(this.LastItem());


                return paginationMenu;
            }
        }



        private HtmlAnchor FirstItem()
        {
            int page = 0;

            if (this.CurrentPage > 1)
            {
                page = 1;
            }

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "fast backward icon", page))
            {
                return anchor;
            }
        }

        private HtmlAnchor PreviousItem()
        {
            int page = 0;
            if (this.CurrentPage > 1)
            {
                page = this.CurrentPage - 1;
            }

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "step backward icon", page))
            {

                return anchor;
            }
        }

        private HtmlAnchor NextItem()
        {
            int page = 0;

            if (this.CurrentPage < this.TotalPages)
            {
                page = this.CurrentPage + 1;
            }

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "step forward icon", page))
            {

                return anchor;
            }
        }

        private HtmlAnchor LastItem()
        {
            int page = 0;

            if (this.CurrentPage < this.TotalPages)
            {
                page = this.TotalPages;
            }

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "fast forward icon", page))
            {

                return anchor;
            }
        }

        private HtmlAnchor GetAnchor(int pageNumber)
        {
            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                if (pageNumber.Equals(this.CurrentPage))
                {
                    anchor.Attributes.Add("class", "active item");
                }
                else
                {
                    anchor.Attributes.Add("class", "item");                    
                }

                anchor.InnerText = pageNumber.ToString(System.Threading.Thread.CurrentThread.CurrentCulture);

                anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}", this.CurrentPageUrl, pageNumber);

                return anchor;
            }
        }

        private HtmlAnchor GetIconAnchor(string cssClass, string iconCssClass, int pageNumber)
        {
            using (HtmlAnchor anchor = new HtmlAnchor())
            {
                anchor.Attributes.Add("class", cssClass);

                using (HtmlGenericControl icon = new HtmlGenericControl("i"))
                {
                    icon.Attributes.Add("class", iconCssClass);
                    anchor.Controls.Add(icon);
                }

                if (pageNumber > 0)
                {
                    anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}", this.CurrentPageUrl, pageNumber);
                }

                return anchor;
            }
        }
    }
}