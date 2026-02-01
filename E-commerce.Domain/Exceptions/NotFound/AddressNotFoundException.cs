namespace E_commerce.Domain.Exceptions.NotFound
{
    public class AddressNotFoundException(string email) : NotFoundException($"Address was not Found For User with Email : {email}")
    {
    }
}
