using System.Collections.Generic;
using System.Globalization;
using MixERP.Net.Common;
using MixERP.Net.Framework.Contracts.Currency;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    internal sealed class Parser
    {
        internal Parser(dynamic response, string source, bool removeSourceCurrencyFromResult, string resultSubKey, int decimalPlaces)
        {
            this.Response = response;
            this.Source = source;
            this.RemoveSourceCurrencyFromResult = removeSourceCurrencyFromResult;
            this.ResultSubKey = resultSubKey;
            this.DecimalPlaces = decimalPlaces;
        }

        internal dynamic Response { get; private set; }
        internal string Source { get; private set; }
        internal bool RemoveSourceCurrencyFromResult { get; private set; }
        internal string ResultSubKey { get; private set; }
        internal int DecimalPlaces { get; private set; }

        internal IEnumerable<CurrencyConversionResult> Parse()
        {
            List<CurrencyConversionResult> result = new List<CurrencyConversionResult>();

            bool removeSource = this.RemoveSourceCurrencyFromResult;
            dynamic success = this.Response.success;

            if (!Conversion.TryCastBoolean(success))
            {
                dynamic error = this.Response["error"];
                string errorCode = error["code"];
                string errorInfo = error["info"];

                throw new CurrencylayerException(errorCode + " " + errorInfo);
            }


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

                if (removeSource)
                {
                    name = name.Replace(this.Source, string.Empty);
                }

                result.Add(new CurrencyConversionResult(name, value));
            }

            return result;
        }
    }
}