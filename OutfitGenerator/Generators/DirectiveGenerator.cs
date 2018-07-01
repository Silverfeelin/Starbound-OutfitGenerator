using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OutfitGenerator.Generators
{
    public class DirectiveGenerator
    {
        /// <summary>
        /// Alpha 0/1 are considered transparent, but #FFFFFF00 on the source bitmap is ignored regardless of the target color.
        /// </summary>
        public static readonly Color TRANSPARENT = Color.FromArgb(0, 255, 255, 255);

        /// <summary>
        /// Generates a replace directive to change from into to.
        /// If the same color is replaced by multiple different colors, only the last one will be remembered (going row by row, left to right, top to bottom).
        /// </summary>
        /// <param name="from">Source image.</param>
        /// <param name="to">Target image.</param>
        /// <returns>Directive string.</returns>
        public static string Generate(Bitmap from, Bitmap to)
        {
            if (from == null || to == null)
            {
                throw new ArgumentNullException("Bitmaps may not be null.");
            }

            if (from.Size != to.Size)
            {
                throw new ArgumentException(string.Format("Bitmap sizes must match.\n" +
                    "Source: {0}" +
                    "Target: {1}",
                    from.Size,
                    to.Size));
            }

            var size = from.Size;

            Dictionary<Color, Color> conversions = new Dictionary<Color, Color>();

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    Color colorFrom = from.GetPixel(x, y),
                          colorTo = to.GetPixel(x, y);

                    if (colorFrom.Equals(TRANSPARENT)) continue;
                    if (colorFrom.Equals(colorTo)) continue;
                    if (colorTo.A == 0) continue;

                    conversions[colorFrom] = colorTo;
                }
            }

            string directives = CreateDirectives(conversions);
            return directives;
        }

        /// <summary>
        /// Converts a Color to a hexidecimal color code, formatted RRGGBBAA.
        /// </summary>
        /// <param name="c">Color to convert.</param>
        /// <returns>Hexadecimal RRGGBBAA color code.</returns>
        public static string ColorToString(Color c)
        {
            string r = c.R.ToString("X2"),
                    g = c.G.ToString("X2"),
                    b = c.B.ToString("X2"),
                    a = c.A.ToString("X2");

            return (r + g + b + a).ToLower();
        }

        /// <summary>
        /// Creates a replace directives string that converts all color keys to their respective color values.
        /// The order of directives can not be guaranteed, and may not match the order in which entries were added to the dictionary.
        /// </summary>
        /// <param name="conversions">Table containing all color conversions. Keys are converted to their value counterpart.</param>
        /// <returns>Replace directives string.</returns>
        public static string CreateDirectives(Dictionary<Color, Color> conversions)
        {
            StringBuilder directives = new StringBuilder("?replace");
            foreach (KeyValuePair<Color, Color> conversion in conversions)
            {
                directives.AppendFormat(";{0}={1}", ColorToString(conversion.Key), ColorToString(conversion.Value));
            }
            return directives.ToString();
        }
    }
}
