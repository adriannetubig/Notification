namespace AuthenticationFunction
{
    public class FRefreshToken : IFRefreshToken
    {
        public string Create(int userId)
        {
            return "RandomRefreshToken"; //ToDo: This should be done in random and saved to a database
        }

        public bool IsValidRefreshToken(int userId, string refreshToken)
        {
            return userId == 1 && refreshToken == "RandomRefreshToken"; //ToDo: This should be checked from a database
        }
    }
}
