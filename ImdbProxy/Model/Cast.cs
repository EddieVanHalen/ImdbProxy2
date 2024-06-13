using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class Cast
{
    [JsonProperty("name")]
    public string Name { get; set; }
}