namespace BitcoinPrice.Services
{
    public class MarketDataApiOptions
    {
        public string CoinDeskUrl { get; set; } = string.Empty;

        public string CoinDeskEndpoint { get; set; } = string.Empty;

        public string CnbUrl { get; set; } = string.Empty;

        public string CnbEndpoint { get; set; } = string.Empty;
    }
}
