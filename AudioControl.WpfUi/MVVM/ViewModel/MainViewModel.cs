using AudioControl.WpfUi.Core;
using AudioDevice.Utility;
using AudioDeviceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DeviceCategoryViewModel _currentView;

        public DeviceCategoryViewModel InputDevicesVm {get; set;}

        public DeviceCategoryViewModel OutputDevicesVm { get; set; }

        public BaseCommand InputDevicesCommand { get; set; }

        public BaseCommand OutputDevicesCommand { get; set; }

        public DeviceCategoryViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel()
        {
            var deviceManager = new DeviceManager();
            var settingsManager = new SettingsManager();
            InputDevicesVm = new DeviceCategoryViewModel(deviceManager, settingsManager, DeviceType.Input);
            OutputDevicesVm = new DeviceCategoryViewModel(deviceManager, settingsManager, DeviceType.Output);
            InputDevicesCommand = new BaseCommand(e =>
            {
                CurrentView = InputDevicesVm;
            });
            OutputDevicesCommand = new BaseCommand(e =>
            {
                CurrentView = OutputDevicesVm;
            });
            Initialize();
        }

        public override void Initialize()
        {
            CurrentView = InputDevicesVm;
        }
    }
}
