using System;

namespace AuthenticationModel
{
    public class Authentication
    {
        public DateTime Expiration { get; set; }
        public DateTime InvalidBefore { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
