using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Web.UI.Data.Office;
using MixERP.Net.Web.UI.Models.Users;
using MixERP.Net.Web.UI.ViewModels;
using Resources;

namespace MixERP.Net.Web.UI.Controllers
{
    [RoutePrefix("sign-in")]
    [Route("{action=index}")]
    [AllowAnonymous]
    public class SignInController : Controller
    {
        public ActionResult Index()
        {
            const string view = "~/Views/Home/SignIn.cshtml";

            if (PageUtility.MaxInvalidAttemptsReached(this.Session))
            {
                return View("~/Views/Home/AccessIsDeinied.cshtml");
            }

            SignIn viewModel = new SignIn
            {
                Cultures = GetCultures(),
                Branches = GetBranches()
            };

            Session["Challenge"] = viewModel.Challenge;
            return View(view, viewModel);
        }

        private static IEnumerable<SignIn.Branch> GetBranches()
        {
            return Offices.GetOffices().Select(office => new SignIn.Branch
            {
                OfficeId = Conversion.TryCastInteger(office.OfficeId),
                OfficeName = office.OfficeCode + " (" + office.OfficeName + " )"
            });
        }

        private static IEnumerable<SignIn.Culture> GetCultures()
        {
            string[] config = ConfigurationHelper.GetMixERPParameter("cultures").Split(',');

            Collection<SignIn.Culture> cultures = new Collection<SignIn.Culture>();


            foreach (
                CultureInfo infos in
                    config.Select(culture => culture.Trim())
                        .SelectMany(cultureName => CultureInfo.GetCultures(CultureTypes.AllCultures)
                            .Where(x => x.TwoLetterISOLanguageName.Equals(cultureName))))
            {
                cultures.Add(new SignIn.Culture
                {
                    CultureCode = infos.Name,
                    CultureName = infos.EnglishName,
                    NativeName = infos.NativeName
                });
            }

            return cultures;
        }

        public JsonResult Authenticate(string username, string password, bool rememberMe, string language, int branchId)
        {
            string challenge = Conversion.TryCastString(this.Session["Challenge"]);

            if (string.IsNullOrWhiteSpace(challenge))
            {
                return Json(Titles.AccessIsDenied);
            }

            Authentication authentication = new Authentication(this.Session);

            string result = authentication.Login(branchId, username, password, language, rememberMe, challenge,
                System.Web.HttpContext.Current);

            return Json(result);
        }
    }
}