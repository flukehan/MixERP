using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.FirstTasks
{
    public class CashRepository : FirstStep
    {
        public CashRepository()
        {
            this.Order = 100;
            this.Name = Titles.CreateCashRepositories;
            this.Category = Titles.FirstTasks;
            this.CategoryAlias = "first-tasks";

            this.Description = Labels.CreateCashRepositoriesDescription;
            this.Icon = "payment icon";
            this.NavigateUrl = "/Modules/BackOffice/CashRepositories.mix";

            int count = this.CountRepositories();

            if (count > 0)
            {
                this.Status = true;
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.NCashRepositoriesInThisOffice, count);
                return;
            }

            this.Message = Labels.NoCashRepositoryDefnied;
        }

        private int CountRepositories()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            const string sql = "SELECT COUNT(*) FROM office.cash_repositories WHERE office_id = @0;";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}