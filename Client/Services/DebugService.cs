namespace WasmLogToBrowser.Client.Services
{
    public class DebugService
    {
        public event Func<Task>? OnChange;
        public bool ConnectToServerConsole = true;
        public List<string> LogMessages = new List<string>();

        public void AddLogMessage(string logMessage)
        {
            LogMessages.Add(logMessage);
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
