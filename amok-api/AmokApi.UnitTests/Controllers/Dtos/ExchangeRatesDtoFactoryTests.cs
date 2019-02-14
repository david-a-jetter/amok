using AmokApi.Controllers.Dtos;
using AmokApi.ExchangeRates;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AmokApi.UnitTests.Controllers.Dtos
{
    public class ExchangeRatesDtoFactoryTests
    {
        [Fact]
        public void WhenExchangeRatesResponseIsNull_ThenBuildDtoThrows()
        {
            var factory = new ExchangeRatesDtoFactory();
            var buildAction = new Action(() => factory.BuildDto(null));

            buildAction.Should().Throw<ArgumentNullException>().WithMessage("*exchangeRates*");
        }

        [Fact]
        public void WhenDtoIsBuilt_ThenPropertiesMatchInputObject()
        {
            var input = new ExchangeRatesResponse(
                "base base and base",
                DateTime.UtcNow,
                new Dictionary<string, decimal>
                {
                    { "curr 1", 11546.55555m },
                    { "curr 2", 555.446m }
                });

            var factory = new ExchangeRatesDtoFactory();

            var dto = factory.BuildDto(input);

            dto.BaseCurrency.Should().Be(input.BaseCurrency);
            dto.Date.Should().Be(input.Date);
            dto.Rates.Should().BeEquivalentTo(input.Rates);
        }
    }
}
