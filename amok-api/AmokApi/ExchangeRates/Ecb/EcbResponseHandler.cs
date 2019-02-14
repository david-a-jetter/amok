using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AmokApi.ExchangeRates.Ecb
{
    public class EcbJsonResponseHandler : IApiResponseHandler
    {
        public ExchangeRatesResponse ParseResponse(string apiResponse)
        {
            if (string.IsNullOrWhiteSpace(apiResponse)) throw new ArgumentNullException(nameof(apiResponse));

            var jobject = JObject.Parse(apiResponse);

            var baseCurrency = jobject["base"].ToString();

            var responseDateComponents = jobject["date"].ToString().Split('-');

            var responseDate = new DateTime(
                int.Parse(responseDateComponents[0]),
                int.Parse(responseDateComponents[1]),
                int.Parse(responseDateComponents[2]));

            var rates = jobject["rates"].ToObject<Dictionary<string, decimal>>();

            var exchangeRates = new ExchangeRatesResponse(
                baseCurrency: baseCurrency,
                date        : responseDate,
                rates       : rates);

            return exchangeRates;
        }

        public string ParseError(string apiResponse)
        {
            if (string.IsNullOrWhiteSpace(apiResponse)) throw new ArgumentNullException(nameof(apiResponse));

            var jobject = JObject.Parse(apiResponse);

            var error = jobject["error"].ToString();

            return error;
        }
    }
}
