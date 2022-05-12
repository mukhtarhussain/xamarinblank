using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RestSharp;
using System;
using System.IO;
using System.Linq;
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

        private async void OnMessageReceived(ResponseMessage msg)
        {
            if (msg.Text.Contains("restart"))
            {
                DependencyService.Get<IRebootDevice>().Reboot();
            } 
            else if(msg.Text.Contains("install"))
            {
                //var blobPath = "1-69-0/Builds/Android/game_SiteSafeV2.apk";
                var blobPath = msg.Text.Split(new char[] { ' ' })[2];
                var blobBytes = await GetAzureBlobBytes(blobPath);
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "game_SiteSafeV2.apk");
                filePath = filePath.Replace(".local/share/", "");
                File.WriteAllBytes(filePath, blobBytes);
                DependencyService.Get<IInstallPackage>().Install(filePath);
            }
            else
            {
                Messages += Environment.NewLine + msg.Text;
            }
        }

        private async Task<byte[]> GetAzureBlobBytes(string blobPath)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=storjbasitesafegameaue;AccountKey=IXsoStnjhHgrN+/AgghPZJlvrkcONaIVxuS0ltPZOh4/yyT9s3jiZpAM/7TyyqZ210Fad4BsMN4lHZ5cE+7EZw==;EndpointSuffix=core.windows.net";
            var client = new BlobServiceClient(connectionString);
            var containerClient = client.GetBlobContainerClient("development");
            var blobClient = containerClient.GetBlobClient(blobPath);
            BlobDownloadInfo downloadInfo = await blobClient.DownloadAsync();
            using var memoryStream = new MemoryStream();
            await downloadInfo.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();


            //var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            //using (var fileStream = File.OpenWrite(filePath))
            //{
            //    await downloadInfo.Content.CopyToAsync(fileStream);
            //}
        }
    }
}
