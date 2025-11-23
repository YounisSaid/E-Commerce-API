using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public record PaginatedResult<TResult>(int PageIndex, int PageSize, int Count, IEnumerable<TResult> Data);
    
}
