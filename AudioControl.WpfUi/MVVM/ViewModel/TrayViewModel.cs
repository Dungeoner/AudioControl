using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.Core.Event;
using AudioControl.WpfUi.Core.Interface;
using AudioDeviceManager.DllImport.Enums;

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
            InputDevice = new TrayDeviceViewModel(input, new MuteIconProvider(EDataFlow.eCapture));
            OutputDevice = new TrayDeviceViewModel(output, new MuteIconProvider(EDataFlow.eRender));
            _deviceProvider.DefaultDeviceChanged += OnDefaultDeviceChanged;
        }

        private void OnDefaultDeviceChanged(object? sender, DefaultDeviceChangedEventArgs e)
        {
            if (e.Device.DeviceType == EDataFlow.eCapture) {
                InputDevice.Device = e.Device;
            } else if(e.Device.DeviceType == EDataFlow.eRender)
            {
                OutputDevice.Device = e.Device;
            }
        }
    }
}
