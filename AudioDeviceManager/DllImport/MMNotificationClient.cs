using AudioControl.Models;
using AudioDeviceManager.DllImport.Enums;
using AudioDeviceManager.DllImport.Event;
using AudioDeviceManager.DllImport.Interfaces;
using System.Runtime.InteropServices;

namespace AudioDeviceManager.DllImport
{
    [Guid(Guids.IMMNotificationClientIIDString)]
    internal class MMNotificationClient : IMMNotificationClient
    {
        public event EventHandler<DeviceStateChangedEventArgs>? DeviceStateChanged;
        public event EventHandler<DeviceNotificationEventArgs>? DeviceAdded;
        public event EventHandler<DeviceNotificationEventArgs>? DeviceRemoved;
        public event EventHandler<DefaultDeviceChangedEventArgs>? DefaultDeviceChanged;
        public event EventHandler<DevicePropertyChangedEventArgs>? DevicePropertyChanged;

        public void OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            DeviceStateChanged?.Invoke(this, new DeviceStateChangedEventArgs(deviceId, newState));
        }

        public void OnDeviceAdded(string deviceId)
        {
            DeviceAdded?.Invoke(this, new DeviceNotificationEventArgs(deviceId));
        }

        public void OnDeviceRemoved(string deviceId)
        {
            DeviceAdded?.Invoke(this, new DeviceNotificationEventArgs(deviceId));
        }

        public void OnDefaultDeviceChanged(EDataFlow dataFlow, ERole role, string deviceId)
        {
            DefaultDeviceChanged?.Invoke(this, new DefaultDeviceChangedEventArgs(deviceId, dataFlow, role));
        }

        public void OnPropertyValueChanged(string deviceId, PropertyKey propertyKey)
         {
            DevicePropertyChanged?.Invoke(this, new DevicePropertyChangedEventArgs(deviceId, propertyKey));
        }
    }
}
