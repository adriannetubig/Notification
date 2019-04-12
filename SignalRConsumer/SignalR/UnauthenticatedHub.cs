using Microsoft.AspNetCore.SignalR.Client;
using SignalRModel;
using System;
using System.Threading.Tasks;

namespace SignalRConsumer.SignalR
{
    public class UnauthenticatedHub: IUnauthenticatedHub
    {
        private Action<Notification> _methodOnMessage;
        private HubConnection _unauthenticatedHubConnection;

        private int _hubReconnectionAttempts;
        private int _hubReconnectionAttemptDelaySeconds;
        private string _uRLSignalR;

        public UnauthenticatedHub(Action<Notification> methodOnMessage, int hubReconnectionAttempts, int hubReconnectionAttemptDelaySeconds, string uRLSignalR)
        {
            _methodOnMessage = methodOnMessage;
            _hubReconnectionAttempts = hubReconnectionAttempts;
            _hubReconnectionAttemptDelaySeconds = hubReconnectionAttemptDelaySeconds;
            _uRLSignalR = uRLSignalR;

            ConnectToUnauthenticatedHub();
        }

        private async void ConnectToUnauthenticatedHub()
        {
            if (_unauthenticatedHubConnection == null || _unauthenticatedHubConnection.State == HubConnectionState.Disconnected)
            {
                _unauthenticatedHubConnection = new HubConnectionBuilder().WithUrl(_uRLSignalR + "/unauthenticatedHub").Build();
                _unauthenticatedHubConnection.Closed += async (error) =>
                {
                    await ReconnectToUnauthenticatedHub(0);
                };
                _unauthenticatedHubConnection.On<Notification>("UnauthorizedMessage", (notification) =>
                {
                    _methodOnMessage.DynamicInvoke(notification);
                });
                await _unauthenticatedHubConnection.StartAsync();
            }
        }

        private async Task ReconnectToUnauthenticatedHub(int attempts)
        {
            ConnectToUnauthenticatedHub();
            attempts++;
            await Task.Delay(_hubReconnectionAttemptDelaySeconds);
            if (_unauthenticatedHubConnection.State == HubConnectionState.Disconnected && (_hubReconnectionAttempts > attempts || _hubReconnectionAttempts == 0))
                await ReconnectToUnauthenticatedHub(attempts);
        }
    }
}
