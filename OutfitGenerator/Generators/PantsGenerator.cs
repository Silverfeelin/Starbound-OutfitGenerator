using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class PantsGenerator : ClothingGenerator
    {
        public override string FileName => "pants";

        private readonly ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(387, 258),
            new Size(387, 301)
        };
        
        public override ISet<Size> SupportedDimensions => _supportedDimensions;

        public override Bitmap Template => Properties.Resources.animatedPantsTemplate;

        public override byte[] Config => Properties.Resources.PantsConfig;

        public override ItemDescriptor Generate(Bitmap bitmap)
        {
            if (bitmap?.Height == 301)
            {
                bitmap = Crop(bitmap, 0, 0, bitmap.Width, 258);
            }
            
            return base.Generate(bitmap);
        }
    }
}
