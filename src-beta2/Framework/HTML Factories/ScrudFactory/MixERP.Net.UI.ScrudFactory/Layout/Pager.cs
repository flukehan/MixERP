using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using MixERP.Net.Common;
using MixERP.Net.Framework;
using MixERP.Net.UI.ScrudFactory.Data;
using MixERP.Net.UI.ScrudFactory.Resources;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class Pager : ScrudLayout
    {
        private readonly string _currentPageUrl;
        private int _blockCount;
        private string _cssClass;
        private int _currentPage;
        private int _pageSize;
        private int _totalPages;
        private int _totalRecords;

        internal Pager(Config config)
            : base(config)
        {
            _currentPageUrl = HttpContext.Current.Request.Url.AbsolutePath;
        }

        public override string Get()
        {
            this._pageSize = ConfigBuilder.GetPageSize(this.Config);
            this._totalRecords = FormHelper.GetTotalRecords(this.Config.ViewSchema, this.Config.View);

            this._currentPage = Conversion.TryCastInteger(HttpContext.Current.Request.QueryString["page"]);
            if (this._currentPage.Equals(0))
            {
                this._currentPage = 1;
            }

            this._cssClass = ConfigBuilder.GetPagerCssClass(this.Config);

            if (this._totalRecords == 0 || this._pageSize > this._totalRecords)
            {
                return string.Empty;
            }

            this._blockCount = 10; //This can be parameterized.

            return this.GetPager();
        }

        private string GetPager()
        {
            if (this._pageSize.Equals(0))
            {
                this._pageSize = this._blockCount;
            }

            this._totalPages = (int) Decimal.Ceiling(Decimal.Divide(this._totalRecords, this._pageSize));

            int start = 1 +
                        (int) Decimal.Ceiling(Decimal.Divide(this._currentPage, this._blockCount) - 1)*this._blockCount;
            int end = start + this._blockCount - 1;

            if (end > this._totalPages)
            {
                end = this._totalPages;
            }

            StringBuilder pager = new StringBuilder();
            TagBuilder.Begin(pager, "div");
            TagBuilder.AddClass(pager, this._cssClass);
            TagBuilder.Close(pager);

            pager.Append(this.FirstItem());
            pager.Append(this.PreviousItem());
            pager.Append(this.GetAnchor(1));

            //The previous page block
            string title;
            if (start - this._blockCount > 0)
            {
                title = string.Format(Thread.CurrentThread.CurrentCulture, Titles.PageN, start - 1);

                pager.Append(this.GetIconAnchor("icon item", "arrow left icon", start - 1, title));
            }

            //Paged items
            for (int i = start; i <= end; i++)
            {
                //Do not create the first and last page
                //because we will create them explicitly
                //which will be followed/led 
                //by next/previous page blocks
                if (i.Equals(1) || i.Equals(this._totalPages))
                {
                    continue;
                }

                pager.Append(this.GetAnchor(i));
            }


            //The next page block
            if (start + this._blockCount < this._totalPages)
            {
                title = string.Format(Thread.CurrentThread.CurrentCulture, Titles.PageN, end + 1);
                pager.Append(this.GetIconAnchor("icon item", "arrow right icon", end + 1, title));
            }

            pager.Append(this.GetAnchor(this._totalPages));
            pager.Append(this.NextItem());
            pager.Append(this.LastItem());

            TagBuilder.EndTag(pager, "div");
            return pager.ToString();
        }

        private string FirstItem()
        {
            int page = 0;

            if (this._currentPage > 1)
            {
                page = 1;
            }

            return this.GetIconAnchor("icon item", "chevron circle left icon", page, Titles.FirstPage);
        }

        private string PreviousItem()
        {
            int page = 0;
            if (this._currentPage > 1)
            {
                page = this._currentPage - 1;
            }

            return this.GetIconAnchor("icon item", "arrow circle left icon", page, Titles.PreviousPage);
        }

        private string NextItem()
        {
            int page = 0;

            if (this._currentPage < this._totalPages)
            {
                page = this._currentPage + 1;
            }

            return this.GetIconAnchor("icon item", "arrow circle right icon", page, Titles.NextPage);
        }

        private string LastItem()
        {
            int page = 0;

            if (this._currentPage < this._totalPages)
            {
                page = this._totalPages;
            }

            return this.GetIconAnchor("icon item", "chevron circle right icon", page, Titles.LastPage);
        }

        private string GetAnchor(int pageNumber)
        {
            StringBuilder anchor = new StringBuilder();
            TagBuilder.Begin(anchor, "a");


            TagBuilder.AddClass(anchor, pageNumber.Equals(this._currentPage) ? "active item" : "item");

            TagBuilder.AddAttribute(anchor, "href",
                string.Format(CultureInfo.InvariantCulture, "{0}?page={1}", this._currentPageUrl, pageNumber));

            TagBuilder.AddTitle(anchor, string.Format(Thread.CurrentThread.CurrentCulture, Titles.PageN, pageNumber));

            TagBuilder.Close(anchor);

            anchor.Append(pageNumber.ToString(Thread.CurrentThread.CurrentCulture));

            TagBuilder.EndTag(anchor, "a");

            return anchor.ToString();
        }

        private string GetIconAnchor(string cssClass, string iconCssClass, int pageNumber, string title)
        {
            StringBuilder anchor = new StringBuilder();
            TagBuilder.Begin(anchor, "a");

            TagBuilder.AddClass(anchor, cssClass);

            if (!string.IsNullOrWhiteSpace(title))
            {
                TagBuilder.AddTitle(anchor, title);
            }

            if (pageNumber > 0)
            {
                TagBuilder.AddAttribute(anchor, "href",
                    string.Format(CultureInfo.InvariantCulture, "{0}?page={1}", this._currentPageUrl, pageNumber));
            }

            TagBuilder.Close(anchor);

            if (!string.IsNullOrWhiteSpace(iconCssClass))
            {
                TagBuilder.AddIcon(anchor, iconCssClass);
            }

            TagBuilder.EndTag(anchor, "a");

            return anchor.ToString();
        }
    }
}