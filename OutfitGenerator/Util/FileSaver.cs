using System;
using System.IO;

namespace OutfitGenerator.Util
{
    public static class FileSaver
    {
        /// <summary>
        /// Saves the given content to the given directory, using a generated file name.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="content"></param>
        /// <returns>Generated file name.</returns>
        public static string Save(DirectoryInfo directory, string content, string fileNamePrefix = "generatedPants")
        {
            string file = string.Format("{0}-{1}.txt", fileNamePrefix, DateTime.Now.ToString("h-mm-ss"));
            string path = Path.Combine(directory.FullName, file);
            File.WriteAllText(path, content);
            return file;
        }
    }
}
