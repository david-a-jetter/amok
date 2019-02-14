using AmokApi.ExchangeRates;
using FluentAssertions;
using System;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates
{
    public class ExchangeRatesRequestTests
    {
        [Fact]
        public void WhenBaseCurrencyIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => new ExchangeRatesRequest(null, DateTime.UtcNow));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenBaseCurrencyIsEmpty_ThenCtorThrows()
        {
            var ctorAction = new Action(() => new ExchangeRatesRequest(string.Empty, DateTime.UtcNow));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenBaseCurrencyIsWhitespace_ThenCtorThrows()
        {
            var ctorAction = new Action(() => new ExchangeRatesRequest(" ", DateTime.UtcNow));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }
    }
}
