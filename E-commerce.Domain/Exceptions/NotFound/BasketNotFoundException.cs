using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Exceptions.NotFound
{
    public class BasketNotFoundException(string basketId) : NotFoundException($"Basket with id : {basketId} was not found.")
    {
    }
}
