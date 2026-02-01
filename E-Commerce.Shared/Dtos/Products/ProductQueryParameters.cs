using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Dtos.Products
{
    public class ProductQueryParameters
    {
        private const int MAXPAGESIZE = 10;
        private const int DEFAULTPAGESIZE = 6;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = DEFAULTPAGESIZE;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : 
                               value < DEFAULTPAGESIZE ? DEFAULTPAGESIZE : value;
        }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortOptions? SortOption { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSortOptions
    {
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4
    }
}
