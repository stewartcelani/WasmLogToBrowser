using Microsoft.AspNetCore.SignalR.Client;

namespace WasmLogToBrowser.Server.Logging
{
    public class LoggingHubConnection : IAsyncDisposable
    {
        private HubConnection? _hubConnection;
        private string _hubUrl;

        public LoggingHubConnection(string hubUrl)
        {
            _hubUrl = hubUrl;
        }

        public async Task Log(string logMessage)
        {
            await EnsureConnection();
            if (_hubConnection != null)
            {
                await _hubConnection.SendAsync("Log", logMessage);
            }
        }

        public async Task EnsureConnection()
        {
            if (_hubConnection == null)
            {
                _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .Build();

                await _hubConnection.StartAsync();
            }
            else if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                try
                {
                    await _hubConnection.StopAsync();
                    await _hubConnection.DisposeAsync();
                }
                catch (Exception ex)
                {
                    NLog.Common.InternalLogger.Error(ex, "Exception in LoggingHubConnection.DisposeAsync");
                }
                finally
                {
                    _hubConnection = null;
                }
            }
        }
    }
}
