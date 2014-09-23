using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Models.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace MixERP.Net.FrontEnd.Base
{
    public class MixERPUserControl : MixERPUserControlBase
    {
        public string GetPageMenu(Page page)
        {
            if (page != null)
            {
                string relativePath = Conversion.GetRelativePath(this.Page.Request.Url.AbsolutePath);
                return MixERPWebpage.GetContentPageMenu(this.Page, relativePath);
            }

            return null;
        }
    }
}