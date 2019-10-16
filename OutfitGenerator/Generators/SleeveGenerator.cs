using System;
using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class SleeveGenerator : ClothingGenerator
    {
        public override string FileName => "sleeves";

        private readonly ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(387,602)
        };

        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Bitmap Template => Properties.Resources.animatedSleevesTemplate;

        public override byte[] Config => Properties.Resources.SleevesConfig;
        
        public override ItemDescriptor Generate(Bitmap bitmap)
        {
            return base.Generate(bitmap);
        }
    }
}
