using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public class ExchangeRatesResponse
    {
        public string   BaseCurrency { get; }
        public DateTime Date         { get; }
        public IDictionary<string, decimal> Rates { get; }

        public ExchangeRatesResponse(
            string baseCurrency,
            DateTime date,
            IDictionary<string, decimal> rates)
        {
            if (string.IsNullOrWhiteSpace(baseCurrency)) throw new ArgumentNullException(nameof(baseCurrency));

            BaseCurrency = baseCurrency;
            Date         = date;
            Rates        = rates ?? throw new ArgumentNullException(nameof(rates));
        }
    }
}
