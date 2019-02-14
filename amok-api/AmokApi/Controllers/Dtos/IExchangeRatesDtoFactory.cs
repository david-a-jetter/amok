using AmokApi.ExchangeRates;

namespace AmokApi.Controllers.Dtos
{
    public interface IExchangeRatesDtoFactory
    {
        ExchangeRatesResponseDto BuildDto(ExchangeRatesResponse exchangeRates);
    }
}
