using Microsoft.AspNetCore.SignalR.Client;
using SignalRModel;
using System;
using System.Threading.Tasks;

namespace SignalRConsumer.SignalR
{
    public class AuthenticatedHub: IAuthenticatedHub
    {
        private Action<Notification> _methodOnMessage;
        private Action _methodOnRefresh;
        private HubConnection _AuthenticatedHubConnection;

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

            ConnectToAuthenticatedHub();
        }

        private async void ConnectToAuthenticatedHub()
        {
            if (_AuthenticatedHubConnection == null || _AuthenticatedHubConnection.State == HubConnectionState.Disconnected)
            {
                _AuthenticatedHubConnection = new HubConnectionBuilder().WithUrl(_uRLSignalR + "/AuthenticatedHub",
                    options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(_jwtToken);
                    }
                    ).Build();
                _AuthenticatedHubConnection.Closed += async (error) =>
                {
                    await ReconnectToAuthenticatedHub(0);
                };
                _AuthenticatedHubConnection.On<Notification>("UnauthorizedMessage", (notification) =>
                {
                    _methodOnMessage.DynamicInvoke(notification);
                });
                await _AuthenticatedHubConnection.StartAsync();
            }
        }

        private async Task ReconnectToAuthenticatedHub(int attempts)
        {
            _methodOnRefresh.DynamicInvoke();
            ConnectToAuthenticatedHub();
            attempts++;
            await Task.Delay(_hubReconnectionAttemptDelaySeconds);
            if (_AuthenticatedHubConnection.State == HubConnectionState.Disconnected && (_hubReconnectionAttempts > attempts || _hubReconnectionAttempts == 0))
                await ReconnectToAuthenticatedHub(attempts);
        }
    }
}
