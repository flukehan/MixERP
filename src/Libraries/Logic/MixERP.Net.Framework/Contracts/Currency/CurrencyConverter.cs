using System;
using System.Collections.Generic;
using System.Linq;

namespace MixERP.Net.Framework.Contracts.Currency
{
    public class CurrencyConverter
    {
        public string Name { get; set; }
        public string AssemblyQualifiedName { get; set; }
        public bool Enabled { get; set; }

        public static IEnumerable<CurrencyConverter> GetEnabled()
        {
            return GetAll().Where(p => p.Enabled);
        }

        public static IEnumerable<CurrencyConverter> GetAll()
        {
            Type type = typeof (ICurrencyConverter);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => p.Name != type.Name)
                .Select(p => new CurrencyConverter
                {
                    Name = GetPropValue(Activator.CreateInstance(p), "ConverterName").ToString(),
                    AssemblyQualifiedName = p.AssemblyQualifiedName,
                    Enabled = bool.Parse(GetPropValue(Activator.CreateInstance(p), "Enabled"))
                });
        }

        private static string GetPropValue(object src, string propName)
        {
            object value = src.GetType().GetProperty(propName).GetValue(src, null);
            return value.ToString();
        }
    }
}