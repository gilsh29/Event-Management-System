using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly string _apiKey = "b69b2125886542749e591729250506 "; // Replace with your key

    public WeatherService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<WeatherResponse> GetWeatherAsync(string location)
    {
        // Try to get cached result
        if (_cache.TryGetValue(location, out WeatherResponse cachedWeather))
        {
            return cachedWeather;
        }

        var url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={location}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var weather = JsonSerializer.Deserialize<WeatherResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Cache the result for 30 minutes
        _cache.Set(location, weather, TimeSpan.FromMinutes(30));

        return weather;
    }
}
