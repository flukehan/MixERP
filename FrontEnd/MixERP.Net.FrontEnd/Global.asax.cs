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
using System.Threading;
using System.Web;
using System.Web.Routing;
using MixERP.Net.Common;

namespace MixERP.Net.FrontEnd
{
    public class Global : HttpApplication
    {
        protected static void RegisterRoutes(RouteCollection routes)
        {
            if (routes != null)
            {
                routes.Ignore("{resource}.axd");
                routes.MapPageRoute("DefaultRoute", "", "~/SignIn.aspx");
                routes.MapPageRoute("Reporting", "Reports/{path}", "~/Reports/ReportMaster.aspx");
                routes.MapPageRoute("Modules", "Modules/{*path}", "~/Modules/Default.aspx");
            }
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Server.GetLastError();

            if (ex == null)
            {
                return;
            }


            if (ex is ThreadAbortException)
            {
                return;
            }

            MixERPExceptionManager.HandleException(ex);
        }

        private void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}