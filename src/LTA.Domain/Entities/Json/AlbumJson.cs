using System.Text.Json.Serialization;

namespace LTA.Domain.Entities.Json;

public class AlbumJson
{
    [JsonPropertyName("userId")] public int UserId { get; set; }

    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; } = null!;
}