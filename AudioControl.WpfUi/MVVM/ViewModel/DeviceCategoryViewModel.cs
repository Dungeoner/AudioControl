using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using AudioDevice.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class DeviceCategoryViewModel : ViewModelBase
    {
        private readonly IAudioDeviceManager _deviceManager;

        private readonly ISettingsManager _settingsManager;

        private readonly DeviceType _deviceType;

        public ObservableCollection<DeviceViewModel> DeviceVmList { get; set; }

        public string DeviceType { get;}

        public DeviceCategoryViewModel(IAudioDeviceManager deviceManager, ISettingsManager settingsMasnager, DeviceType deviceType)
        {
            _deviceManager = deviceManager;
            _settingsManager = settingsMasnager;
            _deviceType = deviceType;
            DeviceType = deviceType.ToString();
            Initialize();
        }

        public async Task Update()
        {

        }

        public override void Initialize()
        {
            DeviceVmList = new ObservableCollection<DeviceViewModel>(_deviceManager.ObtainDeviceCollection()
                .Select(x => new DeviceViewModel(x, _settingsManager)));
        }
    }

    public enum DeviceType
    {
        Input, Output
    }
}
