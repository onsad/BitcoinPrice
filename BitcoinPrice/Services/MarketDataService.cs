using BitcoinPrice.DTOs;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BitcoinPrice.Services
{
    public class MarketDataService(HttpClient httpClient, IOptions<MarketDataApiOptions> apiOptions, ILogger<MarketDataService> logger) : IMarketDataService
    {
        public async Task<decimal> GetBitcoinPriceEurAsync()
        {
            var url = $"{apiOptions.Value.CoinDeskUrl}{apiOptions.Value.CoinDeskEndpoint}";

            var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            return document
                .RootElement
                .GetProperty("Data")
                .GetProperty("BTC-EUR")
                .GetProperty("VALUE")
                .GetDecimal();
        }

        public async Task<decimal> GetEurToCzkRateAsync()
        {
            var builder = new UriBuilder($"{apiOptions.Value.CnbUrl}{apiOptions.Value.CnbEndpoint}");

            builder.Query =$"date={DateTime.Today:yyyy-MM-dd}";

            var response = await httpClient.GetAsync(builder.Uri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<CnbRatesResponse>(json);

            var eurRate = data?.rates.FirstOrDefault(x => x.currencyCode == "EUR");

            return eurRate?.rate / eurRate?.amount ?? 0;
        }
    }
}
