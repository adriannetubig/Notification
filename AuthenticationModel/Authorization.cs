using System;

namespace AuthenticationModel
{
    public class Authorization//ToDo: This can be a nuget package
    {
        public DateTime Expiration => DateTime.UtcNow.AddMinutes(ExpiresMinutes);
        public double ClockSkewMinutes { get; set; }
        public double ExpiresMinutes { get; set; }
        public string IssuerSigningKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string[] AllowedOrigins { get; set; }
        public TimeSpan ClockSkew => TimeSpan.FromMinutes(ClockSkewMinutes);
    }
}
