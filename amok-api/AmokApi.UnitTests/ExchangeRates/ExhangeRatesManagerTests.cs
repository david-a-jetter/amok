using AmokApi.ExchangeRates;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates
{
    public class ExhangeRatesManagerTests
    {
        [Fact]
        public void WhenAccessIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new ExchangeRatesManager(null, Mock.Of<IExchangeRatesEngine>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*access*");
        }

        [Fact]
        public void WhenEngineIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesManager(Mock.Of<IExchangeRatesAccess>(), null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*engine*");
        }

        [Fact]
        public async Task WhenLatestRatesAreRetrieved_ThenAccessDefaultRequestIsUsed()
        {
            var expectedRequest = new ExchangeRatesRequest("DEFAULTBase", DateTime.MaxValue);

            var accessMock = new Mock<IExchangeRatesAccess>();
            accessMock.SetupGet(m => m.DefaultRequest).Returns(expectedRequest);

            ExchangeRatesRequest actualRequest = null;

            accessMock.Setup(m => m.GetRates(It.IsAny<ExchangeRatesRequest>()))
                .Callback<ExchangeRatesRequest>(request => actualRequest = request)
                .ReturnsAsync(new ExchangeRatesResponse(
                    "blah",
                    DateTime.UtcNow,
                    new Dictionary<string, decimal>()));

            var manager = new ExchangeRatesManager(accessMock.Object, Mock.Of<IExchangeRatesEngine>());

            await manager.GetLatestRates();

            actualRequest.Should().Be(expectedRequest);
        }
    }
}
