using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.WpfUi.Core.Event;
using AudioControl.WpfUi.Core.Interface;
using AudioControl.WpfUi.MVVM.Models;
using AudioDevice.Utility;
using AudioDeviceManager.DllImport.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.Core
{
    class DeviceProvider : IDeviceProvider
    {
        private readonly IAudioDeviceManager _deviceManager;

        private readonly ISettingsManager _settingsManager;

        private readonly List<AudioDeviceModel> _devices;

        public DeviceProvider(IAudioDeviceManager deviceManager, ISettingsManager settingsManager)
        { 
            _deviceManager = deviceManager;
            _settingsManager = settingsManager;
            _deviceManager.DeviceRemoved += OnDeviceRemoved;
            _devices = LoadDevices();
        }

        public event EventHandler<DeviceAddedEventArgs> DeviceAdded;

        public event EventHandler<DeviceRemovedEventArgs> DeviceRemoved;

        public AudioDeviceModel GetDefaultDevice(EDataFlow edata)
        {
            var deviceId = _deviceManager.GetDefaultDeviceId(edata);
            return _devices.First(d => d.Id == deviceId);
        }

        public IEnumerable<AudioDeviceModel> ObtainDeviceCollection(EDataFlow eDataFlow)
        {
            return _devices.Where(x => x.DeviceType == eDataFlow);
        }

        public bool SetDefaultDevice(AudioDeviceModel device)
        {
            return _deviceManager.SetDefaultDevice(device.Id, device.DeviceType);
        }

        private void OnDeviceRemoved(object? sender, DeviceNotificationEventArgs e)
        {
            var device = _devices.First(d => d.Id == e.DeviceId);
            _devices.Remove(device);
            DeviceRemoved?.Invoke(this, new DeviceRemovedEventArgs(e.DeviceId));
        }

        private void OnDeviceAdded(object? sender, DeviceNotificationEventArgs e)
        {
            var device = _deviceManager.Get(e.DeviceId);
            var eDataFlow = device.GetDataFlow();
            var deviceModel = new AudioDeviceModel(device, eDataFlow);
            _devices.Add(deviceModel);
            DeviceAdded?.Invoke(this, new DeviceAddedEventArgs(deviceModel));
        }

        private List<AudioDeviceModel> LoadDevices()
        {
            var deviceModelList = new List<AudioDeviceModel>();
            var devices = _deviceManager.ObtainDeviceCollection(EDataFlow.eAll);
            foreach ( var device in devices)
            {
                var deviceModel = new AudioDeviceModel(device, device.GetDataFlow());
                var settings =  _settingsManager.LoadSettings(device.Name);
                if(settings != null)
                {
                    deviceModel.Gain = settings.Gain;
                    deviceModel.IsMuted = settings.IsMuted;
                }
                deviceModelList.Add(deviceModel);
            }
            return deviceModelList;
        }
    }
}
