using System.Globalization;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Framework.Contracts.Checklist;
using MixERP.Net.i18n.Resources;
using PetaPoco;

namespace MixERP.Net.FirstSteps.NewUser.FirstTasks
{
    public class TaxAuthority : FirstStep
    {
        public TaxAuthority()
        {
            this.Order = 201;
            this.Name = Titles.CreateTaxAuthority;
            this.Category = Titles.TaxSetup;
            this.CategoryAlias = "tax-setup";

            this.Description = Labels.CreateTaxAuthorityDescription;
            this.Icon = "tasks icon";
            this.NavigateUrl = "/Modules/BackOffice/Tax/TaxAuthorities.mix";

            int count = this.CountTaxAuthorities();

            if (count > 0)
            {
                this.Status = true;
                this.Message = string.Format(CultureInfo.DefaultThreadCurrentCulture, Labels.NTaxAuthoritiesFound, count);
                return;
            }

            this.Message = Labels.NoTaxAuthorityDefined;
        }

        private int CountTaxAuthorities()
        {
            string catalog = AppUsers.GetCurrentUserDB();
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            const string sql = "SELECT COUNT(*) FROM core.tax_authorities;";
            return Factory.Scalar<int>(catalog, sql, officeId);
        }
    }
}