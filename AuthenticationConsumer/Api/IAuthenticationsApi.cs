using AuthenticationModel;

namespace AuthenticationConsumer.Api
{
    public interface IAuthenticationsApi
    {
        bool Login(User user);
        string Token();
        Authentication Refresh(Authentication authentication);
    }
}
