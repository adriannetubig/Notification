using SignalRConsumer.Api;
using SignalRConsumer.SignalR;
using SignalRModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumerDesktop
{
    public partial class Form1 : Form
    {
        private List<Notification> _unauthenticatedNotifications;

        private int _hubReconnectionAttempts;
        private int _hubReconnectionAttemptDelaySeconds;
        private string _uRLAuthentication;
        private string _uRLSignalR;

        private readonly INotificationsApi _iNotificationsApi;
        private readonly IUnauthenticatedHub _iUnauthenticatedHub;
        public Form1()
        {
            InitializeComponent();
            ConfigureSettings();
            //ConnectToUnauthenticatedHub();
            _iNotificationsApi = new NotificationsApi(_uRLSignalR);
            _iUnauthenticatedHub = new UnauthenticatedHub(RebindUnauthenticatedTable, _hubReconnectionAttempts, _hubReconnectionAttemptDelaySeconds, _uRLSignalR);
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

        private Notification NotificationModel()
        {
            return new Notification()
            {
                Sender = txtSender.Text,
                Message = txtMessage.Text
            };
        }

        private async Task SendNotificationAsync()
        {
            if (tcConsumer.SelectedIndex == 0) //unauthenticated
                await _iNotificationsApi.SendMessageToUnauthenticatedConsumer(NotificationModel());
            //else if (tcConsumer.SelectedIndex == 1) //authenticated
            //    apiUrl = "api/Notifications/SendMessageToAuthenticatedConsumer";

            //HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, Notification());
            //response.EnsureSuccessStatusCode();
        }

        #region UnauthenticatedHub
        private void RebindUnauthenticatedTable(Notification notification)
        {
            _unauthenticatedNotifications.Add(notification);
            var bindingList = new BindingList<Notification>(_unauthenticatedNotifications);
            var source = new BindingSource(bindingList, null);
            dgvUnauthenticated.Invoke((MethodInvoker)delegate
            {
                dgvUnauthenticated.DataSource = source;
            });
        }
        #endregion

    }
}
