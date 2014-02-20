/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web;

namespace MixERP.Net.Common
{
    public static class MixERPExceptionManager
    {
        public static void HandleException(Exception ex)
        {
            if(ex == null)
            {
                return;
            }

            var exception = ex;

            if(ex.InnerException != null)
            {
                exception = ex.InnerException;
            }

            if(HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["ex"] = exception;
                HttpContext.Current.Response.Redirect("~/RuntimeError.aspx", true);
            }
        }
    }
}
