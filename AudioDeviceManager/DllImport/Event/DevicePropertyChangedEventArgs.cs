using AudioControl.Models;

namespace AudioDeviceManager.DllImport.Event
{
    internal class DevicePropertyChangedEventArgs : DeviceNotificationEventArgs
    {
        internal PropertyKey PropertyKey { get; private set; }

        internal DevicePropertyChangedEventArgs(string deviceId, PropertyKey propertyKey) : base(deviceId)
        {
            PropertyKey = propertyKey;
        }
    }
}
