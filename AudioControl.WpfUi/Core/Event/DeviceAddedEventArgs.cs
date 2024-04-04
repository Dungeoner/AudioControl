using AudioControl.WpfUi.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.Core.Event
{
    public class DeviceAddedEventArgs : EventArgs
    {
        public AudioDeviceModel DeviceModel { get; }

        public DeviceAddedEventArgs(AudioDeviceModel device) { 
            DeviceModel = device;
        }
    }
}
