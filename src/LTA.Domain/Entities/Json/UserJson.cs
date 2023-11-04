using System.Text.Json.Serialization;

namespace LTA.Domain.Entities.Json;

public class UserJson
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("username")] public string Username { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonPropertyName("address")] public AddressJson Address { get; set; }

    [JsonPropertyName("phone")] public string Phone { get; set; }

    [JsonPropertyName("website")] public string Website { get; set; }

    [JsonPropertyName("company")] public CompanyJson Company { get; set; }
}