using System.Text;
using Newtonsoft.Json.Linq;

namespace OutfitGenerator.Util
{
    public static class JsonResourceManager
    {
        public static string GetResource(byte[] resource)
        {
            return Encoding.UTF8.GetString(resource);
        }

        public static JObject GetJsonObject(byte[] resource)
        {
            return JObject.Parse(GetResource(resource));
        }

        public static JArray GetJsonArray(byte[] resource)
        {
            return JArray.Parse(GetResource(resource));
        }
    }
}
