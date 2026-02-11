using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace TrainApp.Models;

public class Infos
{
    [JsonPropertyName("message")]
    public string? message {get;set;}

    [JsonPropertyName("type")]
    public string? type {get;set;}

    [JsonPropertyName("network")]
    public string? network {get;set;}

}