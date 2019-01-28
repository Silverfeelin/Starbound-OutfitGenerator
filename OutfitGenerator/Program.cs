using OutfitGenerator.Generators;
using OutfitGenerator.Mergers;
using OutfitGenerator.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Linq;

namespace OutfitGenerator
{
    class Program
    {
        // Used to write to console.
        static ConsoleWriter writer = new ConsoleWriter();

        static void Main(string[] args)
        {
            writer.WriteColorfulLine("$h=$l Outfit Generator");

            // Validate args
            if (!ValidateArgs(args))
            {
                WaitAndExit();
                return;
            }

            // Read images
            Image<Rgba32>[] images;
            try
            {
                images = ValidateImages(args);
            }
            catch (Exception exc)
            {
                writer.WriteLine(ConsoleColor.Red, "Invalid image file. Details:");
                writer.WriteLine(ConsoleColor.Red, exc.ToString());
                WaitAndExit();
                return;
            }

            // Select generator/merger
            if (args.Length == 1)
            {
                var generator = SelectGenerator();
                if (generator == null) return;
                GenerateClothing(generator, images[0]);
            }
            else if (args.Length == 2)
            {
                var merger = SelectMerger();
                if (merger == null) return;
                MergeSprites(merger, images[0], images[1]);
            }
        }

        /* Returns a value indicating whether the arg count is correct and all args are existing files */
        static bool ValidateArgs(string[] args)
        {
            if (args.Length == 0 && args.Length > 0)
            {
                writer.WriteLine(ConsoleColor.Red, "Invalid argument count.");
                writer.WriteLine(ConsoleColor.Red, "arg #1: Path to image to create an outfit.");
                writer.WriteLine(ConsoleColor.Red, "arg #2: Path to images to merge together.");
                return false;
            }

            bool existsA, existsB = true;
            existsA = File.Exists(args[0]);
            if (args.Length > 1) existsB = File.Exists(args[1]);
            if (!existsA || !existsB)
            {
                writer.WriteLine(ConsoleColor.Red, "Invalid arguments.");
                if (!existsA) writer.WriteLine(ConsoleColor.Red, $"File {args[0]} does not exist.");
                if (!existsB) writer.WriteLine(ConsoleColor.Red, $"File {args[1]} does not exist.");
                return false;
            }

            return true;
        }

        /* Returns an array of images from the given file paths */
        static Image<Rgba32>[] ValidateImages(string[] imagePaths)
        {
            var images = new Image<Rgba32>[imagePaths.Length];
            for (int i = 0; i < imagePaths.Length; i++) images[i] = Image.Load<Rgba32>(imagePaths[i]);
            return images;
        }

        static IClothingGenerator SelectGenerator()
        {
            writer.WriteColorfulLine("Select a generator. $l[0]$0 to exit.");
            var type = typeof(IClothingGenerator);
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(type) && t.IsClass && !t.IsAbstract);
            var generators = types.Select(t => (IClothingGenerator)Activator.CreateInstance(t)).OrderBy(g => g.Priority).ToArray();

            // List options
            for (int i = 0; i < generators.Count(); i++)
            {
                var generator = generators[i];
                writer.WriteColorfulLine($"$l[{i + 1}] $0{generator.Name}");
            }

            // Select option
            // Only works up to 10 choices (0-9)!
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape) return null;
                var c = cki.KeyChar;
                if (!char.IsNumber(c))
                {
                    writer.WriteLine(ConsoleColor.Red, "Not a number. Try again.");
                }
                else
                {
                    var i = c - '0';
                    if (i == 0) return null;
                    i--;

                    if (i < generators.Length)
                        return generators[i];
                    else
                        writer.WriteLine(ConsoleColor.Red, "Number out of range. Try again.");
                }
            }
        }

        static void GenerateClothing(IClothingGenerator generator, Image<Rgba32> image)
        {
            ItemDescriptor item = generator.Generate(image);
            string s = CommandGenerator.SpawnItem(item);
            var path = FileSaver.Save(Directory.GetCurrentDirectory(), s);
            writer.WriteColorfulLine($"$kSaved command to:\n$h{path}$k");
        }

        static ISpriteMerger SelectMerger()
        {
            writer.WriteColorfulLine("Select a sprite merger. $l[0]$0 to exit.");
            var type = typeof(ISpriteMerger);
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(type) && t.IsClass && !t.IsAbstract);
            var mergers = types.Select(t => (ISpriteMerger)Activator.CreateInstance(t)).OrderBy(g => g.Priority).ToArray();

            // List options
            for (int i = 0; i < mergers.Count(); i++)
            {
                var merger = mergers[i];
                writer.WriteColorfulLine($"$l[{i + 1}] $0{merger.Name}");
            }

            // Select option
            // Only works up to 10 choices (0-9)!
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape) return null;
                var c = cki.KeyChar;
                if (!char.IsNumber(c))
                {
                    writer.WriteLine(ConsoleColor.Red, "Not a number. Try again.");
                }
                else
                {
                    var i = c - '0';
                    if (i == 0) return null;
                    i--;

                    if (i < mergers.Length)
                        return mergers[i];
                    else
                        writer.WriteLine(ConsoleColor.Red, "Number out of range. Try again.");
                }
            }
        }
        
        static void MergeSprites(ISpriteMerger merger, Image<Rgba32> first, Image<Rgba32> second)
        {
            Image<Rgba32> merged = merger.Merge(first, second);
            var path = FileSaver.SaveImage(Directory.GetCurrentDirectory(), merged);
            writer.WriteColorfulLine($"$kSaved image to:\n$h{path}$k");
        }

        /* Writes an optional message, then waits for any keystroke before existing. */
        public static void WaitAndExit(string message = null, params object[] args)
        {
            if (!string.IsNullOrEmpty(message)) Console.WriteLine(message, args);
            writer.WriteColorfulLine("$hPress any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
