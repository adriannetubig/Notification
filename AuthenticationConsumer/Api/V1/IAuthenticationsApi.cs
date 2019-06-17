using AuthenticationModel;

namespace AuthenticationConsumer.Api.V1
{
    public interface IAuthenticationsApi
    {
        bool Login(User user);
        string Token();
        Authentication Refresh(Authentication authentication);
    }
}
