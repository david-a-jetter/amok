using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public class ExchangeRatesManager : IExchangeRatesManager
    {
        private IExchangeRatesAccess _Access { get; }
        private IExchangeRatesEngine _Engine { get; }

        public ExchangeRatesManager(
            IExchangeRatesAccess exchangeRatesAccess,
            IExchangeRatesEngine exchangeRatesEngine)
        {
            _Access = exchangeRatesAccess ?? throw new ArgumentNullException(nameof(exchangeRatesAccess));
            _Engine = exchangeRatesEngine ?? throw new ArgumentNullException(nameof(exchangeRatesEngine));
        }

        public async Task<ExchangeRatesResponse> GetLatestRates()
        {
            var latestRates = await _Access.GetLatestRates().ConfigureAwait(false);

            return latestRates;
        }

        public async Task<ExchangeRatesResponse> GetLatestRates(string baseCurrency)
        {
            throw new NotImplementedException();
        }
    }
}
