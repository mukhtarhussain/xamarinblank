using RestSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Websocket.Client;
using Xamarin.Forms;

namespace XamarinBlank
{
    public class MainPageViewModel : BindableObject 
    {
        private WebsocketClient _wsClient;
        private string _messages;
        private string _message;

        public bool IsConnected 
        {
            get => _wsClient != null && _wsClient.IsRunning;
        }

        public string Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(CanSend));
            }
        }

        public bool CanSend
        {
            get => _message != null && _message.Length > 0;
        }

        public ICommand ConnectCommand => new Command(async () => await ConnectAsync());

        public ICommand DisconnectCommand => new Command(async () => await DisconnectAsync());

        public ICommand SendCommand => new Command(() => SendMessage());

        public async Task ConnectAsync()
        {
            try
            {
                var client = new RestClient("https://pubsubservice.azurewebsites.net/negotiate");
                var request = new RestRequest().AddQueryParameter("id", "Mukhtar");
                var response = await client.GetAsync(request);
                var url = new Uri(response.Content);
                _wsClient = new WebsocketClient(url)
                {
                    ReconnectTimeout = null
                };
                _wsClient.MessageReceived.Subscribe(msg => OnMessageReceived(msg));
                await _wsClient.StartOrFail();
                OnPropertyChanged(nameof(IsConnected));
            }
            catch (Exception ex)
            {
                _wsClient = null;
            }
        }

        public async Task DisconnectAsync()
        {
            await _wsClient.StopOrFail(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "Disconnect");
            _wsClient.Dispose();
            _wsClient = null;
            OnPropertyChanged(nameof(IsConnected));
        }

        public void SendMessage()
        {
            _wsClient.Send(_message);
            Message = string.Empty;
            OnPropertyChanged(nameof(IsConnected));
        }

        private void OnMessageReceived(ResponseMessage msg)
        {
            if (msg.Text.Contains("restart"))
            {
                DependencyService.Get<IRebootDevice>().Reboot();
            }
            else
            {
                Messages += Environment.NewLine + msg.Text;
            }
        }
    }
}
