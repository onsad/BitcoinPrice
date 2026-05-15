using BitcoinPrice.DTOs;
using BitcoinPrice.Helpers;
using BitcoinPrice.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinPrice.Controllers
{
    public class BitcoinPriceController(IBitcoinPriceService bitcoinPriceService) : Controller
    {
        public async Task<ActionResult<List<SavedBitcoinPriceDto>>> SavedBitcoinPrice()
        {
            var data = await bitcoinPriceService.GetBitcoinPriceRatesAsync();

            var dataForView = data.Select(ModelMappers.MapBitcoinPriceRateEntityToDto).ToList();

            return View(dataForView);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] List<LiveBitcoinPriceDto> data)
        {
            await bitcoinPriceService.SaveBitcoinPriceRatesAsync(data);

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] List<UpdateLiveBitcoinPriceDto> data)
        {
            await bitcoinPriceService.UpdateBitcoinPriceRatesAsync(data);

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] List<int> ids)
        {
            await bitcoinPriceService.DeleteBitcoinPriceRatesAsync(ids);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> HandleTableAction(string action, List<SavedBitcoinPriceDto> model, List<int> selectedIds)
        {
            if (action == "save")
            {
                if (!ModelState.IsValid)
                {
                    var data = await bitcoinPriceService.GetBitcoinPriceRatesAsync();

                    var dataForView = data.Select(ModelMappers.MapBitcoinPriceRateEntityToDto).ToList();

                    return View("SavedBitcoinPrice", dataForView);
                }

                var updates = model.Select(x =>
                    new UpdateLiveBitcoinPriceDto
                    {
                        Id = x.Id,
                        Note = x.Note ?? "",
                        RowVersion = x.RowVersion
                    })
                    .ToList();

                await bitcoinPriceService.UpdateBitcoinPriceRatesAsync(updates);
            }

            if (action == "delete")
            {
                await bitcoinPriceService.DeleteBitcoinPriceRatesAsync(selectedIds);
            }

            return RedirectToAction(nameof(SavedBitcoinPrice));
        }
    }
}
