using MixERP.Net.i18n;

namespace MixERP.Net.ReportManager
{
    internal static class ResourceHelper
    {
        internal static string TryParse(string expression)
        {
            string res = RemoveBraces(expression);
            string[] resource = res.Split('.');

            string key = resource[2];

            expression = expression.Replace(expression, ResourceManager.TryGetResourceFromCache(resource[1], key));

            if (!string.IsNullOrWhiteSpace(expression))
            {
                return expression;
            }

            return key;
        }

        public static string RemoveBraces(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return string.Empty;
            }

            return expression.Replace("{", "").Replace("}", "");
        }
    }
}