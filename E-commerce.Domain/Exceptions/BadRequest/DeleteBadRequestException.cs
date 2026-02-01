using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Exceptions.BadRequest
{
    public class DeleteBadRequestException() : BadRequestException("Delete operation failed.")
    {
    }
}
