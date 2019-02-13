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

        [Fact]
        public void WhenTwoEmptyResponsesAreUsed_ThenTheyAreTheSameReference()
        {
            var empty1 = ExchangeRatesResponse.Empty;
            var empty2 = ExchangeRatesResponse.Empty;

            ReferenceEquals(empty1, empty2).Should().BeTrue();
        }

        [Fact]
        public void WhenTwoResponsesHaveTheSameBaseAndDate_ThenTheyAreEquivalent()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            var response2 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());

            response1.Should().BeEquivalentTo(response2);
        }

        [Fact]
        public void WhenSecondResponseIsNull_ThenEqualsReturnsFalse()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());

            response1.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void WhenTwoResponsesAreTheSameReference_ThenEqualsReturnsTrue()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            var response2 = response1;

            response1.Equals(response2).Should().BeTrue();
        }

        [Fact]
        public void WhenTwoResponsesDifferOnlyInBase_ThenEqualsReturnsFalse()
        {
            var base1 = "BASE";
            var base2 = "EUR";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(base1, date, new Dictionary<string, decimal>());
            var response2 = new ExchangeRatesResponse(base2, date, new Dictionary<string, decimal>());

            response1.Equals(response2).Should().BeFalse();
        }

        [Fact]
        public void WhenTwoResponsesDifferOnlyInDate_ThenEqualsReturnsFalse()
        {
            var base1 = "BASE";
            var date1 = DateTime.UtcNow;
            var date2 = date1.AddSeconds(10);

            var response1 = new ExchangeRatesResponse(base1, date1, new Dictionary<string, decimal>());
            var response2 = new ExchangeRatesResponse(base1, date2, new Dictionary<string, decimal>());

            response1.Equals(response2).Should().BeFalse();
        }

        [Fact]
        public void WhenResponseIsCheckedForEqualityOfADifferentType_ThenReturnsFalse()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            var response2 = new object();

            response1.Equals(response2).Should().BeFalse();
        }

        [Fact]
        public void WhenResponseIsCheckedForEqualityOfNullObject_ThenReturnsFalse()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1    = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            object response2 = null;

            response1.Equals(response2).Should().BeFalse();
        }

        [Fact]
        public void WhenResponseIsCheckedForEqualityOfObjectOfDifferentType_ThenReturnsFalse()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            var response2 = (object)"STRING!";

            response1.Equals(response2).Should().BeFalse();
        }

        [Fact]
        public void WhenTwoResponsesHaveTheSameBaseAndDate_ThenObjectEqualityIsTrue()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            object response2 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());

            response1.Equals(response2).Should().BeTrue();
        }

        [Fact]
        public void WhenTwoResponsesHaveTheSameBaseAndDate_ThenHashCodesAreEqual()
        {
            var baseCurrency = "BASE";
            var date = DateTime.UtcNow;

            var response1 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());
            var response2 = new ExchangeRatesResponse(baseCurrency, date, new Dictionary<string, decimal>());

            response1.GetHashCode().Should().Be(response2.GetHashCode());
        }
    }
}
