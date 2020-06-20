using System.IO;

namespace MinimumRoute
{
    public class FileSystem 
    {
        public string ReadAllLines(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
    }
}
