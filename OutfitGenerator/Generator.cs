using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace OutfitGenerator
{
    public static class Generator
    {
        /// <summary>
        /// Returns the template, used for generating animated pants.
        /// </summary>
        public static Bitmap Template { get; set; } = Properties.Resources.animatedPantsTemplate;

        /// <summary>
        /// Generates a spawnitem command for the generated pants.
        /// TODO: JSON Object (item descriptor)?
        /// </summary>
        /// <param name="sheet">Bitmap containing the custom pants frames. Dimensions of the bitmap should equal 387x258 or 387x301.</param>
        /// <returns>Spawnitem command for the custom pants.</returns>
        /// <exception cref="ArgumentNullException">Thrown if null is passed.</exception>
        /// <exception cref="GeneratorException">Thrown if the dimensions of the given sheet are not valid.</exception>
        public static string Generate(Bitmap sheet, Bitmap bitmapTemplate, string itemTemplate)
        {
            Bitmap template = bitmapTemplate;
            Dictionary<Color, Color> conversions = new Dictionary<Color, Color>();

            Color transparent = Color.FromArgb(0, 255, 255, 255);

            for (int y = sheet.Height - 1; y >= 0; y--)
            {
                for (int x = sheet.Width - 1; x >= 0; x--)
                {
                    Color cFrom = template.GetPixel(x, y),
                        cTo = sheet.GetPixel(x, y);

                    if (cFrom != transparent && !cFrom.Equals(cTo) && cTo.A != 0)
                        conversions[cFrom] = cTo;
                }
            }

            // Create item
            string directives = CreateDirectives(conversions);
            string item = itemTemplate.Replace("{directives}", directives);
            return item;
        }

        /// <summary>
        /// Returns a value indicating whether the sheet is valid to generate drawables for.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="acceptedSizes"></param>
        /// <returns></returns>
        public static bool ValidSheet(Bitmap sheet, params Size[] acceptedSizes)
        {
            if (sheet == null)
                return false;

            foreach (Size size in acceptedSizes)
            {
                if (sheet.Size == size)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Crops out the bottom (unused) row for larger pants sprites.
        /// It's not really necessary, but whatever.
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static Bitmap CropPants(Bitmap sheet)
        {
            return sheet.Size.Height == 301 ? sheet.Clone(new Rectangle(0, 0, 387, 258), sheet.PixelFormat) : sheet;
        }

        /// <summary>
        /// Saves the given content to the given directory, using a generated file name.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="content"></param>
        /// <returns>Generated file name</returns>
        public static string Save(DirectoryInfo directory, string content, string fileNamePrefix = "generatedPants")
        {
            string file = string.Format("{0}-{1}.txt", fileNamePrefix, DateTime.Now.ToString("h-mm-ss"));
            File.WriteAllText(directory.FullName + "\\" + file, content);
            return file;
        }

        /// <summary>
        /// Creates a replace directives string that converts all color keys to their respective color values.
        /// The order of directives can not be guaranteed, and may not match the order in which entries were added to the dictionary.
        /// </summary>
        /// <param name="conversions">Table containing all color conversions. Keys are converted to their value counterpart.</param>
        /// <returns>Replace directives string.</returns>
        public static string CreateDirectives(Dictionary<Color, Color> conversions)
        {
            string directives = "?replace";
            foreach (KeyValuePair<Color, Color> conversion in conversions)
            {
                directives += string.Format(";{0}={1}", ColorToString(conversion.Key), ColorToString(conversion.Value));
            }
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
    }
}
