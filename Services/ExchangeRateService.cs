using System.Net.Http;
using System.Text.Json;
using CurrencyDashboard.Models;

namespace CurrencyDashboard.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "CZOGh7AZmv0rkilVSgLDytePY0EQwTLi";

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExchangeRate>> GetLatestRatesAsync(string baseCurrency)
        {
            var url = $"https://api.apilayer.com/exchangerates_data/latest?base={baseCurrency}&apikey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var rates = new List<ExchangeRate>();
            if (doc.RootElement.TryGetProperty("rates", out var rateObj))
            {
                foreach (var rate in rateObj.EnumerateObject())
                {
                    rates.Add(new ExchangeRate
                    {
                        BaseCurrency = baseCurrency,
                        TargetCurrency = rate.Name,
                        Rate = rate.Value.GetDecimal(),
                        Timestamp = DateTime.UtcNow
                    });
                }
            }

            return rates;
        }
    }
}
