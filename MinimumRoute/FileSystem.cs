using System.IO;

namespace MinimumRoute
{
    public class FileSystem : IFileSystem
    {
        public string ReadAllLines(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
