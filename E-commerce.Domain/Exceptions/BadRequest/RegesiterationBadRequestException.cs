using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Exceptions.BadRequest
{
    public class RegesiterationBadRequestException(List<string> errors) : BadRequestException(string.Join(",", errors))
    {
    }
}
