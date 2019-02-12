using System;
using System.Collections.Generic;

namespace AmokApi.ExchangeRates
{
    public class ExchangeRatesResponse : IEquatable<ExchangeRatesResponse>
    {
        public string   BaseCurrency { get; }
        public DateTime Date         { get; }
        public IDictionary<string, decimal> Rates { get; }

        public static readonly ExchangeRatesResponse Empty =
            new ExchangeRatesResponse(
                "NONE",
                DateTime.UtcNow,
                new Dictionary<string, decimal>());

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

        public bool Equals(ExchangeRatesResponse other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var equals = this.BaseCurrency.Equals(other.BaseCurrency)
                && this.Date.Equals(other.Date);

            return equals;
        }
    }
}
