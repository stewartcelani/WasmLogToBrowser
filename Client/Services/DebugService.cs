using WasmLogToBrowser.Client.Library;

namespace WasmLogToBrowser.Client.Services
{
    public class DebugService
    {
        private readonly JsConsole _console;
        public event Func<Task>? OnChange;
        public bool ConnectToServerConsole = false;
        public List<string> LogMessages = new List<string>();

        public DebugService(JsConsole console)
        {
            _console = console;
        }

        public async Task AddLogMessage(string logMessage)
        {
            LogMessages.Add(logMessage);
            await _console.LogAsync(logMessage);
            HandleOnChange();
        }

        public async Task ClearLogMessages()
        {
            LogMessages.Clear();
            await _console.ClearAsync();
            HandleOnChange();
        }

        public void ToggleConnectToServerConsole()
        {
            ConnectToServerConsole = !ConnectToServerConsole;
            HandleOnChange();
        }

        private void HandleOnChange()
        {
            if (OnChange != null)
            {
                OnChange?.Invoke();
            }
        }
    }
}
