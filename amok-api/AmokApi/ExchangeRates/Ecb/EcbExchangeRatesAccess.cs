using AmokApi.ExchangeRates.Contracts;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates.Ecb
{
    public class EcbExchangeRatesAccess : IExchangeRatesAccess
    {
        private static readonly DateTime _MinDate = new DateTime(1999, 01, 04);

        private IApiResponseHandler _ResponseHandler { get; }
        private HttpClient          _HttpClient      { get; }
        private Uri                 _EcbBaseUri      { get; }

        public ExchangeRatesRequest DefaultRequest => new ExchangeRatesRequest("EUR", DateTime.UtcNow);

        public EcbExchangeRatesAccess(
            IApiResponseHandler responseHandler,
            HttpClient httpClient,
            Uri ecbBaseUri)
        {
            _ResponseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
            _HttpClient      = httpClient      ?? throw new ArgumentNullException(nameof(httpClient));
            _EcbBaseUri      = ecbBaseUri;
        }

        public async Task<ExchangeRatesResponse> GetRates(ExchangeRatesRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (request.Date < _MinDate)
            {
                throw new ArgumentOutOfRangeException($"{nameof(request.Date)} must be greater than {_MinDate} but was {request.Date}");
            }

            var uriBuilder = new UriBuilder(_EcbBaseUri)
            {
                Path  = $"{request.Date.ToString("yyyy-MM-dd")}",
                Query = $"base={request.BaseCurrency}"
            };

            var apiResponse = await _HttpClient.GetAsync(uriBuilder.Uri).ConfigureAwait(false);
            var apiContent  = await apiResponse.Content.ReadAsStringAsync();

            if (! apiResponse.IsSuccessStatusCode)
            {
                var error = _ResponseHandler.ParseError(apiContent);

                throw new HttpRequestException(
                    $"{nameof(apiResponse.StatusCode)}: {apiResponse.StatusCode}. {error}");
            }

            var exchangeRates = _ResponseHandler.ParseResponse(apiContent);

            return exchangeRates;
        }
    }
}
