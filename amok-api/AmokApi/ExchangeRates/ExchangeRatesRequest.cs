using System;

namespace AmokApi.ExchangeRates
{
    public class ExchangeRatesRequest
    {
        public string   BaseCurrency { get; }
        public DateTime Date         { get; }

        public ExchangeRatesRequest(string baseCurrency, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(baseCurrency)) throw new ArgumentNullException(nameof(baseCurrency));

            BaseCurrency = baseCurrency;
            Date         = date;
        }
    }
}
