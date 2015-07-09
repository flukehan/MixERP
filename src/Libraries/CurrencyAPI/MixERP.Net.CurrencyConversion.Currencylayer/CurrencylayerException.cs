
using MixERP.Net.Framework;

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