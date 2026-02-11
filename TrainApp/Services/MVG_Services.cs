using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TrainApp.Models;

namespace TrainApp.Services;

public class MVG_Services
{
    private readonly HttpClient _httpClient = new();
    private string _TargetStation = "Studentenstadt"; 
    private string? _cachedStationId;

    public MVG_Services() {
    }

    private async Task<string?> GetStationIdAsync(string stationName)
    {
        try
        {
            if (!File.Exists("stations.json")) return null;

            string jsonString = await File.ReadAllTextAsync("stations.json");


            var stations = JsonSerializer.Deserialize<List<Station>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return stations?.FirstOrDefault(s => s.name == stationName)?.id;
        }
        catch
        {
             return null;
        }
    }

 public async Task<List<Departure>> GetDeparturesAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(_cachedStationId))
            {
                _cachedStationId = await GetStationIdAsync(_TargetStation);
            }

            if (string.IsNullOrEmpty(_cachedStationId))
            {
                Console.WriteLine("DEBUG: Station ID could not be resolved.");
                return new List<Departure>();
            }

            var url = "https://www.mvg.de/api/bgw-pt/v3/departures?globalId=" + _cachedStationId;
            Console.WriteLine($"DEBUG: Fetching from: {url}");

            var response = await _httpClient.GetFromJsonAsync<List<Departure>>(url);

            if (response != null)
            {
                Console.WriteLine($"DEBUG: API Success! Found {response.Count} departures.");
                return response;
            }

            return new List<Departure>();
        }
        catch (HttpRequestException httpEx)
        {
            // Catches 403 Forbidden, 404, or Connection issues
            Console.WriteLine($"DEBUG: HTTP Error (Is User-Agent set?): {httpEx.Message}");
        }
        catch (System.Text.Json.JsonException jsonEx)
        {
            // Catches mismatches between JSON and your Departure class
            Console.WriteLine($"DEBUG: JSON Parsing Error: {jsonEx.Message}");
        }
        catch (Exception ex)
        {
            // Catches everything else
            Console.WriteLine($"DEBUG: Unexpected Error: {ex.Message}");
        }

        return new List<Departure>();
    }
}