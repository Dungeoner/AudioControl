using AudioDeviceManager.DllImport.Enums;

namespace AudioDeviceManager.DllImport.Event
{
    public class DefaultDeviceChangedEventArgs : DeviceNotificationEventArgs
    {
        internal EDataFlow DataFlow { get; private set; }
        internal ERole Role { get; private set; }

        internal DefaultDeviceChangedEventArgs(string deviceId, EDataFlow dataFlow, ERole role) : base(deviceId)
        {
            DataFlow = dataFlow;
            Role = role;
        }
    }
}
