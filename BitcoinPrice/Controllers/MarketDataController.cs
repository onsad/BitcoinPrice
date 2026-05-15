using BitcoinPrice.DTOs;
using BitcoinPrice.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinPrice.Controllers
{
    [ApiController]
    [Route("api")]
    public class MarketDataController(IMarketDataService marketDataService) : ControllerBase
    {
        [HttpGet("liveBtcPrice")]
        public async Task<LiveBitcoinPriceDto> GetLiveBtcPrice()
        {
            var btcPrice = await marketDataService.GetBitcoinPriceEurAsync();
            var czkRate = await marketDataService.GetEurToCzkRateAsync();

            return new LiveBitcoinPriceDto
            {
                PriceEur = btcPrice,
                EurToCzkRate = czkRate
            };
        }
    }
}
