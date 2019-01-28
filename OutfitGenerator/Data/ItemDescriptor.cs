using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace OutfitGenerator
{
    [JsonObject]
    public class ItemDescriptor
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "perfectlygenericitem";

        [JsonProperty("count")]
        public int Count { get; set; } = 1;

        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Parameters { get; set; } = new JObject();
    }
}
