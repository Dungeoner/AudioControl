using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class TrayViewModel : ViewModelBase
    {
        private readonly IAudioDeviceManager _deviceManager;

        private TrayDeviceViewModel _inputDevice;

        private TrayDeviceViewModel _outputDevice;

        public TrayDeviceViewModel InputDevice
        {
            get => _inputDevice;
            set
            {
                _inputDevice = value;
                OnPropertyChanged(nameof(InputDevice));
            }
        }

        public TrayDeviceViewModel OutputDevice
        {
            get => _outputDevice;
            set
            {
                _outputDevice = value;
                OnPropertyChanged(nameof(OutputDevice));
            }
        }

        public TrayViewModel(IAudioDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
            Initialize();
        }

        public override void Initialize()
        {
            var devices = _deviceManager.ObtainDeviceCollection(EDataFlow.eAll);
            InputDevice = GetDefaultDevice(devices, EDataFlow.eCapture);
            OutputDevice = GetDefaultDevice(devices, EDataFlow.eRender);
        }

        private TrayDeviceViewModel GetDefaultDevice(IEnumerable<IAudioDevice> devices, EDataFlow eDataFlow)
        {
            string defaultDeviceId = _deviceManager.GetDefaultDeviceId(eDataFlow);
            var defaultDevice = devices.First(x => x.Id == defaultDeviceId);
            return new TrayDeviceViewModel(defaultDevice);
        }
    }
}
