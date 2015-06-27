using MixERP.Net.Common.Base;

namespace MixERP.Net.CurrencyConversion.Currencylayer
{
    public class CurrencylayerException : MixERPException
    {
        public CurrencylayerException(string message)
            : base(message)
        {
        }
    }
}