using System.Text.Json.Serialization;

namespace ImdbProxy.Model;

public class MovieGenre
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}