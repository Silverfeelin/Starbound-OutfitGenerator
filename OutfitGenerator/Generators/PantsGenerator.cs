using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class PantsGenerator : ClothingGenerator
    {
        public override string FileName => "pants";

        private ISet<Size> _supportedDimensions = new HashSet<Size>()
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
                bitmap = Crop(bitmap);
            }
            
            return base.Generate(bitmap);
        }
        
        /// <summary>
        /// Crops out the bottom (unused) row for larger pants sprites.
        /// It's not really necessary, but whatever.
        /// </summary>
        public static Bitmap Crop(Bitmap bmp)
        {
            if (bmp.Height == 301)
                return bmp.Clone(new Rectangle(0, 0, bmp.Width, 258), bmp.PixelFormat);
            else
                return bmp;
        }
    }
}
