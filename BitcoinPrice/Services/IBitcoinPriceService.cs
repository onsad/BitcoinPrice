using BitcoinPrice.DTOs;
using BitcoinPrice.Entities;

namespace BitcoinPrice.Services
{
    public interface IBitcoinPriceService
    {
        Task<List<BitcoinPriceRate>> GetBitcoinPriceRatesAsync(string? sortOrder, decimal? priceEur, decimal? eurToCzkRate, decimal? priceCzk, DateTime? downloaded, string? note);
        Task SaveBitcoinPriceRatesAsync(List<LiveBitcoinPriceDto> rates);
        Task DeleteBitcoinPriceRatesAsync(List<int> ids);
        Task UpdateBitcoinPriceRatesAsync(List<UpdateLiveBitcoinPriceDto> rates);
    }
}
