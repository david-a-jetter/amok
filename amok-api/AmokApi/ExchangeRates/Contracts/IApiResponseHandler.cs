using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmokApi.ExchangeRates.Contracts
{
    public interface IApiResponseHandler
    {
        ExchangeRatesResponse ParseResponse(string apiResponse);

        string ParseError(string apiResponse);
    }
}
