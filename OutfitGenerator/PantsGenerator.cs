using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OutfitGenerator
{
    class PantsGenerator
    {
        private static ConsoleWriter writer;
        private static Size newSize = new Size(387, 258);
        private static Size oldSize = new Size(387, 301);

        class GenerationOptions
        {
            public bool? HideBody { get; set; }
        }

        public static void Generate(string[] args)
        {
            writer = new ConsoleWriter(ConsoleColor.Cyan);
            writer.WriteLine("= Pants Generator");
            Console.WriteLine();

            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Program.WaitAndExit("Please drag and drop a valid image on the application (not this window).");
            }

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
            catch (Exception exc)
            {
                Program.WaitAndExit(exc.Message);
                return;
            }

            if (!Generator.ValidSheet(target, newSize, oldSize))
            {
                Program.WaitAndExit("Sheet dimensions must equal 387x258 or 387x301, to match the pants template.");
            }

            target = Generator.CropPants(target);

            // Check/Prompt args
            GenerationOptions options = ParseArgs(args);
            
            // Get template
            string txtTemplate = options.HideBody.Value ? Properties.Resources.invisiblePantsTemplate : Properties.Resources.pantsTemplate;
            
            // Set image template
            if (options.HideBody.Value)
            {
                Generator.Template = options.HideBody.Value ? Properties.Resources.invisibleAnimatedPantsTemplate : Properties.Resources.animatedPantsTemplate;
            }

            // Generating code
            Console.WriteLine("Starting generation. This shouldn't take long.");

            string item;
            try
            {
                item = Generator.Generate(target, Generator.Template, txtTemplate);
            }
            catch (Exception exc)
            {
                Program.WaitAndExit(exc.Message);
                return;
            }

            DirectoryInfo directory = (new FileInfo(args[0])).Directory;

            // Save to disk
            string generatedFileName = Generator.Save(directory, item);
            string generatedFilePath = directory + "\\" + generatedFileName;
            Console.WriteLine("Saved generated pants to {0}!", generatedFilePath);

            // Copy to clipboard
            Clipboard.SetText(item);
            Console.WriteLine("Copied command to clipboard!");

            Program.WaitAndExit("Done!");
        }
        
        static GenerationOptions ParseArgs(string[] args)
        {
            GenerationOptions options = new GenerationOptions();

            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg.StartsWith("-"))
                    arg = arg.Substring(1);
                else
                    continue;

                switch (arg.ToLower())
                {
                    case "hidebody":
                    case "hb":
                        options.HideBody = true;
                        
                        break;
                    case "showbody":
                    case "sb":
                        options.HideBody = false;
                        break;
                }
            }

            // Hide body
            if (!options.HideBody.HasValue)
            {
                options.HideBody = PromptHideBody();
            }


            Console.WriteLine(options.HideBody.Value ? "Hide body: True." : "Hide body: False.");
            Console.WriteLine();
            return options;
        }

        static bool PromptHideBody()
        {
            while (true)
            {
                Console.WriteLine("Would you like to hide your character's body?");
                writer.WriteLine("[1] Show. [2] Hide.");

                var cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        return false;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return true;
                }
            }
        }
    }
}
