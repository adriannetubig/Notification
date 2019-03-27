using System;

namespace AuthenticationWeb.Models
{
    public class AuthenticationResult
    {
        public DateTime Exiration { get; set; }
        public DateTime InvalidBefore { get; set; }
        public string Token { get; set; }
    }
}
