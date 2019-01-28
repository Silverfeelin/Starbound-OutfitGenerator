using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class HidingPantsGenerator : PantsGenerator
    {
        public override string Name => "Pants (hide body)";
        public override int Priority => 60;

        public override Image<Rgba32> Template => ImageResourceManager.GetImage("invisibleAnimatedPantsTemplate.png");
        public override JObject Config => JsonResourceManager.GetJsonObject("HidingPantsConfig.json");
    }
}
