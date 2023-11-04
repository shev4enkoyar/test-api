using System.Text.Json.Serialization;

namespace LTA.Domain.Entities.Json;

public class GeoJson
{
    [JsonPropertyName("lat")] public string Lat { get; set; }

    [JsonPropertyName("lng")] public string Lng { get; set; }
}