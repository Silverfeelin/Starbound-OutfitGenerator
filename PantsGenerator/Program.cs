using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PantsGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Parameter checking
            if (args.Count() != 1)
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

            // Generating code
            Console.WriteLine("Starting generation.");

            string item;
            try
            {
                if (target == null)
                    throw new ArgumentNullException("Sheet may not be null.");

                if (!Generator.ValidSheet(target))
                    throw new GeneratorException("Sheet dimensions must equal 387x258 or 387x301, to match the pants template.");

                target = Generator.CropPants(target);

                item = Generator.Generate(target, Generator.Template, Properties.Resources.template);
            }
            catch (Exception exc)
            {
                WaitAndExit(exc.Message);
                return;
            }

            DirectoryInfo directory = (new FileInfo(args[0])).Directory;

            // Save to disk
            string generatedFileName = PantsGenerator.Generator.Save(directory, item);
            string generatedFilePath = directory + "\\" + generatedFileName;
            Console.WriteLine("Saved generated pants to {0}!", generatedFilePath);

            // Copy to clipboard
            Clipboard.SetText(item);
            Console.WriteLine("Copied command to clipboard!");

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
