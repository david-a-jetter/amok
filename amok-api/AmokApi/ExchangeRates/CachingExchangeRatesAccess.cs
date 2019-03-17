using AmokApi.ExchangeRates.Contracts;
using System;
using System.Globalization;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public class CachingExchangeRatesAccess : IExchangeRatesAccess
    {
        private ObjectCache          _Cache  { get; }
        private IExchangeRatesAccess _Access { get; }

        public ExchangeRatesRequest DefaultRequest => _Access.DefaultRequest;

        public CachingExchangeRatesAccess(ObjectCache cache, IExchangeRatesAccess access)
        {
            _Cache  = cache  ?? throw new ArgumentNullException(nameof(cache));
            _Access = access ?? throw new ArgumentNullException(nameof(access));
        }

        public async Task<ExchangeRatesResponse> GetRates(ExchangeRatesRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var cacheKey = BuildCacheKey(request);
            var response = _Cache.Get(cacheKey) as ExchangeRatesResponse;

            if (response is null)
            {
                response = await _Access.GetRates(request);

                _Cache.Add(cacheKey, response, new CacheItemPolicy());
            }

            return response;
        }

        private string BuildCacheKey(ExchangeRatesRequest request)
        {
            var cacheKey = $"{request.BaseCurrency}|{request.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}";

            return cacheKey;
        }
    }
}
