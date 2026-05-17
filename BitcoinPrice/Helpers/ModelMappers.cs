using BitcoinPrice.DTOs;
using BitcoinPrice.Entities;

namespace BitcoinPrice.Helpers
{
    public static class ModelMappers
    {
        public static SavedBitcoinPriceDto MapBitcoinPriceRateEntityToDto(BitcoinPriceRate entity)
        {
            return new SavedBitcoinPriceDto
            {
                Id = entity.Id,
                PriceEur = entity.PriceInEur,
                EurToCzkRate = entity.EurToCzkRate,
                PriceCzk = entity.PriceCzk,
                DownLoaded = entity.DownloadedAt,
                Note = entity.Note,
                RowVersion = entity.Version
            };
        }
    }
}
