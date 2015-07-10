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

using MixERP.Net.FrontEnd.Base;
using Serilog;
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
using System.Configuration;
using System.Text;
using System.Threading;

namespace MixERP.Net.FrontEnd.Site
{
    public partial class RuntimeError : MixERPWebpage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsLandingPage = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string server = this.Request.ServerVariables["SERVER_SOFTWARE"];

            //This is visual studio.
            if (string.IsNullOrWhiteSpace(server))
            {
                //Display detailed error message on development.
                this.DisplayError();
            }
            else
            {
                bool displayError = ConfigurationManager.AppSettings["DisplayErrorDetails"].Equals("true");

                if (displayError)
                {
                    this.DisplayError();
                }
            }
        }

        private void DisplayError()
        {
            Exception ex = (Exception)this.Page.Session["ex"];
            StringBuilder s = new StringBuilder();

            if (ex != null)
            {
                Log.Verbose("Getting the innermost exception.");

                Log.Verbose("Exception displayed to user.");

                s.Append(string.Format(Thread.CurrentThread.CurrentCulture, "<h2>{0}</h2>", ex.Message));

                Log.Warning("{Exception}.", ex);

                this.ExceptionLiteral.Text = s.ToString();
            }
        }
    }
}