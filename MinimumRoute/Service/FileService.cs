using MinimumRoute.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinimumRoute.Service
{
    public class FileService 
    {
        protected FileSystem _fileSystem;

        public FileService(FileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public string ReadFile(string path)
        {
            return _fileSystem.ReadAllText(path);
        }

        public void WriteFile(string path, string content)
        {
            _fileSystem.WriteAllText(path, content);
        }
    }
}
