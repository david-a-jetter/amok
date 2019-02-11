using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public interface IExchangeRatesEngine
    {
        Task<ExchangeRatesResponse> GetCachedRates(ExchangeRatesRequest request);
        Task SaveRatesToCache(ExchangeRatesRequest request, ExchangeRatesResponse rates);
    }
}
