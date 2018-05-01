using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackGenerator
{
    class BackGenerator
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Parameter checking
            if (args.Length != 1)
                WaitAndExit("Improper usage! Expected parameter: <image_path>\n" +
                            "Try dragging your image file directly on top of the application!");

            // Image checking
            Bitmap target = null;

            try
            {
                target = new Bitmap(args[0]);
            }
            catch (ArgumentException)
            {
                WaitAndExit("The file \"{0}\" is not a valid image or does not exist.", args[0]);
                return;
            }

            // Begin
            Console.WriteLine("Starting generation.");
            Console.WriteLine("");

            string item;
            try
            {
                if (target == null)
                    throw new ArgumentNullException("Sheet may not be null.");

                if (target.Width != 387 || target.Height != 301)
                    throw new PantsGenerator.GeneratorException("Sheet dimensions must equal 387x301, to match the back template.");

                item = PantsGenerator.Generator.Generate(target, Properties.Resources.animatedBackTemplate, Properties.Resources.template);
            }
            catch (Exception exc)
            {
                WaitAndExit(exc.Message);
                return;
            }

            DirectoryInfo directory = (new FileInfo(args[0])).Directory;

            // Save to disk
            string generatedFileName = PantsGenerator.Generator.Save(directory, item, "generatedBack");
            string generatedFilePath = directory + "\\" + generatedFileName;
            Console.WriteLine("Saved generated back item to {0}!", generatedFilePath);

            // Copy to clipboard
            Clipboard.SetText(item);
            Console.WriteLine("Copied command to clipboard!");
            Console.WriteLine("");

            WaitAndExit("Done!");
        }

        public static void WaitAndExit(string message = null, params object[] args)
        {
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message, args);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
