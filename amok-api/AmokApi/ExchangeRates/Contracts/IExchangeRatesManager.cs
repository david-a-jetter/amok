using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates.Contracts
{
    public interface IExchangeRatesManager
    {
        Task<ExchangeRatesResponse> GetLatestRates();

        Task<ExchangeRatesResponse> GetLatestRates(string baseCurrency);
    }
}
