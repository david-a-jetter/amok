using AmokApi.Controllers.Dtos;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AmokApi.UnitTests.Controllers.Dtos
{
    public class ExchangeRatesResponseDtoTests
    {
        [Fact]
        public void WhenBaseCurrencyIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new ExchangeRatesResponseDto(null, DateTime.Now, new Dictionary<string, decimal>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenBaseCurrencyIsEmpty_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponseDto(string.Empty, DateTime.Now, new Dictionary<string, decimal>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenBaseCurrencyIsWhitespace_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponseDto(" " , DateTime.Now, new Dictionary<string, decimal>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*basecurrency*");
        }

        [Fact]
        public void WhenRatesIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesResponseDto("base", DateTime.Now, null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*rates*");
        }

        [Fact]
        public void WhenDtoIsConstructed_ThenPropertiesReturnExpectedValues()
        {
            var baseCurrency = "base base base";
            var date = DateTime.UtcNow;
            var rates = new Dictionary<string, decimal>
            {
                { "string 1", 111.111m },
                { " EURO "  , 44.5123213m }
            };

            var dto = new ExchangeRatesResponseDto(baseCurrency, date, rates);

            dto.BaseCurrency.Should().Be(baseCurrency);
            dto.Date.Should().Be(date);
            dto.Rates.Should().BeEquivalentTo(rates);
        }
    }
}
