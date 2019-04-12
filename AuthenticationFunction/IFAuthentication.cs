using AuthenticationModel;

namespace AuthenticationFunction
{
    public interface IFAuthentication
    {
        Authentication Create(string refreshToken, User user);
        User GetUserDetailsFromToken(string token);
    }
}
