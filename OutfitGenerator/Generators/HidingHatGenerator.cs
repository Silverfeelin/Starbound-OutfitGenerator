using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;

namespace OutfitGenerator.Generators
{
    public class HidingHatGenerator : HatGenerator
    {
        public override string Name => "Hat (hide body)";
        public override int Priority => 30;

        public override JObject Config => JsonResourceManager.GetJsonObject("HidingHatConfig.json");
    }
}
