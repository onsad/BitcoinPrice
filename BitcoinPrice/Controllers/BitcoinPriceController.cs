using BitcoinPrice.DTOs;
using BitcoinPrice.Helpers;
using BitcoinPrice.Services;
using BitcoinPrice.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinPrice.Controllers
{
    public class BitcoinPriceController(IBitcoinPriceService bitcoinPriceService) : Controller
    {
        public async Task<ActionResult<List<SavedBitcoinPriceDto>>> SavedBitcoinPrice(string? sortOrder, decimal? priceEur, decimal? eurToCzkRate, decimal? priceCzk, DateTime? downloaded, string? note)
        {
            ViewBag.PriceEur = priceEur;
            ViewBag.EurToCzkRate = eurToCzkRate;
            ViewBag.PriceCzk = priceCzk;
            ViewBag.Downloaded = downloaded?.ToString("yyyy-MM-dd");
            ViewBag.Note = note;

            ViewData["DownloadedSortParm"] = string.IsNullOrEmpty(sortOrder) ? "downloaded_desc" : "";

            ViewData["PriceEurSortParm"] = sortOrder == "priceeur" ? "priceeur_desc" : "priceeur";

            ViewData["EurToCzkRateParm"] = sortOrder == "eurtoczkrate" ? "eurtoczkrate_desc" : "eurtoczkrate";

            ViewData["PriceCzkSortParm"] = sortOrder == "priceczk" ? "priceczk_desc" : "priceczk";

            var data = await bitcoinPriceService.GetBitcoinPriceRatesAsync(sortOrder, priceEur, eurToCzkRate, priceCzk, downloaded, note);

            var dataForView = data.Select(ModelMappers.MapBitcoinPriceRateEntityToDto).ToList();

            var model = new BitcoinPriceViewModel
            {
                SavedBitcoinPrice = dataForView
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveChanges(BitcoinPriceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill notes for selected records.";

                var refreshedData = await bitcoinPriceService.GetBitcoinPriceRatesAsync(null, null, null, null, null, null);

                return View("SavedBitcoinPrice",
                    new BitcoinPriceViewModel
                    {
                        SavedBitcoinPrice = refreshedData.Select(ModelMappers.MapBitcoinPriceRateEntityToDto).ToList()
                    });
            }

            var updates = model.SavedBitcoinPrice
                .Where(x => x.Selected)
                .Select(x => new UpdateLiveBitcoinPriceDto
                {
                    Id = x.Id,
                    Note = x.Note ?? "",
                    RowVersion = x.RowVersion
                })
                .ToList();

            await bitcoinPriceService.UpdateBitcoinPriceRatesAsync(updates);

            return RedirectToAction(nameof(SavedBitcoinPrice));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelected(BitcoinPriceViewModel model)
        {
            var ids = model.SavedBitcoinPrice.Where(x => x.Selected).Select(x => x.Id).ToList();

            await bitcoinPriceService.DeleteBitcoinPriceRatesAsync(ids);

            return RedirectToAction(nameof(SavedBitcoinPrice));
        }

    }
}
