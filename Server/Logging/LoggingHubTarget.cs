using NLog;
using NLog.Targets;

namespace WasmLogToBrowser.Server.Logging
{
    public class LoggingHubTarget : AsyncTaskTarget
    {
        private LoggingHubConnection? _connection;

        public LoggingHubTarget(string hubUrl)
        {
            _connection = new LoggingHubConnection(hubUrl);
            OptimizeBufferReuse = true;
        }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken token)
        {
            string logMessage = this.Layout.Render(logEvent);
            if (_connection != null)
            {
                await _connection.Log(logMessage);
            }
        }

        protected override async void CloseTarget()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
        }
    }
}
