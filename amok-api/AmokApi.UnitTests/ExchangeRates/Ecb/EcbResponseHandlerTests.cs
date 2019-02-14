using AmokApi.ExchangeRates.Ecb;
using FluentAssertions;
using System;
using Xunit;

namespace AmokApi.UnitTests.ExchangeRates.Ecb
{
    public class EcbResponseHandlerTests
    {
        [Fact]
        public void WhenApiResponseIsNull_ThenParseMethodThrows()
        {
            var handler = new EcbJsonResponseHandler();
            var parseAction = new Action(() => handler.ParseResponse(null));

            parseAction.Should().Throw<ArgumentNullException>().WithMessage("*apiResponse");
        }

        [Fact]
        public void WhenApiResponseIsEmpty_ThenParseMethodThrows()
        {
            var handler = new EcbJsonResponseHandler();
            var parseAction = new Action(() => handler.ParseResponse(string.Empty));

            parseAction.Should().Throw<ArgumentNullException>().WithMessage("*apiResponse");
        }

        [Fact]
        public void WhenApiResponseIsWhitespace_ThenParseMethodThrows()
        {
            var handler = new EcbJsonResponseHandler();
            var parseAction = new Action(() => handler.ParseResponse(" "));

            parseAction.Should().Throw<ArgumentNullException>().WithMessage("*apiResponse");
        }

        [Fact]
        public void WhenApiResponseIsParsed_ExpectedOutputIsReturned()
        {
            var handler = new EcbJsonResponseHandler();

            var output = handler.ParseResponse(_ApiResponse);

            output.BaseCurrency.Should().Be("EUR");
            output.Date.Should().Be(new DateTime(2019, 2, 8));
            output.Rates.Count.Should().Be(32);
        }

        [Fact]
        public void WhenApiResponseIsNull_ThenParseErrorThrows()
        {
            var handler = new EcbJsonResponseHandler();
            var parseAction = new Action(() => handler.ParseError(null));

            parseAction.Should().Throw<ArgumentNullException>().WithMessage("*apiResponse");
        }

        [Fact]
        public void WhenApiResponseIsEmpty_ThenParseErrorThrows()
        {
            var handler = new EcbJsonResponseHandler();
            var parseAction = new Action(() => handler.ParseError(string.Empty));

            parseAction.Should().Throw<ArgumentNullException>().WithMessage("*apiResponse");
        }

        [Fact]
        public void WhenApiResponseIsWhitespace_ThenParseErrorThrows()
        {
            var handler = new EcbJsonResponseHandler();
            var parseAction = new Action(() => handler.ParseError(" "));

            parseAction.Should().Throw<ArgumentNullException>().WithMessage("*apiResponse");
        }

        [Fact]
        public void WhenApiResponseIsParsed_ExpectedErrorIsReturned()
        {
            var errorMessage = "Base 'A' is not supported.";
            var apiResponse  = $"{{\"error\":\"{errorMessage}\"}}";

            var handler = new EcbJsonResponseHandler();
            var output = handler.ParseError(apiResponse);

            output.Should().Be(errorMessage);
        }

        private const string _ApiResponse = @"{
            ""base"": ""EUR"",
            ""date"": ""2019-02-08"",
            ""rates"": {
                ""NZD"": 1.6809,
                ""CAD"": 1.5098,
                ""MXN"": 21.6028,
                ""AUD"": 1.6006,
                ""CNY"": 7.6527,
                ""PHP"": 59.149,
                ""GBP"": 0.8749,
                ""CZK"": 25.806,
                ""USD"": 1.1346,
                ""SEK"": 10.4973,
                ""NOK"": 9.7693,
                ""TRY"": 5.9488,
                ""IDR"": 15844.69,
                ""ZAR"": 15.4417,
                ""MYR"": 4.6173,
                ""HKD"": 8.902,
                ""HUF"": 318.33,
                ""ISK"": 136.4,
                ""HRK"": 7.4075,
                ""JPY"": 124.57,
                ""BGN"": 1.9558,
                ""SGD"": 1.5376,
                ""RUB"": 74.8085,
                ""RON"": 4.7485,
                ""CHF"": 1.1357,
                ""DKK"": 7.4634,
                ""INR"": 80.8655,
                ""KRW"": 1273.96,
                ""THB"": 35.723,
                ""BRL"": 4.2095,
                ""PLN"": 4.3064,
                ""ILS"": 4.1233
            }
        }";
    }
}
