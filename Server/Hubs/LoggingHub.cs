using Microsoft.AspNetCore.SignalR;
using WasmLogToBrowser.Server.Logging;

namespace WasmLogToBrowser.Server.Hubs
{
    public class LoggingHub : Hub
    {
        public async Task Log(string logMessage)
        {
            await Clients.All.SendAsync("Log", logMessage);
        }

        public override Task OnConnectedAsync()
        {
            Logger.Log.Trace("Top");
            Logger.Log.Debug($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? e)
        {
            Logger.Log.Trace("Top");
            Logger.Log.Debug($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
