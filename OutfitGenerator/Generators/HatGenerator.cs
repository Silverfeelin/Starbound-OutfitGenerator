using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace OutfitGenerator.Generators
{
    public class HatGenerator : ClothingGenerator
    {
        public override string Name => "Hat";
        public override int Priority => 10;
        public override string FileName => "hat";

        private readonly ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(86, 215),
            new Size(43, 43)
        };
        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Image<Rgba32> Template => null;
        public override JObject Config => JsonResourceManager.GetJsonObject("HatConfig.json");

        public override ItemDescriptor Generate(Image<Rgba32> image)
        {
            // Parse arguments
            if (image == null)
            {
                throw new ArgumentNullException("Bitmap may not be null.");
            }

            if (!SupportedDimensions.Contains(image.Size()))
            {
                throw new ArgumentException("Bitmap does not match any of the expected dimensions: " + String.Join(", ", SupportedDimensions));
            }

            // Crop
            if (image?.Height == 215)
            {
                image = Crop(image, 43, 0, 43, 43);
            }

            // Generate
            var flipped = image.Clone(ctx => ctx.Flip(FlipMode.Vertical));

            // 01002B00 2B002B00
            // 01000100 2B000100
            StringBuilder dir = new StringBuilder("?crop;0;0;2;2?replace;fff0=fff;0000=fff?setcolor=fff" +
                "?blendmult=/objects/outpost/customsign/signplaceholder.png" +
                "?replace;01000101=01000100;02000101=2B000100?replace;01000201=01002B00;02000201=2B002B00" +
                "?scale=43?crop=0;0;43;43");

            dir.Append("?replace");
            for (int x = 0; x < 43; x++)
            {
                for (int y = 0; y < 43; y++)
                {
                    Rgba32 pixel = flipped[x, y];
                    if (pixel.A == 0) continue;

                    dir.AppendFormat(";{0}00{1}00={2}",
                        x.ToString("X2"),
                        y.ToString("X2"),
                        DirectiveGenerator.ColorToString(pixel));
                }
            }

            // Load descriptor
            ItemDescriptor descriptor = Config["descriptor"].ToObject<ItemDescriptor>();

            // Generate and apply directives
            descriptor.Parameters["directives"] = dir.ToString();

            return descriptor;
        }
    }
}
