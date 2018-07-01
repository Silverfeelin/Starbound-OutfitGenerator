using System;
using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class SleeveGenerator : ClothingGenerator
    {
        public override string FileName => "sleeves";

        private ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(387,602)
        };

        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Bitmap Template => Properties.Resources.animatedSleevesTemplate;

        public override byte[] Config => Properties.Resources.SleevesConfig;
        
        public override ItemDescriptor Generate(Bitmap bitmap)
        {
            // Warn of memory leak (https://github.com/Silverfeelin/Starbound-OutfitGenerator/issues/1)
            Console.WriteLine("== WARNING ==");
            Console.WriteLine("Please note that the 64-bit build of Starbound (1.3.1) has a memory leak, which generated sleeves suffer from.");
            Console.WriteLine("Consider using the 32-bit build of Starbound instead.");
            Console.WriteLine("If anyone else suffers from this problem, please recommend this temporary solution.");
            Console.WriteLine("=============");

            return base.Generate(bitmap);
        }
    }
}
