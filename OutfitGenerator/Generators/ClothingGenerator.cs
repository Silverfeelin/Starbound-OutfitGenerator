using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;

namespace OutfitGenerator.Generators
{
    public abstract class ClothingGenerator : IClothingGenerator
    {
        public abstract string FileName {get;}

        public abstract ISet<Size> SupportedDimensions { get; }

        public abstract Bitmap Template { get; }

        public abstract byte[] Config { get; }
        
        public virtual ItemDescriptor Generate(Bitmap bitmap)
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
            
            // Load descriptor
            JObject config = JsonResourceManager.GetJsonObject(Config);
            ItemDescriptor descriptor = config["descriptor"].ToObject<ItemDescriptor>();

            // Generate and apply directives
            string directives = descriptor.Parameters["directives"].Value<string>();
            directives = directives.Replace("{directives}", DirectiveGenerator.Generate(Template, bitmap));
            descriptor.Parameters["directives"] = directives;

            return descriptor;
        }

        /// <summary>
        /// Crops out the bottom (unused) row for larger pants sprites.
        /// It's not really necessary, but whatever.
        /// </summary>
        public static Bitmap Crop(Bitmap bmp, int x, int y, int width, int height)
        {
            return bmp.Clone(new Rectangle(x, y, width, height), bmp.PixelFormat);
        }
    }
}
