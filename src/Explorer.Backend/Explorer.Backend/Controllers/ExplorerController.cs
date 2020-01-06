using System.Threading;
using System.Threading.Tasks;
using Explorer.Backend.Services;
using Explorer.Backend.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Explorer.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ExplorerController
    {
        private readonly ILogger<ExplorerController> _logger;
        private readonly IExplorerService _explorerService;

        public ExplorerController(ILogger<ExplorerController> logger, IExplorerService explorerService)
        {
            _logger = logger;
            _explorerService = explorerService;
        }

        [HttpGet("directories")]
        public async Task<PagedResult<DirectoryItem>> GetDirectories([FromQuery] string path, [FromQuery] int fromIndex = 0, [FromQuery] int count = 10)
        {
            return await _explorerService.GetDirectories(path, fromIndex, count);
        }
        
        [HttpGet("files")]
        public async Task<PagedResult<FileItem>> GetFiles([FromQuery] string path, [FromQuery] int fromIndex = 0, [FromQuery] int count = 10)
        {
            return await _explorerService.GetFiles(path, fromIndex, count);
        }
    }
}