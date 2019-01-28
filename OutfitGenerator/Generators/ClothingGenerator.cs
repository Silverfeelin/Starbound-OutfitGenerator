using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace OutfitGenerator.Generators
{
    public abstract class ClothingGenerator : IClothingGenerator
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract string FileName {get;}

        public abstract ISet<Size> SupportedDimensions { get; }

        public abstract Image<Rgba32> Template { get; }
        public abstract JObject Config { get; }
        
        public virtual ItemDescriptor Generate(Image<Rgba32> image)
        {
            // Parse arguments
            if (image == null)
                throw new ArgumentNullException("Image may not be null.");

            if (!SupportedDimensions.Contains(image.Size()))
                throw new ArgumentException("Bitmap does not match any of the expected dimensions: " + String.Join(", ", SupportedDimensions));

            // Load descriptor
            ItemDescriptor descriptor = Config["descriptor"].ToObject<ItemDescriptor>();

            // Generate and apply directives
            string directives = descriptor.Parameters["directives"].Value<string>();
            directives = directives.Replace("{directives}", DirectiveGenerator.Generate(Template, image));
            descriptor.Parameters["directives"] = directives;

            return descriptor;
        }

        /// <summary>
        /// Crops an image.
        /// </summary>
        public static Image<Rgba32> Crop(Image<Rgba32> image, int x, int y, int width, int height)
        {
            return image.Clone(ctx => ctx.Crop(new Rectangle(x, y, width, height)));
        }
    }
}
