using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TrainApp.Services;

namespace TrainApp.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        [ObservableProperty] private string _currentTemp = "Loading...";
        [ObservableProperty] private string _locationName = "Loading...";
        [ObservableProperty] private string _mapUrl = "";
        [ObservableProperty] private string _weatherDescription = "Clear";

        private readonly WeatherService _weatherService = new();
        private readonly HttpClient _httpClient = new();
        
        private const string UnsplashAccessKey = "0-Ufv0neYx44ajZl_CMRRsfDmT2CXje4Ai9ENiVTnmI";

        public WeatherViewModel()
        {
            _ = LoadWeatherLoopAsync();
        }

        private async Task LoadWeatherLoopAsync()
        {
            while (true)
            {
                var (temp, code) = await _weatherService.GetFullWeatherAsync();
                CurrentTemp = temp;
                WeatherDescription = code switch {
                    0 => "Clear Sky",
                    1 or 2 or 3 => "Partly Cloudy",
                    45 or 48 => "Foggy",
                    51 or 53 or 55 => "Drizzle",
                    61 or 63 or 65 => "Rainy",
                    71 or 73 or 75 => "Snowy",
                    _ => "Mostly Cloudy"
                };

                await UpdateTravelImageAsync();

                await Task.Delay(TimeSpan.FromMinutes(15));
            }
        }

        private async Task UpdateTravelImageAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(
                    $"https://api.unsplash.com/photos/random?client_id={UnsplashAccessKey}&query=nature,travel,scenic");
                
                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                MapUrl = root.GetProperty("urls").GetProperty("regular").GetString() ?? "";

                if (root.TryGetProperty("location", out var loc))
                {
                    string? city = loc.TryGetProperty("city", out var c) ? c.GetString() : null;
                    string? country = loc.TryGetProperty("country", out var co) ? co.GetString() : null;

                    if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(country))
                        LocationName = $"{city}, {country}".ToUpper();
                    else if (!string.IsNullOrEmpty(country))
                        LocationName = country.ToUpper();
                    else
                        LocationName = "WANDERLUST";
                }
                else
                {
                    LocationName = "A BEAUTIFUL ESCAPE";
                }
            }
            catch
            {
                MapUrl = "https://loremflickr.com/800/600/scenic,travel/all?random=" + DateTime.Now.Ticks;
                LocationName = "COOL PLACE ALERT";
            }
        }
    }
}