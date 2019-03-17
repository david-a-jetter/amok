using AmokApi.ExchangeRates;
using AmokApi.ExchangeRates.Contracts;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates
{
    public class CachingExchangeRatesAccessTests
    {
        [Fact]
        public void WhenCacheIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new CachingExchangeRatesAccess(null, Mock.Of<IExchangeRatesAccess>()));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*cache*");
        }

        [Fact]
        public void WhenAccessIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new CachingExchangeRatesAccess(new MemoryCache("name"), null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*access*");
        }

        [Fact]
        public void WhenRatesRequestIsNull_ThenGetThrows()
        {
            var cachingAccess = new CachingExchangeRatesAccess(
                new MemoryCache("rates"),
                Mock.Of<IExchangeRatesAccess>());

            var getFunc = new Func<Task>(() => cachingAccess.GetRates(null));

            getFunc.Should().Throw<ArgumentNullException>().WithMessage("*request*");
        }

        [Fact]
        public async Task WhenCacheDoesNotContainItem_ThenAccessIsInvoked()
        {
            var expectedRequest = new ExchangeRatesRequest("BASE", DateTime.UtcNow);
            var accessMock      = new Mock<IExchangeRatesAccess>();

            ExchangeRatesRequest actualRequest = null;

            accessMock.Setup(m => m.GetRates(It.IsAny<ExchangeRatesRequest>()))
                .Callback<ExchangeRatesRequest>(request => actualRequest = request)
                .ReturnsAsync(new ExchangeRatesResponse(
                    expectedRequest.BaseCurrency,
                    expectedRequest.Date,
                    new Dictionary<string, decimal>()));

            var cachingAccess = new CachingExchangeRatesAccess(new MemoryCache("rates"), accessMock.Object);

            await cachingAccess.GetRates(expectedRequest);

            actualRequest.Should().BeEquivalentTo(expectedRequest);
        }

        [Fact]
        public async Task WhenCacheDoesContainItem_ThenItemIsReturnedFromCache()
        {
            var expectedRequest = new ExchangeRatesRequest("BASE", DateTime.UtcNow);
            var accessMock      = new Mock<IExchangeRatesAccess>();

            var actualRequests = new List<ExchangeRatesRequest>();

            accessMock.Setup(m => m.GetRates(It.IsAny<ExchangeRatesRequest>()))
                .Callback<ExchangeRatesRequest>(request => actualRequests.Add(request))
                .ReturnsAsync(new ExchangeRatesResponse(
                    expectedRequest.BaseCurrency,
                    expectedRequest.Date,
                    new Dictionary<string, decimal>()));

            var cachingAccess = new CachingExchangeRatesAccess(new MemoryCache("rates"), accessMock.Object);

            await cachingAccess.GetRates(expectedRequest);
            await cachingAccess.GetRates(expectedRequest);

            actualRequests.Should().AllBeEquivalentTo(expectedRequest);
            actualRequests.Count.Should().Be(1);
        }

        [Fact]
        public void WhenDefaultRequestIsRetrieved_ThenItIsTheDefaultOfTheInjectedAccess()
        {
            var expectedRequest = new ExchangeRatesRequest("BASEBASE", DateTime.UtcNow);

            var injectedAccess = new Mock<IExchangeRatesAccess>();
            injectedAccess.SetupGet(m => m.DefaultRequest).Returns(expectedRequest);

            var cachingAccess = new CachingExchangeRatesAccess(
                new MemoryCache("name"),
                injectedAccess.Object);

            var defaultRequest = cachingAccess.DefaultRequest;

            defaultRequest.Should().BeEquivalentTo(expectedRequest);
        }
    }
}
