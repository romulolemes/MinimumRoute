using MinimumRoute.IO;
using System;

namespace MinimumRoute.Service
{
    public class FileService
    {
        protected FileSystem _fileSystem;

        public FileService(FileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public string ReadAllText(string path)
        {
            return _fileSystem.ReadAllText(path);
        }

        public void WriteAllText(string path, string content)
        {
            _fileSystem.WriteAllText(path, content);
        }
    }
}
