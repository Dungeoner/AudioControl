using AudioControl;
using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Linq;
using AudioDeviceManager.DllImport.Interfaces;
using System.Security.Claims;
using AudioDeviceManager.DllImport;
using AudioDeviceManager.DllImport.Event;
using System.Linq.Expressions;

namespace AudioDeviceManager
{
    public class DeviceManager : IAudioDeviceManager
    {
        private readonly MMNotificationClient _notificationClient;

        private readonly IMMDeviceEnumerator _devicEnumerator;

        public event EventHandler<DeviceNotificationEventArgs> DeviceAdded;
        public event EventHandler<DeviceNotificationEventArgs> DeviceRemoved;

        public DeviceManager()
        {
            _notificationClient = new MMNotificationClient();
            _devicEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            Marshal.ThrowExceptionForHR(_devicEnumerator.RegisterEndpointNotificationCallback(_notificationClient));
            _notificationClient.DeviceAdded += DeviceAdded;
            _notificationClient.DeviceStateChanged += DeviceStateChangedHandler;
        }

        private void DeviceStateChangedHandler(object? sender, DeviceStateChangedEventArgs e)
        {
            if(e.DeviceState == DeviceState.NotPresent ||
                e.DeviceState == DeviceState.Unplugged ||
                e.DeviceState == DeviceState.Disabled)
            {
                DeviceRemoved?.Invoke(this, e);
            } else if(e.DeviceState == DeviceState.Active)
            {
                DeviceAdded?.Invoke(this, e);
            }
        }

        public IEnumerable<IAudioDevice> ObtainDeviceCollection(EDataFlow edata)
        {
            Marshal.ThrowExceptionForHR(_devicEnumerator.EnumAudioEndpoints(edata, DeviceState.Active, out IMMDeviceCollection immDeviceCollection));
            Marshal.ThrowExceptionForHR(immDeviceCollection.GetCount(out int deviceCount));

            var deviceCollection = new List<IAudioDevice>();
            for (int i = 0; i < deviceCount; i++)
            {
                immDeviceCollection.Item(i, out IMMDevice immDevice);
                string deviceName = GetDeviceName(immDevice);
                string deviceID = GetDeviceId(immDevice);
                deviceCollection.Add(new AudioDevice(deviceID, deviceName, immDevice));
            }
            return deviceCollection;
        }

        private PropertyValue GetDevicePropertyValue(IMMDevice device, PropertyKey propertyKey)
        {
            // Get the property store for the device
            Marshal.ThrowExceptionForHR(device.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propertyStore));
            // Get the friendly name value
            Marshal.ThrowExceptionForHR(propertyStore.GetValue(ref propertyKey, out PropertyValue propertyValue));
            return propertyValue;
        }

        private string GetDeviceId(IMMDevice device)
        {
            Marshal.ThrowExceptionForHR(device.GetId(out string deviceId));
            return deviceId;
        }

        private string GetDeviceName(IMMDevice device)
        {
            return GetDevicePropertyValue(device, new PropertyKey
            {
                fmtid = new Guid(Guids.DeviceFriendlyNameIIDString),
                pid = (int)EPid.PID_FRIENDLY_NAME
            }).GetValue();
        }

        public IAudioDevice Get(string deviceId)
        {
            Marshal.ThrowExceptionForHR(_devicEnumerator.GetDevice(deviceId, out IMMDevice immDevice));
            var deviceName = GetDeviceName(immDevice);
            return new AudioDevice(deviceId, deviceName, immDevice);
        }
    }
}
