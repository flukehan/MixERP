/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Configuration;
using System.Text;
using System.Threading;
using MixERP.Net.BusinessLayer;

namespace MixERP.Net.FrontEnd
{
    public partial class RuntimeError : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string server = this.Request.ServerVariables["SERVER_SOFTWARE"];

            //This is visual studio.
            if(string.IsNullOrWhiteSpace(server))
            {
                //Display detailed error message on development.
                this.DisplayError();
            }
            else
            {
                bool displayError = ConfigurationManager.AppSettings["DisplayErrorDetails"].Equals("true");
                
                if(displayError)
                {
                    this.DisplayError();
                }
            }

        }

        private void DisplayError()
        {
            Exception ex = (Exception)this.Page.Session["ex"];
            StringBuilder s = new StringBuilder();

            if(ex != null)
            {
                s.Append(string.Format(Thread.CurrentThread.CurrentCulture, "<hr class='hr' />"));
                s.Append(string.Format(Thread.CurrentThread.CurrentCulture, "<h2>{0}</h2>", ex.Message));

                this.ExceptionLiteral.Text = s.ToString();
            }
        }

    }
}