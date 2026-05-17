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
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogWarning(ex, $"Concurrency conflict while deleting BitcoinPriceRate with Ids {string.Join(", ", ids)}");

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete bitcoin price rates.");

                throw;
            }
        }

        public async Task<List<BitcoinPriceRate>> GetBitcoinPriceRatesAsync(string? sortOrder)
        {
            IQueryable<BitcoinPriceRate> query = bitcoinPriceDbContext.BitcoinRates;

            query = sortOrder switch
            {
                "downloaded_desc" => query.OrderByDescending(x => x.DownloadedAt),

                "priceeur" => query.OrderBy(x => x.PriceInEur),

                "priceeur_desc" => query.OrderByDescending(x => x.PriceInEur),

                "priceczk" => query.OrderBy(x => x.PriceCzk),

                "priceczk_desc" => query.OrderByDescending(x => x.PriceCzk),

                _ => query.OrderBy(x => x.DownloadedAt)
            };
            try
            {
                logger.LogInformation("Loading saved bitcoin price rates.");

                return await query.ToListAsync();
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
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogWarning(ex, $"Concurrency conflict while saving BitcoinPriceRate.");

                throw;
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
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogWarning(ex, $"Concurrency conflict while updating BitcoinPriceRate with Ids {string.Join(", ", rates.Select(r => r.Id))}");

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update bitcoin price rates.");

                throw;
            }
        }
    }
}
