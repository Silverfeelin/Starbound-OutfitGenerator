using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System.Collections.Generic;

namespace OutfitGenerator.Generators
{
    public class BackGenerator : ClothingGenerator
    {
        public override string Name => "Back Item";
        public override int Priority => 70;
        public override string FileName => "back";

        private readonly ISet<Size> _supportedDimensions = new HashSet<Size>() { new Size(387,301) };        
        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Image<Rgba32> Template => ImageResourceManager.GetImage("animatedBackTemplate.png");
        public override JObject Config => JsonResourceManager.GetJsonObject("BackConfig.json");
    }
}
