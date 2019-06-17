using AuthenticationModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;

namespace AuthenticationConsumer.Api.V1
{
    public class AuthenticationsApi: IAuthenticationsApi
    {
        private readonly int _cacheMinutes;

        private readonly string _cacheName;
        private readonly string _url;

        private readonly ObjectCache _cache;

        private static readonly object _lock = new object();

        public AuthenticationsApi(int cacheMinutes, string cacheName, string url)
        {
            _cacheMinutes = cacheMinutes;
            _cacheName = cacheName;
            _url = url;
            _cache = MemoryCache.Default;
        }

        public bool Login(User user)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = httpClient.PostAsJsonAsync("api/v1/Authentications/Login", user).Result;
            var authentication = response.Content.ReadAsAsync<Authentication>().Result;

            CacheAuthentication(authentication);
            return authentication != null;
        }

        public string Token()
        {
            var authentication = Authentication();
            return authentication.Token;
        }

        public Authentication Refresh(Authentication authentication)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = httpClient.PostAsJsonAsync("api/v1/Authentications/Refresh", authentication).Result;
            authentication = response.Content.ReadAsAsync<Authentication>().Result;

            CacheAuthentication(authentication);
            return authentication;
        }

        private Authentication Authentication()
        {
            var authentication = (Authentication)_cache[_cacheName];
            if (authentication == null)
                throw new ArgumentException("Please Login");//ToDo: Refactor if needed

            if (authentication.Expiration <= DateTime.UtcNow)
            {
                lock (_lock)
                {
                    // Ensure that the data was not loaded by a concurrent thread 
                    // while waiting for lock.
                    authentication = (Authentication)_cache[_cacheName];
                    if (authentication.Expiration <= DateTime.UtcNow)
                        authentication = Refresh(authentication);
                }
            }
            return authentication;
        }

        private void CacheAuthentication(Authentication authentication)
        {
            var cacheDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_cacheMinutes));
            _cache.Add(_cacheName, authentication, cacheDate);
        }
    }
}
