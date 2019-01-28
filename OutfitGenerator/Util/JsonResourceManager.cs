using System.IO;
using Newtonsoft.Json.Linq;

namespace OutfitGenerator.Util
{
    public static class JsonResourceManager
    {
        public static string basePath = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
            "Resources"
        );

        public static string GetResourcePath(string path) => Path.Combine(basePath, path);
        private static string ReadFile(string path) => File.ReadAllText(GetResourcePath(path));
        public static JObject GetJsonObject(string path) => JObject.Parse(ReadFile(path));
        public static JArray GetJsonArray(string path) => JArray.Parse(ReadFile(path));
    }
}
