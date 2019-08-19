using System.IO;

namespace SwissLife.SxS.Helpers
{
    public static class FileHelpers
    {
        /// <summary>
        ///     Remove illegal file name characters
        /// </summary>
        /// <param name="fileName">File Name to clean</param>
        public static string RemoveIllegalFileNameChars(string fileName)
        {
            return string.Join(string.Empty, fileName.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
