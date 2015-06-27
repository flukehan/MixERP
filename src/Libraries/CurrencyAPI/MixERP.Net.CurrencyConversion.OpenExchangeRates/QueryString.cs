namespace MixERP.Net.CurrencyConversion.OpenExchangeRates
{
    internal sealed class QueryString
    {
        internal QueryString(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        internal string Name { get; private set; }
        internal string Value { get; private set; }
    }
}