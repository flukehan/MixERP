using MixER.Net.ApplicationState.Cache;
using MixERP.Net.Common.Helpers;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.FrontEnd.Controls;
using MixERP.Net.i18n.Resources;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class Stores : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (Scrud scrud = new Scrud())
            {
                scrud.KeyColumn = "store_id";

                scrud.TableSchema = "office";
                scrud.Table = "stores";
                scrud.ViewSchema = "office";
                scrud.View = "store_scrud_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.UseDisplayViewsAsParents = true;

                scrud.Text = Titles.Stores;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.store_types.store_type_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "StoreTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.offices.office_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "OfficeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "office.cash_repositories.cash_repository_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "CashRepositoryDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.sales_taxes.sales_tax_id",
                DbConfig.GetDbParameter(AppUsers.GetCurrentUserDB(), "SalesTaxDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.store_types.store_type_id", "office.store_type_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.cash_account_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "office.offices.office_id", "office.office_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "office.cash_repositories.cash_repository_id",
                "office.cash_repository_scrud_view");
            ScrudHelper.AddDisplayView(displayViews, "core.sales_taxes.sales_tax_id", "core.sales_tax_scrud_view");

            return string.Join(",", displayViews);
        }
    }
}