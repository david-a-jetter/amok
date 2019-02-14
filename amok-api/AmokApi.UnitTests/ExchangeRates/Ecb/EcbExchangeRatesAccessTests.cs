using AmokApi.ExchangeRates;
using AmokApi.ExchangeRates.Ecb;
using AmokApi.UnitTests.Helpers;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates
{
    public class EcbExchangeRatesAccessTests
    {
        [Fact]
        public void WhenResponseHandlerIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new EcbExchangeRatesAccess(
                    null,
                    new HttpClient(),
                    new Uri("http://localhost")));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*responsehandler*");
        }

        [Fact]
        public void WhenHttpClientIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => 
                new EcbExchangeRatesAccess(
                    Mock.Of<IApiResponseHandler>(),
                    null,
                    new Uri("http://localhost")));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*httpclient*");
        }

        [Fact]
        public void WhenHttpClientReturnsServerErrorForLatestRates_ThenAccessThrows()
        {

        }

        [Fact]
        public async Task WhenLatestRatesAreRetrieved_ThenApiResponseIsPassedToHandler()
        {
            var expectedHandlerInput = "some simple json structure";
            var httpMessageHandler = new Mock<HttpMessageHandler>();

            httpMessageHandler.Protected().As<HttpMessageHandlerProtectedMembers>()
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(expectedHandlerInput)
                        });

            string actualHandlerInput = null;

            var responseHandler = new Mock<IApiResponseHandler>();
            responseHandler.Setup(m => m.ParseResponse(It.IsAny<string>()))
                .Callback<string>(input => actualHandlerInput = input);

            var apiAccess = new EcbExchangeRatesAccess(
                responseHandler.Object,
                new HttpClient(httpMessageHandler.Object),
                new Uri("http://localhost"));

            await apiAccess.GetRates(new ExchangeRatesRequest("BASE", DateTime.UtcNow));

            actualHandlerInput.Should().Be(expectedHandlerInput);
        }

        [Fact]
        public void WhenEcbAccessProvidesDefaultRequest_ThenRequestShouldBeforEuroForToday()
        {
            var ecbAccess = new EcbExchangeRatesAccess(
                Mock.Of<IApiResponseHandler>(),
                new HttpClient(),
                new Uri("http://localhost"));

            var defaultRequest = ecbAccess.DefaultRequest;

            defaultRequest.BaseCurrency.Should().Be("EUR");
            defaultRequest.Date.Date.Should().Be(DateTime.UtcNow.Date);
        }

        [Fact]
        public async Task WhenEcbAccessSubmitsRequestToApi_ThenExpectedRequestIsSubmitted()
        {
            var baseUri = "http://localhost";
            var baseCurrency = "baseCur";
            var requestDate = new DateTime(2019, 01, 3);

            var httpMessageHandler = new Mock<HttpMessageHandler>();

            Uri actualUri = default(Uri);

            httpMessageHandler.Protected().As<HttpMessageHandlerProtectedMembers>()
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Callback<HttpRequestMessage, CancellationToken>((message, token) => actualUri = message.RequestUri)
                .ReturnsAsync(
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("content")
                    }
                );

            var ecbAccess = new EcbExchangeRatesAccess(
                Mock.Of<IApiResponseHandler>(),
                new HttpClient(httpMessageHandler.Object),
                new Uri(baseUri));

            await ecbAccess.GetRates(new ExchangeRatesRequest(baseCurrency, requestDate));

            var uriBuilder = new UriBuilder(baseUri)
            {
                Path = $"{requestDate.Date.ToString("yyyy-MM-dd")}",
                Query = $"base={baseCurrency}"
            };

            actualUri.Should().BeEquivalentTo(uriBuilder.Uri);
        }
    }
}
