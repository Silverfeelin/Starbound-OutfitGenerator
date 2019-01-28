using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System.Collections.Generic;

namespace OutfitGenerator.Generators
{
    public interface IClothingGenerator
    {
        string Name { get; }
        int Priority { get; }

        /// <summary>
        /// Format for the exported file name.
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Gets the supported Bitmap dimensions for the generator, in pixels.
        /// </summary>
        ISet<Size> SupportedDimensions { get; }

        /// <summary>
        /// Gets the template used to generate directives.
        /// </summary>
        Image<Rgba32> Template { get; }

        /// <summary>
        /// Gets the template configuration used to generate item descriptors.
        /// </summary>
        JObject Config { get; }

        /// <summary>
        /// Generates an item descriptor from the given image.
        /// <para>
        /// Expected input and generated output differ per implementation.
        /// </para>
        /// </summary>
        /// <param name="bitmap">Non-null Bitmap matching any of <see cref="SupportedDimensions"/>.</param>
        /// <returns>Item Descriptor</returns>
        ItemDescriptor Generate(Image<Rgba32> bitmap);
    }
}
