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

            try
            {
                logger.LogInformation("Loading BTC/EUR price from {Url}", url);

                using var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                using var document = JsonDocument.Parse(json);

                var price = document
                    .RootElement
                    .GetProperty("Data")
                    .GetProperty("BTC-EUR")
                    .GetProperty("VALUE")
                    .GetDecimal();

                logger.LogInformation("BTC/EUR price loaded successfully: {Price}", price);

                return price;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "HTTP error while loading BTC/EUR price from {Url}", url);

                throw;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Invalid JSON response from CoinDesk API");

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while loading BTC/EUR price");

                throw;
            }
        }

        public async Task<decimal> GetEurToCzkRateAsync()
        {
            var builder = new UriBuilder($"{apiOptions.Value.CnbUrl}{apiOptions.Value.CnbEndpoint}")
            {
                Query = $"date={DateTime.Today:yyyy-MM-dd}"
            };

            try
            {
                logger.LogInformation("Loading EUR/CZK exchange rate from CNB API: {Url}", builder.Uri);

                using var response = await httpClient.GetAsync(builder.Uri);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<CnbRatesResponse>(json);

                if (data?.rates == null || !data.rates.Any())
                {
                    logger.LogWarning("CNB API returned empty rates collection");

                    throw new InvalidOperationException("CNB API returned no exchange rates.");
                }

                var eurRate = data.rates.FirstOrDefault(x => x.currencyCode == "EUR");

                if (eurRate == null)
                {
                    logger.LogWarning("EUR exchange rate was not found in CNB response");

                    throw new InvalidOperationException("EUR exchange rate was not found.");
                }

                if (eurRate.amount == 0)
                {
                    logger.LogWarning("EUR amount is zero in CNB response");

                    throw new DivideByZeroException("EUR amount cannot be zero.");
                }

                var rate = eurRate.rate / eurRate.amount;

                logger.LogInformation("EUR/CZK exchange rate loaded successfully: {Rate}", rate);

                return rate;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "HTTP error while loading EUR/CZK exchange rate");

                throw;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Invalid JSON returned from CNB API");

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while loading EUR/CZK exchange rate");

                throw;
            }
        }
    }
}
