namespace MixERP.Net.Framework.Contracts.Currency
{
    public class CurrencyConversionResult
    {
        public CurrencyConversionResult(string currencyCode, decimal exchangeRate)
        {
            this.CurrencyCode = currencyCode;
            this.ExchangeRate = exchangeRate;
        }

        public string CurrencyCode { get; private set; }
        public decimal ExchangeRate { get; private set; }
    }
}