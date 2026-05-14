namespace BitcoinPrice.DTOs
{
    public class LiveBitcoinPriceDto
    {
        public decimal PriceEur { get; set; }

        public decimal EurToCzkRate { get; set; }

        public decimal PriceCzk => PriceEur * EurToCzkRate;

        public DateTime DownLoaded => DateTime.Now;
    }
}
