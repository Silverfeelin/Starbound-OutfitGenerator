using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PantsAndSleevesGenerator
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
                item = PantsGenerator.Generator.Generate(target);
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

            // Sleeves
            Console.WriteLine("Would you also like some sleeves with that?\nY/N");
            ConsoleKeyInfo cki = Console.ReadKey();
            Console.WriteLine();
            if (cki.Key == ConsoleKey.Y)
            {
                string lightColor, darkColor;
                Console.WriteLine("Enter outer (dark) sleeve color:");
                darkColor = Console.ReadLine();
                Console.WriteLine("Enter inner (light) sleeve color:");
                lightColor = Console.ReadLine();

                string sleeves = Properties.Resources.template;
                sleeves = sleeves.Replace("{light}", lightColor).Replace("{dark}", darkColor);

                string sleevesFilePath = directory.FullName + "\\" + generatedFileName.Replace("Pants", "Sleeves");
                File.WriteAllText(sleevesFilePath, sleeves);
                Console.WriteLine("Saved generated sleeves to {0}!", sleevesFilePath);
                Clipboard.SetText(sleeves);
                Console.WriteLine("Copied command to clipboard!");
            }

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
