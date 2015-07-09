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
using System;
using System.Globalization;
using System.Threading;
using System.Web.UI.HtmlControls;

namespace MixERP.Net.WebControls.ScrudFactory.Controls
{
    internal class ScrudPager
    {
        private int TotalPages;
        internal int TotalRecords { get; set; }
        internal int CurrentPage { get; set; }
        internal int PageSize { get; set; }
        internal string CssClass { get; set; }
        internal string ActiveCssClass { get; set; }
        internal string ItemCssClass { get; set; }
        internal string CurrentPageUrl { get; set; }
        internal string QueryString { get; set; }
        public int BlockCount { get; set; }

        internal HtmlGenericControl GetPager()
        {
            if (this.PageSize.Equals(0))
            {
                this.PageSize = this.BlockCount;
            }

            this.TotalPages = (int)Decimal.Ceiling(Decimal.Divide(this.TotalRecords, this.PageSize));

            int start = 1 + (int)Decimal.Ceiling(Decimal.Divide(this.CurrentPage, this.BlockCount) - 1) * this.BlockCount;
            int end = start + this.BlockCount - 1;

            if (end > this.TotalPages)
            {
                end = this.TotalPages;
            }


            using (HtmlGenericControl paginationMenu = new HtmlGenericControl("div"))
            {
                paginationMenu.Attributes.Add("class", this.CssClass);

                paginationMenu.Controls.Add(this.FirstItem());
                paginationMenu.Controls.Add(this.PreviousItem());
                paginationMenu.Controls.Add(this.GetAnchor(1));


                //The previous page block
                string title;
                if (start - this.BlockCount > 0)
                {
                    using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "", start - 1))
                    {
                        title = string.Format(Thread.CurrentThread.CurrentCulture, Titles.PageN, start - 1);
                        anchor.Attributes.Add("title", title);

                        anchor.InnerText = "...";
                        paginationMenu.Controls.Add(anchor);
                    }
                }

                //Paged items
                for (int i = start; i <= end; i++)
                {
                    //Do not create the first and last page
                    //because we will create them explicitly
                    //which will be followed/led 
                    //by next/previous page blocks
                    if (i.Equals(1) || i.Equals(this.TotalPages))
                    {
                        continue;
                    }

                    paginationMenu.Controls.Add(this.GetAnchor(i));
                }


                //The next page block
                if (start + this.BlockCount < this.TotalPages)
                {
                    using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "", end + 1))
                    {
                        title = string.Format(Thread.CurrentThread.CurrentCulture, Titles.PageN, end + 1);
                        anchor.Attributes.Add("title", title);

                        anchor.InnerText = "...";
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

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "chevron circle left icon", page))
            {
                anchor.Attributes.Add("title", Titles.FirstPage);
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

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "arrow circle left icon", page))
            {
                anchor.Attributes.Add("title", Titles.PreviousPage);
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

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "arrow circle right icon", page))
            {
                anchor.Attributes.Add("title", Titles.NextPage);
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

            using (HtmlAnchor anchor = this.GetIconAnchor("icon item", "chevron circle right icon", page))
            {
                anchor.Attributes.Add("title", Titles.LastPage);
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

                anchor.InnerText = pageNumber.ToString(Thread.CurrentThread.CurrentCulture);

                if (this.QueryString == "modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden")
                {
                    anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}&modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden", this.CurrentPageUrl, pageNumber);
                }
                else if (this.QueryString == (string.Format(CultureInfo.InvariantCulture, "page={0}&modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden", this.CurrentPage)))
                {
                    anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}&modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden", this.CurrentPageUrl, pageNumber);
                }
                else
                {
                    anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}", this.CurrentPageUrl, pageNumber);
                }


                string title = string.Format(Thread.CurrentThread.CurrentCulture, Titles.PageN, pageNumber);
                anchor.Attributes.Add("title", title);

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

                if (this.QueryString == "modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden")
                {
                    anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}&modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden", this.CurrentPageUrl, pageNumber);
                    return anchor;
                }
                
                if (this.QueryString == (string.Format(CultureInfo.InvariantCulture, "page={0}&modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden", this.CurrentPage)))
                {
                    anchor.HRef = string.Format(CultureInfo.InvariantCulture, "{0}?page={1}&modal=1&CallBackFunctionName=processCallBackActions&AssociatedControlId=PartyIdHidden", this.CurrentPageUrl, pageNumber);
                    return anchor;
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