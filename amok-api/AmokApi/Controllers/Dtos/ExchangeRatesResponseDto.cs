using System;
using System.Collections.Generic;

namespace AmokApi.Controllers.Dtos
{
    public class ExchangeRatesResponseDto
    {
        public string BaseCurrency { get; }
        public DateTime Date       { get; }
        public IDictionary<string, decimal> Rates { get; }

        public ExchangeRatesResponseDto(
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
