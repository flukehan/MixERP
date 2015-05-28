using System;
using System.Reflection;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.UI.ScrudFactory.Helpers
{
    internal static class ScrudLocalizationHelper
    {
        internal static string GetResourceString(Assembly assembly, string resourceClassName, string key)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            string fullyQualifiedResourceClassName = assembly.GetName().Name + ".Resources." + resourceClassName;
            return LocalizationHelper.GetResourceString(assembly, fullyQualifiedResourceClassName, key);
        }
    }
}