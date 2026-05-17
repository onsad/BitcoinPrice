using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BitcoinPrice.DTOs
{
    public class SavedBitcoinPriceDto : IValidatableObject
    {
        public int Id { get; set; }

        public bool Selected { get; set; }

        [DisplayName("Price in EUR")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal PriceEur { get; set; }
        
        [DisplayName("EUR to CZK Rate")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal EurToCzkRate { get; set; }

        [DisplayName("Price in CZK")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal PriceCzk { get; set; }

        [DisplayName("Downloaded At")]
        public DateTime DownLoaded { get; set; }

        [DisplayName("Note")]
        public string? Note { get; set; }

        public byte[] RowVersion { get; set; } = [];

        public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
        {
            if (Selected && string.IsNullOrWhiteSpace(Note))
            {
                yield return new ValidationResult("Note is required for selected records.",[nameof(Note)]);
            }
        }
    }
}
