namespace SignalRConsumer.SignalR
{
    public interface IAuthenticatedHub
    {
        string JWTToken { set; }
    }
}
