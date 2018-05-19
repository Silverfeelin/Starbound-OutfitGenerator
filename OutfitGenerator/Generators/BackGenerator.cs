using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class BackGenerator : ClothingGenerator
    {
        public override string FileName => "back";

        private ISet<Size> _supportedDimensions = new HashSet<Size>()
        {
            new Size(387,301)
        };
        
        public override ISet<Size> SupportedDimensions => _supportedDimensions;
        
        public override Bitmap Template => Properties.Resources.animatedBackTemplate;

        public override byte[] Config => Properties.Resources.BackConfig;
    }
}
