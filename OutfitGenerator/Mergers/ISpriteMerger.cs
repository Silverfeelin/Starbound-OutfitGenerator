using System.Drawing;

namespace OutfitGenerator.Mergers
{
    public interface ISpriteMerger
    {
        Bitmap Merge(string pathA, string pathB);
    }
}
