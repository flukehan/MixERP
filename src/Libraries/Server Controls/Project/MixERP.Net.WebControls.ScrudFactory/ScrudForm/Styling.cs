/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private string GetAddButtonIconCssClass()
        {
            string cssClass = this.AddButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "AddButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetAllButtonIconCssClass()
        {
            string cssClass = this.AllButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "AllButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetButtonCssClass()
        {
            string cssClass = this.ButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "ButtonCssClass");
            }

            return cssClass;
        }

        private string GetCommandPanelButtonCssClass()
        {
            string cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "CommandPanelButtonCssClass");
            }

            return cssClass;
        }

        private string GetCommandPanelCssClass()
        {
            string cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "CommandPanelCssClass");
            }

            return cssClass;
        }

        private string GetCompactButtonIconCssClass()
        {
            string cssClass = this.CompactButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "CompactButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetDeleteButtonIconCssClass()
        {
            string cssClass = this.DeleteButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "DeleteButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetDescriptionCssClass()
        {
            string cssClass = this.DescriptionCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "DescriptionCssClass");
            }

            return cssClass;
        }

        private string GetEditButtonIconCssClass()
        {
            string cssClass = this.EditButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "EditButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetErrorCssClass()
        {
            string cssClass = this.ErrorCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "ErrorCssClass");
            }

            return cssClass;
        }

        private string GetFailureCssClass()
        {
            string cssClass = this.FailureCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "FailiureCssClass");
            }

            return cssClass;
        }

        private string GetFormCssClass()
        {
            string cssClass = this.FormCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "FormCssClass");
            }

            return cssClass;
        }

        private string GetFormPanelCssClass()
        {
            string cssClass = this.FormPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "FormPanelCssClass");
            }

            return cssClass;
        }

        private string GetGridPanelCssClass()
        {
            string cssClass = this.GridPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "GridPanelCssClass");
            }

            return cssClass;
        }

        private string GetGridViewAlternateRowCssClass()
        {
            string cssClass = this.GridViewAlternateRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "GridViewAlternateRowCssClass");
            }

            return cssClass;
        }

        private string GetGridViewCssClass()
        {
            string cssClass = this.GridViewCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "GridViewCssClass");
            }

            return cssClass;
        }

        private string GetGridViewRowCssClass()
        {
            string cssClass = this.GridViewRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "GridViewRowCssClass");
            }

            return cssClass;
        }

        private string GetPagerCssClass()
        {
            string cssClass = this.PagerCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "PagerCssClass");
            }

            return cssClass;
        }

        private int GetRowLimit()
        {
            int maxRowLimit = this.MaxRowLimit;

            if (maxRowLimit == 0)
            {
                maxRowLimit = Conversion.TryCastInteger(DbConfig.GetScrudParameter(this.Catalog, "MaxRowLimit"));
            }

            if (maxRowLimit == 0)
            {
                maxRowLimit = 1000;
            }

            return maxRowLimit;
        }


        private int GetPageSize()
        {
            int pageSize = this.PageSize;


            if (pageSize == 0)
            {
                pageSize = Conversion.TryCastInteger(DbConfig.GetScrudParameter(this.Catalog, "PageSize"));
            }

            bool showAll = (Conversion.TryCastString(this.Page.Request.QueryString["show"]).Equals("all"));
            
            if (showAll)
            {
                pageSize = this.GetRowLimit();
            }

            if (pageSize == 0)
            {
                pageSize = 10;//Fallback
            }



            return pageSize;
        }

        private string GetPagerCurrentPageCssClass()
        {
            string cssClass = this.PagerCurrentPageCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "PagerCurrentPageCssClass");
            }

            return cssClass;
        }

        private string GetPagerPageButtonCssClass()
        {
            string cssClass = this.PagerPageButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "PagerPageButtonCssClass");
            }

            return cssClass;
        }

        private string GetPrintButtonIconCssClass()
        {
            string cssClass = this.PrintButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "PrintButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetSaveButtonCssClass()
        {
            string cssClass = this.SaveButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "SaveButtonCssClass");
            }

            return cssClass;
        }

        private string GetSelectButtonIconCssClass()
        {
            string cssClass = this.SelectButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "SelectButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetSuccessCssClass()
        {
            string cssClass = this.SuccessCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "SuccessCssClass");
            }

            return cssClass;
        }

        private string GetTitleLabelCssClass()
        {
            string cssClass = this.TitleLabelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = DbConfig.GetScrudParameter(this.Catalog, "TitleLabelCssClass");
            }

            return cssClass;
        }
    }
}