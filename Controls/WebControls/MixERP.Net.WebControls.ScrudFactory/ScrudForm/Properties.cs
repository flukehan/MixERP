/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        /// <summary>
        /// This property when set to true will restrict a user from editing the selected row. 
        /// This property could be enabled at runtime depending upon 
        /// the currently signed in user's role or authorization policy defined by the administrator.
        /// </summary>
        public bool DenyEdit { get; set; }
        
        /// <summary>
        /// This property when set to true will restrict a user from deleting the selected row. 
        /// This property could be enabled at runtime depending upon 
        /// the currently signed in user's role or authorization policy defined by the administrator.
        /// </summary>
        public bool DenyDelete { get; set; }

        /// <summary>
        /// This property when set to true will restrict a user from adding a new row to the view. 
        /// This property could be enabled at runtime depending upon 
        /// the currently signed in user's role or authorization policy defined by the administrator.
        /// </summary>
        public bool DenyAdd { get; set; }
        
        /// <summary>
        /// Description is displayed under the form title. 
        /// Use this property if you want to provide a special hint to the user.
        /// </summary>   
        public string Description { get; set; }

        /// <summary>
        /// If the table has foreign keys, set this to a comma separated list of 
        /// the name of the field or Column Expression to be displayed on the 
        /// respective DropDownList control.
        /// <strong>About Column Expression</strong>
        /// Expressions could be used instead of column name. 
        /// Please refer to the following MSDN articles for more information on using Expression Columns:
        /// http://msdn.microsoft.com/en-us/library/zwxk25bd(v=vs.100).aspx 
        /// http://msdn.microsoft.com/en-us/library/system.data.datacolumn.expression(v=vs.100).aspx
        /// <strong>Syntax</strong>
        /// Comma separated list of [Fully qualified column]-->[display_column or expression]
        /// as in
        /// DisplayFields="office.users.user_id-->user_name, core.accounts.account_id-->account_code + ' (' + account_name + ')'"
        /// </summary>
        public string DisplayFields { get; set; }

        /// <summary>
        /// This property when set a value will enable a popup selection of foreign keys 
        /// by displaying the base tables. Set this to a comma separated list of the name of 
        /// the Database View or Database Table to be displayed on the popup window.
        /// <strong>Syntax</strong>
        /// Comma separated list of [Fully qualified column]-->[fully qualified PostgreSQL view]
        /// as in
        /// DisplayViews="office.users.user_id-->office.user_view, core.accounts.account_id-->core.account_view"
        /// Refer to the source code of BankAccount.aspx:
        /// https://github.com/binodnp/mixerp/blob/master/MixERP.Net.FrontEnd/Finance/Setup/BankAccounts.aspx
        /// </summary>
        public string DisplayViews { get; set; }

        /// <summary>
        /// Exclude a comma separated list of columns for CRUD operation.
        /// </summary>
        public string Exclude { get; set; }

        /// <summary>
        /// The name of the primary key column.
        /// </summary>
        public string KeyColumn { get; set; }

        /// <summary>
        /// Set this to override the default page size of the view, which is 10.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// If the table has foreign keys, set this to a comma separated list of 
        /// the selected values to be displayed on the respective DropDownList control.
        /// </summary>
        public string SelectedValues { get; set; }

        /// <summary>
        /// The name of the table to perform CRUD operation against.
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// The database schema container of the "Table" property of this control.
        /// </summary>
        public string TableSchema { get; set; }


        /// <summary>
        /// The heading or title of this form.
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// The name of the database view or table to select and display the resultset from.
        /// </summary>
        public string View { get; set; }

        /// <summary>
        /// The database schema container of the "View" property of this control.
        /// </summary>
        public string ViewSchema { get; set; }

        /// <summary>
        /// The full inner width of the grid.
        /// </summary>
        public override Unit Width { get; set; }

        public string UpdateProgressTemplateCssClass { get; set; }
        public string UpdateProgressSpinnerImageCssClass { get; set; }
        public string UpdateProgressSpinnerImagePath { get; set; }
        public string CommandPanelButtonCssClass { get; set; }
        public string ButtonCssClass { get; set; }
    }
}
