using AmokApi.Controllers.Dtos;
using AmokApi.ExchangeRates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AmokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private IExchangeRatesManager    _Manager    { get; }
        private IExchangeRatesDtoFactory _DtoFactory { get; }

        public ExchangeRatesController(
            IExchangeRatesManager exchangeRatesManager,
            IExchangeRatesDtoFactory dtoFactory)
        {
            _Manager    = exchangeRatesManager ?? throw new ArgumentNullException(nameof(exchangeRatesManager));
            _DtoFactory = dtoFactory           ?? throw new ArgumentNullException(nameof(dtoFactory));
        }

        [HttpGet]
        public async Task<ActionResult<ExchangeRatesResponseDto>> Index()
        {
            var latestRates = await _Manager.GetLatestRates().ConfigureAwait(false);
            
            var dto = _DtoFactory.BuildDto(latestRates);

            return dto;
        }
    }
}
