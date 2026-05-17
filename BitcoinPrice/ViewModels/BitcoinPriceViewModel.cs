using BitcoinPrice.DTOs;

namespace BitcoinPrice.ViewModels
{
    public class BitcoinPriceViewModel
    {
        public List<SavedBitcoinPriceDto> SavedBitcoinPrice { get; set; } = new List<SavedBitcoinPriceDto>();
    }
}
