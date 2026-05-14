using BitcoinPrice.DTOs;
using BitcoinPrice.Models;
using BitcoinPrice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BitcoinPrice.Controllers
{
    public class HomeController(IMarketDataService marketDataService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
