using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Dtos.Auth
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
