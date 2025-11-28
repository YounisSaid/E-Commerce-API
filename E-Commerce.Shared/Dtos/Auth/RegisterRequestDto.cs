using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Dtos.Auth
{
    public class RegisterRequestDto
    {
        public string UserName { get; init; }
        public string DisplayName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string PhoneNumber { get; init; }
    }
}
