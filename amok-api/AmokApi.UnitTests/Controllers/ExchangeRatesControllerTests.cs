using AmokApi.Controllers;
using AmokApi.Controllers.Dtos;
using AmokApi.ExchangeRates;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AmokApi.UnitTests.Controllers
{
    public class ExchangeRatesControllerTests
    {
        [Fact]
        public void WhenManagerIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new ExchangeRatesController(null, Mock.Of<IExchangeRatesDtoFactory>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*manager*");
        }

        [Fact]
        public void WhenDtoFactoryIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new ExchangeRatesController(Mock.Of<IExchangeRatesManager>(), null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*dtofactory*");
        }

        [Fact]
        public async Task WhenManagerReturnsCurrentRates_ThenResponseIsUsedToBuildDto()
        {
            var currentRates = new ExchangeRatesResponse("EUR", DateTime.Now, new Dictionary<string, decimal>());

            var manager = new Mock<IExchangeRatesManager>();
            manager.Setup(m => m.GetLatestRates()).ReturnsAsync(currentRates);

            ExchangeRatesResponse buildDtoInput = null;

            var dtoFactory = new Mock<IExchangeRatesDtoFactory>();

            dtoFactory.Setup(m => m.BuildDto(It.IsAny<ExchangeRatesResponse>()))
                .Callback<ExchangeRatesResponse>(input => buildDtoInput = input)
                .Returns(new ExchangeRatesResponseDto());

            var controller = new ExchangeRatesController(manager.Object, dtoFactory.Object);

            await controller.CurrentRates();

            buildDtoInput.Should().Be(currentRates);
        }
    }
}
