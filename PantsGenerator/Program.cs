using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PantsGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Parameter checkng
            if (args.Count() != 1)
                WaitAndExit("Improper usage! Expected parameter: <image_path>\n" +
                            "Try dragging your image file directly on top of the application!");

            // Image checking
            Bitmap b1 = Properties.Resources.animatedPantsTemplate, b2 = null;
            
            try
            {
                b2 = new Bitmap(args[0]);
            }
            catch (ArgumentException)
            {
                WaitAndExit("The file \"{0}\" is not a valid image or does not exist.", args[0]);
            }
            
            if (b2.Size != new Size(387, 258) && b2.Size != new Size(387, 301))
                WaitAndExit("Image resolutions do not match (should be 387x258 or 387x301). This make generating quite difficult!");

            // Remove empty bottom row (some base assets have an empty row for reasons, but we don't use it).
            if (b2.Size.Height == 301)
                b2 = b2.Clone(new Rectangle(0, 0, 387, 258), b2.PixelFormat);

            // Comparing images
            Console.WriteLine("Starting generation.");
            Dictionary<Color, Color> conversions = new Dictionary<Color, Color>();
            Color transparent = Color.FromArgb(0, 255, 255, 255);
            
            for (int y = b1.Height - 1; y >= 0; y--)
            {
                for (int x = b1.Width - 1; x >= 0; x--)
                {
                    Color cFrom = b1.GetPixel(x, y),
                        cTo = b2.GetPixel(x, y);

                    if (cFrom != transparent && !cFrom.Equals(cTo) && cTo.A != 0)
                        conversions[cFrom] = cTo;
                }
            }

            // Create item
            string directives = CreateDirectives(conversions);
            string item = Properties.Resources.template.Replace("{directives}", directives);

            // Save and copy
            string file = string.Format("generatedPants-{0}.txt", DateTime.Now.ToString("h-mm-ss"));

            Console.WriteLine("Copied command to clipboard!");
            Clipboard.SetText(item);

            Console.WriteLine("Saving command to: {0}", file);
            File.WriteAllText(file, item);

            WaitAndExit("Done!");
        }

        static void WaitAndExit(string message = null, params object[] args)
        {
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message, args);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static string ColorToString(Color c)
        {
            string  r = c.R.ToString("X2"),
                    g = c.G.ToString("X2"),
                    b = c.B.ToString("X2"),
                    a = c.A.ToString("X2");

            return (r + g + b + a).ToLower();
        }

        static string CreateDirectives(Dictionary<Color, Color> conversions)
        {
            string directives = "?replace";
            foreach (KeyValuePair<Color, Color> conversion in conversions)
            {
                directives += string.Format(";{0}={1}", ColorToString(conversion.Key), ColorToString(conversion.Value));
            }
            return directives;
        }
    }
}
