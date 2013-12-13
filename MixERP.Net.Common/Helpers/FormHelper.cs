using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.Common.Helpers
{
    public static class FormHelper
    {
        public static void MakeDirty(WebControl control)
        {
            if (control != null)
            {
                control.CssClass = "dirty";
                control.Focus();
            }
        }

        public static void RemoveDirty(WebControl control)
        {
            if (control != null)
            {
                control.CssClass = "";
            }
        }
    }
}
