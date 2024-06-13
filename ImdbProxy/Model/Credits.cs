using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class Credits
{
    [JsonProperty("cast")]
    public List<Cast> Cast { get; set; }
}