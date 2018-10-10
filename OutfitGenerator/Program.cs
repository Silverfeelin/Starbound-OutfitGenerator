using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OutfitGenerator.Generators;
using OutfitGenerator.Mergers;
using OutfitGenerator.Util;

namespace OutfitGenerator
{
    class Program
    {
        private static ConsoleWriter writer;
        private static readonly Dictionary<Type, string> visualNames = new Dictionary<Type, string>()
        {
            { typeof(HatGenerator), "Hat" },
            { typeof(MaskedHatGenerator), "Hat (Hide hair)" },
            { typeof(HidingHatGenerator), "Hat (Hide body)" },
            { typeof(SleeveGenerator), "Sleeves" },
            { typeof(PantsGenerator), "Pants" },
            { typeof(HidingPantsGenerator), "Pants (Hide body)" },
            { typeof(BackGenerator), "Back Item" },

            { typeof(ChestPantsMerger), "Merge Chest & Pants" },
            { typeof(SleevesMerger), "Merge Sleeves" }
        };

        // TODO: Clean this method up a bit more.
        [STAThread]
        static void Main(string[] args)
        {
            writer = new ConsoleWriter(ConsoleColor.Cyan);
            writer.WriteLine("= Outfit Generator");
            Console.WriteLine();

            writer.WriteLine("Select the desired generator:");

            if (args.Length > 2)
            {
                WaitAndExit($"Invalid number of parameters: {args.Length}. Expected 1 for outfit generation or 2 for sprite merging.");
                return;
            }

            // Read images.
            Bitmap[] images = new Bitmap[args.Length];
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    images[i] = new Bitmap(args[i]);
                }
            }
            catch (Exception exc)
            {
                WaitAndExit("Couldn't read image file: {0}", exc.Message);
                return;
            }

            // Prompt generator/merge type.
            Type generatorType;
            switch (args.Length)
            {
                case 0:
                    WaitAndExit("Please drag and drop a valid image on the application (not this window).");
                    break;
                case 1:
                    // Prompt clothing to generate
                    generatorType = SelectGenerator(typeof(HatGenerator), typeof(MaskedHatGenerator), typeof(HidingHatGenerator), typeof(SleeveGenerator), typeof(PantsGenerator), typeof(HidingPantsGenerator), typeof(BackGenerator));

                    // Generate clothing
                    IClothingGenerator generator = (IClothingGenerator)Activator.CreateInstance(generatorType);
                    GenerateClothing(generator, images[0]);
                    break;
                case 2:
                    // Prompt sprites to merge
                    generatorType = SelectGenerator(typeof(ChestPantsMerger), typeof(SleevesMerger));

                    // Merge sprites
                    ISpriteMerger merger = (ISpriteMerger)Activator.CreateInstance(generatorType);
                    MergeSprites(merger, args[0], args[1]);
                    break;
            }
        }

        private static Type SelectGenerator(params Type[] typeNames)
        {
            for (int i = 0; i < typeNames.Length; i++)
            {
                string visualName = visualNames[typeNames[i]];
                writer.WriteLine($"[{i + 1}]: {visualName}");
            }

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (char.IsNumber(cki.KeyChar))
                {
                    int choice = (cki.KeyChar - '0');

                    if (--choice < typeNames.Length)
                        return typeNames[choice];
                }
            }
        }

        private static void GenerateClothing(IClothingGenerator generator, Bitmap bmp)
        {
            try
            {
                ItemDescriptor item = generator.Generate(bmp);
                string s = CommandGenerator.SpawnItem(item);
                string file = string.Format("{0} {1}.txt", generator.FileName, DateTime.Now.ToString("MM-dd h.mm.ss"));
                File.WriteAllText(file, s);
                Clipboard.SetText(s);
                WaitAndExit("Saved command to {0} and copied into clipboard", file);
            }
            catch (Exception exc)
            {
                WaitAndExit("Failed to create clothing: {0}", exc.Message);
            }
        }

        private static void MergeSprites(ISpriteMerger merger, string a, string b)
        {
            try
            {
                Bitmap merged = merger.Merge(a, b);
                string file = string.Format("merged {0}.png", DateTime.Now.ToString("MM-dd h.mm.ss"));
                merged.Save(file);
                Console.WriteLine("Saved image to {0}", file);

                //Propt to work with the saved image
                if (PromptWorking())
                {
                    // Prompt clothing to generate
                    Type generatorType = SelectGenerator(typeof(HatGenerator), typeof(MaskedHatGenerator), typeof(HidingHatGenerator), typeof(SleeveGenerator), typeof(PantsGenerator), typeof(HidingPantsGenerator), typeof(BackGenerator));

                    // Generate clothing
                    IClothingGenerator generator = (IClothingGenerator)Activator.CreateInstance(generatorType);
                    GenerateClothing(generator, merged);
                }
                else
                {
                    WaitAndExit("Saved image to {0}", file);
                }
            }
            catch (Exception exc)
            {
                WaitAndExit("Failed to merge sprites: {0}", exc.Message);
            }
        }

        private static bool PromptWorking()
        {
            writer.WriteLine("Would you like to proceed the result? y/n");
            string answer = Console.ReadLine();
            return answer.ToLower().Equals("y") || answer.ToLower().Equals("yes");
        }

        public static void WaitAndExit(string message = null, params object[] args)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message, args);
            }
            writer.WriteLine("Press any key to exit...");

            Console.ReadKey(true);

            Environment.Exit(0);
        }
    }
}
