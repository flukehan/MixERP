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
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Web;
using System.Web.Routing;
using MixERP.Net.Common;

namespace MixERP.Net.FrontEnd
{
    public class Global : HttpApplication
    {

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected static void RegisterRoutes(RouteCollection routes)
        {
            if(routes != null)
            {
                routes.MapPageRoute("DefaultRoute", "", "~/SignIn.aspx");
                routes.MapPageRoute("Reporting", "Reports/{path}", "~/Reports/ReportMaster.aspx");
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = this.Server.GetLastError();
            
            if(ex == null)
            {
                return;
            }

            if(ex is ThreadAbortException)
            {
                return;
            }


            MixERPExceptionManager.HandleException(ex);

        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
