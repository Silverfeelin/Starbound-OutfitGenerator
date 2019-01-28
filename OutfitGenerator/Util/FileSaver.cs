using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace OutfitGenerator.Util
{
    public static class FileSaver
    {
        /// <summary>
        /// Saves the content string to a directory as a text file using a generated file name.
        /// </summary>
        /// <param name="directory">Directory to save the file in.</param>
        /// <param name="content">File contents.</param>
        /// <returns>File path.</returns>
        public static string Save(string directoryPath, string content, string fileNamePrefix = "generatedPants-", string extension = ".txt")
        {
            string file = $"{fileNamePrefix}{DateTime.Now.ToString("h-mm-ss")}{extension}";
            string filePath = Path.Combine(directoryPath, file);
            File.WriteAllText(filePath, content);
            return filePath;
        }

        /// <summary>
        /// Saves the image to a directory.
        /// </summary>
        /// <param name="directoryPath">Directory to save the file in.</param>
        /// <param name="image">Image to save.</param>
        /// <returns>File path.</returns>
        public static string SaveImage(string directoryPath, Image<Rgba32> image, string fileNamePrefix = "merged-")
        {
            string file = $"{fileNamePrefix}{DateTime.Now.ToString("h-mm-ss")}.png";
            string filePath = Path.Combine(directoryPath, file);
            image.Save(filePath);
            return filePath;
        }
    }
}
