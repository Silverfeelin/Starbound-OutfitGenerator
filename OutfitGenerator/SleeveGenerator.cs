using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OutfitGenerator
{
    class SleeveGenerator
    {
        public static void Generate(string[] args)
        {
            // Parameter checking
            if (args.Count() != 1)
                Program.WaitAndExit("Improper usage! Expected parameter: <image_path>\n" +
                            "Try dragging your image file directly on top of the application!");

            // Image checking
            Bitmap target = null;

            try
            {
                target = new Bitmap(args[0]);
            }
            catch (ArgumentException)
            {
                Program.WaitAndExit("The file \"{0}\" is not a valid image or does not exist.", args[0]);
                return;
            }
            
            // Begin
            Console.WriteLine("Starting generation.");
            Console.WriteLine("");

            // Warn of memory leak (https://github.com/Silverfeelin/Starbound-OutfitGenerator/issues/1)
            Console.WriteLine("== WARNING ==");
            Console.WriteLine("Please note that the 64-bit build of Starbound (1.3.1) has a memory leak, which generated sleeves suffer from.");
            Console.WriteLine("Consider using the 32-bit build of Starbound instead.");
            Console.WriteLine("If anyone else suffers from this problem, please recommend this temporary solution.");
            Console.WriteLine("=============");
            Console.WriteLine("");

            string item;
            try
            {
                if (target == null)
                    throw new ArgumentNullException("Sheet may not be null.");

                if (target.Width != 387 || target.Height != 602)
                    throw new GeneratorException("Sheet dimensions must equal 387x602, to match the sleeve template.");
                
                item = Generator.Generate(target, Properties.Resources.animatedSleevesTemplate, Properties.Resources.sleeveTemplate);
            }
            catch (Exception exc)
            {
                Program.WaitAndExit(exc.Message);
                return;
            }
            
            DirectoryInfo directory = (new FileInfo(args[0])).Directory;

            // Save to disk
            string generatedFileName = Generator.Save(directory, item, "generatedSleeves");
            string generatedFilePath = directory + "\\" + generatedFileName;
            Console.WriteLine("Saved generated sleeves to {0}!", generatedFilePath);

            // Copy to clipboard
            Clipboard.SetText(item);
            Console.WriteLine("Copied command to clipboard!");
            Console.WriteLine("");

            Program.WaitAndExit("Done!");
        }
    }
}
