namespace E_Commerce.Shared
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = null!;
        public double DurationDays { get; set; } 
        public string SecurityKey { get; set; } = null!;
        public string Expires { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
