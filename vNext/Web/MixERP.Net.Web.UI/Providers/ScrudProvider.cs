using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Office;
using MixERP.Net.UI.ScrudFactory;

namespace MixERP.Net.Web.UI.Providers
{
    public class ScrudProvider
    {
        public static Config GetScrudConfig()
        {
            SignInView view = CacheProvider.GetSignInView();
            return new Config
            {
                OfficeId = view.OfficeId.ToInt(),
                UserId = view.UserId.ToInt(),
                UserName = view.UserName,
                OfficeCode = view.OfficeCode,
            };
        }
    }
}