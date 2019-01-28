using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OutfitGenerator.Generators
{
    public class DirectiveGenerator
    {
        /// <summary>
        /// Alpha 0/1 are considered transparent, but #FFFFFF00 on the source bitmap is ignored regardless of the target color.
        /// </summary>
        public static readonly Rgba32 TRANSPARENT = new Rgba32(255, 255, 255, 0);

        /// <summary>
        /// Generates a replace directive to change from into to.
        /// If the same color is replaced by multiple different colors, only the last one will be remembered (going row by row, left to right, top to bottom).
        /// </summary>
        /// <param name="from">Source image.</param>
        /// <param name="to">Target image.</param>
        /// <returns>Directive string.</returns>
        public static string Generate(Image<Rgba32> from, Image<Rgba32> to)
        {
            if (from == null || to == null)
                throw new ArgumentNullException("Images may not be null.");

            var size = from.Size();
            if (size != to.Size())
                throw new ArgumentException(string.Format("Image sizes must match.\n" +
                    "Source: {0}" +
                    "Target: {1}",
                    from.Size(),
                    to.Size()));
            
            var conversions = new Dictionary<Rgba32, Rgba32>();

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    Rgba32 colorFrom = from[x, y],
                        colorTo = to[x, y];

                    if (colorFrom.Equals(TRANSPARENT)) continue;
                    if (colorFrom.Equals(colorTo)) continue;
                    if (colorTo.A == 0) continue;

                    conversions[colorFrom] = colorTo;
                }
            }

            string directives = CreateDirectives(conversions);
            return directives;
        }

        static readonly Regex regRepetitiveColor = new Regex("([0-9a-f])\\1([0-9a-f])\\2([0-9a-f])\\3([0-9a-f])\\4");
        /// <summary>
        /// Converts a Color to a hexidecimal color code, formatted RRGGBBAA or RGBA.
        /// </summary>
        /// <param name="c">Color to convert.</param>
        /// <returns>Hexadecimal RRGGBBAA or RGBA color code.</returns>
        public static string ColorToString(Rgba32 c)
        {
            string r = c.R.ToString("X2"),
                    g = c.G.ToString("X2"),
                    b = c.B.ToString("X2"),
                    a = c.A.ToString("X2");

            var res = (r + g + b + a).ToLowerInvariant();
            return regRepetitiveColor.IsMatch(res)
                ? $"{r[0]}{g[0]}{b[0]}{a[0]}"
                : res;
        }

        /// <summary>
        /// Creates a replace directives string that converts all color keys to their respective color values.
        /// The order of directives can not be guaranteed, and may not match the order in which entries were added to the dictionary.
        /// </summary>
        /// <param name="conversions">Table containing all color conversions. Keys are converted to their value counterpart.</param>
        /// <returns>Replace directives string.</returns>
        public static string CreateDirectives(Dictionary<Rgba32, Rgba32> conversions)
        {
            StringBuilder directives = new StringBuilder("?replace");
            foreach (KeyValuePair<Rgba32, Rgba32> conversion in conversions)
            {
                directives.AppendFormat(";{0}={1}", ColorToString(conversion.Key), ColorToString(conversion.Value));
            }
            return directives.ToString();
        }
    }
}
