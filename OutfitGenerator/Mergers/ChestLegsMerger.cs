using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace OutfitGenerator.Mergers
{
    public class ChestLegsMerger : ISpriteMerger
    {
        public string Name => "Merge Chest & Legs";
        public int Priority => 10;

        private const int FRAME_SIZE = 43;
        private const int PANTS_WIDTH = 387;
        private const int PANTS_HEIGHT = 258;
        private const int PANTS_OLD_HEIGHT = 301;
        private const int CHEST_WIDTH = 86;
        private const int CHEST_HEIGHT = 258;

        private static Size chestSize = new Size(CHEST_WIDTH, CHEST_HEIGHT);
        private static Size pantsSize = new Size(PANTS_WIDTH, PANTS_HEIGHT);
        private static Size pantsOldSize = new Size(PANTS_WIDTH, PANTS_OLD_HEIGHT);
        
        public Image<Rgba32> Merge(Image<Rgba32> chest, Image<Rgba32> pants)
        {
            return ApplyMultingChestPants(chest, pants);
        }

        /* This is Degranon's hocus pocus. */
        private static Image<Rgba32> ApplyMultingChestPants(Image<Rgba32> chest, Image<Rgba32> pants)
        {
            Image<Rgba32> result = pants.Clone();

            Image<Rgba32> chestIdle = chest.Clone(ctx => ctx.Crop(new Rectangle(FRAME_SIZE, 0, FRAME_SIZE, FRAME_SIZE)));
            Image<Rgba32> chestIdle2 = chest.Clone(ctx => ctx.Crop(new Rectangle(0, FRAME_SIZE, FRAME_SIZE, FRAME_SIZE)));
            Image<Rgba32> chestIdle3 = chest.Clone(ctx => ctx.Crop(new Rectangle(FRAME_SIZE, FRAME_SIZE, FRAME_SIZE, FRAME_SIZE)));

            Image<Rgba32> chestRun = chest.Clone(ctx => ctx.Crop(new Rectangle(FRAME_SIZE, FRAME_SIZE * 2, FRAME_SIZE, FRAME_SIZE)));

            Image<Rgba32> chestDuck = chest.Clone(ctx => ctx.Crop(new Rectangle(FRAME_SIZE, FRAME_SIZE * 3, FRAME_SIZE, FRAME_SIZE)));

            Image<Rgba32> chestClimb = chest.Clone(ctx => ctx.Crop(new Rectangle(FRAME_SIZE, FRAME_SIZE * 4, FRAME_SIZE, FRAME_SIZE)));
            Image<Rgba32> chestSwim = chest.Clone(ctx => ctx.Crop(new Rectangle(FRAME_SIZE, FRAME_SIZE * 5, FRAME_SIZE, FRAME_SIZE)));
            
            // Personality 1,5
            SuperImpose(result, chestIdle, FRAME_SIZE, 0);
            SuperImpose(result, chestIdle, FRAME_SIZE * 5, 0);

            // Personality 2,4
            SuperImpose(result, chestIdle2, FRAME_SIZE * 2, 0);
            SuperImpose(result, chestIdle2, FRAME_SIZE * 4, 0);

            // Personality 3
            SuperImpose(result, chestIdle3, FRAME_SIZE * 3, 0);

            // Duck
            SuperImpose(result, chestDuck, 344, 0);

            // Sit
            SuperImpose(result, chestIdle, 258, 1);

            // Walking
            SuperImpose(result, chestIdle, FRAME_SIZE, FRAME_SIZE + 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 2, FRAME_SIZE + 2);
            SuperImpose(result, chestIdle, FRAME_SIZE * 3, FRAME_SIZE + 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 4, FRAME_SIZE);
            SuperImpose(result, chestIdle, FRAME_SIZE * 5, FRAME_SIZE + 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 6, FRAME_SIZE + 2);
            SuperImpose(result, chestIdle, FRAME_SIZE * 7, FRAME_SIZE + 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 8, FRAME_SIZE);

            // Running
            SuperImpose(result, chestRun, FRAME_SIZE, FRAME_SIZE * 2);
            SuperImpose(result, chestRun, FRAME_SIZE * 2, FRAME_SIZE * 2 - 1);
            SuperImpose(result, chestRun, FRAME_SIZE * 3, FRAME_SIZE * 2);
            SuperImpose(result, chestRun, FRAME_SIZE * 4, FRAME_SIZE * 2 + 1);
            SuperImpose(result, chestRun, FRAME_SIZE * 5, FRAME_SIZE * 2);
            SuperImpose(result, chestRun, FRAME_SIZE * 6, FRAME_SIZE * 2 - 1);
            SuperImpose(result, chestRun, FRAME_SIZE * 7, FRAME_SIZE * 2);
            SuperImpose(result, chestRun, FRAME_SIZE * 8, FRAME_SIZE * 2 + 1);

            // Jumping
            SuperImpose(result, chestIdle, FRAME_SIZE, FRAME_SIZE * 3 - 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 2, FRAME_SIZE * 3 - 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 3, FRAME_SIZE * 3 - 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 4, FRAME_SIZE * 3 - 1);

            // Falling
            SuperImpose(result, chestIdle, FRAME_SIZE * 5, FRAME_SIZE * 3 - 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 6, FRAME_SIZE * 3 - 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 7, FRAME_SIZE * 3 - 1);
            SuperImpose(result, chestIdle, FRAME_SIZE * 8, FRAME_SIZE * 3 - 1);

            // Climbing
            SuperImpose(result, chestClimb, FRAME_SIZE, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 2, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 3, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 4, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 5, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 6, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 7, FRAME_SIZE * 4);
            SuperImpose(result, chestClimb, FRAME_SIZE * 8, FRAME_SIZE * 4);

            // Swimming
            SuperImpose(result, chestSwim, FRAME_SIZE, FRAME_SIZE * 5);
            SuperImpose(result, chestSwim, FRAME_SIZE * 4, FRAME_SIZE * 5);
            SuperImpose(result, chestSwim, FRAME_SIZE * 5, FRAME_SIZE * 5 + 1);
            SuperImpose(result, chestSwim, FRAME_SIZE * 6, FRAME_SIZE * 5 + 2);
            SuperImpose(result, chestSwim, FRAME_SIZE * 7, FRAME_SIZE * 5 + 1);

            return result;
        }

        private static void SuperImpose(Image<Rgba32> largeBmp, Image<Rgba32> smallBmp, int x, int y)
        {
            largeBmp.Mutate(ctx => ctx.DrawImage(smallBmp, new Point(x, y), 1));
        }
    }
}
