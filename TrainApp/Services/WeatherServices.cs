using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrainApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _client = new();

        public async Task<(string temp, int code)> GetFullWeatherAsync()
        {
            try {
                var url = "https://api.open-meteo.com/v1/forecast?latitude=48.1375&longitude=11.5755&current_weather=true";
                var response = await _client.GetStringAsync(url);
                using var doc = JsonDocument.Parse(response);
                var current = doc.RootElement.GetProperty("current_weather");
                return (
                    $"{current.GetProperty("temperature").GetDouble()}°C",
                    current.GetProperty("weathercode").GetInt32()
                );
            } catch { return ("--°C", 0); }
        }
    }
}