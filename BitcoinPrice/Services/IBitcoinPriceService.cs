using BitcoinPrice.DTOs;
using BitcoinPrice.Entities;

namespace BitcoinPrice.Services
{
    public interface IBitcoinPriceService
    {
        Task<List<BitcoinPriceRate>> GetBitcoinPriceRatesAsync(string? sortOrder);
        Task SaveBitcoinPriceRatesAsync(List<LiveBitcoinPriceDto> rates);
        Task DeleteBitcoinPriceRatesAsync(List<int> ids);
        Task UpdateBitcoinPriceRatesAsync(List<UpdateLiveBitcoinPriceDto> rates);
    }
}
