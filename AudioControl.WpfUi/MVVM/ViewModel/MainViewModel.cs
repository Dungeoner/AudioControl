using AudioControl.Enum;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.Core.Interface;
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

        public BaseCommand CloseCommand { get; set; }

        public BaseCommand MinimizeCommand { get ; set; }

        public DeviceCategoryViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel(IDeviceProvider deviceProvider, ISettingsManager settingsManager)
        {
            InputDevicesVm = new DeviceCategoryViewModel(deviceProvider, settingsManager, EDataFlow.eCapture);
            OutputDevicesVm = new DeviceCategoryViewModel(deviceProvider, settingsManager, EDataFlow.eRender);
            InputDevicesCommand = new BaseCommand(e =>
            {
                CurrentView = InputDevicesVm;
            });
            OutputDevicesCommand = new BaseCommand(e =>
            {
                CurrentView = OutputDevicesVm;
            });
            CloseCommand = new BaseCommand(e =>
            {
                App.Current.MainWindow.Hide();
            });
            MinimizeCommand = new BaseCommand(e =>
            {
                App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
            });
            Initialize();
        }

        public override void Initialize()
        {
            CurrentView = InputDevicesVm;
        }
    }
}
