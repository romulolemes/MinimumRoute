using System.Collections.Generic;

namespace MinimumRoute.Service
{
    public interface IFileService
    {
        List<string> ReadFile(string path);
    }
}