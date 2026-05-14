namespace BitcoinPrice.Services
{
    public interface IMarketDataService
    {
        Task<decimal> GetBitcoinPriceEurAsync();

        Task<decimal> GetEurToCzkRateAsync();
    }
}