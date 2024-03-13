using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class TrayViewModel : ViewModelBase
    {
        private readonly IAudioDeviceManager _deviceManager;

        private DeviceViewModel _inputDevice;

        private DeviceViewModel _outputDevice;

        public DeviceViewModel InputDevice
        {
            get => _inputDevice;
            set
            {
                _inputDevice = value;
                OnPropertyChanged(nameof(InputDevice));
            }
        }

        public DeviceViewModel OutputDevice
        {
            get => _outputDevice;
            set
            {
                _inputDevice = value;
                OnPropertyChanged(nameof(InputDevice));
            }
        }

        public TrayViewModel(IAudioDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        public override void Initialize()
        {
            //InputDevice = _deviceManager.ObtainDeviceCollection(EDataFlow.eCapture).FirstOrDefault(x => x.)
        }
    }
}
