using System.Text.Json.Serialization;

namespace LTA.Domain.Entities.Json;

public class CompanyJson
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("catchPhrase")] public string CatchPhrase { get; set; }

    [JsonPropertyName("bs")] public string Bs { get; set; }
}