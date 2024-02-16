namespace AudioDeviceManager.DllImport.Event
{
    public class DeviceNotificationEventArgs : EventArgs
    {
        public string DeviceId { get; private set; }

        public DeviceNotificationEventArgs(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
