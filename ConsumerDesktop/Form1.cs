using AuthenticationConsumer.Api.V1;
using AuthenticationModel;
using SignalRConsumer.Api.V1;
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
        private List<Notification> _authenticatedNotifications;
        private List<Notification> _unauthenticatedNotifications;

        private int _authenticationCacheMinutes;
        private int _hubReconnectionAttempts;
        private int _hubReconnectionAttemptDelaySeconds;
        private string _authenticationUrl;
        private string _authenticationCacheName;
        private string _uRLSignalR;

        private readonly IAuthenticationsApi _iAuthenticationsApi;
        private readonly INotificationsApi _iNotificationsApi;
        private readonly IAuthenticatedHub _iAuthenticatedHub;
        private readonly IUnauthenticatedHub _iUnauthenticatedHub;
        public Form1()
        {
            InitializeComponent();
            ConfigureSettings();
            _iAuthenticationsApi = new AuthenticationsApi(_authenticationCacheMinutes, _authenticationCacheName, _authenticationUrl);
            _iNotificationsApi = new NotificationsApi(_uRLSignalR);            
            //Attaches method RebindAuthenticatedTable to SignalR Hub
            _iAuthenticatedHub = new AuthenticatedHub(AuthenticatedHubRefreshToken, RebindAuthenticatedTable, _hubReconnectionAttempts, _hubReconnectionAttemptDelaySeconds, _uRLSignalR);
            //Attaches method RebindUnauthenticatedTable to SignalR Hub
            _iUnauthenticatedHub = new UnauthenticatedHub(RebindUnauthenticatedTable, _hubReconnectionAttempts, _hubReconnectionAttemptDelaySeconds, _uRLSignalR);
            Login();
        }
        private async void BtnSend_Click(object sender, EventArgs e)
        {
            await SendNotificationAsync();
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void ConfigureSettings()
        {
            _hubReconnectionAttempts = Convert.ToInt32(ConfigurationManager.AppSettings["HubReconnectionAttempts"]);
            _hubReconnectionAttemptDelaySeconds = Convert.ToInt32(ConfigurationManager.AppSettings["HubReconnectionAttemptDelaySeconds"]) * 1000;
            _uRLSignalR = ConfigurationManager.AppSettings["URLSignalR"];
            
            _authenticationUrl = ConfigurationManager.AppSettings["AuthenticationUrl"];
            _authenticationCacheName = ConfigurationManager.AppSettings["AuthenticationCacheName"];
            _authenticationCacheMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["AuthenticationCacheMinutes"]);

            _authenticatedNotifications = new List<Notification>();
            _unauthenticatedNotifications = new List<Notification>();
        }

        private void Login()
        {
            var user = new User
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Text
            };
            if (_iAuthenticationsApi.Login(user))
                StartAuthenticatedHub();
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
            else if (tcConsumer.SelectedIndex == 1) //authenticated
                await _iNotificationsApi.SendMessageToAuthenticatedConsumer(_iAuthenticationsApi.Token(), NotificationModel());
        }

        private void StartAuthenticatedHub()
        {
            AuthenticatedHubRefreshToken();
            _iAuthenticatedHub.ConnectToHub();
        }

        private void AuthenticatedHubRefreshToken()
        {
            _iAuthenticatedHub.JWTToken = _iAuthenticationsApi.Token();
        }

        private void RebindAuthenticatedTable(Notification notification)
        {
            _authenticatedNotifications.Add(notification);
            var bindingList = new BindingList<Notification>(_authenticatedNotifications);
            var source = new BindingSource(bindingList, null);
            dgvAuthenticated.Invoke((MethodInvoker)delegate
            {
                dgvAuthenticated.DataSource = source;
            });
        }

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

    }
}
