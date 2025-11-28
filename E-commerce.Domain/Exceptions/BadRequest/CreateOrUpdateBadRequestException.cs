using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Exceptions.BadRequest
{
    public class CreateOrUpdateBadRequestException() : BadRequestException("Create or update operation failed.")
    {
    }
}
