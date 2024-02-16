using AudioControl.Enum;

namespace AudioDeviceManager.DllImport.Event
{
    internal class DeviceStateChangedEventArgs : DeviceNotificationEventArgs
    {
        internal DeviceState DeviceState { get; private set; }

        internal DeviceStateChangedEventArgs(string deviceId, DeviceState deviceState) : base(deviceId)
        {
            DeviceState = deviceState;
        }
    }
}
