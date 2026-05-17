using System.ComponentModel.DataAnnotations;

namespace BitcoinPrice.Entities
{
    public class BitcoinPriceRate
    {
        public int Id { get; set; }

        public decimal PriceInEur { get; set; }

        public decimal EurToCzkRate { get; set; }

        public decimal PriceCzk { get; set; }

        public DateTime DownloadedAt { get; set; }

        public string? Note { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = default!;
    }
}
