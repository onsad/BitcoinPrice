using BitcoinPrice.DTOs;

namespace BitcoinPrice.ViewModels
{
    public class BitcoinPriceViewModel
    {
        public List<LiveBitcoinPriceDto> LiveBitcoinPrices { get; set; } = new List<LiveBitcoinPriceDto>();
    }
}
