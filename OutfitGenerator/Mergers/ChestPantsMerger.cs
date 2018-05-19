using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OutfitGenerator.Mergers
{
    public class ChestPantsMerger : ISpriteMerger
    {
        private const int FRAME_SIZE = 43;
        private const int PANTS_WIDTH = 387;
        private const int PANTS_HEIGHT = 258;
        private const int PANTS_OLD_HEIGHT = 301;
        private const int CHEST_WIDTH = 86;
        private const int CHEST_HEIGHT = 258;

        private static Size chestSize = new Size(CHEST_WIDTH, CHEST_HEIGHT);
        private static Size pantsSize = new Size(PANTS_WIDTH, PANTS_HEIGHT);
        private static Size pantsOldSize = new Size(PANTS_WIDTH, PANTS_OLD_HEIGHT);
        
        public Bitmap Merge(string firstPath, string secondPath)
        {
            Bitmap chestBitmap;
            Bitmap pantsBitmap;

            Console.WriteLine("Is the order correct?");
            Console.WriteLine("Chest image: " + firstPath);
            Console.WriteLine("Pants image: " + secondPath);
            Console.WriteLine("Press Enter if it is, press any key otherwise");

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                chestBitmap = new Bitmap(firstPath);
                pantsBitmap = new Bitmap(secondPath);
            }
            else
            {
                chestBitmap = new Bitmap(secondPath);
                pantsBitmap = new Bitmap(firstPath);
            }
            /*
            if (!Generator.ValidSheet(chestBitmap, chestSize) || !Generator.ValidSheet(pantsBitmap, pantsSize, pantsOldSize))
            {
                Program.WaitAndExit("Incorrect size!\nExpected chest dimensions of {0}x{1} and pants dimensions of {2}x{3} or {4}x{5}.", 
                    chestSize.Width, chestSize.Height,
                    pantsSize.Width, pantsSize.Height,
                    pantsOldSize.Width, pantsOldSize.Height);
                return null;
            }
            */
            return ApplyMultingChestPants(chestBitmap, pantsBitmap);
        }

        private static Bitmap ApplyMultingChestPants(Bitmap chest, Bitmap pants)
        {
            Bitmap result = new Bitmap(pants);

            Bitmap chestIdle = chest.Clone(new Rectangle(FRAME_SIZE, 0, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);
            Bitmap chestIdle2 = chest.Clone(new Rectangle(0, FRAME_SIZE, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);
            Bitmap chestIdle3 = chest.Clone(new Rectangle(FRAME_SIZE, FRAME_SIZE, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);

            Bitmap chestRun = chest.Clone(new Rectangle(FRAME_SIZE, FRAME_SIZE * 2, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);

            Bitmap chestDuck = chest.Clone(new Rectangle(FRAME_SIZE, FRAME_SIZE * 3, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);

            Bitmap chestClimb = chest.Clone(new Rectangle(FRAME_SIZE, FRAME_SIZE * 4, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);
            Bitmap chestSwim = chest.Clone(new Rectangle(FRAME_SIZE, FRAME_SIZE * 5, FRAME_SIZE, FRAME_SIZE), chest.PixelFormat);

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

        private static void SuperImpose( Bitmap largeBmp, Bitmap smallBmp, int x, int y)
        {
            Graphics g = Graphics.FromImage(largeBmp);
            g.DrawImage(smallBmp, x, y, smallBmp.Width, smallBmp.Height);
        }
    }
}
