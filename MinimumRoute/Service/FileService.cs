using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinimumRoute.Service
{
    public class FileService : IFileService
    {
        protected IFileSystem _fileSystem;

        public FileService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public string ReadFile(string path)
        {
            return _fileSystem.ReadAllLines(path);
        }
    }
}
