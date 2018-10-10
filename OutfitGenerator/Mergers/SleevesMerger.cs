using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutfitGenerator.Mergers
{
    public class SleevesMerger : ISpriteMerger
    {
        private const int SLEEVES_WIDTH = 387;
        private const int SLEEVES_HEIGHT = 301;
        private static Size sleevesSize = new Size(SLEEVES_WIDTH, SLEEVES_HEIGHT);
        
        public Bitmap Merge(string firstPath, string secondPath)
        {
            Bitmap frontSleeves;
            Bitmap backSleeves;

            // Predict the correct order:
            if (firstPath.ToLower().Contains("fsleeve") && secondPath.ToLower().Contains("bsleeve"))
            {
                frontSleeves = new Bitmap(firstPath);
                backSleeves = new Bitmap(secondPath);
            }
            else if (firstPath.ToLower().Contains("bsleeve") && secondPath.ToLower().Contains("fsleeve"))
            {
                frontSleeves = new Bitmap(secondPath);
                backSleeves = new Bitmap(firstPath);
            }
            else //prediction failed
            {
                Console.WriteLine("Is the order correct?");
                Console.WriteLine("Front sleeve: " + firstPath);
                Console.WriteLine("Back sleeve: " + secondPath);
                Console.WriteLine("Press Enter if it is, press any key otherwise");

                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    frontSleeves = new Bitmap(firstPath);
                    backSleeves = new Bitmap(secondPath);
                }
                else
                {
                    frontSleeves = new Bitmap(secondPath);
                    backSleeves = new Bitmap(firstPath);
                }
            }

            return ApllyMultingSleeves(frontSleeves, backSleeves);
        }

        private static Bitmap ApllyMultingSleeves(Bitmap frontSleeves, Bitmap backSleeves)
        {
            Bitmap result = new Bitmap(SLEEVES_WIDTH, SLEEVES_HEIGHT * 2);

            Superimpose(result, frontSleeves, 0, 0);
            Superimpose(result, backSleeves, 0, SLEEVES_HEIGHT);

            return result;
        }

        private static void Superimpose(Bitmap largeBmp, Bitmap smallBmp, int x, int y)
        {
            Graphics g = Graphics.FromImage(largeBmp);
            g.DrawImage(smallBmp, x, y, smallBmp.Width, smallBmp.Height);
        }
    }
}
