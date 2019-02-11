using AmokApi.ExchangeRates;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates
{
    public class ExchangeRatesResponseTests
    {
        [Fact]
        public void WhenBaseIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponse(null, DateTime.Now, new Dictionary<string, decimal>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenBaseIsEmpty_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponse(string.Empty, DateTime.Now, new Dictionary<string, decimal>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenBaseIsWhitespace_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponse(" ", DateTime.Now, new Dictionary<string, decimal>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenRatesIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponse("EUR", DateTime.Now, null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*rates*");
        }
    }
}
