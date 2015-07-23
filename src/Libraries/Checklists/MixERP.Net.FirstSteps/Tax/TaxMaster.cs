using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.FirstTasks
{
    public class TaxMaster : FirstStep
    {
        public TaxMaster()
        {
            this.Order = 200;
            this.Name = Titles.CreateTaxMaster;
            this.Category = Titles.TaxSetup;
            this.CategoryAlias = "tax-setup";

            this.Description = Labels.CreateTaxMasterDescription;
            this.Icon = "tasks icon";
            this.NavigateUrl = "/Modules/BackOffice/Tax/TaxMaster.mix";

            int count = this.CountTaxMasters();

            if (count > 0)
            {
                this.Status = true;
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.NTaxMasterFound, count);
                return;
            }

            this.Message = Labels.NoTaxMasterDefined;
        }

        private int CountTaxMasters()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            const string sql = "SELECT COUNT(*) FROM core.tax_master;";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}