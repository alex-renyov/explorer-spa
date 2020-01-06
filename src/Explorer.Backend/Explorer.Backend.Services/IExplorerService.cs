using System.Threading.Tasks;
using Explorer.Backend.Services.Models;

namespace Explorer.Backend.Services
{
    public interface IExplorerService
    {
        Task<PagedResult<DirectoryItem>> GetDirectories(string path, int fromIndex, int count);
        Task<PagedResult<FileItem>> GetFiles(string path, int fromIndex, int count);
    }
}