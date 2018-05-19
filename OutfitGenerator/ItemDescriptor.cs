using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace OutfitGenerator
{
    [JsonObject]
    public class ItemDescriptor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; } = 1;

        [JsonProperty("parameters")]
        public JObject Parameters { get; set; } = new JObject();
    }
}
