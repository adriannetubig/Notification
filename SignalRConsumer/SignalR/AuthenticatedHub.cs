using Microsoft.AspNetCore.SignalR.Client;
using SignalRModel;
using System;
using System.Threading.Tasks;

namespace SignalRConsumer.SignalR
{
    public class AuthenticatedHub: IAuthenticatedHub
    {
        private Action _methodOnRefresh;
        private Action<Notification> _methodOnMessage;
        private HubConnection _hubConnection;

        private readonly int _hubReconnectionAttempts;
        private readonly int _hubReconnectionAttemptDelaySeconds;
        private string _jwtToken;
        private readonly string _uRLSignalR;

        public string JWTToken
        {
            set => _jwtToken = value;
        }

        public AuthenticatedHub(Action methodOnRefresh, Action<Notification> methodOnMessage, int hubReconnectionAttempts, int hubReconnectionAttemptDelaySeconds, string uRLSignalR)
        {
            _methodOnRefresh = methodOnRefresh;
            _methodOnMessage = methodOnMessage;
            _hubReconnectionAttempts = hubReconnectionAttempts;
            _hubReconnectionAttemptDelaySeconds = hubReconnectionAttemptDelaySeconds;
            _uRLSignalR = uRLSignalR;
        }

        public async void ConnectToHub()
        {
            if (_hubConnection == null || _hubConnection.State == HubConnectionState.Disconnected)
            {
                _hubConnection = new HubConnectionBuilder().WithUrl(_uRLSignalR + "/AuthenticatedHub",
                    options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(_jwtToken);
                    }
                    ).Build();
                _hubConnection.Closed += async (error) =>
                {
                    await ReconnectToAuthenticatedHub(0);
                };
                _hubConnection.On<Notification>("AuthorizedMessage", (notification) =>
                {
                    _methodOnMessage.DynamicInvoke(notification);
                });
                await _hubConnection.StartAsync();
            }
        }

        private async Task ReconnectToAuthenticatedHub(int attempts)
        {
            _methodOnRefresh.DynamicInvoke();
            ConnectToHub();
            attempts++;
            await Task.Delay(_hubReconnectionAttemptDelaySeconds);
            if (_hubConnection.State == HubConnectionState.Disconnected && (_hubReconnectionAttempts > attempts || _hubReconnectionAttempts == 0))
                await ReconnectToAuthenticatedHub(attempts);
        }
    }
}
