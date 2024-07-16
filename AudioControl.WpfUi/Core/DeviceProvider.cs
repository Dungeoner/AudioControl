using AudioControl.Intefaces;
using AudioControl.WpfUi.Core.Event;
using AudioControl.WpfUi.Core.Interface;
using AudioControl.WpfUi.MVVM.Models;
using AudioDevice.Utility;
using AudioDeviceManager.DllImport.Enums;
using AudioDeviceManager.DllImport.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

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
            _deviceManager.DeviceAdded += OnDeviceAdded; 
            //_deviceManager.DefaultDeviceChanged += OnDefaultDeviceChanged;
            _devices = LoadDevices();
        }

        public event EventHandler<DeviceAddedEventArgs> DeviceAdded;

        public event EventHandler<DeviceRemovedEventArgs> DeviceRemoved;

        public event EventHandler<Core.Event.DefaultDeviceChangedEventArgs> DefaultDeviceChanged;

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
            SupressInputMuting();
            var result = _deviceManager.SetDefaultDevice(device.Id, device.DeviceType);
            if (result) {
                DefaultDeviceChanged?.Invoke(this, new Event.DefaultDeviceChangedEventArgs(device));
            }
            return result;
        }

        private void OnDeviceRemoved(object? sender, DeviceNotificationEventArgs e)
        {
            var device = _devices.FirstOrDefault(d => d.Id == e.DeviceId);
            if(device != null) {
                _devices.Remove(device);
                DeviceRemoved?.Invoke(this, new DeviceRemovedEventArgs(e.DeviceId));
            }
        }

        private void OnDeviceAdded(object? sender, DeviceNotificationEventArgs e)
        {
            var device = _deviceManager.Get(e.DeviceId);
            var eDataFlow = device.GetDataFlow();
            var deviceModel = new AudioDeviceModel(device, eDataFlow);
            _devices.Add(deviceModel);
            DeviceAdded?.Invoke(this, new DeviceAddedEventArgs(deviceModel));
        }

        //private void OnDefaultDeviceChanged(object? sender, DefaultDeviceChangedEventArgs e)
        //{
        //    DefaultDeviceChanged?.Invoke(this, e);
        //}

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

        private void SupressInputMuting()
        {
            var defaultInput = GetDefaultDevice(EDataFlow.eCapture);
            if (!defaultInput.IsMuted)
            {
                DefaultDeviceChanged += (_, _) =>
                {
                    defaultInput.IsMuted = false;
                };
            }
        }
    }
}
