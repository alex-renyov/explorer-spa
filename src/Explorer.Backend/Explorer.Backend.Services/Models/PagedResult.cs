using System.Collections.Generic;
using System.Linq;

namespace Explorer.Backend.Services.Models
{
    public sealed class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> data, long count)
        {
            Data = data;
            Count = count;
        }

        public PagedResult()
        {
            Data = Enumerable.Empty<T>();
            Count = 0;
        }

        public IEnumerable<T> Data { get; }
        
        public long Count { get; }
    }
}