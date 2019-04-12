using Microsoft.AspNetCore.SignalR.Client;
using SignalRModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumerDesktop
{
    public partial class Form1 : Form
    {
        private HubConnection _unauthenticatedHubConnection;
        private List<Notification> _unauthenticatedNotifications;

        private int _hubReconnectionAttempts;
        private int _hubReconnectionAttemptDelaySeconds;
        private string _uRLAuthentication;
        private string _uRLSignalR;
        public Form1()
        {
            InitializeComponent();
            ConfigureSettings();
            ConnectToUnauthenticatedHub();
        }
        private async void BtnSend_Click(object sender, EventArgs e)
        {
            await SendNotificationAsync();
        }

        private void ConfigureSettings()
        {
            _hubReconnectionAttempts = Convert.ToInt32(ConfigurationManager.AppSettings["HubReconnectionAttempts"]);
            _hubReconnectionAttemptDelaySeconds = Convert.ToInt32(ConfigurationManager.AppSettings["HubReconnectionAttemptDelaySeconds"]) * 1000;
            _uRLAuthentication = ConfigurationManager.AppSettings["URLAuthentication"];
            _uRLSignalR = ConfigurationManager.AppSettings["URLSignalR"];

            _unauthenticatedNotifications = new List<Notification>();
        }

        private Notification Notification()
        {
            return new Notification()
            {
                Sender = txtSender.Text,
                Message = txtMessage.Text
            };
        }

        private async Task SendNotificationAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_uRLSignalR);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var apiUrl = string.Empty;
            if (tcConsumer.SelectedIndex == 0) //unauthenticated
                apiUrl = "api/Notifications/SendMessageToUnauthenticatedConsumer";
            else if (tcConsumer.SelectedIndex == 1) //authenticated
                apiUrl = "api/Notifications/SendMessageToAuthenticatedConsumer";

            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, Notification());
            response.EnsureSuccessStatusCode();
        }

        #region UnauthenticatedHub
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
                    _unauthenticatedNotifications.Add(notification);
                    dgvUnauthenticated.Invoke((Action)(() => RebindUnauthenticatedTable()));
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

        private void RebindUnauthenticatedTable()
        {
            var bindingList = new BindingList<Notification>(_unauthenticatedNotifications);
            var source = new BindingSource(bindingList, null);
            dgvUnauthenticated.DataSource = source;
        }
        #endregion

    }
}
