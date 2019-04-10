namespace AuthenticationFunction
{
    public interface IFRefreshToken
    {
        string Create(int userId);
        bool IsValidRefreshToken(int userId, string refreshToken);
    }
}
