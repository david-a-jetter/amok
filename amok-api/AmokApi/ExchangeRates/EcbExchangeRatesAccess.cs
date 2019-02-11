using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public class EcbExchangeRatesAccess : IExchangeRatesAccess
    {
        private const string _LatestRatesRoute = "latest";

        private HttpClient _HttpClient { get; }
        private Uri        _EcbBaseUri { get; }

        public EcbExchangeRatesAccess(HttpClient httpClient, Uri ecbBaseUri)
        {
            _HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _EcbBaseUri = ecbBaseUri;
        }

        public async Task<ExchangeRatesResponse> GetLatestRates()
        {
            var uriBuilder = new UriBuilder(_EcbBaseUri)
            {
                Path = _LatestRatesRoute
            };

            var apiResponse = await _HttpClient.GetAsync(uriBuilder.Uri).ConfigureAwait(false);
            var apiContent  = await apiResponse.Content.ReadAsStringAsync();

            var exchangeRates = BuildResponseFromApi(apiContent);

            return exchangeRates;
        }

        //TODO: Move this to factory implementation
        private ExchangeRatesResponse BuildResponseFromApi(string apiContent)
        {
            var response = JsonConvert.DeserializeObject<ExchangeRatesResponse>(apiContent);

            return response;
        }
    }
}
