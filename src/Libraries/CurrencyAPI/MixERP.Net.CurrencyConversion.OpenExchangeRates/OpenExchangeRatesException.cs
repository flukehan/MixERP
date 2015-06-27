using MixERP.Net.Common.Base;

namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    public class OpenExchangeRatesException : MixERPException
    {
        public OpenExchangeRatesException(string message)
            : base(message)
        {
        }
    }
}