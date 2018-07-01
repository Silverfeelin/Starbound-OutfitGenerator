using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public interface IClothingGenerator
    {
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
        Bitmap Template { get; }

        /// <summary>
        /// Gets the template configuration used to generate item descriptors.
        /// </summary>
        byte[] Config { get; }

        /// <summary>
        /// Generates an item descriptor from the given image.
        /// <para>
        /// Expected input and generated output differ per implementation.
        /// </para>
        /// </summary>
        /// <param name="bitmap">Non-null Bitmap matching any of <see cref="SupportedDimensions"/>.</param>
        /// <returns>Spawnable Item Descriptor</returns>
        ItemDescriptor Generate(Bitmap bitmap);
    }
}
