using System.Threading.Tasks;

namespace AmokApi.ExchangeRates.Contracts
{
    public interface IExchangeRatesAccess
    {
        Task<ExchangeRatesResponse> GetRates(ExchangeRatesRequest request);

        ExchangeRatesRequest DefaultRequest { get; }
    }
}
