using AudioControl.Enum;
using AudioDeviceManager.DllImport.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.Intefaces
{
    public interface IAudioDeviceManager
    {
        IEnumerable<IAudioDevice> ObtainDeviceCollection(EDataFlow eDataFlow);

        IAudioDevice Get(string deviceId);

        event EventHandler<DeviceNotificationEventArgs> DeviceAdded;

        event EventHandler<DeviceNotificationEventArgs> DeviceRemoved;
    }
}
