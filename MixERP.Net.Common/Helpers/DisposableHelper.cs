using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace MixERP.Net.Common.Helpers
{
    public static class DisposableHelper
    {
        public static void DisposeObject(IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
                disposable = null;
            }
        }

    }
}
