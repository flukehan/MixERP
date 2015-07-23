using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.UserManagment
{
    public class User : FirstStep
    {
        public User()
        {
            this.Order = 0;
            this.Name = Titles.AddNewUsers;
            this.Category = Titles.UserManagement;

            this.CategoryAlias = "user-management";
            this.Description = Labels.AddNewUsersDescription;

            this.Icon = "user icon";
            this.NavigateUrl = "/Modules/BackOffice/Users.mix";

            int count = this.CountUsers();

            if (count > 2)
            {
                this.Status = true;
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.TotalUsersN, count);
                return;
            }

            this.Message = Labels.NoAdditionalUserFound;
        }

        private int CountUsers()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            const string sql = "SELECT COUNT(*) FROM office.users WHERE office_id=@0;";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}