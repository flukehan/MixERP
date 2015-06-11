using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.UI.ScrudFactory
{
    public static class ConfigBuilder
    {
        public static string GetAddButtonIconCssClass(Config config)
        {
            string cssClass = config.AddButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("AddButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetAllButtonIconCssClass(Config config)
        {
            string cssClass = config.AllButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("AllButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetButtonCssClass(Config config)
        {
            string cssClass = config.ButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ButtonCssClass");
            }

            return cssClass;
        }

        public static string GetCommandPanelButtonCssClass(Config config)
        {
            string cssClass = config.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelButtonCssClass");
            }

            return cssClass;
        }

        public static string GetCommandPanelCssClass(Config config)
        {
            string cssClass = config.CommandPanelButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CommandPanelCssClass");
            }

            return cssClass;
        }

        public static string GetCompactButtonIconCssClass(Config config)
        {
            string cssClass = config.CompactButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("CompactButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetDeleteButtonIconCssClass(Config config)
        {
            string cssClass = config.DeleteButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DeleteButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetDescriptionCssClass(Config config)
        {
            string cssClass = config.DescriptionCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("DescriptionCssClass");
            }

            return cssClass;
        }

        public static string GetFormDescriptionCssClass(Config config)
        {
            string cssClass = config.DescriptionCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormDescriptionCssClass");
            }

            return cssClass;
        }

        public static string GetEditButtonIconCssClass(Config config)
        {
            string cssClass = config.EditButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("EditButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetErrorCssClass(Config config)
        {
            string cssClass = config.ErrorCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("ErrorCssClass");
            }

            return cssClass;
        }

        public static string GetFailureCssClass(Config config)
        {
            string cssClass = config.FailureCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FailiureCssClass");
            }

            return cssClass;
        }

        public static string GetFormCssClass(Config config)
        {
            string cssClass = config.FormCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormCssClass");
            }

            return cssClass;
        }

        public static string GetFormPanelCssClass(Config config)
        {
            string cssClass = config.FormPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("FormPanelCssClass");
            }

            return cssClass;
        }

        public static string GetGridPanelCssClass(Config config)
        {
            string cssClass = config.GridPanelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridPanelCssClass");
            }

            return cssClass;
        }

        public static string GetGridPanelStyle(Config config)
        {
            if (string.IsNullOrWhiteSpace(config.GridPanelStyle))
            {
                var style = Conversion.TryCastString(ConfigurationHelper.GetScrudParameter("GridPanelStyle"));

                return style;
            }

            return config.GridPanelStyle;
        }

        public static string GetGridViewAlternateRowCssClass(Config config)
        {
            string cssClass = config.GridViewAlternateRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewAlternateRowCssClass");
            }

            return cssClass;
        }

        public static string GetGridViewCssClass(Config config)
        {
            string cssClass = config.GridViewCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewCssClass");
            }

            return cssClass;
        }

        public static string GetGridViewWidth(Config config)
        {
            if (string.IsNullOrWhiteSpace(config.GridViewWidth))
            {
                var width = ConfigurationHelper.GetScrudParameter("GridViewDefaultWidth");

                return width;
            }

            return config.GridViewWidth;
        }

        public static string GetGridViewRowCssClass(Config config)
        {
            string cssClass = config.GridViewRowCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("GridViewRowCssClass");
            }

            return cssClass;
        }

        public static string GetPagerCssClass(Config config)
        {
            string cssClass = config.PagerCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerCssClass");
            }

            return cssClass;
        }

        public static int GetRowLimit(Config config)
        {
            int maxRowLimit = config.MaxRowLimit;

            if (maxRowLimit == 0)
            {
                maxRowLimit = Conversion.TryCastInteger(ConfigurationHelper.GetScrudParameter("MaxRowLimit"));
            }

            if (maxRowLimit == 0)
            {
                maxRowLimit = 1000;
            }

            return maxRowLimit;
        }

        public static int GetPageSize(Config config)
        {
            int pageSize = config.PageSize;


            if (pageSize == 0)
            {
                pageSize = Conversion.TryCastInteger(ConfigurationHelper.GetScrudParameter("PageSize"));
            }


            bool showAll =
                (Conversion.TryCastString(System.Web.HttpContext.Current.Request.QueryString["show"]).Equals("all"));

            if (showAll)
            {
                pageSize = GetRowLimit(config);
            }

            if (pageSize == 0)
            {
                pageSize = 10; //Fallback
            }


            return pageSize;
        }

        public static string GetPagerCurrentPageCssClass(Config config)
        {
            string cssClass = config.PagerCurrentPageCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerCurrentPageCssClass");
            }

            return cssClass;
        }

        public static string GetPagerPageButtonCssClass(Config config)
        {
            string cssClass = config.PagerPageButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PagerPageButtonCssClass");
            }

            return cssClass;
        }

        public static string GetPrintButtonIconCssClass(Config config)
        {
            string cssClass = config.PrintButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("PrintButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetSaveButtonCssClass(Config config)
        {
            string cssClass = config.SaveButtonCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SaveButtonCssClass");
            }

            return cssClass;
        }

        public static string GetSelectButtonIconCssClass(Config config)
        {
            string cssClass = config.SelectButtonIconCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SelectButtonIconCssClass");
            }

            return cssClass;
        }

        public static string GetSuccessCssClass(Config config)
        {
            string cssClass = config.SuccessCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("SuccessCssClass");
            }

            return cssClass;
        }

        public static string GetTitleLabelCssClass(Config config)
        {
            string cssClass = config.TitleLabelCssClass;

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = ConfigurationHelper.GetScrudParameter("TitleLabelCssClass");
            }

            return cssClass;
        }

        public static string GetResourceClassName(Config config)
        {
            string resourceClassName = config.ResourceClassName;

            if (string.IsNullOrWhiteSpace(resourceClassName))
            {
                resourceClassName = ConfigurationHelper.GetScrudParameter("ResourceClassName");
            }

            return resourceClassName;
        }

        public static string GetItemSelectorPath(Config config)
        {
            string itemSelectorPath = config.ItemSelectorPath;

            if (string.IsNullOrWhiteSpace(itemSelectorPath))
            {
                itemSelectorPath = ConfigurationHelper.GetScrudParameter("ItemSelectorPath");
            }

            return itemSelectorPath;
        }


        public static string GetItemSelectorAnchorCssClass(Config config)
        {
            string resourceClassName = config.ItemSelectorAnchorCssClass;

            if (string.IsNullOrWhiteSpace(resourceClassName))
            {
                resourceClassName = ConfigurationHelper.GetScrudParameter("ItemSelectorAnchorCssClass");
            }

            return resourceClassName;
        }

    }
}