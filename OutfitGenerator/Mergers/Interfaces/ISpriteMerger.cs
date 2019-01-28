using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OutfitGenerator.Mergers
{
    public interface ISpriteMerger
    {
        string Name { get; }
        int Priority { get; }

        Image<Rgba32> Merge(Image<Rgba32> imageA, Image<Rgba32> imageB);
    }
}
