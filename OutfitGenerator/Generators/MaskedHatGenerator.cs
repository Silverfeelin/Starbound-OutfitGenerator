using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;

namespace OutfitGenerator.Generators
{
    public class MaskedHatGenerator : HatGenerator
    {
        public override string Name => "Hat (hide hair)";
        public override int Priority => 20;

        public override JObject Config => JsonResourceManager.GetJsonObject("MaskedHatConfig.json");
    }
}
