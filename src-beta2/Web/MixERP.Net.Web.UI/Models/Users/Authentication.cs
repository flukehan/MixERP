using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using MixERP.Net.Common;
using MixERP.Net.Common.Base;
using MixERP.Net.Web.UI.Providers;
using Resources;
using Serilog;

namespace MixERP.Net.Web.UI.Models.Users
{
    public class Authentication
    {
        public Authentication(HttpSessionStateBase session)
        {
            this.Session = session;
        }

        private HttpSessionStateBase Session { get; set; }

        private int GetDelay()
        {
            int attempts = PageUtility.InvalidPasswordAttempts(this.Session);

            if (attempts > 3)
            {
                return attempts*10000;
            }

            return 1000;
        }

        internal string Login(int officeId, string userName, string password, string culture, bool rememberMe,
            string challenge, HttpContext context)
        {
            Thread.Sleep(this.GetDelay());

            try
            {
                long signInId = Data.Office.User.SignIn(officeId, userName, password, culture, rememberMe, challenge,
                    context);

                Log.Information("{UserName} signed in to office : #{OfficeId} from {IP}.", userName, officeId,
                    context.Request.ServerVariables["REMOTE_ADDR"]);

                if (signInId > 0)
                {
                    CacheProvider.SetSignInView(signInId);
                    SetAuthenticationTicket(HttpContext.Current.Response, signInId, rememberMe);

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

        public static void SetAuthenticationTicket(HttpResponse response, long signInId, bool rememberMe)
        {
            if (response == null)
            {
                return;
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                signInId.ToString(CultureInfo.InvariantCulture), DateTime.Now, DateTime.Now.AddMinutes(30), rememberMe,
                String.Empty, FormsAuthentication.FormsCookiePath);

            string encryptedCookie = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie)
            {
                Domain = FormsAuthentication.CookieDomain,
                Path = ticket.CookiePath
            };

            response.Cookies.Add(cookie);
        }

        private void LogInvalidSignIn()
        {
            PageUtility.InvalidPasswordAttempts(this.Session, 1);
        }
    }
}