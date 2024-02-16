using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using AudioDevice.Utility;
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
        private readonly IAudioDeviceManager _deviceManager;

        private readonly ISettingsManager _settingsManager;

        private readonly EDataFlow _deviceType;

        public ObservableCollection<DeviceViewModel> DeviceVmList { get; set; }

        public DeviceCategoryViewModel(IAudioDeviceManager deviceManager, ISettingsManager settingsMasnager, EDataFlow deviceType)
        {
            _deviceManager = deviceManager;
            _settingsManager = settingsMasnager;
            _deviceType = deviceType;
            Initialize();
        }

        public override void Initialize()
        {
            var devices = _deviceManager.ObtainDeviceCollection(_deviceType);
            DeviceVmList = new ObservableCollection<DeviceViewModel>(devices
                .Select(x => new DeviceViewModel(x, _settingsManager)));
            _deviceManager.DeviceAdded += _deviceManager_DeviceAdded;
            _deviceManager.DeviceRemoved += _deviceManager_DeviceRemoved;
        }

        private void _deviceManager_DeviceRemoved(object? sender, DeviceNotificationEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                var device = DeviceVmList.FirstOrDefault(x => x.Id == e.DeviceId);
                if (device != null) { DeviceVmList?.Remove(device); }
            });
        }

        private void _deviceManager_DeviceAdded(object? sender, DeviceNotificationEventArgs e)
        {
            var device = _deviceManager.Get(e.DeviceId);
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                DeviceVmList?.Add(new DeviceViewModel(device, _settingsManager));
            });
        }
    }
}
