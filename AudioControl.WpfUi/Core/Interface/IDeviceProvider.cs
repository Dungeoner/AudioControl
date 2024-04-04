using AudioControl.Enum;
using AudioControl.WpfUi.Core.Event;
using AudioControl.WpfUi.MVVM.Models;
using System;
using System.Collections.Generic;

namespace AudioControl.WpfUi.Core.Interface
{
    public interface IDeviceProvider
    {
        IEnumerable<AudioDeviceModel> ObtainDeviceCollection(EDataFlow eDataFlow);

        AudioDeviceModel GetDefaultDevice(EDataFlow edata);

        public bool SetDefaultDevice(AudioDeviceModel device);

        event EventHandler<DeviceAddedEventArgs> DeviceAdded;

        event EventHandler<DeviceRemovedEventArgs> DeviceRemoved;
    }
}
