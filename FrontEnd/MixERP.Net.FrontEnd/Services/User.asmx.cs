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

using System.Threading;
using System.Web;
using System.Web.Services;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.FrontEnd.Base;
using Resources;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class User : WebService
    {
        [WebMethod(EnableSession = true)]
        public string Authenticate(string username, string password, bool rememberMe, string language, int branchId)
        {
            Thread.Sleep(this.GetDelay());

            string challenge = Conversion.TryCastString(this.Session["Challenge"]);

            if (string.IsNullOrWhiteSpace(challenge))
            {
                return Titles.AccessIsDenied;
            }


            return this.Login(branchId, username, password, language, rememberMe, challenge, this.Context);
        }

        private int GetDelay()
        {
            int attempts = PageUtility.InvalidPasswordAttempts(this.Session);

            if (attempts > 3)
            {
                return attempts * 10000;
            }

            return 1000;
        }

        private string Login(int officeId, string userName, string password, string culture, bool rememberMe, string challenge, HttpContext context)
        {
            try
            {
                long signInId = Data.Office.User.SignIn(officeId, userName, password, culture, rememberMe, challenge, context);

                if (signInId > 0)
                {
                    MixERPWebpage.SetSession(this.Context.Session, signInId);
                    MixERPWebpage.SetAuthenticationTicket(this.Context.Response, signInId, rememberMe);

                    return "OK";
                }

                this.LogInvalidSignIn();
                return Resources.Warnings.UserIdOrPasswordIncorrect;
            }
            catch (MixERPException ex)
            {
                this.LogInvalidSignIn();
                return ex.Message;
            }
        }

        private void LogInvalidSignIn()
        {
            PageUtility.InvalidPasswordAttempts(this.Session, 1);
        }
    };
} ;