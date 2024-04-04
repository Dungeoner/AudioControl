using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.Core.Event;
using AudioControl.WpfUi.Core.Interface;
using AudioDevice.Utility;
using AudioDeviceManager;
using AudioDeviceManager.DllImport.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class DeviceCategoryViewModel : ViewModelBase
    {
        private readonly IDeviceProvider _deviceProvider;

        private readonly ISettingsManager _settingsManager;

        private readonly EDataFlow _deviceType;

        private DeviceViewModel _selectedDevice;

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

        public DeviceCategoryViewModel(IDeviceProvider deviceProvider, ISettingsManager settingsMasnager, EDataFlow deviceType)
        {
            _deviceProvider = deviceProvider;
            _settingsManager = settingsMasnager;
            _deviceType = deviceType;
            Initialize();
        }

        public override void Initialize()
        {
            var devices = _deviceProvider.ObtainDeviceCollection(_deviceType);
            DeviceVmList = new ObservableCollection<DeviceViewModel>(devices
                .Select(x => new DeviceViewModel(x, _settingsManager)));
            _deviceProvider.DeviceAdded += OnDeviceAdded;
            _deviceProvider.DeviceRemoved += OnDeviceRemoved;
            var defaultDevice = _deviceProvider.GetDefaultDevice(_deviceType);
            _selectedDevice = DeviceVmList.First(x => x.Device == defaultDevice);
        }

        private void OnDeviceAdded(object? sender, DeviceAddedEventArgs e)
        {
            var device = e.DeviceModel;
            if (device.DeviceType != _deviceType) return;
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                DeviceVmList?.Add(new DeviceViewModel(device, _settingsManager));
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
