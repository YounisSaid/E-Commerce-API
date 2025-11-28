namespace E_Commerce.Service.Abstraction
{
    public interface IServiceManager    
    {
        public IProductService ProductServices { get; }
        public IBasketService BasketServices { get; }
    }
}
