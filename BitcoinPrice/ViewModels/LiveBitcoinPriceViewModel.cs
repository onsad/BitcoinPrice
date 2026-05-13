using BitcoinPrice.DTOs;

namespace BitcoinPrice.ViewModels
{
    public class LiveBitcoinPriceViewModel
    {
        public List<LiveBitcoinPriceDto> LiveBitcoinPrices { get; set; } = new List<LiveBitcoinPriceDto>();
    }
}
