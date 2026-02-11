using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace TrainApp.Models;
public class Departure
{
    [JsonPropertyName("plannedDepartureTime")]
    public long? plannedDepartureTime { get; set; }

    [JsonPropertyName("realtime")]
    public bool? realtime { get; set; }

    [JsonPropertyName("delayInMinutes")]
    public int? delayInMinutes { get; set; }

    [JsonPropertyName("realtimeDepartureTime")]
    public long? realtimeDepartureTime { get; set; }

    [JsonPropertyName("transportType")]
    public string? transportType { get; set; }

    [JsonPropertyName("label")]
    public string? label { get; set; }

    [JsonPropertyName("divaId")]
    public string? divaId { get; set; }

    [JsonPropertyName("network")]
    public string? network { get; set; }

    [JsonPropertyName("trainType")]
    public string? trainType { get; set; }

    [JsonPropertyName("destination")]
    public string? destination { get; set; }

    [JsonPropertyName("cancelled")]
    public bool? cancelled { get; set; }

    [JsonPropertyName("sev")]
    public bool? sev { get; set; }

    [JsonPropertyName("stopPositionNumber")]
    public int? stopPositionNumber { get; set; }

    [JsonPropertyName("messages")]
    public List<string>? messages {get;set;}

    [JsonPropertyName("infos")]
    public List<Infos>? infos {get;set;}

    [JsonPropertyName("bannerHash")]
    public string? bannerHash { get; set; }


    [JsonPropertyName("occupancy")]
    public string? occupancy { get; set; }

    [JsonPropertyName("stationGlobalId")]
    public string? stationGlobalId { get; set; }


    [JsonPropertyName("stopPointGlobalId")]
    public string? stopPointGlobalId { get; set; }

    [JsonPropertyName("lineId")]
    public string? lineId { get; set; }


    [JsonPropertyName("tripId")]
    public string? tripId { get; set; }


    [JsonPropertyName("tripCode")]
    public int? tripCode { get; set; }


    // This property calculates the countdown for the UI
    public string DisplayTime
    {
        get
        {
            // Current time in Unix Milliseconds
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            
            // DepartureTime is already the "realtime" one based on your JSON
            var diff = (this.realtimeDepartureTime - now) / 60000; 

            if (diff <= 0) return "Now";
            return $"{diff} min";
        }
    }
}