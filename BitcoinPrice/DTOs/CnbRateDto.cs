namespace BitcoinPrice.DTOs
{
    public class CnbRateDto
    {
        public string currencyCode { get; set; } = string.Empty;

        public decimal rate { get; set; }

        public int amount { get; set; }
    }
}
