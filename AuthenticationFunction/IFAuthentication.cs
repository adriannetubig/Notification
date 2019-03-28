using AuthenticationModel;

namespace AuthenticationFunction
{
    public interface IFAuthentication
    {
        Authentication Create(User user);
    }
}
