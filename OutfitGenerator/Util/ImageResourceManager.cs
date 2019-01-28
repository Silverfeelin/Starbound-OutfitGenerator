using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OutfitGenerator.Util
{
    public static class ImageResourceManager
    {
        public static string basePath = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
            "Resources"
        );

        public static string GetResourcePath(string path) => Path.Combine(basePath, path);
        public static Image<Rgba32> GetImage(string path) => Image.Load<Rgba32>(GetResourcePath(path));
    }
}
