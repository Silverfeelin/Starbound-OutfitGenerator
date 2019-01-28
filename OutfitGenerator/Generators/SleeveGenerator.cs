using Newtonsoft.Json.Linq;
using OutfitGenerator.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;

namespace OutfitGenerator.Generators
{
    public class SleeveGenerator : ClothingGenerator
    {
        public override string Name => "Sleeves";
        public override int Priority => 40;
        public override string FileName => "sleeves";

        private readonly ISet<Size> _supportedDimensions = new HashSet<Size>() { new Size(387,602) };
        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Image<Rgba32> Template => ImageResourceManager.GetImage("animatedSleevesTemplate.png");
        public override JObject Config => JsonResourceManager.GetJsonObject("SleevesConfig.json");
        
        public override ItemDescriptor Generate(Image<Rgba32> bitmap)
        {
            // Warn of memory leak (https://github.com/Silverfeelin/Starbound-OutfitGenerator/issues/1)
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("== WARNING ==");
            Console.WriteLine("Please note that the 64-bit build of Starbound (1.3.1) has a memory leak, which these sleeves suffer from.");
            Console.WriteLine("Consider using the 32-bit build of Starbound instead.");
            Console.WriteLine("If anyone else suffers from this problem, please recommend this (temporary) solution.");
            Console.WriteLine("=============");
            Console.ResetColor();

            return base.Generate(bitmap);
        }
    }
}
