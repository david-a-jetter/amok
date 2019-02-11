using AmokApi.ExchangeRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.Controllers.Dtos
{
    public interface IExchangeRatesDtoFactory
    {
        ExchangeRatesResponseDto BuildDto(ExchangeRatesResponse businessModel);
    }
}
