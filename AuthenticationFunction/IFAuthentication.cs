using AuthenticationModel;

namespace AuthenticationFunction
{
    public interface IFAuthentication
    {
        Authentication Create(string refreshTokenn, User user);
        User GetUserDetailsFromToken(string token);
    }
}
