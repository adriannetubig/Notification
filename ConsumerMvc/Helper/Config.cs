namespace ConsumerMvc.Helper
{
    public class Config
    {
        public static string SignalRUrl => System.Configuration.ConfigurationManager.AppSettings["URLSignalR"];
    }
}