using System.ComponentModel.DataAnnotations;

namespace BitcoinPrice.DTOs
{
    public class UpdateLiveBitcoinPriceDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Note is required.")]
        public string Note { get; set; } = string.Empty;
        public byte[] RowVersion { get; set; } = [];
    }
}
