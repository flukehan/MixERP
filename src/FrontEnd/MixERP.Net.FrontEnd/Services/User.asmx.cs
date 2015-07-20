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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common;
using MixERP.Net.Framework;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.i18n.Resources;
using Serilog;
using System.ComponentModel;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class User : WebService
    {
        [WebMethod(EnableSession = true)]
        public string Authenticate(string catalog, string username, string password, bool rememberMe, string language,
            int branchId)
        {
            Thread.Sleep(this.GetDelay());

            string challenge = Conversion.TryCastString(this.Session["Challenge"]);

            if (string.IsNullOrWhiteSpace(challenge))
            {
                return Titles.AccessIsDenied;
            }

            return this.Login(catalog, branchId, username, password, language, rememberMe, challenge, this.Context);
        }

        private int GetDelay()
        {
            int attempts = PageUtility.InvalidPasswordAttempts(this.Session);

            if (attempts > 3)
            {
                return attempts*10000;
            }

            return 1000;
        }

        private string Login(string catalog, int officeId, string userName, string password, string culture,
            bool rememberMe, string challenge, HttpContext context)
        {
            try
            {
                long globalLoginId = Data.Office.User.SignIn(catalog, officeId, userName, password, culture, rememberMe,
                    challenge, context);

                Log.Information("{UserName} signed in to office : #{OfficeId} from {IP}.", userName, officeId,
                    context.Request.ServerVariables["REMOTE_ADDR"]);

                if (globalLoginId > 0)
                {
                    AppUsers.SetCurrentLogin(globalLoginId);
                    MixERPWebpage.SetAuthenticationTicket(this.Context.Response, globalLoginId, rememberMe);


                    return "OK";
                }

                this.LogInvalidSignIn();
                return Warnings.UserIdOrPasswordIncorrect;
            }
            catch (MixERPException ex)
            {
                Log.Warning("{UserName} could not sign in to office : #{OfficeId} from {IP}.", userName, officeId,
                    context.Request.ServerVariables["REMOTE_ADDR"]);

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