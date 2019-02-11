using AmokApi.ExchangeRates;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates
{
    public class EcbExchangeRatesAccessTests
    {
        private static readonly Uri _EcbBaseUri = new Uri("https://api.exchangeratesapi.io");

        [Fact]
        public void WhenHttpClientIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new EcbExchangeRatesAccess(null, new Uri("http://localhost")));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*httpclient*");
        }

        [Fact]
        public async Task WhenLatestRatesAreRetrieved_ThenDefaultBaseCurrencyIsEuro()
        {
            var access = new EcbExchangeRatesAccess(new HttpClient(), _EcbBaseUri);

            var rates = await access.GetLatestRates();
        }
    }
}
