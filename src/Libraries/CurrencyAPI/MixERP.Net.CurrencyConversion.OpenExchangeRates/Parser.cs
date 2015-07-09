using System.Collections.Generic;
using System.Globalization;
using MixERP.Net.Common;
using MixERP.Net.Framework.Contracts.Currency;

namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    internal sealed class Parser
    {
        internal Parser(dynamic response, string resultSubKey, int decimalPlaces)
        {
            this.Response = response;
            this.ResultSubKey = resultSubKey;
            this.DecimalPlaces = decimalPlaces;
        }

        internal dynamic Response { get; private set; }
        internal int DecimalPlaces { get; private set; }
        internal string ResultSubKey { get; private set; }

        internal IEnumerable<CurrencyConversionResult> Parse()
        {
            List<CurrencyConversionResult> result = new List<CurrencyConversionResult>();

            dynamic response = this.Response[this.ResultSubKey];

            if (response == null)
            {
                return result;
            }

            foreach (dynamic item in response)
            {
                string name = item.Name;
                decimal value = decimal.Round(Conversion.TryCastDecimal(item.Value, CultureInfo.InvariantCulture),
                    this.DecimalPlaces);
                result.Add(new CurrencyConversionResult(name, value));
            }

            return result;
        }
    }
}