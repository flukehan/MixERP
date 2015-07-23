using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.FirstTasks
{
    public class Party : FirstStep
    {
        public Party()
        {
            this.Order = 101;
            this.Name = Titles.CreateParties;
            this.Category = Titles.FirstTasks;
            this.CategoryAlias = "first-tasks";

            this.Description = Labels.CreatePartiesDescription;
            this.Icon = "users icon";
            this.NavigateUrl = "/Modules/Inventory/Setup/Parties.mix";

            int count = this.CountParties();

            if (count > 0)
            {
                this.Status = true;
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.NPartiesFound, count);
                return;
            }

            this.Message = Labels.NoPartyFound;
        }

        private int CountParties()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            const string sql = "SELECT COUNT(*) FROM core.parties;";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}