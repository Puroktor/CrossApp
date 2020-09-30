using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossApp
{
    interface IFileWorker
    {
        Task<string> SaveTextAsync(string filename, string text);
        Task<bool> ExistsAsync(string filename);
    }
}
