using System.Text.Json.Serialization;

namespace LTA.Domain.Entities.Json;

public class AddressJson
{
    [JsonPropertyName("street")] public string Street { get; set; }

    [JsonPropertyName("suite")] public string Suite { get; set; }

    [JsonPropertyName("city")] public string City { get; set; }

    [JsonPropertyName("zipcode")] public string Zipcode { get; set; }

    [JsonPropertyName("geo")] public GeoJson Geo { get; set; }
}