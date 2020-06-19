using System.Collections.Generic;

namespace MinimumRoute.Service
{
    public interface IFileService
    {
        string ReadFile(string path);
        void WriteFile(string path, string text);
    }
}