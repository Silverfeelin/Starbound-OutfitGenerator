using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutfitGenerator.Mergers
{
    public class SleevesMerger : ISpriteMerger
    {
        public string Name => "Merge Sleeves";
        public int Priority => 20;

        private const int SLEEVES_WIDTH = 387;
        private const int SLEEVES_HEIGHT = 301;
        private static Size sleevesSize = new Size(SLEEVES_WIDTH, SLEEVES_HEIGHT);
        
        public Image<Rgba32> Merge(Image<Rgba32> frontSleeves, Image<Rgba32> backSleeves)
        {
            return ApplyMultingSleeves(frontSleeves, backSleeves);
        }

        private static Image<Rgba32> ApplyMultingSleeves(Image<Rgba32> frontSleeves, Image<Rgba32> backSleeves)
        {
            Image<Rgba32> result = new Image<Rgba32>(SLEEVES_WIDTH, SLEEVES_HEIGHT * 2);
            Superimpose(result, frontSleeves, 0, 0);
            Superimpose(result, backSleeves, 0, SLEEVES_HEIGHT);

            return result;
        }

        private static void Superimpose(Image<Rgba32> largeBmp, Image<Rgba32> smallBmp, int x, int y)
        {
            largeBmp.Mutate(ctx => ctx.DrawImage(smallBmp, new Point(x, y), 1));
        }
    }
}
