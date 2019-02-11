using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public interface IExchangeRatesAccess
    {
        Task<ExchangeRatesResponse> GetLatestRates();
    }
}
