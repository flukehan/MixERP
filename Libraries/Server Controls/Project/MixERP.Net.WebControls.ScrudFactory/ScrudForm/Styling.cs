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
                cssClass = ConfigurationHelper.GetScrudParameter("AddButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetAllButtonIconCssClass()
        {
            string cssClass = this.AllButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("AllButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetButtonCssClass()
        {
            string cssClass = this.ButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ButtonCssClass");
            }

            return cssClass;
        }

        private string GetCommandPanelButtonCssClass()
        {
            string cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelButtonCssClass");
            }

            return cssClass;
        }

        private string GetCommandPanelCssClass()
        {
            string cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelCssClass");
            }

            return cssClass;
        }

        private string GetCompactButtonIconCssClass()
        {
            string cssClass = this.CompactButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CompactButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetDeleteButtonIconCssClass()
        {
            string cssClass = this.DeleteButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DeleteButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetDescriptionCssClass()
        {
            string cssClass = this.DescriptionCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DescriptionCssClass");
            }

            return cssClass;
        }

        private string GetEditButtonIconCssClass()
        {
            string cssClass = this.EditButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("EditButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetErrorCssClass()
        {
            string cssClass = this.ErrorCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ErrorCssClass");
            }

            return cssClass;
        }

        private string GetFailureCssClass()
        {
            string cssClass = this.FailureCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FailureCssClass");
            }

            return cssClass;
        }

        private string GetFormCssClass()
        {
            string cssClass = this.FormCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormCssClass");
            }

            return cssClass;
        }

        private string GetFormPanelCssClass()
        {
            string cssClass = this.FormPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormPanelCssClass");
            }

            return cssClass;
        }

        private string GetGridPanelCssClass()
        {
            string cssClass = this.GridPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridPanelCssClass");
            }

            return cssClass;
        }

        private string GetGridViewAlternateRowCssClass()
        {
            string cssClass = this.GridViewAlternateRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewAlternateRowCssClass");
            }

            return cssClass;
        }

        private string GetGridViewCssClass()
        {
            string cssClass = this.GridViewCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewCssClass");
            }

            return cssClass;
        }

        private string GetGridViewRowCssClass()
        {
            string cssClass = this.GridViewRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewRowCssClass");
            }

            return cssClass;
        }

        private string GetPagerCssClass()
        {
            string cssClass = this.PagerCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerCssClass");
            }

            return cssClass;
        }

        private string GetPagerCurrentPageCssClass()
        {
            string cssClass = this.PagerCurrentPageCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerCurrentPageCssClass");
            }

            return cssClass;
        }

        private string GetPagerPageButtonCssClass()
        {
            string cssClass = this.PagerPageButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerPageButtonCssClass");
            }

            return cssClass;
        }

        private string GetPrintButtonIconCssClass()
        {
            string cssClass = this.PrintButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PrintButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetSaveButtonCssClass()
        {
            string cssClass = this.SaveButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SaveButtonCssClass");
            }

            return cssClass;
        }

        private string GetSelectButtonIconCssClass()
        {
            string cssClass = this.SelectButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SelectButtonIconCssClass");
            }

            return cssClass;
        }

        private string GetSuccessCssClass()
        {
            string cssClass = this.SuccessCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SuccessCssClass");
            }

            return cssClass;
        }

        private string GetTitleLabelCssClass()
        {
            string cssClass = this.TitleLabelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("TitleLabelCssClass");
            }

            return cssClass;
        }
    }
}