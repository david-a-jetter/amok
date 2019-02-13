using AmokApi.ExchangeRates;
using FluentAssertions;
using Moq;
using System;
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
    }
}
