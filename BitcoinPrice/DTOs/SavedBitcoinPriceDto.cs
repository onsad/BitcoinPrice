namespace BitcoinPrice.DTOs
{
    public class SavedBitcoinPriceDto
    {
        public int Id { get; set; }

        public decimal PriceEur { get; set; }

        public decimal EurToCzkRate { get; set; }

        public decimal PriceCzk { get; set; }

        public DateTime DownLoaded { get; set; }

        public string? Note { get; set; }

        public byte[] RowVersion { get; set; } = [];
    }
}
