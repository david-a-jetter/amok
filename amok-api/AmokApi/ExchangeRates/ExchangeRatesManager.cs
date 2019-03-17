using AmokApi.ExchangeRates.Contracts;
using System;
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
            var request     = _Access.DefaultRequest;
            var latestRates = await _Access.GetRates(request);

            return latestRates;
        }

        public async Task<ExchangeRatesResponse> GetLatestRates(string baseCurrency)
        {
            throw new NotImplementedException();
        }
    }
}
