using AudioDeviceManager.DllImport.Enums;
using AudioDeviceManager.DllImport.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.Intefaces
{
    public interface IAudioDeviceManager
    {
        IEnumerable<IAudioDevice> ObtainDeviceCollection(EDataFlow eDataFlow);

        IAudioDevice Get(string deviceId);

        public string GetDefaultDeviceId(EDataFlow edata);

        public bool SetDefaultDevice(string id, EDataFlow edata);

        event EventHandler<DeviceNotificationEventArgs> DeviceAdded;

        event EventHandler<DeviceNotificationEventArgs> DeviceRemoved;

        event EventHandler<DefaultDeviceChangedEventArgs> DefaultDeviceChanged;
    }
}
