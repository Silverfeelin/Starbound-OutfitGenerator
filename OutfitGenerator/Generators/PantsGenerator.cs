using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System.Collections.Generic;

namespace OutfitGenerator.Generators
{
    public class PantsGenerator : ClothingGenerator
    {
        public override string Name => "Pants";
        public override int Priority => 50;
        public override string FileName => "pants";

        private readonly ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(387, 258),
            new Size(387, 301)
        };
        
        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Image<Rgba32> Template => ImageResourceManager.GetImage("animatedPantsTemplate.png");
        public override JObject Config => JsonResourceManager.GetJsonObject("PantsConfig.json");

        public override ItemDescriptor Generate(Image<Rgba32> bitmap)
        {
            if (bitmap?.Height == 301)
                bitmap = Crop(bitmap, 0, 0, bitmap.Width, 258);
            
            return base.Generate(bitmap);
        }
    }
}
