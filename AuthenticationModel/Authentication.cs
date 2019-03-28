using System;

namespace AuthenticationModel
{
    public class Authentication
    {
        public DateTime Exiration { get; set; }
        public DateTime InvalidBefore { get; set; }
        public string Token { get; set; }
    }
}
