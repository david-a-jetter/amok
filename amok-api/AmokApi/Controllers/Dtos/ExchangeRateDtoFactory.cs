using AmokApi.ExchangeRates;
using System;

namespace AmokApi.Controllers.Dtos
{
    public class ExchangeRatesDtoFactory : IExchangeRatesDtoFactory
    {
        public ExchangeRatesResponseDto BuildDto(ExchangeRatesResponse exchangeRates)
        {
            if (exchangeRates is null) throw new ArgumentNullException(nameof(exchangeRates));

            var dto = new ExchangeRatesResponseDto(
                exchangeRates.BaseCurrency,
                exchangeRates.Date,
                exchangeRates.Rates);

            return dto;
        }
    }
}
