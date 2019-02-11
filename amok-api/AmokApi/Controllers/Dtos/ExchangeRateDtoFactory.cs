using AmokApi.ExchangeRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.Controllers.Dtos
{
    public class ExchangeRatesDtoFactory : IExchangeRatesDtoFactory
    {
        public ExchangeRatesResponseDto BuildDto(ExchangeRatesResponse businessModel)
        {
            var dto = new ExchangeRatesResponseDto();

            return dto;
        }
    }
}
