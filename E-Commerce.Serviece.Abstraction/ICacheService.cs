namespace E_Commerce.Serviece.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object value, TimeSpan duration);
    }
}
