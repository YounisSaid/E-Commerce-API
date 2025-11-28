using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Exceptions.UnAuthorized
{
    public class UnAuthorizedException() : Exception("You are not Authorized")
    {
    }
}
