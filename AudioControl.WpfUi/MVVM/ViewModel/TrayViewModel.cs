using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.Core.Interface;
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
        private readonly IDeviceProvider _deviceProvider;

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

        public TrayViewModel(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
            Initialize();
        }

        public override void Initialize()
        {
            var input = _deviceProvider.GetDefaultDevice(EDataFlow.eCapture);
            var output = _deviceProvider.GetDefaultDevice(EDataFlow.eRender);
            InputDevice = new TrayDeviceViewModel(input);
            OutputDevice = new TrayDeviceViewModel(output);
        }
    }
}
