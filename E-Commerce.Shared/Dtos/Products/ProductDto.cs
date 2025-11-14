using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public  string PicutreURL { get; init; }
        public decimal Price { get; init; }
        public string Brand { get; init; }
        public string Type { get; init; }
    }
}
