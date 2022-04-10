namespace WasmLogToBrowser.Client.Services
{
    public class DebugService
    {
        public event Func<Task>? OnChange;
        public bool ConnectToServerConsole = false;
        public List<string> LogMessages = new List<string>();

        public void AddLogMessage(string logMessage)
        {
            LogMessages.Add(logMessage);
            HandleOnChange();
        }

        public void ClearLogMessages()
        {
            LogMessages.Clear();
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
