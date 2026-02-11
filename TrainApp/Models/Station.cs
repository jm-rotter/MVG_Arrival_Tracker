using System;
using System.Collections.Generic;


namespace TrainApp.Models;

public class Station
{
    // These must match the keys in your stations.json
    public string name { get; set; } = string.Empty;
    public string place { get; set; } = string.Empty;
    public string id { get; set; } = string.Empty;
    public int? divaId { get; set; }
    public string abbreviation { get; set; } = string.Empty;
    public string tariffZones { get; set; } = string.Empty;
    public List<string> products { get; set; } = new();
    public double latitude {get; set; }
    public double longitude {get; set; }

}