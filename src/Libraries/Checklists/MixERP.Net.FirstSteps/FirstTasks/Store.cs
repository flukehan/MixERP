using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.FirstTasks
{
    public class Store : FirstStep
    {
        public Store()
        {
            this.Order = 102;
            this.Name = Titles.CreateStores;
            this.Category = Titles.FirstTasks;
            this.CategoryAlias = "first-tasks";

            this.Description = Labels.CreateStoresDescription;
            this.Icon = "shop icon";
            this.NavigateUrl = "/Modules/Inventory/Setup/Stores.mix";

            int count = this.CountStores();

            if (count > 0)
            {
                this.Status = true;
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.NStoresInThisOffice, count);
                return;
            }

            this.Message = Labels.NoStorePresent;
        }

        private int CountStores()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            const string sql = "SELECT COUNT(*) FROM office.stores WHERE office_id = @0;";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}