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

using System;
using System.Linq;
using System.Collections.Generic;
using MixERP.Net.Common;

namespace MixERP.Net.FrontEnd.Base
{
    public interface ICurrencyConverter
    {
        bool Enabled { get; }
        string ConfigFileName { get; }
        string ConverterName { get; }
        string BaseCurrency { get; set; }
        List<string> CurrencyCodes { get; set; }

        IEnumerable<CurrencyConversionResult> GetResult();
    }

    public class CurrencyConversionResult
    {
        public string CurrencyCode { get; private set; }
        public decimal ExchangeRate { get; private set; }

        public CurrencyConversionResult(string currencyCode, decimal exchangeRate)
        {
            this.CurrencyCode = currencyCode;
            this.ExchangeRate = exchangeRate;
        }
    }

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
            var type = typeof(ICurrencyConverter);

            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p))
                    .Where(p => p.Name != type.Name)
                    .Select(p => new CurrencyConverter
                    {
                        Name = GetPropValue(Activator.CreateInstance(p), "ConverterName").ToString(),
                        AssemblyQualifiedName = p.AssemblyQualifiedName,
                        Enabled = Conversion.TryCastBoolean(GetPropValue(Activator.CreateInstance(p), "Enabled"))
                    });
        }

        private static object GetPropValue(object src, string propName)
        {
            object value = src.GetType().GetProperty(propName).GetValue(src, null);
            return value;
        }

    }
}