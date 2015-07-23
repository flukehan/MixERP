using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.UserManagment
{
    public class MenuAccessPolicy : FirstStep
    {
        public MenuAccessPolicy()
        {
            this.Order = 1;
            this.Name = Titles.MenuAccessPolicy;
            this.Category = Titles.UserManagement;
            this.CategoryAlias = "user-management";

            this.Description = Labels.MenuAccessPolicyDescription;
            this.Icon = "privacy icon";

            this.NavigateUrl = "/Modules/BackOffice/Policy/MenuAccess.mix";

            int count = this.CountPolicy();

            if (count > 0)
            {
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.NotDefinedForNUsers, count);
                return;
            }

            this.Status = true;
            this.Message = Titles.OK;
        }

        private int CountPolicy()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            //COUNT(*) -1 --> The system user does not need menu policy.
            const string sql = "SELECT COUNT(*) -1 FROM office.users WHERE office_id=@0 AND user_id NOT IN (SELECT user_id FROM policy.menu_access WHERE office_id = @0);";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}