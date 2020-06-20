using System.IO;

namespace MinimumRoute.IO
{
    public class FileSystem
    {
        public virtual string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public virtual void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
    }
}
