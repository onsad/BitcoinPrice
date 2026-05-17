using BitcoinPrice.AppDbContext;
using BitcoinPrice.DTOs;
using BitcoinPrice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitcoinPrice.Services
{
    public class BitcoinPriceService(BitcoinPriceDbContext bitcoinPriceDbContext, ILogger<BitcoinPriceService> logger) : IBitcoinPriceService
    {
        public async Task DeleteBitcoinPriceRatesAsync(List<int> ids)
        {
            try
            {
                logger.LogInformation($"Deleting {ids.Count} bitcoin price rates.");

                var entities = await bitcoinPriceDbContext.BitcoinRates.Where(x => ids.Contains(x.Id)).ToListAsync();

                bitcoinPriceDbContext.BitcoinRates.RemoveRange(entities);

                await bitcoinPriceDbContext.SaveChangesAsync();

                logger.LogInformation("Bitcoin price rates deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete bitcoin price rates.");

                throw;
            }
        }

        public async Task<List<BitcoinPriceRate>> GetBitcoinPriceRatesAsync()
        {
            try
            {
                logger.LogInformation("Loading saved bitcoin price rates.");

                return await bitcoinPriceDbContext.BitcoinRates.OrderByDescending(x => x.DownloadedAt).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load bitcoin price rates.");

                throw;
            }
        }

        public async Task SaveBitcoinPriceRatesAsync(List<LiveBitcoinPriceDto> rates)
        {
            try
            {
                logger.LogInformation($"Saving {rates.Count} bitcoin price rates.");

                var entities = rates.Select(x => new BitcoinPriceRate
                {
                    PriceInEur = x.PriceEur,
                    EurToCzkRate = x.EurToCzkRate,
                    PriceCzk = x.PriceCzk,
                    DownloadedAt = x.DownLoaded
                });

                await bitcoinPriceDbContext.BitcoinRates.AddRangeAsync(entities);

                await bitcoinPriceDbContext.SaveChangesAsync();

                logger.LogInformation("Bitcoin price rates saved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to save bitcoin price rates.");

                throw;
            }
        }

        public async Task UpdateBitcoinPriceRatesAsync(List<UpdateLiveBitcoinPriceDto> rates)
        {
            try
            {
                logger.LogInformation("Updating {Count} bitcoin price rates.", rates.Count);

                var ids = rates.Select(x => x.Id).ToList();

                var entities = await bitcoinPriceDbContext.BitcoinRates.Where(x => ids.Contains(x.Id)).ToListAsync();

                foreach (var entity in entities)
                {
                    var updatedRate = rates.First(x => x.Id == entity.Id);

                    entity.Note = updatedRate.Note;
                }

                await bitcoinPriceDbContext.SaveChangesAsync();

                logger.LogInformation("Bitcoin price rates updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update bitcoin price rates.");

                throw;
            }
        }
    }
}
