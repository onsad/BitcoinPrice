namespace BitcoinPrice.DTOs
{
    public class UpdateLiveBitcoinPriceDto
    {
        public int Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public byte[] RowVersion { get; set; } = [];
    }
}
