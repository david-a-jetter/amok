using System.Threading.Tasks;

namespace AmokApi.ExchangeRates
{
    public interface IExchangeRatesAccess
    {
        Task<ExchangeRatesResponse> GetRates(ExchangeRatesRequest request);

        ExchangeRatesRequest DefaultRequest { get; }
    }
}
