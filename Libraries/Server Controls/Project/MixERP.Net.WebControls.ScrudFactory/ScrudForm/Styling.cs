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
        private string GetAddButtonCssClass()
        {
            var cssClass = this.AddButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("AddButtonCssClass");
            }

            return cssClass;
        }

        private string GetAllButtonCssClass()
        {
            var cssClass = this.AllButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("AllButtonCssClass");
            }

            return cssClass;
        }

        private string GetButtonCssClass()
        {
            var cssClass = this.ButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ButtonCssClass");
            }

            return cssClass;
        }

        private string GetCommandPanelButtonCssClass()
        {
            var cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelButtonCssClass");
            }

            return cssClass;
        }

        private string GetCommandPanelCssClass()
        {
            var cssClass = this.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelCssClass");
            }

            return cssClass;
        }

        private string GetCompactButtonCssClass()
        {
            var cssClass = this.CompactButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CompactButtonCssClass");
            }

            return cssClass;
        }

        private string GetDateControlCssClass()
        {
            var cssClass = this.DateControlCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DateControlCssClass");
            }

            return cssClass;
        }

        private string GetDeleteButtonCssClass()
        {
            var cssClass = this.DeleteButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DeleteButtonCssClass");
            }

            return cssClass;
        }

        private string GetDescriptionCssClass()
        {
            var cssClass = this.DescriptionCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DescriptionCssClass");
            }

            return cssClass;
        }

        private string GetEditButtonCssClass()
        {
            var cssClass = this.EditButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("EditButtonCssClass");
            }

            return cssClass;
        }

        private string GetErrorCssClass()
        {
            var cssClass = this.ErrorCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ErrorCssClass");
            }

            return cssClass;
        }

        private string GetFailureCssClass()
        {
            var cssClass = this.FailureCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FailureCssClass");
            }

            return cssClass;
        }

        private string GetFormCssClass()
        {
            var cssClass = this.FormCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormCssClass");
            }

            return cssClass;
        }

        private string GetFormPanelButtonCssClass()
        {
            var cssClass = this.FormPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormPanelButtonCssClass");
            }

            return cssClass;
        }

        private string GetGridPanelCssClass()
        {
            var cssClass = this.GridPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridPanelCssClass");
            }

            return cssClass;
        }

        private string GetGridViewAlternateRowCssClass()
        {
            var cssClass = this.GridViewAlternateRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewAlternateRowCssClass");
            }

            return cssClass;
        }

        private string GetGridViewCssClass()
        {
            var cssClass = this.GridViewCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewCssClass");
            }

            return cssClass;
        }

        private string GetGridViewRowCssClass()
        {
            var cssClass = this.GridViewRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewRowCssClass");
            }

            return cssClass;
        }

        private string GetPagerCssClass()
        {
            var cssClass = this.PagerCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerCssClass");
            }

            return cssClass;
        }

        private string GetPagerCurrentPageCssClass()
        {
            var cssClass = this.PagerCurrentPageCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerCurrentPageCssClass");
            }

            return cssClass;
        }

        private string GetPagerPageButtonCssClass()
        {
            var cssClass = this.PagerPageButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerPageButtonCssClass");
            }

            return cssClass;
        }

        private string GetPrintButtonCssClass()
        {
            var cssClass = this.PrintButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PrintButtonCssClass");
            }

            return cssClass;
        }

        private string GetSaveButtonCssClass()
        {
            var cssClass = this.SaveButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SaveButtonCssClass");
            }

            return cssClass;
        }

        private string GetSelectButtonCssClass()
        {
            var cssClass = this.SelectButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SelectButtonCssClass");
            }

            return cssClass;
        }

        private string GetSuccessCssClass()
        {
            var cssClass = this.SuccessCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SuccessCssClass");
            }

            return cssClass;
        }

        private string GetUpdateProgressSpinnerImageCssClass()
        {
            var cssClass = this.UpdateProgressSpinnerImageCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("UpdateProgressSpinnerImageCssClass");
            }

            return cssClass;
        }

        private string GetUpdateProgressTemplateCssClass()
        {
            var cssClass = this.UpdateProgressTemplateCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("UpdateProgressTemplateCssClass");
            }

            return cssClass;
        }
    }
}