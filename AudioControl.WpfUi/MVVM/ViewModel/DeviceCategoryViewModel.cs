﻿using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.Core.Event;
using AudioControl.WpfUi.Core.Interface;
using AudioDevice.Utility;
using AudioDeviceManager.DllImport.Enums;
using System.Collections.ObjectModel;
using System.Linq;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class DeviceCategoryViewModel : ViewModelBase
    {
        private readonly IDeviceProvider _deviceProvider;

        private readonly EDataFlow _deviceFlow;

        private MuteIconProvider _muteIconProvider;

        private DeviceViewModel _selectedDevice;

        private SaveCommand _saveCommand;

        public ObservableCollection<DeviceViewModel> DeviceVmList { get; set; }

        public DeviceViewModel SelectedDeviceVm
        {
            get => _selectedDevice;
            set
            {
                if (_deviceProvider.SetDefaultDevice(value.Device))
                {
                    _selectedDevice = value;
                    OnPropertyChanged(nameof(SelectedDeviceVm));
                }
            }
        }

        public DeviceCategoryViewModel(IDeviceProvider deviceProvider, ISettingsManager settingsManager, EDataFlow deviceFlow)
        {
            _deviceProvider = deviceProvider;
            _deviceFlow = deviceFlow;
            _saveCommand = new SaveCommand(settingsManager);
            Initialize();
        }

        public override void Initialize()
        {
            var devices = _deviceProvider.ObtainDeviceCollection(_deviceFlow);
            _muteIconProvider = new MuteIconProvider(_deviceFlow);
            DeviceVmList = new ObservableCollection<DeviceViewModel>(devices
                .Select(x => new MainDeviceViewModel(x, _saveCommand, _muteIconProvider)));
            _deviceProvider.DeviceAdded += OnDeviceAdded;
            _deviceProvider.DeviceRemoved += OnDeviceRemoved;
            var defaultDevice = _deviceProvider.GetDefaultDevice(_deviceFlow);
            _selectedDevice = DeviceVmList.First(x => x.Device == defaultDevice);
        }

        private void OnDeviceAdded(object? sender, DeviceAddedEventArgs e)
        {
            var device = e.DeviceModel;
            if (device.DeviceType != _deviceFlow) return;
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                DeviceVmList?.Add(new MainDeviceViewModel(device, _saveCommand, _muteIconProvider));
            });
        }

        private void OnDeviceRemoved(object? sender, DeviceRemovedEventArgs e)
        {
            var device = DeviceVmList.FirstOrDefault(x => x.Device.Id == e.DeviceId);
            if (device == null) return;
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                DeviceVmList?.Remove(device);
            });
        }
    }
}
