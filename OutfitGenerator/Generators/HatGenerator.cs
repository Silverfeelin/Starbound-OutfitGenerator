using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OutfitGenerator.Generators
{
    public class HatGenerator : ClothingGenerator
    {
        public override string FileName => "hat";

        private ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(86, 215),
            new Size(43, 43)
        };

        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Bitmap Template => null;

        public override byte[] Config => Properties.Resources.HatConfig;

        public override ItemDescriptor Generate(Bitmap bitmap)
        {
            // Parse arguments
            if (bitmap == null)
            {
                throw new ArgumentNullException("Bitmap may not be null.");
            }

            if (!SupportedDimensions.Contains(bitmap.Size))
            {
                throw new ArgumentException("Bitmap does not match any of the expected dimensions: " + String.Join(", ", SupportedDimensions));
            }

            // Crop
            if (bitmap?.Height == 215)
            {
                bitmap = Crop(bitmap, 43, 0, 43, 43);
            }

            // Gemerate
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

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
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.A == 0) continue;

                    dir.AppendFormat(";{0}00{1}00={2}",
                        x.ToString("X2"),
                        y.ToString("X2"),
                        ColorToString(pixel));
                }
            }

            // Load descriptor
            JObject config = JsonResourceManager.GetJsonObject(Config);
            ItemDescriptor descriptor = config["descriptor"].ToObject<ItemDescriptor>();

            // Generate and apply directives
            descriptor.Parameters["directives"] = dir.ToString();

            return descriptor;
        }

        public string ColorToString(Color color)
        {
            return color.R.ToString("X2") +
                color.G.ToString("X2") +
                color.B.ToString("X2") +
                color.A.ToString("X2");
        }
    }
}
