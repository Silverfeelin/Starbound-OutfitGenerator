using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class HidingPantsGenerator : PantsGenerator
    {
        public override Bitmap Template => Properties.Resources.invisibleAnimatedPantsTemplate;

        public override byte[] Config => Properties.Resources.HidingPantsConfig;
    }
}
